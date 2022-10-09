using System.Runtime.InteropServices;
using System.Security;

namespace BearsEngine.Win32API;

// https://docs.microsoft.com/en-us/windows/win32/gdi/windows-gdi

/// <summary>
/// Windows Graphic Design Interface
/// </summary>
[SuppressUnmanagedCodeSecurity]
internal static class GDI32
{
    private const string Library = "gdi32.dll";

    // https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-choosepixelformat

    /// <summary>
    /// The ChoosePixelFormat function attempts to match an appropriate pixel format supported by a device context to a given pixel format specification.
    /// </summary>
    /// <param name="hdc">Specifies the device context that the function examines to determine the best match for the pixel format descriptor pointed to by ppfd.</param>
    /// <param name="ppfd">Pointer to a PIXELFORMATDESCRIPTOR structure that specifies the requested pixel format.</param>
    /// <returns>If the function succeeds, the return value is a pixel format index (one-based) that is the closest match to the given pixel format descriptor.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library)]
    public static extern int ChoosePixelFormat(IntPtr hdc, IntPtr ppfd);

    /// <summary>
    /// The ChoosePixelFormat function attempts to match an appropriate pixel format supported by a device context to a given pixel format specification.
    /// </summary>
    /// <param name="hdc">Specifies the device context that the function examines to determine the best match for the pixel format descriptor pointed to by ppfd.</param>
    /// <param name="ppfd">A PIXELFORMATDESCRIPTOR structure that specifies the requested pixel format.</param>
    /// <returns>If the function succeeds, the return value is a pixel format index (one-based) that is the closest match to the given pixel format descriptor.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    public static int ChoosePixelFormat(IntPtr hdc, PIXELFORMATDESCRIPTOR ppfd)
    {
        int pixelformat = 0;
        GCHandle pfd_ptr = GCHandle.Alloc(ppfd, GCHandleType.Pinned);
        try
        {
            pixelformat = ChoosePixelFormat(hdc, pfd_ptr.AddrOfPinnedObject());
        }
        finally
        {
            pfd_ptr.Free();
        }
        return pixelformat;
    }
    

    // https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-createsolidbrush

    /// <summary>
    /// The CreateSolidBrush function creates a logical brush that has the specified solid color.
    /// </summary>
    /// <param name="crColor">The color of the brush. To create a COLORREF color value, use the RGB macro. https://docs.microsoft.com/en-us/windows/win32/gdi/colorref</param>
    /// <returns>If the function succeeds, the return value identifies a logical brush.
    /// If the function fails, the return value is NULL.</returns>
    [DllImport(Library)]
    public static extern IntPtr CreateSolidBrush(uint crColor);
    

    // https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-deleteobject

    /// <summary>
    /// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.
    /// </summary>
    /// <param name="ho">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the specified handle is not valid or is currently selected into a DC, the return value is zero.</returns>
    [DllImport(Library)]
    public static extern bool DeleteObject(IntPtr ho);
    

    // https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-describepixelformat

    /// <summary>
    /// The DescribePixelFormat function obtains information about the pixel format identified by iPixelFormat of the device associated with hdc. The function sets the members of the PIXELFORMATDESCRIPTOR structure pointed to by ppfd with that pixel format data.
    /// </summary>
    /// <param name="hdc">Specifies the device context.</param>
    /// <param name="iPixelFormat">Index that specifies the pixel format. The pixel formats that a device context supports are identified by positive one-based integer indexes.</param>
    /// <param name="nBytes">The size, in bytes, of the structure pointed to by ppfd. The DescribePixelFormat function stores no more than nBytes bytes of data to that structure. Set this value to sizeof(PIXELFORMATDESCRIPTOR).</param>
    /// <param name="ppfd">Pointer to a PIXELFORMATDESCRIPTOR structure whose members the function sets with pixel format data. The function stores the number of bytes copied to the structure in the structure's nSize member. If, upon entry, ppfd is NULL, the function writes no data to the structure. This is useful when you only want to obtain the maximum pixel format index of a device context.</param>
    /// <returns>If the function succeeds, the return value is the maximum pixel format index of the device context. In addition, the function sets the members of the PIXELFORMATDESCRIPTOR structure pointed to by ppfd according to the specified pixel format.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library)]
    public static extern int DescribePixelFormat(IntPtr hdc, int iPixelFormat, int nBytes, ref PIXELFORMATDESCRIPTOR ppfd);
    

    // * * * CLEANED UP ABOVE THIS LINE * * *




    // https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-getdevicecaps

    /// <summary>
    /// The GetDeviceCaps function retrieves device-specific information for the specified device.
    /// </summary>
    /// <param name="hDC">A handle to the DC.</param>
    /// <param name="nIndex">The item to be returned.</param>
    /// <returns></returns>
    [DllImport(Library)]
    public static extern int GetDeviceCaps(IntPtr hDC, DeviceCaps nIndex);
    

    [DllImport(Library, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetPixelFormat(IntPtr dc, int format, ref PIXELFORMATDESCRIPTOR pfd);
    

    [DllImport(Library)]
    public static extern uint SetTextColor(IntPtr hdc, int crColor);
    

    [DllImport(Library)]
    public static extern bool SwapBuffers(IntPtr dc);
    

    [DllImport(Library, CharSet = CharSet.Unicode)]
    public static extern bool TextOut(IntPtr hdc, int nXStart, int nYStart, string lpString, int cbString);
    
}
