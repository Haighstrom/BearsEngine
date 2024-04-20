using System.Runtime.InteropServices;

namespace BearsEngine.WinAPI;

#region Enums
/// <summary>
/// The return value of <see cref="Shell32.SHAppBarMessage"/> when <see cref="APPBARMESSAGE.ABM_GETSTATE"/> is requested.
/// </summary>
[Flags]
internal enum ABM_GETSTATE_VALUE : int
{
    /// <summary>
    /// The taskbar is in the always-on-top state. [!Note] As of Windows 7, ABS_ALWAYSONTOP is no longer returned because the taskbar is always in that state.
    /// </summary>
    ABS_ALWAYSONTOP = 0x0000002,

    /// <summary>
    /// The taskbar is in the autohide state.
    /// </summary>
    ABS_AUTOHIDE = 0x0000001,
}

/// <summary>
/// Appbar message value to send. https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shappbarmessage
/// </summary>
internal enum APPBARMESSAGE : uint
{
    /// <summary>
    /// Registers a new appbar and specifies the message identifier that the system should use to send notification messages to the appbar.
    /// </summary>
    ABM_NEW = 0x00000000,

    /// <summary>
    /// Unregisters an appbar, removing the bar from the system's internal list.
    /// </summary>
    ABM_REMOVE = 0x00000001,

    /// <summary>
    /// Requests a size and screen position for an appbar.
    /// </summary>
    ABM_QUERYPOS = 0x00000002,

    /// <summary>
    /// Sets the size and screen position of an appbar.
    /// </summary>
    ABM_SETPOS = 0x00000003,

    /// <summary>
    /// Retrieves the autohide and always-on-top states of the Windows taskbar.
    /// </summary>
    ABM_GETSTATE = 0x00000004,

    /// <summary>
    /// Retrieves the bounding rectangle of the Windows taskbar. Note that this applies only to the system taskbar. Other objects, particularly toolbars supplied with third-party software, also can be present. As a result, some of the screen area not covered by the Windows taskbar might not be visible to the user. To retrieve the area of the screen not covered by both the taskbar and other app bars—the working area available to your application—, use the GetMonitorInfo function.
    /// </summary>
    ABM_GETTASKBARPOS = 0x00000005,

    /// <summary>
    /// Notifies the system to activate or deactivate an appbar. The lParam member of the APPBARDATA pointed to by pData is set to TRUE to activate or FALSE to deactivate.
    /// </summary>
    ABM_ACTIVATE = 0x00000006,

    /// <summary>
    /// Retrieves the handle to the autohide appbar associated with a particular edge of the screen.
    /// </summary>
    ABM_GETAUTOHIDEBAR = 0x00000007,

    /// <summary>
    /// Registers or unregisters an autohide appbar for an edge of the screen.
    /// </summary>
    ABM_SETAUTOHIDEBAR = 0x00000008,

    /// <summary>
    /// Notifies the system when an appbar's position has changed.
    /// </summary>
    ABM_WINDOWPOSCHANGED = 0x00000009,

    /// <summary>
    /// Windows XP and later: Sets the state of the appbar's autohide and always-on-top attributes.
    /// </summary>
    ABM_SETSTATE = 0x0000000A,

    /// <summary>
    /// Windows XP and later: Retrieves the handle to the autohide appbar associated with a particular edge of a particular monitor.
    /// </summary>
    ABM_GETAUTOHIDEBAREX = 0x0000000B,

    /// <summary>
    /// Windows XP and later: Registers or unregisters an autohide appbar for an edge of a particular monitor.
    /// </summary>
    ABM_SETAUTOHIDEBAREX = 0x0000000C,
}
#endregion

#region Structs
/// <summary>
/// Contains information about a system appbar message.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct APPBARDATA
{
    /// <summary>
    /// A value that specifies an edge of the screen.
    /// </summary>
    public enum ABEDGE : uint
    {
        /// <summary>
        /// Bottom edge.
        /// </summary>
        ABE_BOTTOM = 3,

        /// <summary>
        /// Left edge.
        /// </summary>
        ABE_LEFT = 0,

        /// <summary>
        /// Right edge.
        /// </summary>
        ABE_RIGHT = 2,

        /// <summary>
        /// Top edge.
        /// </summary>
        ABE_TOP = 1,
    }

    /// <summary>
    /// The size of the structure, in bytes.
    /// </summary>
    public uint cbSize;

    /// <summary>
    /// The handle to the appbar window. Not all messages use this member. See the individual message page to see if you need to provide an hWind value.
    /// </summary>
    public IntPtr hWnd;

    /// <summary>
    /// An application-defined message identifier. The application uses the specified identifier for notification messages that it sends to the appbar identified by the hWnd member. This member is used when sending the ABM_NEW message.
    /// </summary>
    public uint uCallbackMessage;

    /// <summary>
    /// A value that specifies an edge of the screen. This member is used when sending one of these messages: ABM_GETAUTOHIDEBAR, ABM_SETAUTOHIDEBAR, ABM_GETAUTOHIDEBAREX, ABM_SETAUTOHIDEBAREX, ABM_QUERYPOS, ABM_SETPOS
    /// </summary>
    public ABEDGE uEdge;

    /// <summary>
    /// A RECT structure whose use varies depending on the message:
    /// ABM_GETTASKBARPOS, ABM_QUERYPOS, ABM_SETPOS: The bounding rectangle, in screen coordinates, of an appbar or the Windows taskbar.
    /// ABM_GETAUTOHIDEBAREX, ABM_SETAUTOHIDEBAREX: The monitor on which the operation is being performed. This information can be retrieved through the GetMonitorInfo function.
    /// </summary>
    public RECT rc;

    /// <summary>
    /// A message-dependent value. This member is used with these messages: ABM_SETAUTOHIDEBAR ABM_SETAUTOHIDEBAREX ABM_SETSTATE. See the individual message pages for details.
    /// </summary>
    public int lParam;
}
#endregion

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
    public static extern IntPtr SHAppBarMessage(APPBARMESSAGE dwMessage, [In] ref APPBARDATA pData);
}
