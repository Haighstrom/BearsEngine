using System.Security;
using System.Runtime.InteropServices;
using System.Text;

namespace BearsEngine.OpenGL;

[SuppressUnmanagedCodeSecurity]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "OpenGL functions have dumb naming conventions but I'm keeping these APIs pure.")]
public static partial class OpenGL32
{
    #region Delegates
    private delegate void Delegate_glActiveTexture(TEXTURE_UNIT unit);
    private delegate void Delegate_glAttachShader(int program, int shader);
    private delegate void Delegate_glBindBuffer(BUFFER_TARGET target, int buffer);
    private delegate void Delegate_glBindFramebuffer(FRAMEBUFFER_TARGET target, int frameBuffer);
    private delegate void Delegate_glBindSampler(TEXTURE_UNIT unit, int sampler);
    private delegate void Delegate_glBlendFuncSeparate(BLEND_SCALE_FACTOR srcRGB, BLEND_SCALE_FACTOR dstRGB, BLEND_SCALE_FACTOR srcAlpha, BLEND_SCALE_FACTOR dstAlpha);
    private delegate void Delegate_glBufferData(BUFFER_TARGET target, IntPtr size, IntPtr data, USAGE_PATTERN usage);
    private delegate void Delegate_glCompileShader(int program);
    private delegate int Delegate_glCreateProgram();
    private delegate int Delegate_glCreateShader(SHADER_TYPE shaderType);
    private delegate void Delegate_glDebugMessageCallback(IntPtr callback, IntPtr userParam);
    private delegate void Delegate_glDebugMessageControl(DEBUGMESSAGE_SOURCE source, DEBUGMESSAGE_TYPE type, DEBUGMESSAGE_SEVERITY severity, int count, int[] ids, bool enabled);
    private delegate void Delegate_glDeleteBuffers(int n, int[] buffers);
    private delegate void Delegate_glDeleteFramebuffers(int n, int[] framebuffers);
    private delegate void Delegate_glDeleteProgram(int program);
    private delegate void Delegate_glDeleteShader(int shader);
    private delegate void Delegate_glDetachShader(int program, int shader);
    private delegate void Delegate_glDisableVertexAttribArray(int index);
    private delegate void Delegate_glEnableVertexAttribArray(int index);
    private delegate void Delegate_glFramebufferTexture2D(FRAMEBUFFER_TARGET target, FRAMEBUFFER_ATTACHMENT_POINT attachment, TEXTURE_TARGET texTarget, int texture, int level);
    private delegate void Delegate_glGenBuffers(int n, int[] buffers);
    private delegate void Delegate_glGenFrameBuffers(int n, int[] buffers);
    private delegate int Delegate_glGetAttribLocation(int programObj, string name);
    private unsafe delegate void Delegate_glGetProgramInfoLog(int program, int bufSize, out int length, StringBuilder infoLog);
    private unsafe delegate void Delegate_glGetShaderiv(int shader, GETSHADER_NAME pname, int* @params);
    private unsafe delegate void Delegate_glGetShaderInfoLog(int shader, int bufSize, out int length, StringBuilder infoLog);
    private delegate int Delegate_glGetUniformLocation(int program, string name);
    private delegate void Delegate_glLinkProgram(int program);
    private delegate void Delegate_glShaderSource(int shader, int count, string[] strings, int[]? lengths);
    private delegate void Delegate_glTexImage2DMultisample(TEXTURE_TARGET target, int samples, TEXTURE_INTERNALFORMAT internalFormat, int width, int height, bool fixedsamplelocations);
    private delegate void Delegate_glTexStorage2D(TEXTURE_TARGET target, int levels, TEXTURE_INTERNALFORMAT internalFormat, int width, int height);
    private delegate void Delegate_glUniform1f(int location, float v0);
    private delegate void Delegate_glUniform1i(int location, int v0);
    private delegate void Delegate_glUniform2f(int location, float v0, float v1);
    private delegate void Delegate_glUniform2i(int location, int v0, int v1);
    private delegate void Delegate_glUniform3f(int location, float v0, float v1, float v2);
    private delegate void Delegate_glUniform3i(int location, int v0, int v1, int v2);
    private delegate void Delegate_glUniform4f(int location, float v0, float v1, float v2, float v3);
    private delegate void Delegate_glUniform4i(int location, int v0, int v1, int v2, int v3);
    private unsafe delegate void Delegate_glUniformMatrix3fv(int location, int count, bool transpose, float* value);
    private unsafe delegate void Delegate_glUniformMatrix4fv(int location, int count, bool transpose, float* value);
    private delegate void Delegate_glUseProgram(int program);
    private delegate void Delegate_glVertexAttribPointer(int index, int size, int type, bool normalized, int stride, IntPtr pointer);
    private delegate void Delegate_glValidateProgram(int program);

    /// <summary>
    /// Delegate for use with <see cref="glDebugMessageCallback"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="type"></param>
    /// <param name="id"></param>
    /// <param name="severity"></param>
    /// <param name="length">The length of debug message whose character string is in the array pointed to by message</param>
    /// <param name="message"></param>
    /// <param name="userParam">will be set to the value passed in the userParam parameter to the most recent call to glDebugMessageCallback</param>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void DEBUGPROC(DEBUGMESSAGE_SOURCE source, DEBUGMESSAGE_TYPE type, int id, DEBUGMESSAGE_SEVERITY severity, int length, IntPtr message, IntPtr userParam);
    #endregion
    
    #region Entry Points
    private static Delegate_glActiveTexture? _glActiveTexture;
    private static Delegate_glAttachShader? _glAttachShader;
    private static Delegate_glBindBuffer? _glBindBuffer;
    private static Delegate_glBindFramebuffer? _glBindFramebuffer;
    private static Delegate_glBindSampler? _glBindSampler;
    private static Delegate_glBlendFuncSeparate? _glBlendFuncSeparate;
    private static Delegate_glBufferData? _glBufferData;
    private static Delegate_glCompileShader? _glCompileShader;
    private static Delegate_glCreateProgram? _glCreateProgram;
    private static Delegate_glCreateShader? _glCreateShader;
    private static Delegate_glDebugMessageCallback? _glDebugMessageCallback;
    private static Delegate_glDebugMessageControl? _glDebugMessageControl;
    private static Delegate_glDeleteBuffers? _glDeleteBuffers;
    private static Delegate_glDeleteFramebuffers? _glDeleteFramebuffers;
    private static Delegate_glDeleteShader? _glDeleteShader;
    private static Delegate_glDeleteProgram? _glDeleteProgram;
    private static Delegate_glDisableVertexAttribArray? _glDisableVertexAttribArray;
    private static Delegate_glDetachShader? _glDetachShader;
    private static Delegate_glEnableVertexAttribArray? _glEnableVertexAttribArray;
    private static Delegate_glFramebufferTexture2D? _glFramebufferTexture2D;
    private static Delegate_glGenBuffers? _glGenBuffers;
    private static Delegate_glGenFrameBuffers? _glGenFramebuffers;
    private static Delegate_glGetAttribLocation? _glGetAttribLocation;
    private static Delegate_glGetProgramInfoLog? _glGetProgramInfoLog;
    private static Delegate_glGetShaderiv? _glGetShaderiv;
    private static Delegate_glGetShaderInfoLog? _glGetShaderInfoLog;
    private static Delegate_glGetUniformLocation? _glGetUniformLocation;
    private static Delegate_glLinkProgram? _glLinkProgram;
    private static Delegate_glShaderSource? _glShaderSource;
    private static Delegate_glTexImage2DMultisample? _glTexImage2DMultisample;
    private static Delegate_glTexStorage2D? _glTexStorage2D;
    private static Delegate_glUniform1f? _glUniform1f;
    private static Delegate_glUniform1i? _glUniform1i;
    private static Delegate_glUniform2f? _glUniform2f;
    private static Delegate_glUniform2i? _glUniform2i;
    private static Delegate_glUniform3f? _glUniform3f;
    private static Delegate_glUniform3i? _glUniform3i;
    private static Delegate_glUniform4f? _glUniform4f;
    private static Delegate_glUniform4i? _glUniform4i;
    private static Delegate_glUniformMatrix3fv? _glUniformMatrix3fv;
    private static Delegate_glUniformMatrix4fv? _glUniformMatrix4fv;
    private static Delegate_glUseProgram? _glUseProgram;
    private static Delegate_glValidateProgram? _glValidateProgram;
    private static Delegate_glVertexAttribPointer? _glVertexAttribPointer;
    #endregion

    /// <summary>
    /// This needs to be called after an OpenGL v3+ render context has been created and before any of these functions are called
    /// </summary>
    internal static void LoadOpenGL3Extensions()
    {
        GL.GetProcAddress("glActiveTexture", out _glActiveTexture);
        GL.GetProcAddress("glAttachShader", out _glAttachShader);
        GL.GetProcAddress("glBindBuffer", out _glBindBuffer);
        GL.GetProcAddress("glBindFramebuffer", out _glBindFramebuffer);
        GL.GetProcAddress("glBindSampler", out _glBindSampler);
        GL.GetProcAddress("glBlendFuncSeparate", out _glBlendFuncSeparate);
        GL.GetProcAddress("glBufferData", out _glBufferData);
        GL.GetProcAddress("glCompileShader", out _glCompileShader);
        GL.GetProcAddress("glCreateProgram", out _glCreateProgram);
        GL.GetProcAddress("glCreateShader", out _glCreateShader);
        GL.GetProcAddress("glDebugMessageCallback", out _glDebugMessageCallback);
        GL.GetProcAddress("glDebugMessageControl", out _glDebugMessageControl);
        GL.GetProcAddress("glDeleteBuffers", out _glDeleteBuffers);
        GL.GetProcAddress("glDeleteFramebuffers", out _glDeleteFramebuffers);
        GL.GetProcAddress("glDeleteProgram", out _glDeleteProgram);
        GL.GetProcAddress("glDeleteShader", out _glDeleteShader);
        GL.GetProcAddress("glDetachShader", out _glDetachShader);
        GL.GetProcAddress("glDisableVertexAttribArray", out _glDisableVertexAttribArray);
        GL.GetProcAddress("glEnableVertexAttribArray", out _glEnableVertexAttribArray);
        GL.GetProcAddress("glFramebufferTexture2D", out _glFramebufferTexture2D);
        GL.GetProcAddress("glGenBuffers", out _glGenBuffers);
        GL.GetProcAddress("glGenFramebuffers", out _glGenFramebuffers);
        GL.GetProcAddress("glGetAttribLocation", out _glGetAttribLocation);
        GL.GetProcAddress("glGetProgramInfoLog", out _glGetProgramInfoLog);
        GL.GetProcAddress("glGetShaderInfoLog", out _glGetShaderInfoLog);
        GL.GetProcAddress("glGetShaderiv", out _glGetShaderiv);
        GL.GetProcAddress("glGetUniformLocation", out _glGetUniformLocation);
        GL.GetProcAddress("glLinkProgram", out _glLinkProgram);
        GL.GetProcAddress("glShaderSource", out _glShaderSource);
        GL.GetProcAddress("glTexImage2DMultisample", out _glTexImage2DMultisample);
        GL.GetProcAddress("glTexStorage2D", out _glTexStorage2D);
        GL.GetProcAddress("glUniform1f", out _glUniform1f);
        GL.GetProcAddress("glUniform1i", out _glUniform1i);
        GL.GetProcAddress("glUniform2f", out _glUniform2f);
        GL.GetProcAddress("glUniform2i", out _glUniform2i);
        GL.GetProcAddress("glUniform3f", out _glUniform3f);
        GL.GetProcAddress("glUniform3i", out _glUniform3i);
        GL.GetProcAddress("glUniform4f", out _glUniform4f);
        GL.GetProcAddress("glUniform4i", out _glUniform4i);
        GL.GetProcAddress("glUniformMatrix3fv", out _glUniformMatrix3fv);
        GL.GetProcAddress("glUniformMatrix4fv", out _glUniformMatrix4fv);
        GL.GetProcAddress("glUseProgram", out _glUseProgram);
        GL.GetProcAddress("glValidateProgram", out _glValidateProgram);
        GL.GetProcAddress("glVertexAttribPointer", out _glVertexAttribPointer);
    }

    /// <summary>
    /// Selects which texture unit subsequent texture state calls will affect.
    /// </summary>
    /// <param name="texture">Specifies which texture unit to make active. The number of texture units is implementation dependent, but must be at least 80. texture must be one of GL_TEXTUREi, where i ranges from zero to the value of GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS minus one. The initial value is GL_TEXTURE0.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glActiveTexture(TEXTURE_UNIT texture)
    {
        if (_glActiveTexture is null)
            throw new EntryPointNotFoundException($"{nameof(_glActiveTexture)} was called but the entrypoint was not loaded.");

        _glActiveTexture(texture);
    }

    /// <summary>
    /// Attaches a shader object to a program object
    /// </summary>
    /// <param name="program">Specifies the program object to which a shader object will be attached.</param>
    /// <param name="shader">Specifies the shader object that is to be attached.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glAttachShader(int program, int shader)
    {
        if (_glAttachShader is null)
            throw new EntryPointNotFoundException($"{nameof(_glAttachShader)} was called but the entrypoint was not loaded.");

        _glAttachShader(program, shader);
    }

    /// <summary>
    /// Binds a buffer object to the specified buffer binding point. When a buffer object is bound to a target, the previous binding for that target is automatically broken.
    /// </summary>
    /// <param name="target">Specifies the target to which the buffer object is bound.</param>
    /// <param name="buffer">Specifies the name of a buffer object.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glBindBuffer(BUFFER_TARGET target, int buffer)
    {
        if (_glBindBuffer is null)
            throw new EntryPointNotFoundException($"{nameof(_glBindBuffer)} was called but the entrypoint was not loaded.");

        _glBindBuffer(target, buffer);
    }

    /// <summary>
    /// Bind a framebuffer to a framebuffer target. Requires an OpenGL4 context and extension entrypoints to have been loaded.
    /// </summary>
    /// <param name="target">Specifies the framebuffer target of the binding operation.</param>
    /// <param name="frameBuffer">Specifies the name of the framebuffer object to bind.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glBindFramebuffer(FRAMEBUFFER_TARGET target, int frameBuffer)
    {
        if (_glBindFramebuffer is null)
            throw new EntryPointNotFoundException($"{nameof(_glBindFramebuffer)} was called but the entrypoint was not loaded.");

        _glBindFramebuffer(target, frameBuffer);
    }

    /// <summary>
    /// Bind a named sampler to a texturing target. Requires an OpenGL4 context and extension entrypoints to have been loaded.
    /// </summary>
    /// <param name="unit">Specifies the index of the texture unit to which the sampler is bound.</param>
    /// <param name="sampler">Specifies the name of a sampler. Sampler must be zero or the name of a sampler object previously returned from a call to glGenSamplers.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glBindSampler(TEXTURE_UNIT unit, int sampler)
    {
        if (_glBindSampler is null)
            throw new EntryPointNotFoundException($"{nameof(_glBindSampler)} was called but the entrypoint was not loaded.");

        _glBindSampler(unit, sampler);
    }

    /// <summary>
    /// Specify pixel arithmetic for RGB and alpha components separately
    /// </summary>
    /// <param name="srcRGB">Specifies how the red, green, and blue blending factors are computed. The initial value is GL_ONE.</param>
    /// <param name="dstRGB">Specifies how the red, green, and blue destination blending factors are computed. The initial value is GL_ZERO.</param>
    /// <param name="srcAlpha">Specified how the alpha source blending factor is computed. The initial value is GL_ONE.</param>
    /// <param name="dstAlpha">Specified how the alpha destination blending factor is computed. The initial value is GL_ZERO.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glBlendFuncSeperate(BLEND_SCALE_FACTOR srcRGB, BLEND_SCALE_FACTOR dstRGB, BLEND_SCALE_FACTOR srcAlpha, BLEND_SCALE_FACTOR dstAlpha)
    {
        if (_glBlendFuncSeparate is null)
            throw new EntryPointNotFoundException($"{nameof(_glBlendFuncSeparate)} was called but the entrypoint was not loaded.");

        _glBlendFuncSeparate(srcRGB, dstRGB, srcAlpha, dstAlpha);
    }

    /// <summary>
    /// Creates and initializes a buffer object's data store.
    /// </summary>
    /// <param name="target">Specifies the target to which the buffer object is bound.</param>
    /// <param name="size">Specifies the size in bytes of the buffer object's new data store.</param>
    /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization, or NULL if no data is to be copied.</param>
    /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glBufferData(BUFFER_TARGET target, IntPtr size, IntPtr data, USAGE_PATTERN usage)
    {
        if (_glBufferData is null)
            throw new EntryPointNotFoundException($"{nameof(_glBufferData)} was called but the entrypoint was not loaded.");

        _glBufferData(target, size, data, usage);
    }

    /// <summary>
    /// Compiles the source code strings that have been stored in the shader object specified by shader.
    /// </summary>
    /// <param name="shader">Specifies the shader object to be compiled.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glCompileShader(int shader)
    {
        if (_glCompileShader is null)
            throw new EntryPointNotFoundException($"{nameof(_glCompileShader)} was called but the entrypoint was not loaded.");

        _glCompileShader(shader);
    }

    /// <summary>
    /// Creates an empty program object. A program object is an object to which shader objects can be attached. This provides a mechanism to specify the shader objects that will be linked to create a program. It also provides a means for checking the compatibility of the shaders that will be used to create a program (for instance, checking the compatibility between a vertex shader and a fragment shader).
    /// </summary>
    /// <returns>Returns a non-zero value which by which the program object can be referenced. Returns 0 if an error occurs creating the program object.</returns>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static int glCreateProgram()
    {
        if (_glCreateProgram is null)
            throw new EntryPointNotFoundException($"{nameof(_glCreateProgram)} was called but the entrypoint was not loaded.");

        return _glCreateProgram();
    }

    /// <summary>
    /// Creates an empty shader object. A shader object is used to maintain the source code strings that define a shader.
    /// </summary>
    /// <param name="shaderType">Specifies the type of shader to be created.</param>
    /// <returns>Returns a non-zero value by which the shader object can be referenced. This function returns 0 if an error occurs creating the shader object. GL_INVALID_ENUM is generated if shaderType is not an accepted value.</returns>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static int glCreateShader(SHADER_TYPE shaderType)
    {
        if (_glCreateShader is null)
            throw new EntryPointNotFoundException($"{nameof(_glCreateShader)} was called but the entrypoint was not loaded.");

        return _glCreateShader(shaderType);
    }

    /// <summary>
    /// Specify a callback to receive debugging messages from the GL.
    /// </summary>
    /// <param name="callback">The address of a callback function that will be called when a debug message is generated.</param>
    /// <param name="userParam">A user supplied pointer that will be passed on each invocation of callback.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glDebugMessageCallback(DEBUGPROC callback, IntPtr userParam)
    {
        if (_glDebugMessageCallback is null)
            throw new EntryPointNotFoundException($"{nameof(_glDebugMessageCallback)} was called but the entrypoint was not loaded.");

        _glDebugMessageCallback(Marshal.GetFunctionPointerForDelegate(callback), userParam);
    }

    /// <summary>
    /// Control the reporting of debug messages in a debug context
    /// </summary>
    /// <param name="source">The source of debug messages to enable or disable.</param>
    /// <param name="type">The type of debug messages to enable or disable.</param>
    /// <param name="severity">The severity of debug messages to enable or disable.</param>
    /// <param name="count">The length of the array ids.</param>
    /// <param name="ids">The address of an array of unsigned integers contianing the ids of the messages to enable or disable.</param>
    /// <param name="enabled">A Boolean flag determining whether the selected messages should be enabled or disabled.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glDebugMessageControl(DEBUGMESSAGE_SOURCE source, DEBUGMESSAGE_TYPE type, DEBUGMESSAGE_SEVERITY severity, int count, int[] ids, bool enabled)
    {
        if (_glDebugMessageControl is null)
            throw new EntryPointNotFoundException($"{nameof(_glDebugMessageControl)} was called but the entrypoint was not loaded.");

        _glDebugMessageControl(source, type, severity, count, ids, enabled);
    }

    /// <summary>
    /// Delete named buffer objects.
    /// </summary>
    /// <param name="n">Specifies the number of buffer objects to be deleted.</param>
    /// <param name="buffers">Specifies an array of buffer objects to be deleted.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glDeleteBuffers(int n, int[] buffers)
    {
        if (_glDeleteBuffers is null)
            throw new EntryPointNotFoundException($"{nameof(_glDeleteBuffers)} was called but the entrypoint was not loaded.");

        _glDeleteBuffers(n, buffers);
    }

    /// <summary>
    /// Deletes a shader object.
    /// </summary>
    /// <param name="shader">Specifies the shader object to be deleted.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glDeleteShader(int shader)
    {
        if (_glDeleteShader is null)
            throw new EntryPointNotFoundException($"{nameof(_glDeleteShader)} was called but the entrypoint was not loaded.");

        _glDeleteShader(shader);
    }

    /// <summary>
    /// Deletes a program object.
    /// </summary>
    /// <param name="program">Specifies the program object to be deleted.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glDeleteProgram(int program)
    {
        if (_glDeleteProgram is null)
            throw new EntryPointNotFoundException($"{nameof(_glDeleteProgram)} was called but the entrypoint was not loaded.");

        _glDeleteProgram(program);
    }

    /// <summary>
    /// Delete named framebuffer objects.
    /// </summary>
    /// <param name="n">Specifies the number of framebuffer objects to be deleted.</param>
    /// <param name="framebuffers">Specifies an array of framebuffer objects to be deleted.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glDeleteFramebuffers(int n, int[] framebuffers)
    {
        if (_glDeleteFramebuffers is null)
            throw new EntryPointNotFoundException($"{nameof(_glDeleteFramebuffers)} was called but the entrypoint was not loaded.");

        _glDeleteFramebuffers(n, framebuffers);
    }

    /// <summary>
    /// Detaches a shader object from a program object to which it is attached. This command can be used to undo the effect of the command <see cref="glAttachShader"/>.
    /// </summary>
    /// <param name="program">Specifies the program object from which to detach the shader object.</param>
    /// <param name="shader">Specifies the shader object to be detached.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glDetachShader(int program, int shader)
    {
        if (_glDetachShader is null)
            throw new EntryPointNotFoundException($"{nameof(_glDetachShader)} was called but the entrypoint was not loaded.");

        _glDetachShader(program, shader);
    }

    /// <summary>
    /// Disable a generic vertex attribute array, using the currently bound vertex array object for the operation.
    /// </summary>
    /// <param name="index">Specifies the index of the generic vertex attribute to be disabled.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glDisableVertexAttribArray(int index)
    {
        if (_glDisableVertexAttribArray is null)
            throw new EntryPointNotFoundException($"{nameof(_glDisableVertexAttribArray)} was called but the entrypoint was not loaded.");

        _glDisableVertexAttribArray(index);
    }

    /// <summary>
    /// Enable a generic vertex attribute array, using the currently bound vertex array object for the operation.
    /// </summary>
    /// <param name="index">Specifies the index of the generic vertex attribute to be enabled.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glEnableVertexAttribArray(int index)
    {
        if (_glEnableVertexAttribArray is null)
            throw new EntryPointNotFoundException($"{nameof(_glEnableVertexAttribArray)} was called but the entrypoint was not loaded.");

        _glEnableVertexAttribArray(index);
    }

    /// <summary>
    /// Attach a texture image to a framebuffer object.
    /// </summary>
    /// <param name="target">Specifies the framebuffer target. The symbolic constant must be GL_FRAMEBUFFER.</param>
    /// <param name="attachment">Specifies whether the texture image should be attached to the framebuffer object's color, depth, or stencil buffer. A texture image may not be attached to the default framebuffer object name 0.</param>
    /// <param name="textarget">Specifies the texture target. Must be one of the following symbolic constants: GL_TEXTURE_2D, GL_TEXTURE_CUBE_MAP_POSITIVE_X, GL_TEXTURE_CUBE_MAP_NEGATIVE_X, GL_TEXTURE_CUBE_MAP_POSITIVE_Y, GL_TEXTURE_CUBE_MAP_NEGATIVE_Y, GL_TEXTURE_CUBE_MAP_POSITIVE_Z, or GL_TEXTURE_CUBE_MAP_NEGATIVE_Z.</param>
    /// <param name="texture">Specifies the texture object whose image is to be attached.</param>
    /// <param name="level">Specifies the mipmap level of the texture image to be attached, which must be 0.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glFramebufferTexture2D(FRAMEBUFFER_TARGET target, FRAMEBUFFER_ATTACHMENT_POINT attachment, TEXTURE_TARGET textarget, int texture, int level)
    {
        if (_glFramebufferTexture2D is null)
            throw new EntryPointNotFoundException($"{nameof(_glFramebufferTexture2D)} was called but the entrypoint was not loaded.");

        _glFramebufferTexture2D(target, attachment, textarget, texture, level);
    }

    /// <summary>
    /// Generate buffer object names.
    /// </summary>
    /// <param name="n">Specifies the number of buffer object names to be generated.</param>
    /// <param name="buffers">Specifies an array in which the generated buffer object names are stored.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glGenBuffers(int n, int[] buffers)
    {
        if (_glGenBuffers is null)
            throw new EntryPointNotFoundException($"{nameof(_glGenBuffers)} was called but the entrypoint was not loaded.");

        _glGenBuffers(n, buffers);
    }

    /// <summary>
    /// Generate framebuffer object names.
    /// </summary>
    /// <param name="n">Specifies the number of framebuffer object names to generate.</param>
    /// <param name="buffers">Specifies an array in which the generated framebuffer object names are stored.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glGenFramebuffers(int n, int[] buffers)
    {
        if (_glGenFramebuffers is null)
            throw new EntryPointNotFoundException($"{nameof(_glGenFramebuffers)} was called but the entrypoint was not loaded.");

        _glGenFramebuffers(n, buffers);
    }

    /// <summary>
    /// Returns the location of an attribute variable.
    /// </summary>
    /// <param name="program">Specifies the program object to be queried.</param>
    /// <param name="name">Points to a null terminated string containing the name of the attribute variable whose location is to be queried.</param>
    /// <returns>Returns the index of the generic vertex attribute that is bound to the attribute variable specified by name.</returns>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static int glGetAttribLocation(int program, string name)
    {
        if (_glGetAttribLocation is null)
            throw new EntryPointNotFoundException($"{nameof(_glGetAttribLocation)} was called but the entrypoint was not loaded.");

        return _glGetAttribLocation(program, name);
    }

    /// <summary>
    /// Returns the information log for a program object
    /// </summary>
    /// <param name="program">Specifies the program object whose information log is to be queried.</param>
    /// <param name="maxLength">Specifies the size of the character buffer for storing the returned information log.</param>
    /// <param name="length">Returns the length of the string returned in infoLog (excluding the null terminator).</param>
    /// <param name="infoLog">Specifies an array of characters that is used to return the information log.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glGetProgramInfoLog(int program, int maxLength, out int length, StringBuilder infoLog)
    {
        if (_glGetProgramInfoLog is null)
            throw new EntryPointNotFoundException($"{nameof(_glGetProgramInfoLog)} was called but the entrypoint was not loaded.");

        _glGetProgramInfoLog(program, maxLength, out length, infoLog);
    }

    /// <summary>
    /// Returns a parameter from a shader object
    /// </summary>
    /// <param name="shader">Specifies the shader object to be queried.</param>
    /// <param name="pname">Specifies the object parameter. Accepted symbolic names are GL_SHADER_TYPE, GL_DELETE_STATUS, GL_COMPILE_STATUS, GL_INFO_LOG_LENGTH, GL_SHADER_SOURCE_LENGTH.</param>
    /// <param name="params">Returns the requested object parameter.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public unsafe static void glGetShader(int shader, GETSHADER_NAME pname, int* @params)
    {
        if (_glGetShaderiv is null)
            throw new EntryPointNotFoundException($"{nameof(_glGetShaderiv)} was called but the entrypoint was not loaded.");

        _glGetShaderiv(shader, pname, @params);
    }

    /// <summary>
    /// Returns the information log for a shader object.
    /// </summary>
    /// <param name="shader">Specifies the shader object whose information log is to be queried.</param>
    /// <param name="maxLength">Specifies the size of the character buffer for storing the returned information log.</param>
    /// <param name="length">Returns the length of the string returned in infoLog (excluding the null terminator).</param>
    /// <param name="infoLog">Specifies an array of characters that is used to return the information log.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glGetShaderInfoLog(int shader, int maxLength, out int length, StringBuilder infoLog)
    {
        if (_glGetShaderInfoLog is null)
            throw new EntryPointNotFoundException($"{nameof(_glGetShaderInfoLog)} was called but the entrypoint was not loaded.");

        _glGetShaderInfoLog(shader, maxLength, out length, infoLog);
    }

    /// <summary>
    /// Returns the location of a uniform variable.
    /// </summary>
    /// <param name="program">Specifies the program object to be queried.</param>
    /// <param name="name">Points to a null terminated string containing the name of the uniform variable whose location is to be queried.</param>
    /// <returns>Returns an integer that represents the location of a specific uniform variable within a program object.</returns>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static int glGetUniformLocation(int program, string name)
    {
        if (_glGetUniformLocation is null)
            throw new EntryPointNotFoundException($"{nameof(_glGetUniformLocation)} was called but the entrypoint was not loaded.");

        return _glGetUniformLocation(program, name);
    }

    /// <summary>
    /// Links a program object.
    /// </summary>
    /// <param name="program">Specifies the handle of the program object to be linked.</param>
    /// <remarks>glLinkProgram links the program object specified by program. If any shader objects of type GL_VERTEX_SHADER are attached to program, they will be used to create an executable that will run on the programmable vertex processor. If any shader objects of type GL_GEOMETRY_SHADER are attached to program, they will be used to create an executable that will run on the programmable geometry processor. If any shader objects of type GL_FRAGMENT_SHADER are attached to program, they will be used to create an executable that will run on the programmable fragment processor.
    /// <para>The status of the link operation will be stored as part of the program object's state. This value will be set to GL_TRUE if the program object was linked without errors and is ready for use, and GL_FALSE otherwise. It can be queried by calling glGetProgram with arguments program and GL_LINK_STATUS.</para>
    /// <para>As a result of a successful link operation, all active user-defined uniform variables belonging to program will be initialized to 0, and each of the program object's active uniform variables will be assigned a location that can be queried by calling glGetUniformLocation. Also, any active user-defined attribute variables that have not been bound to a generic vertex attribute index will be bound to one at this time.</para></remarks>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glLinkProgram(int program)
    {
        if (_glLinkProgram is null)
            throw new EntryPointNotFoundException($"{nameof(_glLinkProgram)} was called but the entrypoint was not loaded.");

        _glLinkProgram(program);
    }

    /// <summary>
    /// Sets or replaces the source code in a shader object.
    /// </summary>
    /// <param name="shader">Specifies the handle of the shader object whose source code is to be replaced.</param>
    /// <param name="count">Specifies the number of elements in the string and length arrays.</param>
    /// <param name="strings">Specifies an array of pointers to strings containing the source code to be loaded into the shader.</param>
    /// <param name="lengths">Specifies an array of string lengths.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glShaderSource(int shader, int count, string[] strings, int[]? lengths)
    {
        if (_glShaderSource is null)
            throw new EntryPointNotFoundException($"{nameof(_glShaderSource)} was called but the entrypoint was not loaded.");

        _glShaderSource(shader, count, strings, lengths);
    }

    /// <summary>
    /// Establish the data storage, format, dimensions, and number of samples of a multisample texture's image
    /// </summary>
    /// <param name="target">Specifies the target of the operation. target must be GL_TEXTURE_2D_MULTISAMPLE or GL_PROXY_TEXTURE_2D_MULTISAMPLE.</param>
    /// <param name="samples">The number of samples in the multisample texture's image.</param>
    /// <param name="internalFormat">The internal format to be used to store the multisample texture's image. internalformat must specify a color-renderable, depth-renderable, or stencil-renderable format.</param>
    /// <param name="width">The width of the multisample texture's image, in texels.</param>
    /// <param name="height">The height of the multisample texture's image, in texels.</param>
    /// <param name="fixedSampleLocations">Specifies whether the image will use identical sample locations and the same number of samples for all texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glTexImage2DMultisample(TEXTURE_TARGET target, int samples, TEXTURE_INTERNALFORMAT internalFormat, int width, int height, bool fixedSampleLocations)
    {
        if (_glTexImage2DMultisample is null)
            throw new EntryPointNotFoundException($"{nameof(_glTexImage2DMultisample)} was called but the entrypoint was not loaded.");

        _glTexImage2DMultisample(target, samples, internalFormat, width, height, fixedSampleLocations);
    }

    /// <summary>
    /// Simultaneously specify storage for all levels of a two-dimensional or one-dimensional array texture.
    /// </summary>
    /// <param name="target">Specifies the target to which the texture object is bound for glTexStorage2D. Must be one of GL_TEXTURE_2D, GL_TEXTURE_1D_ARRAY, GL_TEXTURE_RECTANGLE, GL_TEXTURE_CUBE_MAP, GL_PROXY_TEXTURE_2D, GL_PROXY_TEXTURE_1D_ARRAY, GL_PROXY_TEXTURE_RECTANGLE, or GL_PROXY_TEXTURE_CUBE_MAP.</param>
    /// <param name="levels">Specify the number of texture levels.</param>
    /// <param name="internalFormat">Specifies the sized internal format to be used to store texture image data.</param>
    /// <param name="width">Specifies the width of the texture, in texels.</param>
    /// <param name="height">Specifies the height of the texture, in texels.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glTexStorage2D(TEXTURE_TARGET target, int levels, TEXTURE_INTERNALFORMAT internalFormat, int width, int height)
    {
        if (_glTexStorage2D is null)
            throw new EntryPointNotFoundException($"{nameof(_glTexStorage2D)} was called but the entrypoint was not loaded.");

        _glTexStorage2D(target, levels, internalFormat, width, height);
    }

    /// <summary>
    /// Specify the value of a uniform variable for the current program object.
    /// </summary>
    /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
    /// <param name="v0">Specifies the new value to be used.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glUniform1f(int location, float v0)
    {
        if (_glUniform1f is null)
            throw new EntryPointNotFoundException($"{nameof(_glUniform1f)} was called but the entrypoint was not loaded.");

        _glUniform1f(location, v0);
    }

    /// <summary>
    /// Specify the value of a uniform variable for the current program object.
    /// </summary>
    /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
    /// <param name="v0">Specifies the new value to be used.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glUniform1i(int location, int v0)
    {
        if (_glUniform1i is null)
            throw new EntryPointNotFoundException($"{nameof(_glUniform1i)} was called but the entrypoint was not loaded.");

        _glUniform1i(location, v0);
    }

    /// <summary>
    /// Specify the value of a uniform variable for the current program object.
    /// </summary>
    /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
    /// <param name="v0">Specifies the new value to be used.</param>
    /// <param name="v1">Specifies the new value to be used.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glUniform2f(int location, float v0, float v1)
    {
        if (_glUniform2f is null)
            throw new EntryPointNotFoundException($"{nameof(_glUniform2f)} was called but the entrypoint was not loaded.");

        _glUniform2f(location, v0, v1);
    }

    /// <summary>
    /// Specify the value of a uniform variable for the current program object.
    /// </summary>
    /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
    /// <param name="v0">Specifies the new value to be used.</param>
    /// <param name="v1">Specifies the new value to be used.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glUniform2i(int location, int v0, int v1)
    {
        if (_glUniform2i is null)
            throw new EntryPointNotFoundException($"{nameof(_glUniform2i)} was called but the entrypoint was not loaded.");

        _glUniform2i(location, v0, v1);
    }

    /// <summary>
    /// Specify the value of a uniform variable for the current program object.
    /// </summary>
    /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
    /// <param name="v0">Specifies the new value to be used.</param>
    /// <param name="v1">Specifies the new value to be used.</param>
    /// <param name="v2">Specifies the new value to be used.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glUniform3f(int location, float v0, float v1, float v2)
    {
        if (_glUniform3f is null)
            throw new EntryPointNotFoundException($"{nameof(_glUniform3f)} was called but the entrypoint was not loaded.");

        _glUniform3f(location, v0, v1, v2);
    }

    /// <summary>
    /// Specify the value of a uniform variable for the current program object.
    /// </summary>
    /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
    /// <param name="v0">Specifies the new value to be used.</param>
    /// <param name="v1">Specifies the new value to be used.</param>
    /// <param name="v2">Specifies the new value to be used.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glUniform3i(int location, int v0, int v1, int v2)
    {
        if (_glUniform3i is null)
            throw new EntryPointNotFoundException($"{nameof(_glUniform3i)} was called but the entrypoint was not loaded.");

        _glUniform3i(location, v0, v1, v2);
    }

    /// <summary>
    /// Specify the value of a uniform variable for the current program object.
    /// </summary>
    /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
    /// <param name="v0">Specifies the new value to be used.</param>
    /// <param name="v1">Specifies the new value to be used.</param>
    /// <param name="v2">Specifies the new value to be used.</param>
    /// <param name="v3">Specifies the new value to be used.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glUniform4f(int location, float v0, float v1, float v2, float v3)
    {
        if (_glUniform4f is null)
            throw new EntryPointNotFoundException($"{nameof(_glUniform4f)} was called but the entrypoint was not loaded.");

        _glUniform4f(location, v0, v1, v2, v3);
    }

    /// <summary>
    /// Specify the value of a uniform variable for the current program object.
    /// </summary>
    /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
    /// <param name="v0">Specifies the new value to be used.</param>
    /// <param name="v1">Specifies the new value to be used.</param>
    /// <param name="v2">Specifies the new value to be used.</param>
    /// <param name="v3">Specifies the new value to be used.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glUniform4i(int location, int v0, int v1, int v2, int v3)
    {
        if (_glUniform4i is null)
            throw new EntryPointNotFoundException($"{nameof(_glUniform4i)} was called but the entrypoint was not loaded.");

        _glUniform4i(location, v0, v1, v2, v3);
    }

    /// <summary>
    /// Specify the value of a uniform variable for the current program object.
    /// </summary>
    /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
    /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
    /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
    /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public unsafe static void glUniformMatrix3fv(int location, int count, bool transpose, float* value)
    {
        if (_glUniformMatrix3fv is null)
            throw new EntryPointNotFoundException($"{nameof(_glUniformMatrix3fv)} was called but the entrypoint was not loaded.");

        _glUniformMatrix3fv(location, count, transpose, value);
    }

    /// <summary>
    /// Specify the value of a uniform variable for the current program object.
    /// </summary>
    /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
    /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
    /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
    /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public unsafe static void glUniformMatrix4fv(int location, int count, bool transpose, float* value)
    {
        if (_glUniformMatrix4fv is null)
            throw new EntryPointNotFoundException($"{nameof(_glUniformMatrix4fv)} was called but the entrypoint was not loaded.");

        _glUniformMatrix4fv(location, count, transpose, value);
    }

    /// <summary>
    /// Installs a program object as part of current rendering state
    /// </summary>
    /// <param name="program">Specifies the handle of the program object whose executables are to be used as part of current rendering state.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glUseProgram(int program)
    {
        if (_glUseProgram is null)
            throw new EntryPointNotFoundException($"{nameof(_glUseProgram)} was called but the entrypoint was not loaded.");

        _glUseProgram(program);
    }

    /// <summary>
    /// Validates a program object.
    /// </summary>
    /// <param name="program">Specifies the handle of the program object to be validated.</param>
    /// <remarks>The status of the validation operation will be stored as part of the program object's state. This value will be set to GL_TRUE if the validation succeeded, and GL_FALSE otherwise. It can be queried by calling glGetProgram with arguments program and GL_VALIDATE_STATUS. If validation is successful, program is guaranteed to execute given the current state. Otherwise, program is guaranteed to not execute.</remarks>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glValidateProgram(int program)
    {
        if (_glValidateProgram is null)
            throw new EntryPointNotFoundException($"{nameof(_glValidateProgram)} was called but the entrypoint was not loaded.");

        _glValidateProgram(program);
    }

    /// <summary>
    /// Define an array of generic vertex attribute data.
    /// </summary>
    /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
    /// <param name="size">Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, 4. Additionally, the symbolic constant GL_BGRA is accepted by glVertexAttribPointer. The initial value is 4.</param>
    /// <param name="type">Specifies the data type of each component in the array. The symbolic constants GL_BYTE, GL_UNSIGNED_BYTE, GL_SHORT, GL_UNSIGNED_SHORT, GL_INT, and GL_UNSIGNED_INT are accepted by glVertexAttribPointer and glVertexAttribIPointer. Additionally GL_HALF_FLOAT, GL_FLOAT, GL_DOUBLE, GL_FIXED, GL_INT_2_10_10_10_REV, GL_UNSIGNED_INT_2_10_10_10_REV and GL_UNSIGNED_INT_10F_11F_11F_REV are accepted by glVertexAttribPointer. GL_DOUBLE is also accepted by glVertexAttribLPointer and is the only token accepted by the type parameter for that function. The initial value is GL_FLOAT.</param>
    /// <param name="normalised">For glVertexAttribPointer, specifies whether fixed-point data values should be normalized (GL_TRUE) or converted directly as fixed-point values (GL_FALSE) when they are accessed.</param>
    /// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. If stride is 0, the generic vertex attributes are understood to be tightly packed in the array. The initial value is 0.</param>
    /// <param name="pointer">Specifies a offset of the first component of the first generic vertex attribute in the array in the data store of the buffer currently bound to the GL_ARRAY_BUFFER target. The initial value is 0.</param>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public static void glVertexAttribPointer(int index, int size, VERTEX_DATA_TYPE type, bool normalised, int stride, int pointer)
    {
        if (_glVertexAttribPointer is null)
            throw new EntryPointNotFoundException($"{nameof(_glVertexAttribPointer)} was called but the entrypoint was not loaded.");

        _glVertexAttribPointer(index, size, (int)type, normalised, stride, new IntPtr(pointer));
    }
}
