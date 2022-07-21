using System.Runtime.InteropServices;
using BearsEngine.Win32API;
using Microsoft.Win32;

namespace BearsEngine.Input;

public sealed class MouseManager : IMouseManager
{
    #region Fields
    private readonly List<MouseState> _mice = new();
    private readonly List<string> _names = new();
    private readonly Dictionary<IntPtr, int> _regdDevices = new();
    private readonly object _syncRoot = new();
    private readonly IntPtr _msgWindowHandle;
    #endregion

    #region Constructors
    public MouseManager(IntPtr messageWindowHandle)
    {
        if (messageWindowHandle == IntPtr.Zero)
            throw new ArgumentNullException("messageWindowHandle");

        _msgWindowHandle = messageWindowHandle;

        RefreshDevices();
    }
    #endregion

    #region Methods

    #region GetDeviceName
    static string GetDeviceName(RawInputDeviceList dev)
    {
        // get name size
        uint size = 0;
        User32.GetRawInputDeviceInfo(dev.Device, RawInputDeviceInfoEnum.DEVICENAME, IntPtr.Zero, ref size);

        // get actual name
        IntPtr name_ptr = Marshal.AllocHGlobal((IntPtr)size);
        User32.GetRawInputDeviceInfo(dev.Device, RawInputDeviceInfoEnum.DEVICENAME, name_ptr, ref size);
        string name = Marshal.PtrToStringAnsi(name_ptr);
        Marshal.FreeHGlobal(name_ptr);

        return name;
    }
    #endregion

    #region FindRegistryKey
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
    #endregion

    #region RegisterRawDevice
    private void RegisterRawDevice(IntPtr window, string device)
    {
        // Mouse is 1/2 (page/id). See http://www.microsoft.com/whdc/device/input/HID_HWID.mspx
        RawInputDevice rid = new();
        rid.UsagePage = 1;
        rid.Usage = 2;
        rid.Flags = RawInputDeviceFlags.INPUTSINK;
        rid.Target = window;

        if (!User32.RegisterRawInputDevices(rid))
        {
            Console.WriteLine("[Warning] Raw input registration failed with error: {0}. Device: {1}",
                Marshal.GetLastWin32Error(), rid.ToString());
        }
        else
        {
            Console.WriteLine("Registered Mouse {0}: {1}", _mice.Count, device);
        }
    }
    #endregion

    #endregion

    #region IMouseManager
    #region State
    public MouseState State
    {
        get
        {
            lock (_syncRoot)
            {
                MouseState consolidated = new();
                foreach (MouseState m in _mice)
                {
                    consolidated.MergeBits(m);
                }
                POINT p = new();
                User32.GetCursorPos(ref p);
                consolidated.ScreenX = p.X;
                consolidated.ScreenY = p.Y;
                return consolidated;
            }
        }
    }
    #endregion

    #region GetState(int index)
    public MouseState GetState(int index)
    {
        lock (_syncRoot)
        {
            if (index < _mice.Count)
                return _mice[index];
            else
                return new MouseState();
        }
    }
    #endregion

    #region RefreshDevices()
    public void RefreshDevices()
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
            User32.GetRawInputDeviceList(null, ref count, RawInputDeviceList.Size);

            RawInputDeviceList[] ridl = new RawInputDeviceList[count];
            for (int i = 0; i < count; i++)
                ridl[i] = new RawInputDeviceList();

            User32.GetRawInputDeviceList(ridl, ref count, RawInputDeviceList.Size);

            foreach (RawInputDeviceList d in ridl)
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
                            User32.GetRawInputDeviceInfo(d.Device, RawInputDeviceInfoEnum.DEVICEINFO,
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
    #endregion

    #region ProcessInput(RawInput data)
    internal bool ProcessInput(RawInput data)
    {
        RawMouse mData = data.Data.Mouse;
        IntPtr dHandle = data.Header.Device;

        //this is needed for track pads or something
        if (!_regdDevices.ContainsKey(dHandle))
            RefreshDevices();

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
            state.EnableBit((int)MouseButton.Left);
            //capture the mouse temporarily to capture mouseup events outside the window.
            User32.SetCapture(_msgWindowHandle);
        }
        if ((mData.ButtonFlags & RawInputMouseState.LEFT_BUTTON_UP) != 0)
        {
            state.DisableBit((int)MouseButton.Left);
            User32.ReleaseCapture();
        }
        if ((mData.ButtonFlags & RawInputMouseState.RIGHT_BUTTON_DOWN) != 0)
        {
            state.EnableBit((int)MouseButton.Right);
            User32.SetCapture(_msgWindowHandle);
        }
        if ((mData.ButtonFlags & RawInputMouseState.RIGHT_BUTTON_UP) != 0)
        {
            state.DisableBit((int)MouseButton.Right);
            User32.ReleaseCapture();
        }
        if ((mData.ButtonFlags & RawInputMouseState.MIDDLE_BUTTON_DOWN) != 0)
        {
            state.EnableBit((int)MouseButton.Middle);
            User32.SetCapture(_msgWindowHandle);
        }
        if ((mData.ButtonFlags & RawInputMouseState.MIDDLE_BUTTON_UP) != 0)
        {
            state.DisableBit((int)MouseButton.Middle);
            User32.ReleaseCapture();
        }
        if ((mData.ButtonFlags & RawInputMouseState.BUTTON_4_DOWN) != 0)
        {
            state.EnableBit((int)MouseButton.Button1);
            User32.SetCapture(_msgWindowHandle);
        }
        if ((mData.ButtonFlags & RawInputMouseState.BUTTON_4_UP) != 0)
        {
            state.DisableBit((int)MouseButton.Button1);
            User32.ReleaseCapture();
        }
        if ((mData.ButtonFlags & RawInputMouseState.BUTTON_5_DOWN) != 0)
        {
            state.EnableBit((int)MouseButton.Button2);
            User32.SetCapture(_msgWindowHandle);
        }
        if ((mData.ButtonFlags & RawInputMouseState.BUTTON_5_UP) != 0)
        {
            state.DisableBit((int)MouseButton.Button2);
            User32.ReleaseCapture();
        }

        if ((mData.ButtonFlags & RawInputMouseState.WHEEL) != 0)
            state.SetScrollRelative(0, (short)mData.ButtonData / 120.0f);

        if ((mData.ButtonFlags & RawInputMouseState.HWHEEL) != 0)
            state.SetScrollRelative((short)mData.ButtonData / 120.0f, 0);

        if ((mData.Flags & RawMouseFlags.MOUSE_MOVE_ABSOLUTE) != 0)
        {
            state.AbsX = mData.LastX;
            state.AbsY = mData.LastY;
        }
        else
        {   // Seems like MOUSE_MOVE_RELATIVE is the default, unless otherwise noted.
            state.AbsX += mData.LastX;
            state.AbsY += mData.LastY;
        }

        lock (_syncRoot)
        {
            _mice[mouseID] = state;
            return true;
        }
    }
    #endregion
    #endregion
}
