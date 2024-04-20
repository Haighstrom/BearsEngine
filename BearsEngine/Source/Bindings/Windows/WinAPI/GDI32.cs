using System.Runtime.InteropServices;
using System.Security;

namespace BearsEngine.WinAPI;

#region Enums
/// <summary>
/// The item to be returned in GetDeviceCaps. https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-getdevicecaps
/// </summary>
internal enum GETDEVICECAPS_INDEX : int
{
    /// <summary>
    /// The device driver version.
    /// </summary>
    DRIVERVERSION = 0,

    /// <summary>
    /// Device technology. If the hdc parameter is a handle to the DC of an enhanced metafile, the device technology is that of the referenced device as specified to the CreateEnhMetaFile function. To determine whether it is an enhanced metafile DC, use the GetObjectType function.
    /// </summary>
    TECHNOLOGY = 2,

    /// <summary>
    /// Width, in millimeters, of the physical screen.
    /// </summary>
    HORZSIZE = 4,

    /// <summary>
    /// Height, in millimeters, of the physical screen.
    /// </summary>
    VERTSIZE = 6,

    /// <summary>
    /// Width, in pixels, of the screen; or for printers, the width, in pixels, of the printable area of the page.
    /// </summary>
    HORZRES = 8,

    /// <summary>
    /// Height, in raster lines, of the screen; or for printers, the height, in pixels, of the printable area of the page.
    /// </summary>
    VERTRES = 10,

    /// <summary>
    /// Number of pixels per logical inch along the screen width. In a system with multiple display monitors, this value is the same for all monitors.
    /// </summary>
    LOGPIXELSX = 88,

    /// <summary>
    /// Number of pixels per logical inch along the screen height. In a system with multiple display monitors, this value is the same for all monitors.
    /// </summary>
    LOGPIXELSY = 90,

    /// <summary>
    /// Number of adjacent color bits for each pixel.
    /// </summary>
    BITSPIXEL = 12,

    /// <summary>
    /// Number of color planes.
    /// </summary>
    PLANES = 14,

    /// <summary>
    /// Number of device-specific brushes.
    /// </summary>
    NUMBRUSHES = 16,

    /// <summary>
    /// Number of device-specific pens.
    /// </summary>
    NUMPENS = 18,

    /// <summary>
    /// Number of device-specific fonts.
    /// </summary>
    NUMFONTS = 22,

    /// <summary>
    /// Number of entries in the device's color table, if the device has a color depth of no more than 8 bits per pixel. For devices with greater color depths, 1 is returned.
    /// </summary>
    NUMCOLORS = 24,

    /// <summary>
    /// Relative width of a device pixel used for line drawing.
    /// </summary>
    ASPECTX = 40,

    /// <summary>
    /// Relative height of a device pixel used for line drawing.
    /// </summary>
    ASPECTY = 42,

    /// <summary>
    /// Diagonal width of the device pixel used for line drawing.
    /// </summary>
    ASPECTXY = 44,

    /// <summary>
    /// Reserved.
    /// </summary>
    PDEVICESIZE = 26,

    /// <summary>
    /// Flag that indicates the clipping capabilities of the device. If the device can clip to a rectangle, it is 1. Otherwise, it is 0.
    /// </summary>
    CLIPCAPS = 36,

    /// <summary>
    /// Number of entries in the system palette. This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is available only if the driver is compatible with 16-bit Windows.
    /// </summary>
    SIZEPALETTE = 104,

    /// <summary>
    /// Number of reserved entries in the system palette. This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is available only if the driver is compatible with 16-bit Windows.
    /// </summary>
    NUMRESERVED = 106,

    /// <summary>
    /// Actual color resolution of the device, in bits per pixel. This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is available only if the driver is compatible with 16-bit Windows.
    /// </summary>
    COLORRES = 108,

    /// <summary>
    /// For printing devices: the width of the physical page, in device units. For example, a printer set to print at 600 dpi on 8.5-x11-inch paper has a physical width value of 5100 device units. Note that the physical page is almost always greater than the printable area of the page, and never smaller.
    /// </summary>
    PHYSICALWIDTH = 110,

    /// <summary>
    /// For printing devices: the height of the physical page, in device units. For example, a printer set to print at 600 dpi on 8.5-by-11-inch paper has a physical height value of 6600 device units. Note that the physical page is almost always greater than the printable area of the page, and never smaller.
    /// </summary>
    PHYSICALHEIGHT = 111,

    /// <summary>
    /// For printing devices: the distance from the left edge of the physical page to the left edge of the printable area, in device units. For example, a printer set to print at 600 dpi on 8.5-by-11-inch paper, that cannot print on the leftmost 0.25-inch of paper, has a horizontal physical offset of 150 device units.
    /// </summary>
    PHYSICALOFFSETX = 112,

    /// <summary>
    /// For printing devices: the distance from the top edge of the physical page to the top edge of the printable area, in device units. For example, a printer set to print at 600 dpi on 8.5-by-11-inch paper, that cannot print on the topmost 0.5-inch of paper, has a vertical physical offset of 300 device units.
    /// </summary>
    PHYSICALOFFSETY = 113,

    /// <summary>
    /// For display devices: the current vertical refresh rate of the device, in cycles per second (Hz). A vertical refresh rate value of 0 or 1 represents the display hardware's default refresh rate. This default rate is typically set by switches on a display card or computer motherboard, or by a configuration program that does not use display functions such as ChangeDisplaySettings.
    /// </summary>
    VREFRESH = 116,

    /// <summary>
    /// Scaling factor for the x-axis of the printer.
    /// </summary>
    SCALINGFACTORX = 114,

    /// <summary>
    /// Scaling factor for the y-axis of the printer.
    /// </summary>
    SCALINGFACTORY = 115,

    /// <summary>
    /// Preferred horizontal drawing alignment, expressed as a multiple of pixels. For best drawing performance, windows should be horizontally aligned to a multiple of this value. A value of zero indicates that the device is accelerated, and any alignment may be used.
    /// </summary>
    BLTALIGNMENT = 119,

    /// <summary>
    /// Value that indicates the shading and blending capabilities of the device. See Remarks for further comments.
    /// </summary>
    SHADEBLENDCAPS = 45,

    /// <summary>
    /// Value that indicates the raster capabilities of the device
    /// </summary>
    RASTERCAPS = 38,

    /// <summary>
    /// Value that indicates the curve capabilities of the device
    /// </summary>
    CURVECAPS = 28,

    /// <summary>
    /// Value that indicates the line capabilities of the device
    /// </summary>
    LINECAPS = 30,

    /// <summary>
    /// Value that indicates the polygon capabilities of the device
    /// </summary>
    POLYGONALCAPS = 32,

    /// <summary>
    /// Value that indicates the text capabilities of the device
    /// </summary>
    TEXTCAPS = 34,

    /// <summary>
    /// Value that indicates the color management capabilities of the device
    /// </summary>
    COLORMGMTCAPS = 121,
}
#endregion

#region Structs
/// <summary>
/// The PIXELFORMATDESCRIPTOR structure describes the pixel format of a drawing surface
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal class PIXELFORMATDESCRIPTOR
{
    // https://docs.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-pixelformatdescriptor
    // https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-emf/1db036d6-2da8-4b92-b4f8-e9cab8cc93b7
    [Flags]
    public enum Flags : uint
    {
        /// <summary>
        /// The buffer can draw to a window or device surface.
        /// </summary>
        PFD_DRAW_TO_WINDOW = 0x00000004,

        /// <summary>
        /// The buffer can draw to a memory bitmap.
        /// </summary>
        PFD_DRAW_TO_BITMAP = 0x00000008,

        /// <summary>
        /// The buffer supports GDI drawing. This flag and PFD_DOUBLEBUFFER are mutually exclusive in the current generic implementation.
        /// </summary>
        PFD_SUPPORT_GDI = 0x00000010,

        /// <summary>
        /// The buffer supports OpenGL drawing.
        /// </summary>
        PFD_SUPPORT_OPENGL = 0x00000020,

        /// <summary>
        /// The pixel format is supported by a device driver that accelerates the generic implementation. If this flag is clear and the PFD_GENERIC_FORMAT flag is set, the pixel format is supported by the generic implementation only.
        /// </summary>
        PFD_GENERIC_ACCELERATED = 0x00001000,

        /// <summary>
        /// The pixel format is supported by the GDI software implementation, which is also known as the generic implementation. If this bit is clear, the pixel format is supported by a device driver or hardware.
        /// </summary>
        PFD_GENERIC_FORMAT = 0x00000040,

        /// <summary>
        /// The buffer uses RGBA pixels on a palette-managed device. A logical palette is required to achieve the best results for this pixel type. Colors in the palette should be specified according to the values of the cRedBits, cRedShift, cGreenBits, cGreenShift, cBluebits, and cBlueShift members. The palette should be created and realized in the device context before calling wglMakeCurrent.
        /// </summary>
        PFD_NEED_PALETTE = 0x00000080,

        /// <summary>
        /// 	Defined in the pixel format descriptors of hardware that supports one hardware palette in 256-color mode only. For such systems to use hardware acceleration, the hardware palette must be in a fixed order (for example, 3-3-2) when in RGBA mode or must match the logical palette when in color-index mode.When this flag is set, you must call SetSystemPaletteUse in your program to force a one-to-one mapping of the logical palette and the system palette. If your OpenGL hardware supports multiple hardware palettes and the device driver can allocate spare hardware palettes for OpenGL, this flag is typically clear. This flag is not set in the generic pixel formats.
        /// </summary>
        PFD_NEED_SYSTEM_PALETTE = 0x00000100,

        /// <summary>
        /// The buffer is double-buffered. This flag and PFD_SUPPORT_GDI are mutually exclusive in the current generic implementation.
        /// </summary>
        PFD_DOUBLEBUFFER = 0x00000001,

        /// <summary>
        /// The buffer is stereoscopic. This flag is not supported in the current generic implementation.
        /// </summary>
        PFD_STEREO = 0x00000002,

        /// <summary>
        /// Indicates whether a device can swap individual layer planes with pixel formats that include double-buffered overlay or underlay planes. Otherwise all layer planes are swapped together as a group. When this flag is set, wglSwapLayerBuffers is supported.
        /// </summary>
        PFD_SWAP_LAYER_BUFFERS = 0x00000800,

        /// <summary>
        /// You can specify this bit flag when calling ChoosePixelFormat. The requested pixel format can either have or not have a depth buffer. To select a pixel format without a depth buffer, you must specify this flag. The requested pixel format can be with or without a depth buffer. Otherwise, only pixel formats with a depth buffer are considered.
        /// </summary>
        PFD_DEPTH_DONTCARE = 0x20000000,

        /// <summary>
        /// You can specify this bit flag when calling ChoosePixelFormat. The requested pixel format can be either single- or double-buffered.
        /// </summary>
        PFD_DOUBLEBUFFER_DONTCARE = 0x40000000,

        /// <summary>
        /// You can specify this bit flag when calling ChoosePixelFormat. The requested pixel format can be either monoscopic or stereoscopic.
        /// </summary>
        PFD_STEREO_DONTCARE = 0x80000000,

        /// <summary>
        /// With the glAddSwapHintRectWIN extension function, this flag is included for the PIXELFORMATDESCRIPTOR pixel format structure. Specifies the content of the back buffer in the double-buffered main color plane following a buffer swap. Swapping the color buffers causes the content of the back buffer to be copied to the front buffer. The content of the back buffer is not affected by the swap. PFD_SWAP_COPY is a hint only and might not be provided by a driver.
        /// </summary>
        PFD_SWAP_COPY = 0x00000400,

        /// <summary>
        /// With the glAddSwapHintRectWIN extension function, this flag is included for the PIXELFORMATDESCRIPTOR pixel format structure. Specifies the content of the back buffer in the double-buffered main color plane following a buffer swap. Swapping the color buffers causes the exchange of the back buffer's content with the front buffer's content. Following the swap, the back buffer's content contains the front buffer's content before the swap. PFD_SWAP_EXCHANGE is a hint only and might not be provided by a driver.
        /// </summary>
        PFD_SWAP_EXCHANGE = 0x00000200,

        // Below only available in this documentation: https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-emf/1db036d6-2da8-4b92-b4f8-e9cab8cc93b7
        /// <summary>
        /// The pixel buffer supports DirectDraw drawing, which allows applications to have low-level control of the output drawing surface.
        /// </summary>
        PFD_SUPPORT_DIRECTDRAW = 0x00002000,

        /// <summary>
        /// The pixel buffer supports Direct3D drawing, which accellerated rendering in three dimensions.
        /// </summary>
        PFD_DIRECT3D_ACCELERATED = 0x00004000,

        /// <summary>
        /// The pixel buffer supports compositing, which indicates that source pixels MAY overwrite or be combined with background pixels.
        /// </summary>
        PFD_SUPPORT_COMPOSITION = 0x00008000,
    };

    /// <summary>
    /// Type of pixel data
    /// </summary>
    public enum PixelType : byte
    {
        /// <summary>
        /// RGBA pixels. Each pixel has four components in this order: red, green, blue, and alpha.
        /// </summary>
        PFD_TYPE_RGBA = 0,

        /// <summary>
        /// Color-index pixels. Each pixel uses a color-index value.
        /// </summary>
        PFD_TYPE_COLORINDEX = 1
    }

    public PIXELFORMATDESCRIPTOR()
    {
    }

    /// <summary>
    /// Specifies the size of this data structure. This value should be set to sizeof(PIXELFORMATDESCRIPTOR).
    /// </summary>
    private readonly short nSize = (short)Marshal.SizeOf<PIXELFORMATDESCRIPTOR>();

    /// <summary>
    /// Specifies the version of this data structure. This value should be set to 1.
    /// </summary>
    private readonly short nVersion = 1;

    /// <summary>
    /// A set of bit flags that specify properties of the pixel buffer. The properties are generally not mutually exclusive.
    /// </summary>
    public Flags dwFlags;

    /// <summary>
    /// Specifies the type of pixel data.
    /// </summary>
    public PixelType iPixelType;

    /// <summary>
    /// Specifies the number of color bitplanes in each color buffer. For RGBA pixel types, it is the size of the color buffer, excluding the alpha bitplanes. For color-index pixels, it is the size of the color-index buffer.
    /// </summary>
    public byte cColorBits;

    /// <summary>
    /// Specifies the number of red bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cRedBits;

    /// <summary>
    /// Specifies the shift count for red bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cRedShift;

    /// <summary>
    /// Specifies the number of green bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cGreenBits;

    /// <summary>
    /// Specifies the shift count for green bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cGreenShift;

    /// <summary>
    /// Specifies the number of blue bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cBlueBits;

    /// <summary>
    /// Specifies the shift count for blue bitplanes in each RGBA color buffer.
    /// </summary>
    public byte cBlueShift;

    /// <summary>
    /// Specifies the number of alpha bitplanes in each RGBA color buffer. Alpha bitplanes are not supported.
    /// </summary>
    public byte cAlphaBits;

    /// <summary>
    /// Specifies the shift count for alpha bitplanes in each RGBA color buffer. Alpha bitplanes are not supported.
    /// </summary>
    public byte cAlphaShift;

    /// <summary>
    /// Specifies the total number of bitplanes in the accumulation buffer.
    /// </summary>
    public byte cAccumBits;

    /// <summary>
    /// Specifies the number of red bitplanes in the accumulation buffer.
    /// </summary>
    public byte cAccumRedBits;

    /// <summary>
    /// Specifies the number of green bitplanes in the accumulation buffer.
    /// </summary>
    public byte cAccumGreenBits;

    /// <summary>
    /// Specifies the number of blue bitplanes in the accumulation buffer.
    /// </summary>
    public byte cAccumBlueBits;

    /// <summary>
    /// Specifies the number of alpha bitplanes in the accumulation buffer.
    /// </summary>
    public byte cAccumAlphaBits;

    /// <summary>
    /// Specifies the depth of the depth (z-axis) buffer.
    /// </summary>
    public byte cDepthBits;

    /// <summary>
    /// Specifies the depth of the stencil buffer.
    /// </summary>
    public byte cStencilBits;

    /// <summary>
    /// Specifies the number of auxiliary buffers. Auxiliary buffers are not supported.
    /// </summary>
    public byte cAuxBuffers;

    /// <summary>
    /// Ignored. Earlier implementations of OpenGL used this member, but it is no longer used.
    /// </summary>
    private readonly byte iLayerType;

    /// <summary>
    /// Specifies the number of overlay and underlay planes. Bits 0 through 3 specify up to 15 overlay planes and bits 4 through 7 specify up to 15 underlay planes.
    /// </summary>
    private readonly byte bReserved;

    /// <summary>
    /// Ignored. Earlier implementations of OpenGL used this member, but it is no longer used.
    /// </summary>
    private readonly int dwLayerMask;

    /// <summary>
    /// Specifies the transparent color or index of an underlay plane. When the pixel type is RGBA, dwVisibleMask is a transparent RGB color value. When the pixel type is color index, it is a transparent index value.
    /// </summary>
    public int dwVisibleMask;

    /// <summary>
    /// Ignored. Earlier implementations of OpenGL used this member, but it is no longer used.
    /// </summary>
    private readonly int dwDamageMask;
}
#endregion

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
