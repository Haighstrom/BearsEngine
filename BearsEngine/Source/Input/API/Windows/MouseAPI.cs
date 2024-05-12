using System.Runtime.InteropServices;
using BearsEngine.WinAPI;
using Microsoft.Win32;

namespace BearsEngine.Input.Windows;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This is part of the Windows API which will only be invoked if on Windows platform.")]
internal class MouseAPI : IMouseAPI
{
    private readonly List<MouseState> _mice = new();
    private readonly List<string> _names = new();
    private readonly Dictionary<IntPtr, int> _regdDevices = new();
    private readonly object _syncRoot = new();
    private readonly IntPtr _msgWindowHandle;

    public MouseAPI(IntPtr messageWindowHandle)
    {
        if (messageWindowHandle == IntPtr.Zero)
        {
            throw new ArgumentNullException("messageWindowHandle");
        }

        _msgWindowHandle = messageWindowHandle;

        UpdateDevices();
    }
    
    static string GetDeviceName(RAWINPUTDEVICELIST dev)
    {
        // get name size
        uint size = 0;
        User32.GetRawInputDeviceInfo(dev.Device, RAWINPUTDEVICEINFOFLAG.RIDI_DEVICENAME, IntPtr.Zero, ref size);

        // get actual name
        IntPtr name_ptr = Marshal.AllocHGlobal((IntPtr)size);
        User32.GetRawInputDeviceInfo(dev.Device, RAWINPUTDEVICEINFOFLAG.RIDI_DEVICENAME, name_ptr, ref size);
        string name = Marshal.PtrToStringAnsi(name_ptr);
        Marshal.FreeHGlobal(name_ptr);

        return name;
    }

    static private RegistryKey FindRegistryKey(string name)
    {
        if (name.Length < 4)
            return null;

        // remove the \??\
        name = name.Substring(4);

        string[] split = name.Split('#');
        if (split.Length < 3)
            return null;

        string id_01 = split[0];    // ACPI (Class code)
        string id_02 = split[1];    // PNP0303 (SubClass code)
        string id_03 = split[2];    // 3&13c0b0c5&0 (Protocol code)
        // The final part is the class GUID and is not needed here

        string findme = string.Format(
            @"System\CurrentControlSet\Enum\{0}\{1}\{2}",
            id_01, id_02, id_03);

        RegistryKey regkey = Registry.LocalMachine.OpenSubKey(findme);
        return regkey;
    }

    private void RegisterRawDevice(IntPtr window, string device)
    {
        // Mouse is 1/2 (page/id). See http://www.microsoft.com/whdc/device/input/HID_HWID.mspx
        RAWINPUTDEVICE rid = new();
        rid.usUsagePage = RAWINPUTDEVICE.USAGEPAGE.HID_USAGE_PAGE_GENERIC;
        rid.usUsage = RAWINPUTDEVICE.USAGE.HID_USAGE_GENERIC_MOUSE;
        rid.dwFlags = RAWINPUTDEVICE.FLAGS.RIDEV_INPUTSINK;
        rid.hwndTarget = window;

        RAWINPUTDEVICE[] rids = { rid };

        if (!User32.RegisterRawInputDevices(rids, rids.Length, Marshal.SizeOf(typeof(RAWINPUTDEVICE))))
        {
            Log.Warning($"Raw input registration failed with error: {Marshal.GetLastWin32Error()}. Device: {rid}");
        }
        else
        {
            Log.Information($"Registered Mouse {_mice.Count}: {device}");
        }
    }
    
    public void UpdateDevices()
    {
        lock (_syncRoot)
        {
            //mark all devices as disconnected
            for (int i = 0; i < _mice.Count; i++)
            {
                MouseState state = _mice[i];
                state.IsConnected = false;
                _mice[i] = state;
            }

            int count = 0;
            User32.GetRawInputDeviceList(null, ref count, RAWINPUTDEVICELIST.Size);

            RAWINPUTDEVICELIST[] ridl = new RAWINPUTDEVICELIST[count];
            for (int i = 0; i < count; i++)
                ridl[i] = new RAWINPUTDEVICELIST();

            User32.GetRawInputDeviceList(ridl, ref count, RAWINPUTDEVICELIST.Size);

            foreach (RAWINPUTDEVICELIST d in ridl)
            {
                if (_regdDevices.ContainsKey(d.Device))
                {
                    //device already registered, mark as connected
                    MouseState state = _mice[_regdDevices[d.Device]];
                    state.IsConnected = true;
                    _mice[_regdDevices[d.Device]] = state;
                    continue;
                }

                //unregistered device, find out what it is
                string name = GetDeviceName(d);
                if (name.ToLower().Contains("root"))
                {
                    //this is a terminal services device, skip
                    continue;
                }
                else if (d.Type == RawInputDeviceType.MOUSE || d.Type == RawInputDeviceType.HID)
                {
                    //mouse or USB device (which could be a mouse).
                    RegistryKey regKey = FindRegistryKey(name);
                    if (regKey == null)
                        continue;

                    string deviceDesc = (string)regKey.GetValue("DeviceDesc");
                    string deviceClass = (string)regKey.GetValue("Class");
                    if (deviceClass == null)
                    {
                        string deviceClassGUID = (string)regKey.GetValue("ClassGUID");
                        RegistryKey classGUIDKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\" + deviceClassGUID);
                        deviceClass = classGUIDKey != null ? (string)classGUIDKey.GetValue("Class") : string.Empty;
                    }
                    if (string.IsNullOrEmpty(deviceDesc))
                        deviceDesc = "Windows Mouse " + _mice.Count;
                    else
                        deviceDesc = deviceDesc.Substring(deviceDesc.LastIndexOf(';') + 1);

                    if (!string.IsNullOrEmpty(deviceClass) && deviceClass.ToLower().Equals("mouse"))
                    {
                        if (!_regdDevices.ContainsKey(d.Device))
                        {
                            // Register the device:
                            RawInputDeviceInfo info = new();
                            int devInfoSize = info.Size;
                            User32.GetRawInputDeviceInfo(d.Device, RAWINPUTDEVICEINFOFLAG.RIDI_DEVICEINFO,
                                    info, ref devInfoSize);

                            RegisterRawDevice(_msgWindowHandle, deviceDesc);
                            MouseState state = new();
                            state.IsConnected = true;
                            _mice.Add(state);
                            _names.Add(deviceDesc);
                            _regdDevices.Add(d.Device, _mice.Count - 1);
                        }
                    }
                }

            }

        }

        //Console.WriteLine();
    }
    
    public bool ProcessInputData(RawInput data)
    {
        RawMouse mData = data.Data.Mouse;
        IntPtr dHandle = data.Header.Device;

        //this is needed for track pads or something
        if (!_regdDevices.ContainsKey(dHandle))
            UpdateDevices();

        if (_mice.Count == 0)
            return false;

        //if this device is firing an event but isn't in our dictionary for some reason I guess we process it anyway (add it to the first mouse)
        int mouseID = _regdDevices.ContainsKey(dHandle) ? _regdDevices[dHandle] : 0;

        MouseState state = _mice[mouseID];

        //set mouse on screen X,Y
        POINT p = new();
        User32.GetCursorPos(ref p);
        state.ScreenX = p.X;
        state.ScreenY = p.Y;

        if ((mData.ButtonFlags & RawInputMouseState.LEFT_BUTTON_DOWN) != 0)
        {
            state.SetButton(MouseButton.Left, true);
            //capture the mouse temporarily to capture mouseup events outside the window.
            User32.SetCapture(_msgWindowHandle);
        }
        if ((mData.ButtonFlags & RawInputMouseState.LEFT_BUTTON_UP) != 0)
        {
            state.SetButton(MouseButton.Left, false);
            User32.ReleaseCapture();
        }
        if ((mData.ButtonFlags & RawInputMouseState.RIGHT_BUTTON_DOWN) != 0)
        {
            state.SetButton(MouseButton.Right, true);
            User32.SetCapture(_msgWindowHandle);
        }
        if ((mData.ButtonFlags & RawInputMouseState.RIGHT_BUTTON_UP) != 0)
        {
            state.SetButton(MouseButton.Right, false);
            User32.ReleaseCapture();
        }
        if ((mData.ButtonFlags & RawInputMouseState.MIDDLE_BUTTON_DOWN) != 0)
        {
            state.SetButton(MouseButton.Middle, true);
            User32.SetCapture(_msgWindowHandle);
        }
        if ((mData.ButtonFlags & RawInputMouseState.MIDDLE_BUTTON_UP) != 0)
        {
            state.SetButton(MouseButton.Middle, false);
            User32.ReleaseCapture();
        }
        if ((mData.ButtonFlags & RawInputMouseState.BUTTON_4_DOWN) != 0)
        {
            state.SetButton(MouseButton.Mouse4, true);
            User32.SetCapture(_msgWindowHandle);
        }
        if ((mData.ButtonFlags & RawInputMouseState.BUTTON_4_UP) != 0)
        {
            state.SetButton(MouseButton.Mouse4, false);
            User32.ReleaseCapture();
        }
        if ((mData.ButtonFlags & RawInputMouseState.BUTTON_5_DOWN) != 0)
        {
            state.SetButton(MouseButton.Mouse5, true);
            User32.SetCapture(_msgWindowHandle);
        }
        if ((mData.ButtonFlags & RawInputMouseState.BUTTON_5_UP) != 0)
        {
            state.SetButton(MouseButton.Mouse5, false);
            User32.ReleaseCapture();
        }

        if ((mData.ButtonFlags & RawInputMouseState.WHEEL) != 0)
            state.WheelY += (short)mData.ButtonData / 120.0f;

        //if ((mData.ButtonFlags & RawInputMouseState.HWHEEL) != 0)
        //    state.SetScrollRelative((short)mData.ButtonData / 120.0f, 0);

        //if ((mData.Flags & RawMouseFlags.MOUSE_MOVE_ABSOLUTE) != 0)
        //{
        //    state.AbsX = mData.LastX;
        //    state.AbsY = mData.LastY;
        //}
        //else
        //{   // Seems like MOUSE_MOVE_RELATIVE is the default, unless otherwise noted.
        //    state.AbsX += mData.LastX;
        //    state.AbsY += mData.LastY;
        //}

        lock (_syncRoot)
        {
            _mice[mouseID] = state;
            return true;
        }
    }

    public MouseState GetAggregateState()
    {
        lock (_syncRoot)
        {
            MouseState answer = new();

            if (!_mice.Any())
                return answer;

            POINT p = new();
            User32.GetCursorPos(ref p);

            answer.ButtonStates[MouseButton.Left] = _mice.Any(s => s.ButtonStates[MouseButton.Left]);
            answer.ButtonStates[MouseButton.Right] = _mice.Any(s => s.ButtonStates[MouseButton.Right]);
            answer.ButtonStates[MouseButton.Middle] = _mice.Any(s => s.ButtonStates[MouseButton.Middle]);
            answer.ButtonStates[MouseButton.Mouse4] = _mice.Any(s => s.ButtonStates[MouseButton.Mouse4]);
            answer.ButtonStates[MouseButton.Mouse5] = _mice.Any(s => s.ButtonStates[MouseButton.Mouse5]);
            //answer.AbsX = states.Sum(s => s.AbsX);
            //answer.AbsY = states.Sum(s => s.AbsY);
            answer.ScreenX = p.X;
            answer.ScreenY = p.Y;
            answer.WheelY = _mice.Sum(s => s.WheelY);
            answer.IsConnected = _mice.Any(s => s.IsConnected);

            return answer;
        }
    }
}
