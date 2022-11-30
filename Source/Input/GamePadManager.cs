using BearsEngine.Win32API;
using Microsoft.Win32;
using System.Runtime.InteropServices;


namespace BearsEngine.Input;

public class GamePadManager : IGamePadManager
{
    private readonly List<GamePadState> _gamePadStates = new();
    private readonly List<string> _names = new();
    private readonly Dictionary<IntPtr, int> _regdDevices = new();
    private readonly object _syncRoot = new();
    private readonly IntPtr _msgWindowHandle;
    

    

    public GamePadManager(IntPtr messageWindowHandle)
    {
        if (messageWindowHandle == IntPtr.Zero)
            throw new ArgumentNullException("messageWindowHandle");

        _msgWindowHandle = messageWindowHandle;

        RefreshDevices();
    }
    

    private static RegistryKey FindRegistryKey(string name)
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
    

    private static string GetDeviceName(RAWINPUTDEVICELIST dev)
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
    

    public void RefreshDevices()
    {
        lock (_syncRoot)
        {
            //mark all devices as disconnected
            for (int i = 0; i < _gamePadStates.Count; i++)
            {
                GamePadState state = _gamePadStates[i];
                state.IsConnected = false;
                _gamePadStates[i] = state;
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
                    GamePadState state = _gamePadStates[_regdDevices[d.Device]];
                    state.IsConnected = true;
                    _gamePadStates[_regdDevices[d.Device]] = state;
                    continue;
                }

                //unregistered device, find out what it is
                string name = GetDeviceName(d);
                if (name.ToLower().Contains("root"))
                {
                    //this is a terminal services device, skip
                    continue;
                }
                else if (d.Type == RawInputDeviceType.HID)
                {
                    //USB device (which could be a gamepad).
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
                        deviceDesc = "Windows Mouse " + _gamePadStates.Count;
                    else
                        deviceDesc = deviceDesc.Substring(deviceDesc.LastIndexOf(';') + 1);

                 //   HConsole.Log(deviceClass.ToLower());


                    if (!string.IsNullOrEmpty(deviceClass) && deviceClass.ToLower().Equals("keyboard"))
                    {
                        if (!_regdDevices.ContainsKey(d.Device))
                        {
                            // Register the device:
                            RawInputDeviceInfo info = new();
                            int devInfoSize = info.Size;
                            User32.GetRawInputDeviceInfo(d.Device, RAWINPUTDEVICEINFOFLAG.RIDI_DEVICEINFO,
                                    info, ref devInfoSize);

                            RegisterRawDevice(_msgWindowHandle, deviceDesc);
                            GamePadState state = new();
                            state.IsConnected = true;
                            _gamePadStates.Add(state);
                            _names.Add(deviceDesc);
                            _regdDevices.Add(d.Device, _gamePadStates.Count - 1);
                        }
                    }
                }

            }

        }
    }
    

    private void RegisterRawDevice(IntPtr window, string device)
    {
        RAWINPUTDEVICE[] rids = new RAWINPUTDEVICE[1];
        // Keyboard is 1/6 (page/id). See http://www.microsoft.com/whdc/device/input/HID_HWID.mspx
        rids[0] = new()
        {
            usUsagePage = RAWINPUTDEVICEUSAGEPAGE.HID_USAGE_PAGE_GENERIC,
            usUsage = RAWINPUTDEVICE_usUsage.HID_USAGE_GENERIC_KEYBOARD,
            dwFlags = RAWINPUTDEVICEFLAGS.RIDEV_INPUTSINK,
            hwndTarget = window
        };

        if (!User32.RegisterRawInputDevices(rids, rids.Length, Marshal.SizeOf(typeof(RAWINPUTDEVICE))))
        {
            Console.WriteLine("[Warning] Raw input registration failed with error: {0}. Device: {1}",
                Marshal.GetLastWin32Error(), rids[0].ToString());
        }
        else
        {
            Console.WriteLine("Registered GamePad {0}: {1}", _gamePadStates.Count, device);
        }
    }
    
}
