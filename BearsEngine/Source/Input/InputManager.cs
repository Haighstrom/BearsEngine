using System.Runtime.InteropServices;

namespace BearsEngine.Input;

/// <summary>
/// Class for getting information about input devices and input received.
/// </summary>
public class InputManager : IInputManager
{
    private readonly IInputManager _api;
    private bool _disposed;

    public InputManager()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            _api = new Windows.WinAPIInputManager();
        else
            throw new PlatformNotSupportedException();
    }

    /// <summary>
    /// The default time for double clicking based on the OS settings.
    /// </summary>
    public int DoubleClickTime => _api.DoubleClickTime;

    /// <summary>
    /// The state of the mouse.
    /// </summary>
    /// <remarks>If multiple mice are attached their states will be aggregated.</remarks>
    public MouseState MouseState => _api.MouseState;

    /// <summary>
    /// The state of the keyboard.
    /// </summary>
    /// <remarks>If multiple keyboards are attached their states will be aggregated.</remarks>
    public KeyboardState KeyboardState => _api.KeyboardState;

    protected virtual void Dispose(bool disposedCorrectly)
    {
        if (!_disposed)
        {
            if (disposedCorrectly)
            {
                _api.Dispose();
            }
            else
                Log.Warning($"Did not dispose {nameof(InputManager)} correctly.");

            _disposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposedCorrectly: true);
        GC.SuppressFinalize(this);
    }
}
