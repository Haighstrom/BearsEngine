using System.Runtime.InteropServices;

namespace BearsEngine.Win32API;

internal static class Shell32
{

    // * * * CLEANED UP ABOVE THIS LINE * * *
    [DllImport("shell32.dll")]
    internal static extern IntPtr SHAppBarMessage(ABM dwMessage, [In] ref APPBARDATA pData);
    
}