using System.Runtime.InteropServices;
using System.Threading;
using BearsEngine.WinAPI;

namespace BearsEngine.Input.Windows;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This is part of the Windows API which will only be invoked if on Windows platform.")]
internal class WinAPIInputManager : IInputReader
{
    private static readonly Guid GUID_DEVINTERFACE_HID = new("4D1E55B2-F16F-11CF-88CB-001111000030"); //https://learn.microsoft.com/en-us/windows-hardware/drivers/install/guid-devinterface-hid

    private InputProcessingWindow? _inputWindow;
    private KeyboardAPI? _keyboardManager;
    private MouseAPI? _mouseManager;
    private RawInput _rawInput = new();
    private readonly AutoResetEvent _inputWindowThreadReadyCheck = new(false);
    private readonly Thread _inputWindowThread;

    public WinAPIInputManager()
    {
        _inputWindowThread = new Thread(ProcessInputData)
        {
            Name = "InputDeviceManager Thread",
            IsBackground = true,
        };

        _inputWindowThread.SetApartmentState(ApartmentState.STA);

        _inputWindowThread.Start();

        _inputWindowThreadReadyCheck.WaitOne();
    }

    public int DoubleClickTime => User32.GetDoubleClickTime();

    public MouseState MouseState => _mouseManager!.GetAggregateState();

    public KeyboardState KeyboardState => _keyboardManager!.GetAggregateState;

    private void ProcessInputData()
    {
        _inputWindow = new InputProcessingWindow(WindowProcedure);

        Log.WriteSectionHeader(LogLevel.Information, "Input Devices");

        _mouseManager = new MouseAPI(_inputWindow.Handle);
        _keyboardManager = new KeyboardAPI(_inputWindow.Handle);

        Log.WriteSectionBreak(LogLevel.Information);
        Log.WriteNewLine(LogLevel.Information);

        RegisterForRawInput();

        _inputWindowThreadReadyCheck.Set();

        //n.b. call Dispose or DestroyWindow to stop this loop
        _inputWindow.ProcessEventsUntilDestroyed();
    }

    private void RegisterForRawInput()
    {
        DEV_BROADCAST_DEVICEINTERFACE_A dbHdr = new();
        dbHdr.dbcc_size = Marshal.SizeOf(dbHdr);
        dbHdr.dbcc_devicetype = DEVBROADCASTTYPE.DBT_DEVTYP_DEVICEINTERFACE;
        dbHdr.dbcc_classguid = GUID_DEVINTERFACE_HID;

        unsafe
        {
            User32.RegisterDeviceNotification(_inputWindow!.Handle, new IntPtr(&dbHdr), DEVICENOTIFYFLAGS.DEVICE_NOTIFY_WINDOW_HANDLE);
        }
    }

    private void OnDeviceChange()
    {
        Log.Information("Input Devices Change detected. Identifying new devices:");

        _mouseManager!.UpdateDevices();
        _keyboardManager!.UpdateDevices();
    }

    private void OnInput(IntPtr lParam)
    {
        int size = 0;
        User32.GetRawInputData(lParam, RAWINPUTDATAFLAG.RID_INPUT, IntPtr.Zero, ref size, RawInputHeader.SIZE);

        if (User32.GetRawInputData(lParam, RAWINPUTDATAFLAG.RID_INPUT, out _rawInput, ref size, RawInputHeader.SIZE) > 0)
        {
            switch (_rawInput.Header.Type)
            {
                case RawInputDeviceType.KEYBOARD:
                    _keyboardManager!.ProcessInputData(_rawInput);
                    break;
                case RawInputDeviceType.MOUSE:
                    _mouseManager!.ProcessInputData(_rawInput);
                    break;
                default:
                    Log.Warning("Input received from a device that was not processed.");
                    break;
            }
        }
        else
            Log.Warning("Input received from a device that was not processed.");
    }

    private IntPtr WindowProcedure(IntPtr handle, WINDOWMESSAGE message, IntPtr wParam, IntPtr lParam)
    {
        switch (message)
        {
            case WINDOWMESSAGE.WM_DEVICECHANGE:
                OnDeviceChange();
                break;
            case WINDOWMESSAGE.WM_INPUT:
                OnInput(lParam);
                break;
        }
        return IntPtr.Zero;
    }


    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposedCorrectly)
    {
        if (!_disposed)
        {
            if (disposedCorrectly)
            {
            }
            if (_inputWindow is not null)
                _inputWindow.Destroy();
            _disposed = true;
        }
    }

    ~WinAPIInputManager()
    {
        Dispose(false);
    }
}
