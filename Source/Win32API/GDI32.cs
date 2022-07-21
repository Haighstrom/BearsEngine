using System.Runtime.InteropServices;
using System.Security;

namespace BearsEngine.Win32API;

[SuppressUnmanagedCodeSecurity]
internal static class GDI32
{
    #region CreateSolidBrush
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-createsolidbrush
    /// The CreateSolidBrush function creates a logical brush that has the specified solid color.
    /// </summary>
    /// <param name="crColor">The color of the brush. To create a COLORREF color value, use the RGB macro. https://docs.microsoft.com/en-us/windows/win32/gdi/colorref</param>
    /// <returns>If the function succeeds, the return value identifies a logical brush.
    /// If the function fails, the return value is NULL.</returns>
    [DllImport("gdi32.dll")]
    static extern IntPtr CreateSolidBrush(uint crColor);
    #endregion

    #region DeleteObject
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-deleteobject
    /// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.
    /// </summary>
    /// <param name="ho">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the specified handle is not valid or is currently selected into a DC, the return value is zero.</returns>
    [DllImport("gdi32.dll")]
    static extern bool DeleteObject(IntPtr ho);
    #endregion

    // * * * CLEANED UP ABOVE THIS LINE * * *

    #region ChoosePixelFormat
    [DllImport("gdi32.dll")]
    internal static extern int ChoosePixelFormat(IntPtr dc, [In] IntPtr pfd);

    internal static int ChoosePixelFormat(IntPtr deviceContext, PixelFormatDescriptor pfd)
    {
        int pixelformat = 0;
        GCHandle pfd_ptr = GCHandle.Alloc(pfd, GCHandleType.Pinned);
        try
        {
            pixelformat = ChoosePixelFormat(deviceContext, pfd_ptr.AddrOfPinnedObject());
        }
        finally
        {
            pfd_ptr.Free();
        }
        return pixelformat;
    }
    internal static int ChoosePixelFormat(IntPtr deviceContext, PIXELFORMATDESCRIPTOR pfd)
    {
        int pixelformat = 0;
        GCHandle pfd_ptr = GCHandle.Alloc(pfd, GCHandleType.Pinned);
        try
        {
            pixelformat = ChoosePixelFormat(deviceContext, pfd_ptr.AddrOfPinnedObject());
        }
        finally
        {
            pfd_ptr.Free();
        }
        return pixelformat;
    }
    #endregion

    #region DescribePixelFormat
    [DllImport("gdi32.dll")]
    internal static extern int DescribePixelFormat(IntPtr deviceContext, int pixel, int pfdSize, ref PixelFormatDescriptor pixelFormat);
    #endregion

    #region GetDeviceCaps
    [DllImport("gdi32.dll")]
    internal static extern int GetDeviceCaps(IntPtr hDC, DeviceCaps nIndex);
    #endregion

    #region SetPixelFormat
    [DllImport("gdi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool SetPixelFormat(IntPtr dc, int format, ref PixelFormatDescriptor pfd);
    #endregion

    #region SetTextColor
    [DllImport("gdi32.dll")]
    internal static extern uint SetTextColor(IntPtr hdc, int crColor);
    #endregion

    #region SwapBuffers
    [DllImport("gdi32.dll")]
    internal static extern bool SwapBuffers(IntPtr dc);
    #endregion

    #region TextOut
    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern bool TextOut(IntPtr hdc, int nXStart, int nYStart, string lpString, int cbString);
    #endregion
}
