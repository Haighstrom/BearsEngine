using System.Runtime.InteropServices;
using System.Security;

namespace BearsEngine.Win32API;

[SuppressUnmanagedCodeSecurity]
internal static class GLU32
{

    // * * * CLEANED UP ABOVE THIS LINE * * *

    [DllImport("glu32.dll")]
    internal extern static int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, IntPtr data);
    

    [DllImport("glu32.dll")]
    internal static extern void gluLookAt(double eyex, double eyey, double eyez, double centerx, double centery, double centerz, double upx, double upy, double upz);
    

    [DllImport("glu32.dll")]
    internal static extern void gluPerspective(double fovy, double aspect, double zNear, double zFar);
    
}