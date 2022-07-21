using System.Runtime.InteropServices;
using System.Security;

namespace BearsEngine.Win32API;

[SuppressUnmanagedCodeSecurity]
internal static class WinMM
{

    // * * * CLEANED UP ABOVE THIS LINE * * *

    [DllImport("Winmm.dll")]
    public static extern JoystickError joyGetDevCaps(int uJoyID, out JoyCaps pjc, int cbjc);
    [DllImport("Winmm.dll")]
    public static extern JoystickError joyGetPos(int uJoyID, ref JoyInfo pji);
    [DllImport("Winmm.dll")]
    public static extern JoystickError joyGetPosEx(int uJoyID, ref JoyInfoEx pji);
    [DllImport("Winmm.dll")]
    public static extern int joyGetNumDevs();
    [DllImport("Winmm.dll")]
    public static extern JoystickError joyConfigChanged(int flags);
}