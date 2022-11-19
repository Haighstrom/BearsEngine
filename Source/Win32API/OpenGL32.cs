using System.Security;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace BearsEngine.Win32API;

[SuppressUnmanagedCodeSecurity]
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
    public static extern void glClear(ClearMask mask);

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

    // ***CLEANED UP ABOVE THIS LINE***

    [DllImport(Library)]
    public static extern void glColor3f(float red, float green, float blue);
    

    [DllImport(Library)]
    public static extern void glColor4f(float red, float green, float blue, float alpha);
    

    [DllImport(Library)]
    public static extern void glDeleteTextures(int n, uint[] textures);
    

    [DllImport(Library)]
    public static extern void glDepthMask(bool flag);
    

    [DllImport(Library)]
    public static extern void glDisable(int cap);
    

    [DllImport(Library)]
    public static extern void glDisableClientState(uint array);
    

    [DllImport(Library)]
    public static extern void glDrawArrays(int mode, int first, int count);
    

    [DllImport(Library)]
    public static extern void glEnable(int cap);
    

    [DllImport(Library)]
    public static extern void glEnableClientState(uint array);
    

    [DllImport(Library)]
    public static extern void glEnd();
    

    [DllImport(Library)]
    public static extern void glFlush();
    

    [DllImport(Library)]
    public static extern void glFrontFace(int mode);
    

    [DllImport(Library)]
    public static extern void glGetBooleanv(int pname, [Out] out bool[] data);
    

    [DllImport(Library)]
    public static extern uint glGetError();
    

    [DllImport(Library)]
    public static extern void glGetIntegerv(int pname, out int result);
    

    [DllImport(Library)]
    public static extern void glGetIntegerv(int pname, int[] result);
    

    [DllImport(Library)]
    public static extern void glGetTexImage(int target, int level, int format, int type, IntPtr pixels);
    

    [DllImport(Library)]
    public static extern void glGenTextures(int n, uint[] textures);
    

    [DllImport(Library)]
    public unsafe static extern sbyte* glGetString(uint name);
    

    [DllImport(Library)]
    public static extern byte glIsEnabled(int cap);
    

    [DllImport(Library)]
    public static extern void glLightfv(int light, int pname, float[] @params);
    

    [DllImport(Library)]
    public static extern void glLineWidth(float width);
    

    [DllImport(Library)]
    public static extern void glLoadIdentity();
    

    [DllImport(Library)]
    public static extern void glMaterialfv(int face, int pname, float[] @params);
    

    [DllImport(Library)]
    public static extern void glMatrixMode(int mode);
    

    [DllImport(Library)]
    public static extern void glNormal3f(float nx, float ny, float nz);
    

    [DllImport(Library)]
    public static extern void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar);
    

    [DllImport(Library)]
    public static extern void glReadPixels(int x, int y, int width, int height, int format, int type, [Out] IntPtr data);
    

    [DllImport(Library)]
    public static extern void glPixelStorei(int pname, int param);
    

    [DllImport(Library)]
    public static extern void glPointSize(float size);
    

    [DllImport(Library)]
    public static extern void glPolygonMode(int face, int mode);
    

    [DllImport(Library)]
    public static extern void glPopMatrix();
    

    [DllImport(Library)]
    public static extern void glPushMatrix();
    

    [DllImport(Library)]
    public static extern void glRotatef(float angle, float x, float y, float z);
    

    [DllImport(Library)]
    public static extern void glScalef(float x, float y, float z);
    

    [DllImport(Library)]
    public static extern void glShadeModel(int mode);
    

    [DllImport(Library)]
    public static extern void glTexCoord2f(float s, float t);
    

    [DllImport(Library)]
    public static extern void glTexImage1D(int target, int level, int internalformat, int width, int border, int format, int type, IntPtr pixels);
    

    [DllImport(Library)]
    public static extern void glTexImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type, IntPtr pixels);
    

    [DllImport(Library)]
    public static extern void glTexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, IntPtr pixels);
    

    [DllImport(Library)]
    public static extern void glTexParameteri(int target, int pname, int param);
    

    [DllImport(Library)]
    public static extern void glTranslatef(float x, float y, float z);
    

    [DllImport(Library)]
    public static extern void glVertex2f(float x, float y);
    

    [DllImport(Library)]
    public static extern void glVertex3f(float x, float y, float z);
    

    [DllImport(Library)]
    public static extern void glVertexPointer(int size, uint type, int stride, float[] pointer);
    

    [DllImport(Library)]
    public static extern void glViewport(int x, int y, int width, int height);
    
    

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
        GetProcAddress("glUniform2f", out _glUniform2f);
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
    


    

    public static void Colour(Colour colour)
    {
        glColor4f(((float)colour.R) / 255, ((float)colour.G) / 255, ((float)colour.B) / 255, ((float)colour.A) / 255);
    }
    public static void Colour(byte red, byte green, byte blue)
    {
        glColor3f(((float)red) / 255, ((float)green) / 255, ((float)blue) / 255);
    }

    public static void Colour(byte red, byte green, byte blue, byte alpha)
    {
        glColor4f(((float)red) / 255, ((float)green) / 255, ((float)blue) / 255, ((float)alpha) / 255);
    }
                   

    public static void DeleteTexture(uint texture)
    {
        uint[] textures = new uint[1];

        glDeleteTextures(1, textures);
    }
    public static void DeleteTexture(uint[] textures)
    {
        int n = textures.Length;

        glDeleteTextures(n, textures);
    }
    

    public static void DepthMask(bool flag)
    {
        glDepthMask(flag);
    }
    

    public static void Disable(EnableCap cap)
    {
        glDisable((int)cap);
    }
    

    public static void DisableClientState(VertexArray array)
    {
        glDisableClientState((uint)array);
    }
    

    public static void DrawArrays(PrimitiveType mode, int first, int count)
    {
        glDrawArrays((int)mode, first, count);
    }
    

    public static void Enable(EnableCap cap)
    {
        glEnable((int)cap);
    }
    

    public static void EnableClientState(VertexArray array)
    {
        glEnableClientState((uint)array);
    }
    

    public static void Flush()
    {
        glFlush();
    }
    

    public static void FrontFace(FrontFaceDirection mode)
    {
        glFrontFace((int)mode);
    }
    

    public static uint GenTexture()
    {
        uint[] textures = new uint[1];

        glGenTextures(1, textures);

        return textures[0];
    }
    public static uint[] GenTextures(int n)
    {
        uint[] textures = new uint[n];

        glGenTextures(n, textures);

        return textures;
    }
    

    public static bool[] GetBool(GLEnumPName pname)
    {
        glGetBooleanv((int)pname, out bool[] bools);

        return bools;
    }
    

    public static int GetInt(GLEnumPName pname)
    {
        glGetIntegerv((int)pname, out int i);

        return i;
    }
    

    public static Rect GetViewport()
    {
        int[] ints = new int[4];
        glGetIntegerv((int)GLEnumPName.Viewport, ints);
        return new Rect(ints[0], ints[1], ints[2], ints[3]);
    }
    

    public static OpenGLErrorCode GetError()
    {
        return (OpenGLErrorCode)glGetError();
    }
    

    public static string GetString(GetStringEnum name)
    {
        unsafe
        {
            return new string(glGetString((uint)name));
        }
    }
    

    public static void GetTexImage(TextureTarget target, int level, PixelFormat format, PixelType type, IntPtr pixels)
    {
        glGetTexImage((int)target, level, (int)format, (int)type, pixels);
    }
    public static void GetTexImage<T>(TextureTarget target, int level, PixelFormat format, PixelType type, T[] pixels)
    {
        GCHandle pinnedArray = GCHandle.Alloc(pixels, GCHandleType.Pinned);

        IntPtr pointer = pinnedArray.AddrOfPinnedObject();

        glGetTexImage((int)target, level, (int)format, (int)type, pointer);

        pinnedArray.Free();
    }
    

    public static bool IsEnabled(EnableCap cap)
    {
        return glIsEnabled((int)cap) == 1;
    }
    

    public static void Light(LightName light, LightParameter pname, float[] @params)
    {
        glLightfv((int)light, (int)pname, @params);
    }
    

    public static void LineWidth(float width)
    {
        glLineWidth(width);
    }
    

    public static void LoadIdentity()
    {
        glLoadIdentity();
    }
    

    public static void Material(MaterialFace face, MaterialParameter pname, float[] @params)
    {
        glMaterialfv((int)face, (int)pname, @params);
    }
    

    public static void MatrixMode(MatrixMode mode)
    {
        glMatrixMode((int)mode);
    }
    

    public static void Normal(float nx, float ny, float nz)
    {
        glNormal3f(nx, ny, nz);
    }
    

    public static void Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        glOrtho(left, right, bottom, top, zNear, zFar);
    }
    

    public static void PixelStore(PixelStoreParameter pname, int param)
    {
        glPixelStorei((int)pname, param);
    }
    

    public static void PointSize(float size)
    {
        glPointSize(size);
    }
    

    public static void PolygonMode(MaterialFace face, PolygonMode mode)
    {
        glPolygonMode((int)face, (int)mode);
    }
    

    public static void PopMatrix()
    {
        glPopMatrix();
    }
    

    public static void PushMatrix()
    {
        glPushMatrix();
    }
    

    public static void ReadPixels(int x, int y, int w, int h, PixelFormat format, PixelType type, IntPtr data)
    {
        glReadPixels(x, y, w, h, (int)format, (int)type, data);
    }
    

    public static void Rotate(float angle, float x, float y, float z)
    {
        glRotatef(angle, x, y, z);
    }
    

    public static void Scale(float x, float y, float z)
    {
        glScalef(x, y, z);
    }
    

    public static void ShadeModel(ShadingModel mode)
    {
        glShadeModel((int)mode);
    }
    

    public static void TexCoord(float s, float t)
    {
        glTexCoord2f(s, t);
    }
    

    public static void TexImage1D(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int border, PixelFormat format, PixelType type, IntPtr pixels)
    {
        glTexImage1D((int)target, level, (int)internalFormat, width, border, (int)format, (int)type, pixels);
    }
    

    public static void TexImage2D(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType type, IntPtr pixels)
    {
        glTexImage2D((int)target, level, (int)internalFormat, width, height, border, (int)format, (int)type, pixels);
    }
    public static void TexImage2D<T>(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType type, T[] pixels)
    {
        GCHandle pinnedArray = GCHandle.Alloc(pixels, GCHandleType.Pinned);

        IntPtr pointer = pinnedArray.AddrOfPinnedObject();

        glTexImage2D((int)target, level, (int)internalFormat, width, height, border, (int)format, (int)type, pointer);

        pinnedArray.Free();
    }
    public static void TexImage2D<T>(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType type, T[,] pixels)
    {
        GCHandle pinnedArray = GCHandle.Alloc(pixels, GCHandleType.Pinned);

        IntPtr pointer = pinnedArray.AddrOfPinnedObject();

        glTexImage2D((int)target, level, (int)internalFormat, width, height, border, (int)format, (int)type, pointer);

        pinnedArray.Free();
    }
    

    public static void TexSubImage2D(TextureTarget target, int level, int xOffset, int yOffset, int width, int height, PixelFormat format, PixelType type, IntPtr pixels)
    {
        glTexSubImage2D((int)target, level, xOffset, yOffset, width, height, (int)format, (int)type, pixels);
    }
    

    public static void TexParameter(TextureTarget target, TextureParameterName pname, TextureParameter param)
    {
        glTexParameteri((int)target, (int)pname, (int)param);
    }
    

    public static void Translate(float x, float y, float z)
    {
        glTranslatef(x, y, z);
    }
    

    public static void Vertex(float x, float y)
    {
        glVertex2f(x, y);
    }

    public static void Vertex(float x, float y, float z)
    {
        glVertex3f(x, y, z);
    }
    

    public static void VertexPointer(int size, DataType type, int stride, float[] pointer)
    {
        glVertexPointer(size, (uint)type, stride, pointer);
    }
    

    public static void Viewport(int x, int y, int w, int h)
    {
        glViewport(x, y, w, h);
    }
    public static void Viewport(Rect viewport)
    {
        glViewport((int)viewport.X, (int)viewport.Y, (int)viewport.W, (int)viewport.H);
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
    

    private delegate void DEL_glUniform2f(int location, float v0, float v1);
    private static DEL_glUniform2f _glUniform2f;

    private delegate void DEL_glUniform2i(int location, int v0, int v1);
    private static DEL_glUniform2i _glUniform2i;

    /// <summary>
    /// glUniform2f
    /// </summary>
    public static void Uniform2(int location, Point value)
    {
        _glUniform2f(location, value.X, value.Y);
    }
    

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