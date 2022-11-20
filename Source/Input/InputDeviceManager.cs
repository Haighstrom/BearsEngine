using BearsEngine.Win32API;
using System.Runtime.InteropServices;
using BearsEngine.Window;
using System.Threading;

namespace BearsEngine.Input;

public class InputDeviceManager : IInputDeviceManager
{
    private static RawInput _rawInput = new(); 
    static readonly Guid DeviceInterfaceHid = new("4D1E55B2-F16F-11CF-88CB-001111000030");
    

    private MouseManager _mouseManager;
    private KeyboardManager _keyboardManager;
    private GamePadManager _gamePadManager;

    private MessageOnlyWindow _inputWindow;
    private readonly Thread _thread;
    private readonly AutoResetEvent _ready = new(false);

    private IntPtr _registrationHandle;
    

    public InputDeviceManager()
    {
        _thread = new Thread(ProcessInputData);
        _thread.Name = "InputDeviceManager Thread";
        _thread.SetApartmentState(ApartmentState.STA);
        _thread.IsBackground = true;
        _thread.Start();

        _ready.WaitOne();
    }

    private void ProcessInputData()
    {
        _inputWindow = new MessageOnlyWindow(WindowProcedure);

        CreateDrivers();

        _ready.Set();

        //n.b. call Dispose or DestroyWindow to stop this loop
        _inputWindow.ProcessEventsUntilDestroyed();
    }
    private void CreateDrivers()
    {
        BE.Logging.Information("--------Input Devices--------\n");
        _mouseManager = new MouseManager(_inputWindow.Handle);
        _keyboardManager = new KeyboardManager(_inputWindow.Handle);
        _gamePadManager = new GamePadManager(_inputWindow.Handle);
        BE.Logging.Information("\n-----------------------------\n");

        RegisterForRawInput();
    }
    private void RegisterForRawInput()
    {
        DevBroadcastHDR dbHdr = new();
        dbHdr.Size = Marshal.SizeOf(dbHdr);
        dbHdr.DeviceType = DeviceBroadcastType.INTERFACE;
        dbHdr.ClassGuid = DeviceInterfaceHid;
        unsafe
        {
            _registrationHandle = User32.RegisterDeviceNotification(_inputWindow.Handle, new IntPtr(&dbHdr), DeviceNotification.WINDOW_HANDLE);
        }
        if (_registrationHandle == IntPtr.Zero)
        {
            //todo: warnings
        }
    }

    private IntPtr _unhandled = new(-1);
    private IntPtr WindowProcedure(IntPtr handle, WINDOWMESSAGE message, IntPtr wParam, IntPtr lParam)
    {
        switch (message)
        {
            case WINDOWMESSAGE.WM_INPUT:
                int size = 0;
                User32.GetRawInputData(lParam, GetRawInputDataEnum.INPUT, IntPtr.Zero, ref size, RawInputHeader.SIZE);

                if (size == User32.GetRawInputData(lParam, GetRawInputDataEnum.INPUT, out _rawInput, ref size, RawInputHeader.SIZE))
                {
                    switch (_rawInput.Header.Type)
                    {
                        case RawInputDeviceType.KEYBOARD:
                            if (_keyboardManager.ProcessInput(_rawInput))
                            {
                                return IntPtr.Zero;
                            }
                            break;
                        case RawInputDeviceType.MOUSE:
                            if (_mouseManager.ProcessInput(_rawInput))
                            {
                                return IntPtr.Zero;
                            }
                            break;
                        case RawInputDeviceType.HID:
                            break;
                    }
                }
                break;
            

            case WINDOWMESSAGE.WM_DEVICECHANGE:
                BE.Logging.Information("Input Devices Change detected. Identifying new devices...");

                _mouseManager.RefreshDevices();
                _keyboardManager.RefreshDevices();

                break;
            
        }
        return _unhandled;
    }
    


    public IMouseManager MouseManager => _mouseManager;

    public IKeyboardManager KeyboardManager => _keyboardManager;
    


    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool manual)
    {
        if (!_disposed)
        {
            if (manual)
            {
                if (_inputWindow != null)
                {
                    _inputWindow.Close();
                    _inputWindow.Dispose();
                }
                else
                {
                    //todo: warnings
                }
            }
            _disposed = true;
        }
    }

    ~InputDeviceManager()
    {
        //todo: warning for leakage
        Dispose(false);
    }

    
}
