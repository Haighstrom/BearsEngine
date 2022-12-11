using System.Runtime.InteropServices;
using System.Security;

namespace HaighFramework.Win32API;

/// <summary>
/// Graphics Device Interface (GDI) functions for device output, such as those for drawing and font management. https://docs.microsoft.com/en-us/windows/win32/gdi/windows-gdi
/// </summary>
[SuppressUnmanagedCodeSecurity]
internal static class GDI32
{
    private const string Library = "Gdi32.dll";

    /// <summary>
    /// The ChoosePixelFormat function attempts to match an appropriate pixel format supported by a device context to a given pixel format specification.
    /// </summary>
    /// <param name="hdc">Specifies the device context that the function examines to determine the best match for the pixel format descriptor pointed to by ppfd.</param>
    /// <param name="ppfd">Pointer to a PIXELFORMATDESCRIPTOR structure that specifies the requested pixel format.</param>
    /// <returns>If the function succeeds, the return value is a pixel format index (one-based) that is the closest match to the given pixel format descriptor.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
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

    /// <summary>
    /// The CreateSolidBrush function creates a logical brush that has the specified solid color.
    /// </summary>
    /// <param name="crColor">The color of the brush. To create a COLORREF color value, use the RGB macro. https://docs.microsoft.com/en-us/windows/win32/gdi/colorref</param>
    /// <returns>If the function succeeds, the return value identifies a logical brush.
    /// If the function fails, the return value is NULL.</returns>
    [DllImport(Library)]
    public static extern IntPtr CreateSolidBrush(uint crColor);
    
    /// <summary>
    /// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.
    /// </summary>
    /// <param name="ho">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the specified handle is not valid or is currently selected into a DC, the return value is zero.</returns>
    [DllImport(Library)]
    public static extern bool DeleteObject(IntPtr ho);
    
    /// <summary>
    /// The DescribePixelFormat function obtains information about the pixel format identified by iPixelFormat of the device associated with hdc. The function sets the members of the PIXELFORMATDESCRIPTOR structure pointed to by ppfd with that pixel format data.
    /// </summary>
    /// <param name="hdc">Specifies the device context.</param>
    /// <param name="iPixelFormat">Index that specifies the pixel format. The pixel formats that a device context supports are identified by positive one-based integer indexes.</param>
    /// <param name="nBytes">The size, in bytes, of the structure pointed to by ppfd. The DescribePixelFormat function stores no more than nBytes bytes of data to that structure. Set this value to sizeof(PIXELFORMATDESCRIPTOR).</param>
    /// <param name="ppfd">Pointer to a PIXELFORMATDESCRIPTOR structure whose members the function sets with pixel format data. The function stores the number of bytes copied to the structure in the structure's nSize member. If, upon entry, ppfd is NULL, the function writes no data to the structure. This is useful when you only want to obtain the maximum pixel format index of a device context.</param>
    /// <returns>If the function succeeds, the return value is the maximum pixel format index of the device context. In addition, the function sets the members of the PIXELFORMATDESCRIPTOR structure pointed to by ppfd according to the specified pixel format.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern int DescribePixelFormat(IntPtr hdc, int iPixelFormat, int nBytes, ref PIXELFORMATDESCRIPTOR ppfd);

    /// <summary>
    /// The GetDeviceCaps function retrieves device-specific information for the specified device.
    /// </summary>
    /// <param name="hDC">A handle to the DC.</param>
    /// <param name="nIndex">The item to be returned.</param>
    /// <returns>The return value specifies the value of the desired item.</returns>
    [DllImport(Library)]
    public static extern int GetDeviceCaps(IntPtr hDC, GETDEVICECAPS_INDEX nIndex);

    /// <summary>
    /// The SetPixelFormat function sets the pixel format of the specified device context to the format specified by the iPixelFormat index.
    /// </summary>
    /// <param name="hdc">Specifies the device context whose pixel format the function attempts to set.</param>
    /// <param name="format">Index that identifies the pixel format to set. The various pixel formats supported by a device context are identified by one-based indexes.</param>
    /// <param name="ppfd">Pointer to a PIXELFORMATDESCRIPTOR structure that contains the logical pixel format specification. The system's metafile component uses this structure to record the logical pixel format specification. The structure has no other effect upon the behavior of the SetPixelFormat function.</param>
    /// <returns>If the function succeeds, the return value is TRUE. If the function fails, the return value is FALSE. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool SetPixelFormat(IntPtr hdc, int format, ref PIXELFORMATDESCRIPTOR ppfd);

    /// <summary>
    /// The SetTextColor function sets the text color for the specified device context to the specified color.
    /// </summary>
    /// <param name="hdc">A handle to the device context.</param>
    /// <param name="color">The color of the text.</param>
    /// <returns>If the function succeeds, the return value is a color reference for the previous text color as a COLORREF value. If the function fails, the return value is CLR_INVALID.</returns>
    [DllImport(Library)]
    public static extern uint SetTextColor(IntPtr hdc, uint color);

    /// <summary>
    /// The SwapBuffers function exchanges the front and back buffers if the current pixel format for the window referenced by the specified device context includes a back buffer.
    /// </summary>
    /// <param name="unnamedParam1">Specifies a device context. If the current pixel format for the window referenced by this device context includes a back buffer, the function exchanges the front and back buffers.</param>
    /// <returns>If the function succeeds, the return value is TRUE. If the function fails, the return value is FALSE. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool SwapBuffers(IntPtr unnamedParam1);

    /// <summary>
    /// The TextOut function writes a character string at the specified location, using the currently selected font, background color, and text color.
    /// </summary>
    /// <param name="hdc">A handle to the device context.</param>
    /// <param name="x">The x-coordinate, in logical coordinates, of the reference point that the system uses to align the string.</param>
    /// <param name="y">The y-coordinate, in logical coordinates, of the reference point that the system uses to align the string.</param>
    /// <param name="lpString">A pointer to the string to be drawn. The string does not need to be zero-terminated, because cchString specifies the length of the string.</param>
    /// <param name="c">The length of the string pointed to by lpString, in characters.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
    [DllImport(Library, CharSet = CharSet.Unicode)]
    public static extern bool TextOut(IntPtr hdc, int x, int y, string lpString, int c);
}