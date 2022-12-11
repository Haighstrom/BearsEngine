using System.Runtime.InteropServices;

namespace HaighFramework.Win32API;

internal static class Shell32
{
    private const string Library = "Shell32.dll";

    /// <summary>
    /// Sends an appbar message to the system.
    /// </summary>
    /// <param name="dwMessage">Appbar message value to send.</param>
    /// <param name="pData">A pointer to an APPBARDATA structure. The content of the structure on entry and on exit depends on the value set in the dwMessage parameter. See the individual message pages for specifics.</param>
    /// <returns>This function returns a message-dependent value. For more information, see the Windows SDK documentation for the specific appbar message sent. Links to those documents are given in the See Also section.</returns>
    [DllImport(Library)]
    internal static extern IntPtr SHAppBarMessage(APPBARMESSAGE dwMessage, [In] ref APPBARDATA pData);
}