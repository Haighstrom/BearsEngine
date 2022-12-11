using System.Security;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using HaighFramework.Win32API;

namespace HaighFramework.OpenGL;

#region Enums
/// <summary>
/// <see cref="OpenGL32.glClear"/> takes a single argument that is the bitwise OR of several values indicating which buffer(s) are to be cleared.
/// </summary>
[Flags]
internal enum CLEAR_MASK : uint
{
    /// <summary>
    /// Indicates the buffers currently enabled for color writing.
    /// </summary>
    GL_COLOR_BUFFER_BIT = 0x00004000,

    /// <summary>
    /// Indicates the depth buffer.
    /// </summary>
    GL_DEPTH_BUFFER_BIT = 0x00000100,

    /// <summary>
    /// Indicates the accumulation buffer.
    /// </summary>
    GL_ACCUM_BUFFER_BIT = 0x00000200,

    /// <summary>
    /// Indicates the stencil buffer.
    /// </summary>
    GL_STENCIL_BUFFER_BIT = 0x00000400,
}
#endregion

[SuppressUnmanagedCodeSecurity]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "OpenGL functions have dumb naming conventions but I'm keeping these APIs pure.")]
internal static class OpenGL32
{
    private const string Library = "Opengl32.dll";

    /// <summary>
    /// Specify the alpha test function. Alpha testing is performed only in RGBA mode.
    /// </summary>
    /// <param name="func">Specifies the alpha comparison function. Symbolic constants GL_NEVER, GL_LESS, GL_EQUAL, GL_LEQUAL, GL_GREATER, GL_NOTEQUAL, GL_GEQUAL, and GL_ALWAYS are accepted. The initial value is GL_ALWAYS.</param>
    /// <param name="ref">Specifies the reference value that incoming alpha values are compared to. This value is clamped to the range 0 1 , where 0 represents the lowest possible alpha value and 1 the highest possible value. The initial reference value is 0.</param>
    [DllImport(Library)]
    public static extern void glAlphaFunc(GLAlphaFuncEnum func, float @ref);

    /// <summary>
    /// Delimit the vertices of a primitive or a group of like primitives
    /// </summary>
    /// <param name="mode">Specifies the primitive or primitives that will be created from vertices presented between glBegin and the subsequent glEnd. Ten symbolic constants are accepted: GL_POINTS, GL_LINES, GL_LINE_STRIP, GL_LINE_LOOP, GL_TRIANGLES, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_QUADS, GL_QUAD_STRIP, and GL_POLYGON.</param>
    [DllImport(Library)]
    public static extern void glBegin(int mode);

    /// <summary>
    /// Bind a named texture to a texturing target
    /// </summary>
    /// <param name="target">Specifies the target to which the texture is bound. Must be one of GL_TEXTURE_1D, GL_TEXTURE_2D, GL_TEXTURE_3D, GL_TEXTURE_1D_ARRAY, GL_TEXTURE_2D_ARRAY, GL_TEXTURE_RECTANGLE, GL_TEXTURE_CUBE_MAP, GL_TEXTURE_CUBE_MAP_ARRAY, GL_TEXTURE_BUFFER, GL_TEXTURE_2D_MULTISAMPLE or GL_TEXTURE_2D_MULTISAMPLE_ARRAY.</param>
    /// <param name="texture">Specifies the name of a texture.</param>
    [DllImport(Library)]
    public static extern void glBindTexture(TextureTarget target, uint texture);

    /// <summary>
    /// Specify pixel arithmetic
    /// </summary>
    /// <param name="sfactor">Specifies how the red, green, blue, and alpha source blending factors are computed. The initial value is GL_ONE.</param>
    /// <param name="dfactor">Specifies how the red, green, blue, and alpha destination blending factors are computed. The following symbolic constants are accepted: GL_ZERO, GL_ONE, GL_SRC_COLOR, GL_ONE_MINUS_SRC_COLOR, GL_DST_COLOR, GL_ONE_MINUS_DST_COLOR, GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA, GL_DST_ALPHA, GL_ONE_MINUS_DST_ALPHA. GL_CONSTANT_COLOR, GL_ONE_MINUS_CONSTANT_COLOR, GL_CONSTANT_ALPHA, and GL_ONE_MINUS_CONSTANT_ALPHA. The initial value is GL_ZERO.</param>
    [DllImport(Library)]
    public static extern void glBlendFunc(BlendScaleFactor sfactor, BlendScaleFactor dfactor);

    /// <summary>
    /// Clear buffers to preset values
    /// </summary>
    /// <param name="mask">Bitwise OR of masks that indicate the buffers to be cleared. The three masks are GL_COLOR_BUFFER_BIT, GL_DEPTH_BUFFER_BIT, and GL_STENCIL_BUFFER_BIT.</param>
    [DllImport(Library)]
    public static extern void glClear(CLEAR_MASK mask);

    /// <summary>
    /// Specify clear values for the color buffers. Specify the red, green, blue, and alpha values used when the color buffers are cleared. The initial values are all 0.
    /// </summary>
    /// <param name="red">Specify the red value [0-1].</param>
    /// <param name="green">Specify the green value [0-1].</param>
    /// <param name="blue">Specify the blue value [0-1].</param>
    /// <param name="alpha">Specify the alpha value [0-1].</param>
    [DllImport(Library)]
    public static extern void glClearColor(float red, float green, float blue, float alpha);

    /// <summary>
    /// Specify clear values for the color buffers. Specify the red, green, blue, and alpha values used when the color buffers are cleared. The initial values are all 0.
    /// </summary>
    /// <param name="red">Specify the red value [0-255].</param>
    /// <param name="green">Specify the green value [0-255].</param>
    /// <param name="blue">Specify the blue value [0-255].</param>
    /// <param name="alpha">Specify the alpha value [0-255].</param>
    public static void glClearColour(byte red, byte green, byte blue, byte alpha = 255) => glClearColor(((float)red) / 255, ((float)green) / 255, ((float)blue) / 255, ((float)alpha) / 255);

    /// <summary>
    /// Specify clear values for the color buffers. The initial values is Transparent / Black.
    /// </summary>
    /// <param name="colour">The new colour ([0-255] components).</param>
    public static void glClearColour(Colour colour) => glClearColor(((float)colour.R) / 255, ((float)colour.G) / 255, ((float)colour.B) / 255, ((float)colour.A) / 255);

    /// <summary>
    /// Sets the current color.
    /// </summary>
    /// <param name="red">The new red value for the current color. [0,1]</param>
    /// <param name="green">The new green value for the current color. [0,1]</param>
    /// <param name="blue">The new blue value for the current color. [0,1]</param>
    /// <remarks>The current alpha value is set to 1.0 (full intensity) implicitly.</remarks>
    [DllImport(Library)]
    public static extern void glColor3f(float red, float green, float blue);

    /// <summary>
    /// Sets the current color.
    /// </summary>
    /// <param name="red">The new red value for the current color. [0,1]</param>
    /// <param name="green">The new green value for the current color. [0,1]</param>
    /// <param name="blue">The new blue value for the current color. [0,1]</param>
    /// <param name="alpha">The new alpha value for the current color. [0,1]</param>
    [DllImport(Library)]
    public static extern void glColor4f(float red, float green, float blue, float alpha);

    /// <summary>
    /// Delete named textures.
    /// </summary>
    /// <param name="n">Specifies the number of textures to be deleted.</param>
    /// <param name="textures">Specifies an array of textures to be deleted.</param>
    [DllImport(Library)]
    public static extern void glDeleteTextures(int n, uint[] textures);

    /// <summary>
    /// Enable or disable writing into the depth buffer. If flag is GL_FALSE, depth buffer writing is disabled. Otherwise, it is enabled. Initially, depth buffer writing is enabled.
    /// </summary>
    /// <param name="flag">Specifies whether the depth buffer is enabled for writing. If flag is GL_FALSE, depth buffer writing is disabled. Otherwise, it is enabled.</param>
    [DllImport(Library)]
    public static extern void glDepthMask(bool flag);

    /// <summary>
    /// The glEnable and glDisable functions enable or disable OpenGL capabilities.
    /// </summary>
    /// <param name="cap">A symbolic constant indicating an OpenGL capability. For discussion of the values cap can take, see the following Remarks section.</param>
    [DllImport(Library)]
    public static extern void glDisable(GLCAP cap);

    /// <summary>
    /// The glEnableClientState and glDisableClientState functions enable and disable arrays respectively.
    /// </summary>
    /// <param name="array">A symbolic constant for the array you want to enable or disable. </param>
    [DllImport(Library)]
    public static extern void glDisableClientState(STATEARRAY array);

    /// <summary>
    /// Render primitives from array data.
    /// </summary>
    /// <param name="mode">Specifies what kind of primitives to render.</param>
    /// <param name="first">Specifies the starting index in the enabled arrays.</param>
    /// <param name="count">Specifies the number of indices to be rendered.</param>
    [DllImport(Library)]
    public static extern void glDrawArrays(PRIMITIVEMODE mode, int first, int count);

    /// <summary>
    /// glEnable and glDisable enable and disable various capabilities. Use glIsEnabled or glGet to determine the current setting of any capability. The initial value for each capability with the exception of GL_DITHER and GL_MULTISAMPLE is GL_FALSE. The initial value for GL_DITHER and GL_MULTISAMPLE is GL_TRUE.
    /// </summary>
    /// <param name="cap">Specifies a symbolic constant indicating a GL capability.</param>
    [DllImport(Library)]
    public static extern void glEnable(GLCAP cap);

    /// <summary>
    /// glEnableClientState and glDisableClientState enable or disable individual client-side capabilities. By default, all client-side capabilities are disabled. Both glEnableClientState and glDisableClientState take a single argument, cap, which can assume one of the following values:
    /// </summary>
    /// <param name="array">Specifies the capability to disable.</param>
    [DllImport(Library)]
    public static extern void glEnableClientState(STATEARRAY array);

    /// <summary>
    /// The glBegin and glEnd functions delimit the vertices of a primitive or a group of like primitives.
    /// </summary>
    [DllImport(Library)]
    public static extern void glEnd();

    /// <summary>
    /// glFlush — force execution of GL commands. Different GL implementations buffer commands in several different locations, including network buffers and the graphics accelerator itself. glFlush empties all of these buffers, causing all issued commands to be executed as quickly as they are accepted by the actual rendering engine. Though this execution may not be completed in any particular time period, it does complete in finite time.
    /// </summary>
    [DllImport(Library)]
    public static extern void glFlush();

    /// <summary>
    /// define front- and back-facing polygons
    /// </summary>
    /// <param name="mode">Specifies the orientation of front-facing polygons. GL_CW and GL_CCW are accepted. The initial value is GL_CCW.</param>
    [DllImport(Library)]
    public static extern void glFrontFace(FRONTFACEMODE mode);

    /// <summary>
    /// The glGetBooleanv function returns the value or values of a selected parameter.
    /// </summary>
    /// <param name="pname">The parameter value to be returned.</param>
    /// <param name="data">Returns the value or values of the specified parameter.</param>
    [DllImport(Library)]
    public static extern void glGetBooleanv(GLGET pname, [Out] out bool[] data);

    /// <summary>
    /// Return error information. Each detectable error is assigned a numeric code and symbolic name. When an error occurs, the error flag is set to the appropriate error code value. No other errors are recorded until glGetError is called, the error code is returned, and the flag is reset to GL_NO_ERROR. If a call to glGetError returns GL_NO_ERROR, there has been no detectable error since the last call to glGetError, or since the GL was initialized.
    /// </summary>
    /// <returns>Returns the value of the error flag. </returns>
    [DllImport(Library)]
    public static extern OpenGLErrorCode glGetError();

    /// <summary>
    /// return the value or values of a selected parameter
    /// </summary>
    /// <param name="pname">Specifies the parameter value to be returned for non-indexed versions of glGet.</param>
    /// <param name="result">Returns the value or values of the specified parameter.</param>
    [DllImport(Library)]
    public static extern void glGetIntegerv(GLGET pname, out int result);

    /// <summary>
    /// Return the value or values of a selected parameter
    /// </summary>
    /// <param name="pname">Specifies the parameter value to be returned for non-indexed versions of glGet</param>
    /// <param name="result">Returns the values of the specified parameter.</param>
    [DllImport(Library)]
    public static extern void glGetIntegerv(int pname, int[] result);

    /// <summary>
    /// Returns a pointer to a static string describing some aspect of the current GL connection.
    /// </summary>
    /// <param name="name">Specifies a symbolic constant</param>
    /// <returns>Returns a pointer to a static string describing some aspect of the current GL connection</returns>
    [DllImport(Library)]
    public unsafe static extern sbyte* glGetString(GetStringEnum name);

    /// <summary>
    /// return a texture image
    /// </summary>
    /// <param name="target">Specifies the target to which the texture is bound for glGetTexImage and glGetnTexImage functions.</param>
    /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
    /// <param name="format">Specifies a pixel format for the returned data.</param>
    /// <param name="type">Specifies a pixel type for the returned data.</param>
    /// <param name="pixels">Returns the texture image. Should be a pointer to an array of the type specified by type.</param>
    [DllImport(Library)]
    public static extern void glGetTexImage(TextureTarget target, int level, PixelFormat format, PixelType type, IntPtr pixels);

    /// <summary>
    /// returns n texture names in textures. There is no guarantee that the names form a contiguous set of integers; however, it is guaranteed that none of the returned names was in use immediately before the call to glGenTextures. The generated textures have no dimensionality; they assume the dimensionality of the texture target to which they are first bound(see glBindTexture). Texture names returned by a call to glGenTextures are not returned by subsequent calls, unless they are first deleted with glDeleteTextures.
    /// </summary>
    /// <param name="n">Specifies the number of texture names to be generated.</param>
    /// <param name="textures">Specifies an array in which the generated texture names are stored.</param>
    [DllImport(Library)]
    public static extern void glGenTextures(int n, uint[] textures);

    /// <summary>
    /// returns GL_TRUE if cap is an enabled capability and returns GL_FALSE otherwise. Boolean states that are indexed may be tested with glIsEnabledi. For glIsEnabledi, index specifies the index of the capability to test. index must be between zero and the count of indexed capabilities for cap. Initially all capabilities except GL_DITHER are disabled; GL_DITHER is initially enabled.
    /// </summary>
    /// <param name="cap">Specifies a symbolic constant indicating a GL capability.</param>
    /// <returns>returns GL_TRUE if cap is an enabled capability and returns GL_FALSE otherwise</returns>
    [DllImport(Library)]
    public static extern bool glIsEnabled(GLCAP cap); //marshalasbool?

    /// <summary>
    /// returns light source parameter values.
    /// </summary>
    /// <param name="light">The identifier of a light. The number of possible lights depends on the implementation, but at least eight lights are supported. They are identified by symbolic names of the form GL_LIGHTi where i is a value: 0 to GL_MAX_LIGHTS - 1.</param>
    /// <param name="pname">A light source parameter for light.</param>
    /// <param name="params">Specifies the value that parameter pname of light source light will be set to.</param>
    /// <remarks>The glLightfv function sets the value or values of individual light source parameters. The light parameter names the light and is a symbolic name of the form GL_LIGHTi, where 0 = i less than GL_MAX_LIGHTS. The pname parameter specifies one of the light source parameters, again by symbolic name.The params parameter is either a single value or a pointer to an array that contains the new values. Lighting calculation is enabled and disabled using glEnable and glDisable with argument GL_LIGHTING.When lighting is enabled, light sources that are enabled contribute to the lighting calculation.Light source i is enabled and disabled using glEnable and glDisable with argument GL_LIGHTi. It is always the case that GL_LIGHTi = GL_LIGHT0 + i.</remarks>
    [DllImport(Library)]
    public static extern void glLightfv(LightName light, LightParameter pname, float[] @params);

    /// <summary>
    /// specify the width of rasterized lines
    /// </summary>
    /// <param name="width">Specifies the width of rasterized lines. The initial value is 1.</param>
    [DllImport(Library)]
    public static extern void glLineWidth(float width);

    /// <summary>
    /// replace the current matrix with the identity matrix
    /// </summary>
    [DllImport(Library)]
    public static extern void glLoadIdentity();

    /// <summary>
    /// The glMaterialfv function specifies material parameters for the lighting model.
    /// </summary>
    /// <param name="face">The face or faces that are being updated. Must be one of the following: GL_FRONT, GL_BACK, or GL_FRONT and GL_BACK.</param>
    /// <param name="pname">The material parameter of the face or faces being updated.</param>
    /// <param name="params">The value to which parameter GL_SHININESS will be set.</param>
    [DllImport(Library)]
    public static extern void glMaterialfv(MaterialFace face, MaterialParameter pname, float[] @params);

    /// <summary>
    /// specify which matrix is the current matrix
    /// </summary>
    /// <param name="mode">Specifies which matrix stack is the target for subsequent matrix operations.</param>
    [DllImport(Library)]
    public static extern void glMatrixMode(MatrixMode mode);

    /// <summary>
    /// Sets the current normal vector.
    /// </summary>
    /// <param name="nx">Specifies the x-coordinate for the new current normal vector.</param>
    /// <param name="ny">Specifies the y-coordinate for the new current normal vector.</param>
    /// <param name="nz">Specifies the z-coordinate for the new current normal vector.</param>
    [DllImport(Library)]
    public static extern void glNormal3f(float nx, float ny, float nz);

    /// <summary>
    /// glOrtho describes a transformation that produces a parallel projection. The current matrix (see glMatrixMode) is multiplied by the orthographic matrix and the result replaces the current matrix.
    /// </summary>
    /// <param name="left">Specify the coordinates for the left and right vertical clipping planes.</param>
    /// <param name="right">Specify the coordinates for the left and right vertical clipping planes.</param>
    /// <param name="bottom">Specify the coordinates for the bottom and top horizontal clipping planes.</param>
    /// <param name="top">Specify the coordinates for the bottom and top horizontal clipping planes.</param>
    /// <param name="zNear">Specify the distances to the nearer and farther depth clipping planes. These values are negative if the plane is to be behind the viewer.</param>
    /// <param name="zFar">Specify the distances to the nearer and farther depth clipping planes. These values are negative if the plane is to be behind the viewer.</param>
    [DllImport(Library)]
    public static extern void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar);

    /// <summary>
    /// glReadPixels and glReadnPixels return pixel data from the frame buffer, starting with the pixel whose lower left corner is at location (x, y), into client memory starting at location data.
    /// </summary>
    /// <param name="x">Specify the window coordinates of the first pixel that is read from the frame buffer. This location is the lower left corner of a rectangular block of pixels.</param>
    /// <param name="y">Specify the window coordinates of the first pixel that is read from the frame buffer. This location is the lower left corner of a rectangular block of pixels.</param>
    /// <param name="width">Specify the dimensions of the pixel rectangle. width and height of one correspond to a single pixel.</param>
    /// <param name="height">Specify the dimensions of the pixel rectangle. width and height of one correspond to a single pixel.</param>
    /// <param name="format">Specifies the format of the pixel data.</param>
    /// <param name="type">Specifies the data type of the pixel data.</param>
    /// <param name="data">Returns the pixel data.</param>
    [DllImport(Library)]
    public static extern void glReadPixels(int x, int y, int width, int height, PixelFormat format, PixelType type, [Out] IntPtr data);

    /// <summary>
    /// glPixelStorei sets pixel storage modes that affect the operation of subsequent glReadPixels as well as the unpacking of texture patterns (see glTexImage2D and glTexSubImage2D).
    /// </summary>
    /// <param name="pname">a symbolic constant indicating the parameter to be set</param>
    /// <param name="param">the new value</param>
    [DllImport(Library)]
    public static extern void glPixelStorei(PixelStoreParameter pname, int param);

    /// <summary>
    /// glPointSize specifies the rasterized diameter of points
    /// </summary>
    /// <param name="size">The size in pixels</param>
    /// <remarks>If point size mode is disabled (see glEnable with parameter GL_PROGRAM_POINT_SIZE), this value will be used to rasterize points. Otherwise, the value written to the shading language built-in variable gl_PointSize will be used.</remarks>
    [DllImport(Library)]
    public static extern void glPointSize(float size);

    /// <summary>
    /// glPolygonMode controls the interpretation of polygons for rasterization.
    /// </summary>
    /// <param name="face">Specifies the polygons that mode applies to.</param>
    /// <param name="mode">Specifies how polygons will be rasterized.</param>
    [DllImport(Library)]
    public static extern void glPolygonMode(MaterialFace face, PolygonMode mode);

    /// <summary>
    /// glPopMatrix pops the current matrix stack, replacing the current matrix with the one below it on the stack.
    /// </summary>
    [DllImport(Library)]
    public static extern void glPopMatrix();

    /// <summary>
    /// glPushMatrix pushes the current matrix stack down by one, duplicating the current matrix. That is, after a glPushMatrix call, the matrix on top of the stack is identical to the one below it.
    /// </summary>
    [DllImport(Library)]
    public static extern void glPushMatrix();

    /// <summary>
    /// multiply the current matrix by a rotation matrix
    /// </summary>
    /// <param name="angle">Specifies the angle of rotation, in degrees.</param>
    /// <param name="x">Specify the x, y, and z coordinates of a vector, respectively.</param>
    /// <param name="y">Specify the x, y, and z coordinates of a vector, respectively.</param>
    /// <param name="z">Specify the x, y, and z coordinates of a vector, respectively.</param>
    [DllImport(Library)]
    public static extern void glRotatef(float angle, float x, float y, float z);

    /// <summary>
    /// The glScaled and glScalef functions multiply the current matrix by a general scaling matrix.
    /// </summary>
    /// <param name="x">Scale factors along the x axis.</param>
    /// <param name="y">Scale factors along the y axis.</param>
    /// <param name="z">Scale factors along the z axis.</param>
    [DllImport(Library)]
    public static extern void glScalef(float x, float y, float z);

    /// <summary>
    /// Select flat or smooth shading
    /// </summary>
    /// <param name="mode">Specifies a symbolic value representing a shading technique.</param>
    [DllImport(Library)]
    public static extern void glShadeModel(ShadingModel mode);

    /// <summary>
    /// set the current texture coordinates
    /// </summary>
    /// <param name="s">Specify s texture coordinate.</param>
    /// <param name="t">Specify t texture coordinate.</param>
    [DllImport(Library)]
    public static extern void glTexCoord2f(float s, float t);

    /// <summary>
    /// specify a one-dimensional texture image
    /// </summary>
    /// <param name="target">Specifies the target texture.</param>
    /// <param name="level">Specifies the level-of-detail number.</param>
    /// <param name="internalformat">Specifies the number of color components in the texture.</param>
    /// <param name="width">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide. The height of the 1D texture image is 1.</param>
    /// <param name="border">This value must be 0.</param>
    /// <param name="format">Specifies the format of the pixel data.</param>
    /// <param name="type">Specifies the data type of the pixel data.</param>
    /// <param name="data">Specifies a pointer to the image data in memory.</param>
    [DllImport(Library)]
    public static extern void glTexImage1D(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int border, PixelFormat format, PixelType type, IntPtr data);

    /// <summary>
    /// specify a two-dimensional texture image
    /// </summary>
    /// <param name="target">Specifies the target texture.</param>
    /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
    /// <param name="internalformat">Specifies the number of color components in the texture.</param>
    /// <param name="width">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide.</param>
    /// <param name="height">Specifies the height of the texture image, or the number of layers in a texture array, in the case of the GL_TEXTURE_1D_ARRAY and GL_PROXY_TEXTURE_1D_ARRAY targets. All implementations support 2D texture images that are at least 1024 texels high, and texture arrays that are at least 256 layers deep.</param>
    /// <param name="border">This value must be 0.</param>
    /// <param name="format">Specifies the format of the pixel data.</param>
    /// <param name="type">Specifies the data type of the pixel data.</param>
    /// <param name="data">Specifies a pointer to the image data in memory.</param>
    [DllImport(Library)]
    public static extern void glTexImage2D(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType type, IntPtr data);

    /// <summary>
    /// specify a two-dimensional texture subimage
    /// </summary>
    /// <param name="target">Specifies the target to which the texture is bound for glTexSubImage2D.</param>
    /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
    /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
    /// <param name="yoffset">Specifies a texel offset in the y direction within the texture array.</param>
    /// <param name="width">Specifies the width of the texture subimage.</param>
    /// <param name="height">Specifies the height of the texture subimage.</param>
    /// <param name="format">Specifies the format of the pixel data.</param>
    /// <param name="type">Specifies the data type of the pixel data.</param>
    /// <param name="pixels">Specifies a pointer to the image data in memory.</param>
    [DllImport(Library)]
    public static extern void glTexSubImage2D(TextureTarget target, int level, int xOffset, int yOffset, int width, int height, PixelFormat format, PixelType type, IntPtr pixels);

    /// <summary>
    /// set texture parameters
    /// </summary>
    /// <param name="target">Specifies the target to which the texture is bound for glTexParameter functions.</param>
    /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter.</param>
    /// <param name="param">Specifies the value of pname.</param>
    [DllImport(Library)]
    public static extern void glTexParameteri(TextureTarget target, TextureParameterName pname, TextureParameter param);

    /// <summary>
    /// multiply the current matrix by a translation matrix
    /// </summary>
    /// <param name="x">Specify the x, y, and z coordinates of a translation vector.</param>
    /// <param name="y">Specify the x, y, and z coordinates of a translation vector.</param>
    /// <param name="z">Specify the x, y, and z coordinates of a translation vector.</param>
    [DllImport(Library)]
    public static extern void glTranslatef(float x, float y, float z);

    /// <summary>
    /// The glVertex function commands are used within glBegin/glEnd pairs to specify point, line, and polygon vertices. The current color, normal, and texture coordinates are associated with the vertex when glVertex is called. When only x and y are specified, z defaults to 0.0 and w defaults to 1.0. When x, y, and z are specified, w defaults to 1.0. Invoking glVertex outside of a glBegin/glEnd pair results in undefined behavior.
    /// </summary>
    /// <param name="x">Specifies the x-coordinate of a vertex.</param>
    /// <param name="y">Specifies the y-coordinate of a vertex.</param>
    [DllImport(Library)]
    public static extern void glVertex2f(float x, float y);

    /// <summary>
    /// The glVertex function commands are used within glBegin/glEnd pairs to specify point, line, and polygon vertices. The current color, normal, and texture coordinates are associated with the vertex when glVertex is called. When only x and y are specified, z defaults to 0.0 and w defaults to 1.0. When x, y, and z are specified, w defaults to 1.0. Invoking glVertex outside of a glBegin/glEnd pair results in undefined behavior.
    /// </summary>
    /// <param name="x">Specifies the x-coordinate of a vertex.</param>
    /// <param name="y">Specifies the y-coordinate of a vertex.</param>
    /// <param name="z">Specifies the z-coordinate of a vertex.</param>
    [DllImport(Library)]
    public static extern void glVertex3f(float x, float y, float z);

    /// <summary>
    /// glVertexPointer specifies the location and data format of an array of vertex coordinates to use when rendering
    /// </summary>
    /// <param name="size">Specifies the number of coordinates per vertex. Must be 2, 3, or 4. The initial value is 4.</param>
    /// <param name="type">Specifies the data type of each coordinate in the array. Symbolic constants GL_SHORT, GL_INT, GL_FLOAT, or GL_DOUBLE are accepted. The initial value is GL_FLOAT.</param>
    /// <param name="stride">Specifies the byte offset between consecutive vertices. If stride is 0, the vertices are understood to be tightly packed in the array. The initial value is 0.</param>
    /// <param name="pointer">Specifies a pointer to the first coordinate of the first vertex in the array. The initial value is 0.</param>
    [DllImport(Library)]
    public static extern void glVertexPointer(int size, DataType type, int stride, float[] pointer);

    /// <summary>
    /// Specifies the affine transformation of x and y from normalized device coordinates to window coordinates.
    /// </summary>
    /// <param name="x">Specify the lower left corner of the viewport rectangle, in pixels. The initial value is (0,0).</param>
    /// <param name="y">Specify the lower left corner of the viewport rectangle, in pixels. The initial value is (0,0).</param>
    /// <param name="width">Specify the width and height of the viewport. When a GL context is first attached to a window, width and height are set to the dimensions of that window.</param>
    /// <param name="height">Specify the width and height of the viewport. When a GL context is first attached to a window, width and height are set to the dimensions of that window.</param>
    [DllImport(Library)]
    public static extern void glViewport(int x, int y, int width, int height);

    // ***CLEANED UP ABOVE THIS LINE***





    
    

    [DllImport(Library)]
    public extern static IntPtr wglCreateContext(IntPtr hDc);
    

    [DllImport(Library)]
    public extern static bool wglDeleteContext(IntPtr hRC);
    

    [DllImport(Library)]
    public static extern int wglDescribePixelFormat(IntPtr hdc, int ipfd, uint cjpfd, [In, MarshalAs(UnmanagedType.LPStruct)] PIXELFORMATDESCRIPTOR ppfd);
    

    [DllImport(Library)]
    public extern static IntPtr wglGetCurrentContext();
    

    [DllImport(Library)]
    public extern static IntPtr wglGetProcAddress(string lpszProc);
    

    [DllImport(Library)]
    public extern static bool wglMakeCurrent(IntPtr hDc, IntPtr hRC);
    

    [DllImport(Library)]
    public extern static bool wglShareLists(IntPtr hglrc1, IntPtr hglrc2);
    
    

    private static void GetProcAddress<T>(string functionName, out T functionPointer)
    {
        IntPtr procAddress = wglGetProcAddress(functionName);
        if (procAddress == IntPtr.Zero)
            throw new Win32Exception($"Failed to load entrypoint for {functionName}.");
        functionPointer = (T)(object)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(T));
    }
    

    public static void LoadWGLExtensions()
    {
        GetProcAddress("wglGetExtensionsStringARB", out _wglGetExtensionsStringARB);
        GetProcAddress("wglCreateContextAttribsARB", out _wglCreateContextAttribsARB);
        GetProcAddress("wglSwapIntervalEXT", out _wglSwapIntervalEXT);
        GetProcAddress("wglGetSwapIntervalEXT", out _wglGetSwapIntervalEXT);
        GetProcAddress("wglChoosePixelFormatARB", out _wglChoosePixelFormatARB);
        GetProcAddress("wglGetPixelFormatAttribivARB", out _wglGetPixelFormatAttribivARB);
    }
    

    /// <summary>
    /// This needs to be called after an OpenGL4 render context has been created and before any of these functions are called
    /// </summary>
    public static void LoadOpenGL3Extensions()
    {
        GetProcAddress("glActiveTexture", out _glActiveTexture);
        GetProcAddress("glAttachShader", out _glAttachShader);
        GetProcAddress("glBindBuffer", out _glBindBuffer);
        GetProcAddress("glBindFramebuffer", out _glBindFramebuffer);
        GetProcAddress("glBindSampler", out _glBindSampler);
        GetProcAddress("glBlendFuncSeparate", out glBlendFuncSeparate);
        GetProcAddress("glBufferData", out _glBufferData);
        GetProcAddress("glCompileShader", out _glCompileShader);
        GetProcAddress("glCreateProgram", out _glCreateProgram);
        GetProcAddress("glCreateShader", out _glCreateShader);
        GetProcAddress("glDebugMessageCallback", out _glDebugMessageCallback);
        GetProcAddress("glDebugMessageControl", out _glDebugMessageControl);
        GetProcAddress("glDeleteBuffers", out _glDeleteBuffers);
        GetProcAddress("glDeleteFramebuffers", out _glDeleteFramebuffers);
        GetProcAddress("glDeleteProgram", out _glDeleteProgram);
        GetProcAddress("glDeleteShader", out _glDeleteShader);
        GetProcAddress("glDetachShader", out _glDetachShader);
        GetProcAddress("glDisableVertexAttribArray", out _glDisableVertexAttribArray);
        GetProcAddress("glEnableVertexAttribArray", out _glEnableVertexAttribArray);
        GetProcAddress("glFramebufferTexture2D", out _glFramebufferTexture2D);
        GetProcAddress("glGenBuffers", out _glGenBuffers);
        GetProcAddress("glGenFramebuffers", out _glGenFramebuffers);
        GetProcAddress("glGetAttribLocation", out _glGetAttribLocation);
        GetProcAddress("glGetProgramInfoLog", out _glGetProgramInfoLog);
        GetProcAddress("glGetShaderInfoLog", out _glGetShaderInfoLog);
        GetProcAddress("glGetShaderiv", out _glGetShaderiv);
        GetProcAddress("glGetUniformLocation", out _glGetUniformLocation);
        GetProcAddress("glLinkProgram", out _glLinkProgram);
        GetProcAddress("glShaderSource", out _glShaderSource);
        GetProcAddress("glTexImage2DMultisample", out _glTexImage2DMultisample);
        GetProcAddress("glTexStorage2D", out _glTexStorage2D);
        GetProcAddress("glUniform1f", out _glUniform1f);
        GetProcAddress("glUniform1i", out _glUniform1i);
        GetProcAddress("glUniform2f", out glUniform2f);
        GetProcAddress("glUniform3f", out _glUniform3f);
        GetProcAddress("glUniform4f", out _glUniform4f);
        GetProcAddress("glUniformMatrix3fv", out _glUniformMatrix3fv);
        GetProcAddress("glUniformMatrix4fv", out _glUniformMatrix4fv);
        GetProcAddress("glUseProgram", out _glUseProgram);
        GetProcAddress("glValidateProgram", out _glValidateProgram);
        GetProcAddress("glVertexAttribPointer", out _glVertexAttribPointer);
    }
    

    public unsafe static Bitmap PremultiplyAlpha(Bitmap bitmap)
    {
        //todo: put some tries here as this fucks up sometimes

        // Lock the entire bitmap for Read/Write access as we'll be reading the pixel
        // colour values and altering them in-place.
        var bmlock = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                     System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);

        // This code only works with 32bit argb images - assume no alpha if not this format
        if (bmlock.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            throw new InvalidOperationException($"Unsupported pixel format {bmlock.PixelFormat}. Should be Formar32bppArgb to use TexturePremultiplier");

        var ptr = (byte*)bmlock.Scan0.ToPointer();

        for (var y = 0; y < bmlock.Height; y++)
        {
            for (var x = 0; x < bmlock.Width; x++)
            {
                // Obtain the memory location where our pixel data resides and cast it
                // into a struct to improve sanity.
                var color = (RgbaColor*)(ptr + (y * bmlock.Stride) + (x * sizeof(RgbaColor)));

                var alphaFloat = (*color).Alpha / 255.0f;

                (*color).Red = Convert.ToByte(alphaFloat * (*color).Red);
                (*color).Green = Convert.ToByte(alphaFloat * (*color).Green);
                (*color).Blue = Convert.ToByte(alphaFloat * (*color).Blue);
            }
        }

        // The bitmap lock is freed here
        bitmap.UnlockBits(bmlock);

        return bitmap;
    }
    

    public static List<string> GetAvailableExtensions()
    {
        string s = GetExtensionsStringARB(User32.GetDC(IntPtr.Zero));

        return s == null ? new List<string>() : s.Split(' ').ToList();
    }
    

    public static IntPtr CreateRenderContext(IntPtr deviceContext, (int major, int minor) openGLversion)
    {
        int[] attribs = {
            (int)ArbCreateContext.MajorVersion, openGLversion.major,
            (int)ArbCreateContext.MinorVersion, openGLversion.minor,
            (int)ArbCreateContext.ProfileMask, (int)ArbCreateContext.ForwardCompatibleBit,
            0 };

        var rC = CreateContextAttribsARB(deviceContext, sharedContext: IntPtr.Zero, attribs);

        if (rC == IntPtr.Zero)
            throw new Win32Exception($"Something went wrong with wglCreateContextAttribsARB: {Marshal.GetLastWin32Error()}");

        return rC;
    }    

    public static uint GenTexture()
    {
        uint[] textures = new uint[1];

        glGenTextures(1, textures);

        return textures[0];
    }

    public static Rect GetViewport()
    {
        int[] ints = new int[4];
        glGetIntegerv((int)GLGET.Viewport, ints);
        return new Rect(ints[0], ints[1], ints[2], ints[3]);
    }
    

    public static string GetString(GetStringEnum name)
    {
        unsafe
        {
            return new string(glGetString(name));
        }
    }
    

    public static void GetTexImage<T>(TextureTarget target, int level, PixelFormat format, PixelType type, T[] pixels)
    {
        GCHandle pinnedArray = GCHandle.Alloc(pixels, GCHandleType.Pinned);

        IntPtr pointer = pinnedArray.AddrOfPinnedObject();

        glGetTexImage(target, level, format, type, pointer);

        pinnedArray.Free();
    }
    public static void TexImage2D<T>(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType type, T[] pixels)
    {
        GCHandle pinnedArray = GCHandle.Alloc(pixels, GCHandleType.Pinned);

        IntPtr pointer = pinnedArray.AddrOfPinnedObject();

        glTexImage2D(target, level, internalFormat, width, height, border, format, type, pointer);

        pinnedArray.Free();
    }
    public static void TexImage2D<T>(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType type, T[,] pixels)
    {
        GCHandle pinnedArray = GCHandle.Alloc(pixels, GCHandleType.Pinned);

        IntPtr pointer = pinnedArray.AddrOfPinnedObject();

        glTexImage2D(target, level, internalFormat, width, height, border, format, type, pointer);

        pinnedArray.Free();
    }

    private delegate string wglGetExtensionsStringARB(IntPtr hDc);
    private static wglGetExtensionsStringARB _wglGetExtensionsStringARB;
    /// <summary>
    /// wglGetExtensionStringARB
    /// </summary>
    public static string GetExtensionsStringARB(IntPtr hDc) => _wglGetExtensionsStringARB(hDc);
    

    private delegate bool wglGetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, int[] piAttributes, [Out] int[] piValues);
    private static wglGetPixelFormatAttribivARB _wglGetPixelFormatAttribivARB;
    public static bool GetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, int[] piAttributes, [Out] int[] piValues)
        => _wglGetPixelFormatAttribivARB(hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes, piValues);
    

    private delegate bool wglChoosePixelFormatARB(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, int nMaxFormats, [Out] int[] piFormats, out int nNumFormats);
    private static wglChoosePixelFormatARB _wglChoosePixelFormatARB;
    public static bool ChoosePixelFormatARB(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, int nMaxFormats, [Out] int[] piFormats, out int nNumFormats)
        => _wglChoosePixelFormatARB(hdc, piAttribIList, pfAttribFList, nMaxFormats, piFormats, out nNumFormats);
    

    //note that this is necessarily created before LoadOpenGLExtensions is called
    private delegate IntPtr wglCreateContextAttribsARB(IntPtr hDc, IntPtr sharedContext, int[] attribList);
    private static wglCreateContextAttribsARB _wglCreateContextAttribsARB;

    public static IntPtr CreateContextAttribsARB(IntPtr hDc, IntPtr sharedContext, int[] attribList)
        => _wglCreateContextAttribsARB(hDc, sharedContext, attribList);
    

    private delegate bool wglSwapIntervalEXT(int value);
    private static wglSwapIntervalEXT _wglSwapIntervalEXT;
    /// <summary>
    /// Set VSync options, -1, 0 or 1
    /// https://stackoverflow.com/questions/589064/how-to-enable-vertical-sync-in-opengl
    /// </summary>
    /// <param name="value"></param>
    public static bool SwapIntervalEXT(int value) => _wglSwapIntervalEXT(value);
    

    private delegate int wglGetSwapIntervalEXT();
    private static wglGetSwapIntervalEXT _wglGetSwapIntervalEXT;
    public static int GetSwapIntervalEXT() => _wglGetSwapIntervalEXT();
    
    

    private delegate void DEL_glActiveTexture(int unit);
    private static DEL_glActiveTexture _glActiveTexture;
    /// <summary>
    /// glActiveTexture
    /// </summary>
    public static void ActiveTexture(TextureUnit unit)
    {
        _glActiveTexture((int)unit);
    }
    

    private delegate void DEL_glAttachShader(uint program, int shader);
    private static DEL_glAttachShader _glAttachShader;
    /// <summary>
    /// glAttachShader
    /// </summary>
    public static void AttachShader(uint program, int shader)
    {
        _glAttachShader(program, shader);
    }
    

    private delegate void DEL_glBindBuffer(int target, uint buffer);
    private static DEL_glBindBuffer _glBindBuffer;
    /// <summary>
    /// glBindBuffer
    /// </summary>
    public static void BindBuffer(BufferTarget target, uint buffer)
    {
        _glBindBuffer((int)target, buffer);
    }
    

    private delegate void DEL_glBindFramebuffer(int target, uint frameBuffer);
    private static DEL_glBindFramebuffer _glBindFramebuffer;
    /// <summary>
    /// glBindFramebuffer
    /// </summary>
    public static void BindFramebuffer(FramebufferTarget target, uint frameBuffer)
    {
        _glBindFramebuffer((int)target, frameBuffer);
    }
    

    private delegate void DEL_glBindSampler(int unit, int sampler);
    private static DEL_glBindSampler _glBindSampler;
    /// <summary>
    /// glBindSampler
    /// </summary>
    public static void BindSampler(TextureUnit unit, int sampler)
    {
        _glBindSampler((int)unit, sampler);
    }
    

    private delegate void DEL_glBlendFuncSeparate(int srcRGB, int dstRGB, int srcAlpha, int dstAlpha);
    private static DEL_glBlendFuncSeparate glBlendFuncSeparate;

    public static void BlendFuncSeperate(BlendScaleFactor srcRGB, BlendScaleFactor dstRGB, BlendScaleFactor srcAlpha, BlendScaleFactor dstAlpha)
    {
        glBlendFuncSeparate((int)srcRGB, (int)dstRGB, (int)srcAlpha, (int)dstAlpha);
    }
    

    private delegate void DEL_glBufferData(int target, IntPtr size, IntPtr data, int usage);
    private static DEL_glBufferData _glBufferData;
    /// <summary>
    /// glBufferData
    /// </summary>
    public static void BufferData<T>(BufferTarget target, int size, T[] data, BufferUsageHint usage) where T : struct
    {
        GCHandle pinnedArray = GCHandle.Alloc(data, GCHandleType.Pinned);

        IntPtr pointer = pinnedArray.AddrOfPinnedObject();

        _glBufferData((int)target, (IntPtr)size, pointer, (int)usage);

        pinnedArray.Free();
    }
    

    private delegate void DEL_glCompileShader(int program);
    private static DEL_glCompileShader _glCompileShader;
    /// <summary>
    /// glCompileShader
    /// </summary>
    public static void CompileShader(int program)
    {
        _glCompileShader(program);
    }
    

    private delegate uint DEL_glCreateProgram();
    private static DEL_glCreateProgram _glCreateProgram;
    /// <summary>
    /// glCreateProgram
    /// </summary>
    public static uint CreateProgram()
    {
        return _glCreateProgram();
    }
    

    private delegate int DEL_glCreateShader(int shaderType);
    private static DEL_glCreateShader _glCreateShader;
    /// <summary>
    /// glCreateShader
    /// </summary>
    public static int CreateShader(ShaderType shaderType)
    {
        return _glCreateShader((int)shaderType);
    }
    

    private delegate void DEL_glDebugMessageCallback(IntPtr callback, IntPtr userParam);
    private static DEL_glDebugMessageCallback _glDebugMessageCallback;
    public static void DebugMessageCallback(DebugMessageDelegate callbackFunction)
    {
        _glDebugMessageCallback(Marshal.GetFunctionPointerForDelegate(callbackFunction), IntPtr.Zero);
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void DebugMessageDelegate(DebugSource source, DebugType type, uint id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam);
    

    private delegate void DEL_glDebugMessageControl(int source, int type, int severity, int count, uint[] ids, bool enabled);
    private static DEL_glDebugMessageControl _glDebugMessageControl;

    public static void DebugMessageControl(DebugSource source, DebugType type, DebugSeverity severity, int count, uint[] ids, bool enabled)
    {
        _glDebugMessageControl((int)source, (int)type, (int)severity, count, ids, enabled);
    }

    public static void DebugMessageControl(DebugSource source, DebugType type, DebugSeverity severity, bool enabled)
    {
        DebugMessageControl(source, type, severity, 0, null, enabled);
    }
    

    private delegate void DEL_glDeleteBuffers(int n, uint[] buffers);
    private static DEL_glDeleteBuffers _glDeleteBuffers;
    /// <summary>
    /// glDeleteBuffers
    /// </summary>
    public static void DeleteBuffers(uint[] buffers)
    {
        int n = buffers.Length;

        _glDeleteBuffers(n, buffers);
    }
    /// <summary>
    /// glDeleteBuffers
    /// </summary>
    public static void DeleteBuffer(uint buffer)
    {
        uint[] buffers = { buffer };

        _glDeleteBuffers(1, buffers);
    }
    

    private delegate void DEL_glDeleteFramebuffers(int n, uint[] framebuffers);
    private static DEL_glDeleteFramebuffers _glDeleteFramebuffers;
    /// <summary>
    /// glDeleteFramebuffers
    /// </summary>
    public static void DeleteFramebuffers(uint[] framebuffers)
    {
        int n = framebuffers.Length;

        _glDeleteFramebuffers(n, framebuffers);
    }
    /// <summary>
    /// glDeleteFramebuffers
    /// </summary>
    public static void DeleteFramebuffer(uint framebuffer)
    {
        uint[] framebuffers = { framebuffer };

        _glDeleteFramebuffers(1, framebuffers);
    }
    

    private delegate void DEL_glDeleteShader(int shader);
    private static DEL_glDeleteShader _glDeleteShader;
    /// <summary>
    /// glDeleteShader
    /// </summary>
    public static void DeleteShader(int shader)
    {
        _glDeleteShader(shader);
    }
    

    private delegate void DEL_glDeleteProgram(uint program);
    private static DEL_glDeleteProgram _glDeleteProgram;
    /// <summary>
    /// glDeleteProgram
    /// </summary>
    public static void DeleteProgram(uint program)
    {
        _glDeleteProgram(program);
    }
    

    private delegate void DEL_glDetachShader(uint program, int shader);
    private static DEL_glDetachShader _glDetachShader;
    /// <summary>
    /// glDetachShader
    /// </summary>
    public static void DetachShader(uint program, int shader)
    {
        _glDetachShader(program, shader);
    }
    

    private delegate void DEL_glDisableVertexAttribArray(uint index);
    private static DEL_glDisableVertexAttribArray _glDisableVertexAttribArray;
    /// <summary>
    /// glDisableVertexAttribArray
    /// </summary>
    public static void DisableVertexAttribArray(int index)
    {
        _glDisableVertexAttribArray((uint)index);
    }
    

    private delegate void DEL_glEnableVertexAttribArray(uint index);
    private static DEL_glEnableVertexAttribArray _glEnableVertexAttribArray;
    /// <summary>
    /// glEnableVertexAttribArray
    /// </summary>
    public static void EnableVertexAttribArray(int index)
    {
        _glEnableVertexAttribArray((uint)index);
    }
    

    private delegate void DEL_glFramebufferTexture2D(int target, int attachment, int texTarget, uint texture, int level);
    private static DEL_glFramebufferTexture2D _glFramebufferTexture2D;
    /// <summary>
    /// glFramebufferTexture2D
    /// </summary>
    public static void FramebufferTexture2D(FramebufferTarget target, FramebufferAttachment attachment, TextureTarget texTarget, uint texture, int level)
    {
        _glFramebufferTexture2D((int)target, (int)attachment, (int)texTarget, texture, level);
    }
    

    private delegate void DEL_glGenBuffers(int n, uint[] buffers);
    private static DEL_glGenBuffers _glGenBuffers;
    /// <summary>
    /// glGenBuffers
    /// </summary>
    public static uint[] GenBuffers(int n)
    {
        uint[] buffers = new uint[n];

        _glGenBuffers(n, buffers);

        return buffers;
    }
    /// <summary>
    /// glGenBuffers
    /// </summary>
    public static uint GenBuffer()
    {
        uint[] buffers = new uint[1];

        _glGenBuffers(1, buffers);

        return buffers[0];
    }
    

    private delegate void DEL_glGenFrameBuffers(int n, uint[] buffers);
    private static DEL_glGenFrameBuffers _glGenFramebuffers;

    public static uint[] GenFramebuffers(int n)
    {
        uint[] buffers = new uint[n];

        _glGenFramebuffers(1, buffers);

        return buffers;
    }
    public static uint GenFramebuffer()
    {
        uint[] buffers = new uint[1];

        _glGenFramebuffers(1, buffers);

        return buffers[0];
    }
    

    private delegate int DEL_glGetAttribLocation(uint programObj, string name);
    private static DEL_glGetAttribLocation _glGetAttribLocation;
    /// <summary>
    /// glGetAttribLocation
    /// </summary>
    public static int GetAttribLocation(uint program, string name)
    {
        return _glGetAttribLocation(program, name);
    }
    

    private unsafe delegate void DEL_glGetProgramInfoLog(uint program, int bufSize, out int length, StringBuilder infoLog);
    private static DEL_glGetProgramInfoLog _glGetProgramInfoLog;
    /// <summary>
    /// glGetProgramInfoLog
    /// </summary>
    public static string GetProgramInfoLog(uint program)
    {
        StringBuilder stringPtr = new(255);
        int count;
        _glGetProgramInfoLog(program, 255, out count, stringPtr);

        return stringPtr.ToString();
    }
    

    private unsafe delegate void DEL_glGetShaderiv(uint shader, int pname, int* @params);
    private static DEL_glGetShaderiv _glGetShaderiv;
    /// <summary>
    /// glGetShaderiv
    /// </summary>
    public static int GetShader(uint shader, ShaderParameter pname)
    {
        unsafe
        {
            int i;
            _glGetShaderiv(shader, (int)pname, &i);
            return i;
        }
    }
    

    private unsafe delegate void DEL_glGetShaderInfoLog(uint shader, int bufSize, out int length, StringBuilder infoLog);
    private static DEL_glGetShaderInfoLog _glGetShaderInfoLog;
    /// <summary>
    /// glGetShaderInfoLog
    /// </summary>
    public static string GetShaderInfoLog(int shader)
    {
        StringBuilder stringPtr = new(255);
        int count;
        _glGetShaderInfoLog((uint)shader, 255, out count, stringPtr);

        return stringPtr.ToString();
    }
    

    private delegate int DEL_glGetUniformLocation(uint program, string name);
    private static DEL_glGetUniformLocation _glGetUniformLocation;
    /// <summary>
    /// glGetUniformLocation
    /// </summary>
    public static int GetUniformLocation(uint program, string name)
    {
        return _glGetUniformLocation(program, name);
    }
    

    private delegate void DEL_glLinkProgram(uint program);
    private static DEL_glLinkProgram _glLinkProgram;
    /// <summary>
    /// glLinkProgram
    /// </summary>
    public static void LinkProgram(uint program)
    {
        _glLinkProgram(program);
    }
    

    private delegate int DEL_glShaderSource(int shader, int count, string[] strings, int[] lengths);
    private static DEL_glShaderSource _glShaderSource;
    /// <summary>
    /// glShaderSource
    /// </summary>
    public static int ShaderSource(int shader, string source)
    {
        string[] strings = new string[1] { source };

        return _glShaderSource(shader, 1, strings, null);
    }
    

    private delegate int DEL_glTexImage2DMultisample(int target, int samples, int internalformat, int width, int height, bool fixedsamplelocations);
    private static DEL_glTexImage2DMultisample _glTexImage2DMultisample;
    /// <summary>
    /// glTexImage2DMultisample
    /// </summary>
    public static void TexImage2DMultisample(TextureTargetMultisample target, int samples, PixelInternalFormat internalFormat, int width, int height, bool fixedSampleLocations)
    {
        _glTexImage2DMultisample((int)target, samples, (int)internalFormat, width, height, fixedSampleLocations);
    }
    /// <summary>
    /// glTexImage2DMultisample
    /// </summary>
    public static void TexImage2DMultisample(TextureTargetMultisample target, MSAA_Samples samples, PixelInternalFormat internalFormat, int width, int height, bool fixedSampleLocations) => TexImage2DMultisample(target, (int)samples, internalFormat, width, height, fixedSampleLocations);
    

    private delegate void DEL_glTexStorage2D(uint target, int levels, int internalFormat, int width, int height);
    private static DEL_glTexStorage2D _glTexStorage2D;

    public static void TexStorage2D(TextureTarget target, int levels, TexInternalFormat internalFormat, int width, int height)
    {
        _glTexStorage2D((uint)target, levels, (int)internalFormat, width, height);
    }
    

    private delegate void DEL_glUniform1f(int location, float v0);
    private static DEL_glUniform1f _glUniform1f;

    private delegate void DEL_glUniform1i(int location, int v0);
    private static DEL_glUniform1i _glUniform1i;

    /// <summary>
    /// glUniform1f
    /// </summary>
    public static void Uniform(int location, float value)
    {
        _glUniform1f(location, value);
    }
    /// <summary>
    /// glUniform1i
    /// </summary>
    public static void Uniform(int location, int value)
    {
        _glUniform1i(location, value);
    }
    

    public delegate void DEL_glUniform2f(int location, float v0, float v1);
    public static DEL_glUniform2f? glUniform2f;

    private delegate void DEL_glUniform2i(int location, int v0, int v1);
    private static DEL_glUniform2i _glUniform2i;
    

    private delegate void DEL_glUniform3f(int location, float v0, float v1, float v2);
    private static DEL_glUniform3f _glUniform3f;

    private delegate void DEL_glUniform3i(int location, int v0, int v1, int v2);
    private static DEL_glUniform3i _glUniform3i;

    /// <summary>
    /// glUniform3f
    /// </summary>
    public static void Uniform3(int location, Point3 value)
    {
        _glUniform3f(location, value.x, value.y, value.z);
    }
    

    private delegate void DEL_glUniform4f(int location, float v0, float v1, float v2, float v3);
    private static DEL_glUniform4f _glUniform4f;

    private delegate void DEL_glUniform4i(int location, int v0, int v1, int v2, int v3);
    private static DEL_glUniform4i _glUniform4i;

    /// <summary>
    /// glUniform4f
    /// </summary>
    public static void Uniform4(int location, Point4 value)
    {
        _glUniform4f(location, value.x, value.y, value.z, value.w);
    }
    public static void Uniform4(int location, Colour value)
    {
        _glUniform4f(location, value.R / 255f, value.G / 255f, value.B / 255f, value.A / 255f);
    }
    

    private unsafe delegate void DEL_glUniformMatrix3fv(int location, int count, bool transpose, float* value);
    private static DEL_glUniformMatrix3fv _glUniformMatrix3fv;
    /// <summary>
    /// glUniformMatrix4fv
    /// </summary>
    public static void UniformMatrix3(int location, int count, bool transpose, float[] values)
    {
        unsafe
        {
            fixed (float* valuePointer = values)
            {
                _glUniformMatrix3fv(location, count, transpose, valuePointer);
            }
        }
    }
    /// <summary>
    /// glUniformMatrix3fv
    /// </summary>
    public static void UniformMatrix3(int location, float[] values) => UniformMatrix3(location, 1, false, values);
    /// <summary>
    /// glUniformMatrix3fv
    /// </summary>
    public static void UniformMatrix3(int location, ref Matrix4 matrix) => UniformMatrix3(location, 1, false, matrix.Values);
    

    private unsafe delegate void DEL_glUniformMatrix4fv(int location, int count, bool transpose, float* value);
    private static DEL_glUniformMatrix4fv _glUniformMatrix4fv;
    /// <summary>
    /// glUniformMatrix4fv
    /// </summary>
    public static void UniformMatrix4(int location, int count, bool transpose, float[] values)
    {
        unsafe
        {
            fixed (float* valuePointer = values)
            {
                _glUniformMatrix4fv(location, count, transpose, valuePointer);
            }
        }
    }
    /// <summary>
    /// glUniformMatrix4fv
    /// </summary>
    public static void UniformMatrix4(int location, float[] values) => UniformMatrix4(location, 1, false, values);
    /// <summary>
    /// glUniformMatrix4fv
    /// </summary>
    public static void UniformMatrix4(int location, ref Matrix4 matrix) => UniformMatrix4(location, 1, false, matrix.Values);
    

    private delegate void DEL_glUseProgram(uint program);
    private static DEL_glUseProgram _glUseProgram;
    /// <summary>
    /// glUseProgram
    /// </summary>
    public static void UseProgram(uint program)
    {
        _glUseProgram(program);
    }
    

    private delegate void DEL_glValidateProgram(uint program);
    private static DEL_glValidateProgram _glValidateProgram;
    /// <summary>
    /// glValidateProgram
    /// </summary>
    public static void ValidateProgram(uint program)
    {
        _glValidateProgram(program);
    }
    

    private delegate void DEL_glVertexAttribPointer(uint index, int size, int type, bool normalized, int stride, IntPtr pointer);
    private static DEL_glVertexAttribPointer _glVertexAttribPointer;
    /// <summary>
    /// glVertexAttribPointer
    /// </summary>
    public static void VertexAttribPointer(int index, int size, VertexAttribPointerType type, bool normalised, int stride, int offset)
    {
        _glVertexAttribPointer((uint)index, size, (int)type, normalised, stride, new IntPtr(offset));
    }
    
    
}