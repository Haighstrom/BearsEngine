using System.Runtime.InteropServices;
using System.Security;

namespace HaighFramework.Win32API;

/// <summary>
/// Graphics Library Utilities https://registry.khronos.org/OpenGL-Refpages/gl2.1/
/// </summary>
[SuppressUnmanagedCodeSecurity]
internal static class GLU32
{
    private const string Library = "Glu32.dll";

    /// <summary>
    /// Builds a two-dimensional mipmap
    /// </summary>
    /// <param name="target">Specifies the target texture. Must be GLU_TEXTURE_2D.</param>
    /// <param name="internalFormat">Requests the internal storage format of the texture image. The most current version of the SGI implementation of GLU does not check this value for validity before passing it on to the underlying OpenGL implementation. A value that is not accepted by the OpenGL implementation will lead to an OpenGL error. The benefit of not checking this value at the GLU level is that OpenGL extensions can add new internal texture formats without requiring a revision of the GLU implementation. Older implementations of GLU check this value and raise a GLU error if it is not 1, 2, 3, or 4 or one of the following symbolic constants: GLU_ALPHA, GLU_ALPHA4, GLU_ALPHA8, GLU_ALPHA12, GLU_ALPHA16, GLU_LUMINANCE, GLU_LUMINANCE4, GLU_LUMINANCE8, GLU_LUMINANCE12, GLU_LUMINANCE16, GLU_LUMINANCE_ALPHA, GLU_LUMINANCE4_ALPHA4, GLU_LUMINANCE6_ALPHA2, GLU_LUMINANCE8_ALPHA8, GLU_LUMINANCE12_ALPHA4, GLU_LUMINANCE12_ALPHA12, GLU_LUMINANCE16_ALPHA16, GLU_INTENSITY, GLU_INTENSITY4, GLU_INTENSITY8, GLU_INTENSITY12, GLU_INTENSITY16, GLU_RGB, GLU_R3_G3_B2, GLU_RGB4, GLU_RGB5, GLU_RGB8, GLU_RGB10, GLU_RGB12, GLU_RGB16, GLU_RGBA, GLU_RGBA2, GLU_RGBA4, GLU_RGB5_A1, GLU_RGBA8, GLU_RGB10_A2, GLU_RGBA12, or GLU_RGBA16.</param>
    /// <param name="width">Specifies in pixels the width and height, respectively, of the texture image.</param>
    /// <param name="height">Specifies in pixels the width and height, respectively, of the texture image.</param>
    /// <param name="format">Specifies the format of the pixel data. Must be one of GLU_COLOR_INDEX, GLU_DEPTH_COMPONENT, GLU_RED, GLU_GREEN, GLU_BLUE, GLU_ALPHA, GLU_RGB, GLU_RGBA, GLU_BGR, GLU_BGRA, GLU_LUMINANCE, or GLU_LUMINANCE_ALPHA.</param>
    /// <param name="type">Specifies the data type for data. Must be one of GLU_UNSIGNED_BYTE, GLU_BYTE, GLU_BITMAP, GLU_UNSIGNED_SHORT, GLU_SHORT, GLU_UNSIGNED_INT, GLU_INT, GLU_FLOAT, GLU_UNSIGNED_BYTE_3_3_2, GLU_UNSIGNED_BYTE_2_3_3_REV, GLU_UNSIGNED_SHORT_5_6_5, GLU_UNSIGNED_SHORT_5_6_5_REV, GLU_UNSIGNED_SHORT_4_4_4_4, GLU_UNSIGNED_SHORT_4_4_4_4_REV, GLU_UNSIGNED_SHORT_5_5_5_1, GLU_UNSIGNED_SHORT_1_5_5_5_REV, GLU_UNSIGNED_INT_8_8_8_8, GLU_UNSIGNED_INT_8_8_8_8_REV, GLU_UNSIGNED_INT_10_10_10_2, or GLU_UNSIGNED_INT_2_10_10_10_REV.</param>
    /// <param name="data">Specifies a pointer to the image data in memory.</param>
    /// <returns>A return value of zero indicates success, otherwise a GLU error code is returned.</returns>
    [DllImport(Library)]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, IntPtr data);

    /// <summary>
    /// Define a viewing transformation
    /// </summary>
    /// <param name="eyeX">Specifies the position of the eye point.</param>
    /// <param name="eyeY">Specifies the position of the eye point.</param>
    /// <param name="eyeZ">Specifies the position of the eye point.</param>
    /// <param name="centerX">Specifies the position of the reference point.</param>
    /// <param name="centerY">Specifies the position of the reference point.</param>
    /// <param name="centerZ">Specifies the position of the reference point.</param>
    /// <param name="upX">Specifies the direction of the up vector.</param>
    /// <param name="upY">Specifies the direction of the up vector.</param>
    /// <param name="upZ">Specifies the direction of the up vector.</param>
    [DllImport(Library)]
    public static extern void gluLookAt(double eyeX, double eyeY, double eyeZ, double centerX, double centerY, double centerZ, double upX, double upY, double upZ);

    /// <summary>
    /// Set up a perspective projection matrix
    /// </summary>
    /// <param name="fovy">Specifies the field of view angle, in degrees, in the y direction.</param>
    /// <param name="aspect">Specifies the aspect ratio that determines the field of view in the x direction. The aspect ratio is the ratio of x (width) to y (height).</param>
    /// <param name="zNear">Specifies the distance from the viewer to the near clipping plane (always positive).</param>
    /// <param name="zFar">Specifies the distance from the viewer to the far clipping plane (always positive).</param>
    [DllImport(Library)]
    public static extern void gluPerspective(double fovy, double aspect, double zNear, double zFar);
}