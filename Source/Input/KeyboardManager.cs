using BearsEngine.Win32API;
using Microsoft.Win32;
using System.Runtime.InteropServices;


namespace BearsEngine.Input;

public class KeyboardManager : IKeyboardManager
{
    private readonly List<KeyboardState> _keyboards = new();
    private readonly List<string> _names = new();
    private readonly Dictionary<IntPtr, int> _regdDevices = new();
    private readonly object _syncRoot = new();
    private readonly IntPtr _msgWindowHandle;
    

    public KeyboardManager(IntPtr messageWindowHandle)
    {
        if (messageWindowHandle == IntPtr.Zero)
            throw new ArgumentNullException("messageWindowHandle");

        _msgWindowHandle = messageWindowHandle;

        RefreshDevices();
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
    

    private void RegisterRawDevice(IntPtr window, string device)
    {
        RawInputDevice[] rid = new RawInputDevice[1];
        // Keyboard is 1/6 (page/id). See http://www.microsoft.com/whdc/device/input/HID_HWID.mspx
        rid[0] = new RawInputDevice();
        rid[0].UsagePage = 1;
        rid[0].Usage = 6;
        rid[0].Flags = RawInputDeviceFlags.INPUTSINK;
        rid[0].Target = window;

        if (!User32.RegisterRawInputDevices(rid))
        {
            Console.WriteLine("[Warning] Raw input registration failed with error: {0}. Device: {1}",
                Marshal.GetLastWin32Error(), rid[0].ToString());
        }
        else
        {
            Console.WriteLine("Registered Keyboard {0}: {1}", _keyboards.Count, device);
        }
    }
    
    

    public KeyboardState State
    {
        get
        {
            lock (_syncRoot)
            {
                KeyboardState consolidated = new();
                foreach (KeyboardState k in _keyboards)
                {
                    consolidated.MergeBits(k);
                }
                return consolidated;
            }
        }
    }
    

    public KeyboardState GetState(int index)
    {
        lock (_syncRoot)
        {
            if (index < _keyboards.Count)
                return _keyboards[index];
            else
                return new KeyboardState();
        }
    }
    

    public void RefreshDevices()
    {
        lock (_syncRoot)
        {
            //mark all devices as disconnected
            for (int i = 0; i < _keyboards.Count; i++)
            {
                KeyboardState state = _keyboards[i];
                state.IsConnected = false;
                _keyboards[i] = state;
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
                    KeyboardState state = _keyboards[_regdDevices[d.Device]];
                    state.IsConnected = true;
                    _keyboards[_regdDevices[d.Device]] = state;
                    continue;
                }

                //unregistered device, find out what it is
                string name = GetDeviceName(d);
                if (name.ToLower().Contains("root"))
                {
                    //this is a terminal services device, skip
                    continue;
                }
                else if (d.Type == RawInputDeviceType.KEYBOARD || d.Type == RawInputDeviceType.HID)
                {
                    //keyboard or USB device (which could be a keyboard).
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
                        deviceDesc = "Windows Mouse " + _keyboards.Count;
                    else
                        deviceDesc = deviceDesc.Substring(deviceDesc.LastIndexOf(';') + 1);

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
                            KeyboardState state = new();
                            state.IsConnected = true;
                            _keyboards.Add(state);
                            _names.Add(deviceDesc);
                            _regdDevices.Add(d.Device, _keyboards.Count - 1);
                        }
                    }
                }

            }

        }
    }
    

    internal bool ProcessInput(RawInput data)
    {
        IntPtr dHandle = data.Header.Device;
        RawKeyboard kData = data.Data.Keyboard;

        bool pressed = kData.Message == (int)WINDOWMESSAGE.WM_KEYDOWN || kData.Message == (int)WINDOWMESSAGE.WM_SYSKEYDOWN;

        var scancode = kData.MakeCode;
        var vkey = kData.VKey;

        bool extended0 = (int)(kData.Flags & RawInputKeyboardDataFlags.E0) != 0;
        bool extended1 = (int)(kData.Flags & RawInputKeyboardDataFlags.E1) != 0;

        //this is needed for weird devices... or something
        if (!_regdDevices.ContainsKey(dHandle))
            RefreshDevices();

        if (_keyboards.Count == 0)
            return false;

        //if this device is firing an event but isn't in our dictionary for some reason I guess we process it anyway (add it to the first mouse)
        int keyboardID = _regdDevices.ContainsKey(dHandle) ? _regdDevices[dHandle] : 0;
        KeyboardState keyboard = _keyboards[keyboardID];

        //now we're ready to process the data
        Key key = KeyMap.TranslateKey(scancode, vkey, extended0);
        bool processed = false;

        if (key != Key.Unknown)
        {
            keyboard.SetKeyState(key, pressed);
            processed = true;
        }
        lock (_syncRoot)
        {
            _keyboards[keyboardID] = keyboard;
            return processed;
        }
    }

    

    /// <summary>
    /// Called whenever a character, text number or symbol, is input by the keyboard. Will not record modifier keys like shift and alt.
    /// This reflects the actual character input, ie takes into account caps lock, shift keys, numlock etc etc and will catch rapid-fire inputs from a key held down for an extended time. 
    /// Use for eg text box input, rather than for controlling a game character (Use Input.GetKeyboardState)
    /// </summary>
    public event EventHandler<KeyboardCharEventArgs> CharEntered;

    /// <summary>
    /// Called whenever a keyboard key is pressed
    /// </summary>
    public event EventHandler<KeyboardKeyEventArgs> KeyDown;

    /// <summary>
    /// Called whenever a keyboard key is released
    /// </summary>
    public event EventHandler<KeyboardKeyEventArgs> KeyUp;
    
}
