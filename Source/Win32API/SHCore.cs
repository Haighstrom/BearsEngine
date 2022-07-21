using System.Runtime.InteropServices;
using System.Security;

namespace BearsEngine.Win32API;

[SuppressUnmanagedCodeSecurity]
internal static class SHCore
{
    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool SetProcessDpiAwarenessContext(IntPtr dpiFlag);
    internal static bool SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT dpiFlag) => SetProcessDpiAwarenessContext(new IntPtr((int)dpiFlag));

    [DllImport("SHCore.dll", SetLastError = true)]
    internal static extern bool SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);

    [DllImport("shcore.dll")]
    internal static extern uint GetDpiForMonitor(IntPtr hmonitor, MonitorDpiType dpiType, out uint dpiX, out uint dpiY);
}
