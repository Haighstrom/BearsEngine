using System.Runtime.InteropServices;

namespace BearsEngine.Taskbar;

public class TaskbarManager : ITaskbarManager
{
    private readonly ITaskbarManager _api;

    public TaskbarManager()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            _api = new API.Windows.Win32TaskbarManager();
        else
            throw new NotImplementedException();
    }

    public bool AlwaysOnTop => _api.AlwaysOnTop;

    public bool AutoHide => _api.AutoHide;

    public int Height => _api.Height;

    public int Width => _api.Width;

    public TaskbarPosition Position => _api.Position;

    public int X => _api.X;

    public int Y => _api.Y;
}
