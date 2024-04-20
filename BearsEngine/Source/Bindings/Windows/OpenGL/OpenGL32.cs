using System.Security;
using System.Runtime.InteropServices;

namespace BearsEngine.OpenGL;

#region Enums
/// <summary>
/// The alpha comparison function used in <see cref="OpenGL32.glAlphaFunc"/>
/// </summary>
public enum ALPHA_COMPARISON_FUNCTION : int
{
    /// <summary>
    /// Never passes.
    /// </summary>
    GL_NEVER = 0x0200,

    /// <summary>
    /// Passes if the incoming alpha value is less than the reference value.
    /// </summary>
    GL_LESS = 0x0201,

    /// <summary>
    /// Passes if the incoming alpha value is equal to the reference value.
    /// </summary>
    GL_EQUAL = 0x0202,

    /// <summary>
    /// Passes if the incoming alpha value is less than or equal to the reference value.
    /// </summary>
    GL_LEQUAL = 0x0203,

    /// <summary>
    /// Passes if the incoming alpha value is greater than the reference value.
    /// </summary>
    GL_GREATER = 0x0204,

    /// <summary>
    /// Passes if the incoming alpha value is not equal to the reference value.
    /// </summary>
    GL_NOTEQUAL = 0x0205,

    /// <summary>
    /// Passes if the incoming alpha value is greater than or equal to the reference value.
    /// </summary>
    GL_GEQUAL = 0x0206,

    /// <summary>
    /// Always passes (initial value).
    /// </summary>
    GL_ALWAYS = 0x0207,
}

/// <summary>
/// Keys in the key-value-pairs used in the attribList parameter of <see cref="OpenGL32.wglCreateContextAttribsARB"/>
/// </summary>
public enum ARBCREATECONTEXT_ATTRIBUTE
{
    //https://registry.khronos.org/OpenGL/extensions/ARB/WGL_ARB_create_context.txt

    /// <summary>
    /// Request an OpenGL context supporting the specified version of the API (Major version). Default value is 1.
    /// </summary>
    WGL_CONTEXT_MAJOR_VERSION_ARB = 0x2091,

    /// <summary>
    /// Request an OpenGL context supporting the specified version of the API (Minor version). Default value is 0.
    /// </summary>
    WGL_CONTEXT_MINOR_VERSION_ARB = 0x2092,

    /// <summary>
    /// specifies the layer plane that the rendering context is bound to. The attribute value has the same meaning and allowed values as the iLayerPlane parameter of wglCreateLayerContext. The default value for WGL_CONTEXT_LAYER_PLANE is 0 (the main plane).
    /// </summary>
    WGL_CONTEXT_LAYER_PLANE_ARB = 0x2093,

    /// <summary>
    /// Specifies a set of flag bits affecting the rendering context. Default is 0.
    /// </summary>
    WGL_CONTEXT_FLAGS_ARB = 0x2094,

    /// <summary>
    /// Requests an OpenGL context supporting a specific profile of the API. If the requested OpenGL version is less than 3.2, WGL_CONTEXT_PROFILE_MASK_ARB is ignored and the functionality of the context is determined solely by the requested version. Default is <see cref="ARBCREATECONTEXT_ATTRIBUTE_VALUES.WGL_CONTEXT_CORE_PROFILE_BIT_ARB"/>.
    /// </summary>
    WGL_CONTEXT_PROFILE_MASK_ARB = 0x9126,
}

/// <summary>
/// Values in the key-value-pairs used in the attribList parameter of <see cref="OpenGL32.wglCreateContextAttribsARB"/>
/// </summary>
public enum ARBCREATECONTEXT_ATTRIBUTE_VALUES
{
    /// <summary>
    /// Use with WGL_CONTEXT_MAJOR_VERSION_ARB
    /// </summary>
    WGL_MAJOR_1 = 1,

    /// <summary>
    /// Use with WGL_CONTEXT_MAJOR_VERSION_ARB
    /// </summary>
    WGL_MAJOR_2 = 2,

    /// <summary>
    /// Use with WGL_CONTEXT_MAJOR_VERSION_ARB
    /// </summary>
    WGL_MAJOR_3 = 3,

    /// <summary>
    /// Use with WGL_CONTEXT_MAJOR_VERSION_ARB
    /// </summary>
    WGL_MAJOR_4 = 4,

    /// <summary>
    /// Use with WGL_CONTEXT_MINOR_VERSION_ARB
    /// </summary>
    WGL_MINOR_1 = 1,

    /// <summary>
    /// Use with WGL_CONTEXT_MINOR_VERSION_ARB
    /// </summary>
    WGL_MINOR_2 = 2,

    /// <summary>
    /// Use with WGL_CONTEXT_MINOR_VERSION_ARB
    /// </summary>
    WGL_MINOR_3 = 3,

    /// <summary>
    /// Use with WGL_CONTEXT_MINOR_VERSION_ARB
    /// </summary>
    WGL_MINOR_4 = 4,

    /// <summary>
    /// Use with WGL_CONTEXT_MINOR_VERSION_ARB
    /// </summary>
    WGL_MINOR_5 = 5,

    /// <summary>
    /// Use with WGL_CONTEXT_MINOR_VERSION_ARB
    /// </summary>
    WGL_MINOR_6 = 6,

    /// <summary>
    /// Use with WGL_CONTEXT_FLAGS_ARB. If the WGL_CONTEXT_DEBUG_BIT_ARB flag bit is set in WGL_CONTEXT_FLAGS_ARB, then a <debug context> will be created. Debug contexts are intended for use during application development, and provide additional runtime checking, validation, and logging functionality while possibly incurring performance penalties.
    /// </summary>
    WGL_CONTEXT_DEBUG_BIT_ARB = 0x0001,

    /// <summary>
    /// Use with WGL_CONTEXT_FLAGS_ARB. If the WGL_CONTEXT_FORWARD_COMPATIBLE_BIT_ARB is set in WGL_CONTEXT_FLAGS_ARB, then a <forward-compatible> context will be created. Forward-compatible contexts are defined only for OpenGL versions 3.0 and later. They must not support functionality marked as <deprecated> by that version of the API, while a non-forward-compatible context must support all functionality in that version, deprecated or not.
    /// </summary>
    WGL_CONTEXT_FORWARD_COMPATIBLE_BIT_ARB = 0x0002,

    /// <summary>
    /// Use with WGL_CONTEXT_PROFILE_MASK_ARB.
    /// </summary>
    WGL_CONTEXT_CORE_PROFILE_BIT_ARB = 0x0001,

    /// <summary>
    /// Use with WGL_CONTEXT_PROFILE_MASK_ARB. If the WGL_CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB bit is set, then a context implementing the compatibility profile is returned. 
    /// </summary>
    WGL_CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB = 0x0002,
}

/// <summary>
/// Specifies how colour blending calculations are performed. The color specified by glBlendColor is referred to as (Rc,Gc,Bc,Ac). They are understood to have integer values between 0 and (kR,kG,kB,kA), where kc=2mc−1 and(mR, mG, mB, mA) is the number of red, green, blue, and alpha bitplanes. Source and destination scale factors are referred to as (sR, sG, sB, sA) and (dR, dG, dB, dA). The scale factors described in the table, denoted (fR, fG, fB, fA), represent either source or destination factors. All scale factors have range[0, 1].
/// </summary>
public enum BLEND_SCALE_FACTOR : int
{
    /// <summary>
    /// Maps (0,0,0,0)
    /// </summary>
    GL_ZERO = 0,

    /// <summary>
    /// (1,1,1,1)
    /// </summary>
    GL_ONE = 1,

    /// <summary>
    /// (R? / kR , G? / kG , B? / kB , A? / kA )
    /// </summary>
    GL_SRC_COLOR = 0x0300,

    /// <summary>
    /// (1,1,1,1) - (R? / kR , G? / kG , B? / kB , A? / kA )
    /// </summary>
    GL_ONE_MINUS_SRC_COLOR = 0x0301,

    /// <summary>
    /// (Rd / kR , Gd / kG , Bd / kB , Ad / kA )
    /// </summary>
    GL_DST_COLOR = 0x0306,

    /// <summary>
    /// (1,1,1,1) - (Rd / kR , Gd / kG , Bd / kB , Ad / kA )
    /// </summary>
    GL_ONE_MINUS_DST_COLOR = 0x0307,

    /// <summary>
    /// (A? / kA , A? / kA , A? / kA , A? / kA )
    /// </summary>
    GL_SRC_ALPHA = 0x0302,

    /// <summary>
    /// (1,1,1,1) - (A? / kA , A? / kA , A? / kA , A? / kA )
    /// </summary>
    GL_ONE_MINUS_SRC_ALPHA = 0x0303,

    /// <summary>
    /// (Ad / kA , Ad / kA , Ad / kA , Ad / kA )
    /// </summary>
    GL_DST_ALPHA = 0x0304,

    /// <summary>
    /// (1,1,1,1) - (Ad / kA , Ad / kA , Ad / kA , Ad / kA )
    /// </summary>
    GL_ONE_MINUS_DST_ALPHA = 0x0305,

    /// <summary>
    /// (Rc,Gc,Bc,Ac)
    /// </summary>
    GL_CONSTANT_COLOR = 0x8001,

    /// <summary>
    /// (1,1,1,1)−(Rc,Gc,Bc,Ac)
    /// </summary>
    GL_ONE_MINUS_CONSTANT_COLOR = 0x8002,

    /// <summary>
    /// (Ac,Ac,Ac,Ac)
    /// </summary>
    GL_CONSTANT_ALPHA = 0x8003,

    /// <summary>
    /// (1,1,1,1)−(Ac,Ac,Ac,Ac)
    /// </summary>
    GL_ONE_MINUS_CONSTANT_ALPHA = 0x8004,

    /// <summary>
    /// (i, i, i, 1), i = min (A? , kA - Ad ) / kA
    /// </summary>
    GL_SRC_ALPHA_SATURATE = 0x0308,

    /// <summary>
    /// (Rs1 / kR, Gs1 / kG, Bs1 / kB, As1 / kA)
    /// </summary>
    GL_SRC1_COLOR = 0x88F9,

    /// <summary>
    /// (1,1,1,1) − (Rs1 / kR, Gs1 / kG, Bs1 / kB, As1 / kA)
    /// </summary>
    GL_ONE_MINUS_SRC1_COLOR = 0x88FA,

    /// <summary>
    /// (As1 / kA, As1 / kA,As1 / kA, As1 / kA)
    /// </summary>
    GL_SRC1_ALPHA = 0x8589,

    /// <summary>
    /// (1,1,1,1) − (As1 / kA, As1 / kA, As1 / kA, As1 / kA)
    /// </summary>
    GL_ONE_MINUS_SRC1_ALPHA = 0x88FB,
}

/// <summary>
/// The buffer mask to clear in <see cref="OpenGL32.glClear"/>.
/// </summary>
[Flags]
public enum BUFFER_MASK : uint
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

/// <summary>
/// Buffer binding targets for use with <see cref="OpenGL32.glBindBuffer"/>.
/// </summary>
public enum BUFFER_TARGET : int
{
    /// <summary>
    /// Vertex attributes.
    /// </summary>
    GL_ARRAY_BUFFER = 0x8892,

    /// <summary>
    /// Atomic counter storage.
    /// </summary>
    GL_ATOMIC_COUNTER_BUFFER = 0x92C0,

    /// <summary>
    /// Buffer copy source.
    /// </summary>
    GL_COPY_READ_BUFFER = 0x8F36,

    /// <summary>
    /// Buffer copy destination.
    /// </summary>
    GL_COPY_WRITE_BUFFER = 0x8F37,

    /// <summary>
    /// Indirect compute dispatch commands.
    /// </summary>
    GL_DISPATCH_INDIRECT_BUFFER = 0x90EE,

    /// <summary>
    /// Indirect command arguments.
    /// </summary>
    GL_DRAW_INDIRECT_BUFFER = 0x8F3F,

    /// <summary>
    /// Vertex array indices.
    /// </summary>
    GL_ELEMENT_ARRAY_BUFFER = 0x8893,

    /// <summary>
    /// Pixel read target.
    /// </summary>
    GL_PIXEL_PACK_BUFFER = 0x88EB,

    /// <summary>
    /// Texture data source.
    /// </summary>
    GL_PIXEL_UNPACK_BUFFER = 0x88EC,

    /// <summary>
    /// Query result buffer.
    /// </summary>
    GL_QUERY_BUFFER = 0x9192,

    /// <summary>
    /// Read-write storage for shaders.
    /// </summary>
    GL_SHADER_STORAGE_BUFFER = 0x90D2,

    /// <summary>
    /// Texture data buffer.
    /// </summary>
    GL_TEXTURE_BUFFER = 0x8C2A,

    /// <summary>
    /// Transform feedback buffer.
    /// </summary>
    GL_TRANSFORM_FEEDBACK_BUFFER = 0x8C8E,

    /// <summary>
    /// Uniform block storage.
    /// </summary>
    GL_UNIFORM_BUFFER = 0x8A11,
}

/// <summary>
/// The message severity (how important it is) in a debug message.
/// </summary>
public enum DEBUGMESSAGE_SEVERITY : int
{
    /// <summary>
    /// All OpenGL Errors, shader compilation/linking errors, or highly-dangerous undefined behavior.
    /// </summary>
    GL_DEBUG_SEVERITY_LOW = 0x9148,

    /// <summary>
    /// Major performance warnings, shader compilation/linking warnings, or the use of deprecated functionality.
    /// </summary>
    GL_DEBUG_SEVERITY_MEDIUM = 0x9147,

    /// <summary>
    /// All OpenGL Errors, shader compilation/linking errors, or highly-dangerous undefined behavior.
    /// </summary>
    GL_DEBUG_SEVERITY_HIGH = 0x9146,

    /// <summary>
    /// Anything that isn't an error or performance issue.
    /// </summary>
    GL_DEBUG_SEVERITY_NOTIFICATION = 0x826B,

    /// <summary>
    /// Wildcard - allows all severities.
    /// </summary>
    GL_DONT_CARE = 0x1100,
}

/// <summary>
/// The source that produced the message in a debug message.
/// </summary>
public enum DEBUGMESSAGE_SOURCE : int
{
    /// <summary>
    /// Calls to the OpenGL API.
    /// </summary>
    GL_DEBUG_SOURCE_API = 0x8246,

    /// <summary>
    /// Calls to a window-system API.
    /// </summary>
    GL_DEBUG_SOURCE_WINDOW_SYSTEM = 0x8247,

    /// <summary>
    /// A compiler for a shading language.
    /// </summary>
    GL_DEBUG_SOURCE_SHADER_COMPILER = 8248,

    /// <summary>
    /// An application associated with OpenGL.
    /// </summary>
    GL_DEBUG_SOURCE_THIRD_PARTY = 0x8249,

    /// <summary>
    /// Generated by the user of this application.
    /// </summary>
    GL_DEBUG_SOURCE_APPLICATION = 0x824A,

    /// <summary>
    /// Some source that isn't one of these.
    /// </summary>
    GL_DEBUG_SOURCE_OTHER = 0x824B,

    /// <summary>
    /// Wildcard - allows all sources.
    /// </summary>
    GL_DONT_CARE = 0x1100,
}

/// <summary>
/// The type of message in a Debug Message.
/// </summary>
public enum DEBUGMESSAGE_TYPE : int
{
    /// <summary>
    /// An error, typically from the API.
    /// </summary>
    GL_DEBUG_TYPE_ERROR = 0x824C,

    /// <summary>
    /// Some behavior marked deprecated has been used.
    /// </summary>
    GL_DEBUG_TYPE_DEPRECATED_BEHAVIOR = 0x824D,

    /// <summary>
    /// Something has invoked undefined behavior.
    /// </summary>
    GL_DEBUG_TYPE_UNDEFINED_BEHAVIOR = 0x824E,

    /// <summary>
    /// Some functionality the user relies upon is not portable.
    /// </summary>
    GL_DEBUG_TYPE_PORTABILITY = 0x824F,

    /// <summary>
    /// Code has triggered possible performance issues.
    /// </summary>
    GL_DEBUG_TYPE_PEFORMANCE = 0x8250,

    /// <summary>
    /// Command stream annotation.
    /// </summary>
    GL_DEBUG_TYPE_MARKER = 0x824B,

    /// <summary>
    /// Group pushing.
    /// </summary>
    GL_DEBUG_TYPE_PUSH_GROUP = 0x8269,

    /// <summary>
    /// Group popping.
    /// </summary>
    GL_DEBUG_TYPE_POP_GROUP = 0x826A,

    /// <summary>
    /// Some type that isn't one of these.
    /// </summary>
    GL_DEBUG_TYPE_OTHER = 0x8251,

    /// <summary>
    /// Wildcard - allows all types.
    /// </summary>
    GL_DONT_CARE = 0x1100,
}

/// <summary>
/// Specifies the attachment point of the framebuffer.
/// </summary>
public enum FRAMEBUFFER_ATTACHMENT_POINT : int
{
    GL_DEPTH_STENCIL_ATTACHMENT = 0x821A,
    GL_COLOR_ATTACHMENT0 = 0x8CE0,
    GL_COLOR_ATTACHMENT1 = 0x8CE1,
    GL_COLOR_ATTACHMENT2 = 0x8CE2,
    GL_COLOR_ATTACHMENT3 = 0x8CE3,
    GL_COLOR_ATTACHMENT4 = 0x8CE4,
    GL_COLOR_ATTACHMENT5 = 0x8CE5,
    GL_COLOR_ATTACHMENT6 = 0x8CE6,
    GL_COLOR_ATTACHMENT7 = 0x8CE7,
    GL_COLOR_ATTACHMENT8 = 0x8CE8,
    GL_COLOR_ATTACHMENT9 = 0x8CE9,
    GL_COLOR_ATTACHMENT10 = 0x8CEA,
    GL_COLOR_ATTACHMENT11 = 0x8CEB,
    GL_COLOR_ATTACHMENT12 = 0x8CEC,
    GL_COLOR_ATTACHMENT13 = 0x8CED,
    GL_COLOR_ATTACHMENT14 = 0x8CEE,
    GL_COLOR_ATTACHMENT15 = 0x8CEF,
    GL_DEPTH_ATTACHMENT = 0x8D00,
    GL_STENCIL_ATTACHMENT = 0x8D20,
}

/// <summary>
/// A framebuffer target.
/// </summary>
public enum FRAMEBUFFER_TARGET : int
{
    /// <summary>
    /// Specify read-only framebuffer operations such as glReadPixels.
    /// </summary>
    GL_READ_FRAMEBUFFER = 0x8CA8,

    /// <summary>
    /// Specify write-only framebuffer operations such as glWritePixels.
    /// </summary>
    GL_DRAW_FRAMEBUFFER = 0x8CA9,

    /// <summary>
    /// Specify all read and write framebuffer operations.
    /// </summary>
    GL_FRAMEBUFFER = 0x8D40,
}

/// <summary>
/// Specifies the orientation of front-facing polygons.
/// </summary>
public enum FRONTFACE_MODE : int
{
    /// <summary>
    /// Selects counterclockwise polygons as front-facing. The projection of a polygon to window coordinates is said to have a clockwise winding if an imaginary object following the path from its first vertex, its second vertex, and so on, to its last vertex, and finally back to its first vertex, moves in a clockwise direction about the interior of the polygon.
    /// </summary>
    GL_CW = 0x0900,

    /// <summary>
    /// Selects clockwise polygons as front-facing. The projection of a polygon to window coordinates is said to have a counter-clockwise winding if an imaginary object following the path from its first vertex, its second vertex, and so on, to its last vertex, and finally back to its first vertex, moves in a counter-clockwise direction about the interior of the polygon.
    /// </summary>
    GL_CCW = 0x0901,
}

/// <summary>
/// <see cref="OpenGL32.glGetShader"/> returns in params the value of a parameter for a specific shader object.
/// </summary>
public enum GETSHADER_NAME : int
{
    /// <summary>
    /// params returns GL_VERTEX_SHADER if shader is a vertex shader object, GL_GEOMETRY_SHADER if shader is a geometry shader object, and GL_FRAGMENT_SHADER if shader is a fragment shader object.
    /// </summary>
    GL_SHADER_TYPE = 0x8B4F,

    /// <summary>
    /// params returns GL_TRUE if shader is currently flagged for deletion, and GL_FALSE otherwise.
    /// </summary>
    GL_DELETE_STATUS = 0x8B80,

    /// <summary>
    /// params returns GL_TRUE if the last compile operation on shader was successful, and GL_FALSE otherwise.
    /// </summary>
    GL_COMPILE_STATUS = 0x8B81,

    /// <summary>
    /// params returns the number of characters in the information log for shader including the null termination character (i.e., the size of the character buffer required to store the information log). If shader has no information log, a value of 0 is returned.
    /// </summary>
    GL_INFO_LOG_LENGTH = 0x8B84,

    /// <summary>
    /// params returns the length of the concatenation of the source strings that make up the shader source for the shader, including the null termination character. (i.e., the size of the character buffer required to store the shader source). If no source code exists, 0 is returned.
    /// </summary>
    GL_SHADER_SOURCE_LENGTH = 0x8B88,
}

/// <summary>
/// Specifies the string to retrieve in <see cref="OpenGL32.glGetString"/>
/// </summary>
public enum GETSTRING_NAME : uint
{
    /// <summary>
    /// Returns the company responsible for this GL implementation. This name does not change from release to release.
    /// </summary>
    GL_VENDOR = 0x1F00,

    /// <summary>
    /// Returns the name of the renderer. This name is typically specific to a particular configuration of a hardware platform. It does not change from release to release.
    /// </summary>
    GL_RENDERER = 0x1F01,

    /// <summary>
    /// Returns a version or release number.
    /// </summary>
    GL_VERSION = 0x1F02,

    /// <summary>
    /// Returns a version or release number for the shading language.
    /// </summary>
    GL_SHADING_LANGUAGE_VERSION = 0x8B8C,

    /// <summary>
    /// For glGetStringi only, returns the extension string supported by the implementation at index.
    /// </summary>
    GL_EXTENSIONS = 0x1F03,
}

/// <summary>
/// Error codes returned by the <see cref="OpenGL32.glGetError"/> function.
/// </summary>
public enum GL_ERROR : uint
{
    /// <summary>
    /// No (more) errors are available
    /// </summary>
    GL_NO_ERROR = 0,

    /// <summary>
    /// Given when an enumeration parameter is not a legal enumeration for that function. This is given only for local problems; if the spec allows the enumeration in certain circumstances, where other parameters or state dictate those circumstances, then GL_INVALID_OPERATION is the result instead.
    /// </summary>
    GL_INVALID_ENUM = 0x0500,

    /// <summary>
    /// Given when a value parameter is not a legal value for that function. This is only given for local problems; if the spec allows the value in certain circumstances, where other parameters or state dictate those circumstances, then GL_INVALID_OPERATION is the result instead.
    /// </summary>
    GL_INVALID_VALUE = 0x0501,

    /// <summary>
    /// Given when the set of state for a command is not legal for the parameters given to that command. It is also given for commands where combinations of parameters define what the legal parameters are.
    /// </summary>
    GL_INVALID_OPERATION = 0x0502,

    /// <summary>
    /// Given when a stack pushing operation cannot be done because it would overflow the limit of that stack's size.
    /// </summary>
    GL_STACK_OVERFLOW = 0x0503,

    /// <summary>
    /// Given when a stack popping operation cannot be done because the stack is already at its lowest point.
    /// </summary>
    GL_STACK_UNDERFLOW = 0x0504,

    /// <summary>
    /// Given when performing an operation that can allocate memory, and the memory cannot be allocated. The results of OpenGL functions that return this error are undefined; it is allowable for partial execution of an operation to happen in this circumstance.
    /// </summary>
    GL_OUT_OF_MEMORY = 0x0505,

    /// <summary>
    /// Given when doing anything that would attempt to read from or write/render to a framebuffer that is not complete.
    /// </summary>
    GL_INVALID_FRAMEBUFFER_OPERATION = 0x0506,

    /// <summary>
    /// Given if the OpenGL context has been lost, due to a graphics card reset.
    /// </summary>
    GL_CONTEXT_LOST = 0x0507,

    /// <summary>
    /// [Deprecated] Part of the ARB_imaging extension.
    /// </summary>
    GL_TABLE_TOO_LARGE = 0x8031
}

/// <summary>
/// Specifies a light.
/// </summary>
public enum LIGHT : int
{
    GL_LIGHT0 = 0x4000,
    GL_LIGHT1 = 0x4001,
    GL_LIGHT2 = 0x4002,
    GL_LIGHT3 = 0x4003,
    GL_LIGHT4 = 0x4004,
    GL_LIGHT5 = 0x4005,
    GL_LIGHT6 = 0x4006,
    GL_LIGHT7 = 0x4007,
}

/// <summary>
/// A light source parameter for light in <see cref="OpenGL32.glLightfv"/>.
/// </summary>
public enum LIGHTSOURCE_NAME : int
{
    /// <summary>
    /// The params parameter contains four floating-point values that specify the ambient RGBA intensity of the light. Floating-point values are mapped directly. Neither integer nor floating-point values are clamped. The default ambient light intensity is (0.0, 0.0, 0.0, 1.0).
    /// </summary>
    GL_AMBIENT = 0x1200,

    /// <summary>
    /// The params parameter contains four floating-point values that specify the diffuse RGBA intensity of the light. Floating-point values are mapped directly. Neither integer nor floating-point values are clamped. The default diffuse intensity is (0.0, 0.0, 0.0, 1.0) for all lights other than light zero. The default diffuse intensity of light zero is (1.0, 1.0, 1.0, 1.0).
    /// </summary>
    GL_DIFFUSE = 0x1201,

    /// <summary>
    /// The params parameter contains four floating-point values that specify the specular RGBA intensity of the light. Floating-point values are mapped directly. Neither integer nor floating-point values are clamped. The default specular intensity is (0.0, 0.0, 0.0, 1.0) for all lights other than light zero. The default specular intensity of light zero is (1.0, 1.0, 1.0, 1.0).
    /// </summary>
    GL_SPECULAR = 0x1202,

    /// <summary>
    /// The params parameter contains four floating-point values that specify the position of the light in homogeneous object coordinates. Both integer and floating-point values are mapped directly. Neither integer nor floating-point values are clamped. The position is transformed by the modelview matrix when glLightfv is called (just as if it were a point), and it is stored in eye coordinates. If the w component of the position is 0.0, the light is treated as a directional source. Diffuse and specular lighting calculations take the light's direction, but not its actual position, into account, and attenuation is disabled. Otherwise, diffuse and specular lighting calculations are based on the actual location of the light in eye coordinates, and attenuation is enabled. The default position is (0,0,1,0); thus, the default light source is directional, parallel to, and in the direction of the -z axis.
    /// </summary>
    GL_POSITION = 0x1203,

    /// <summary>
    /// The params parameter contains three floating-point values that specify the direction of the light in homogeneous object coordinates. Both integer and floating-point values are mapped directly. Neither integer nor floating-point values are clamped. The spot direction is transformed by the inverse of the modelview matrix when glLightfv is called (just as if it were a normal), and it is stored in eye coordinates. It is significant only when GL_SPOT_CUTOFF is not 180, which it is by default. The default direction is (0,0,1).
    /// </summary>
    GL_SPOT_DIRECTION = 0x1204,

    /// <summary>
    /// The params parameter is a single floating-point value that specifies the intensity distribution of the light. Integer and floating-point values are mapped directly. Only values in the range [0, 128] are accepted. Effective light intensity is attenuated by the cosine of the angle between the direction of the light and the direction from the light to the vertex being lighted, raised to the power of the spot exponent. Thus, higher spot exponents result in a more focused light source, regardless of the spot cutoff angle. The default spot exponent is 0, resulting in uniform light distribution.
    /// </summary>
    GL_SPOT_EXPONENT = 0x1205,

    /// <summary>
    /// The params parameter is a single floating-point value that specifies the maximum spread angle of a light source. Integer and floating-point values are mapped directly. Only values in the range [0, 90], and the special value 180, are accepted. If the angle between the direction of the light and the direction from the light to the vertex being lighted is greater than the spot cutoff angle, then the light is completely masked. Otherwise, its intensity is controlled by the spot exponent and the attenuation factors. The default spot cutoff is 180, resulting in uniform light distribution.
    /// </summary>
    GL_SPOT_CUTOFF = 0x1206,

    /// <summary>
    /// The params parameter is a single floating-point value that specifies one of the three light attenuation factors. Integer and floating-point values are mapped directly. Only nonnegative values are accepted. If the light is positional, rather than directional, its intensity is attenuated by the reciprocal of the sum of: the constant factor, the linear factor multiplied by the distance between the light and the vertex being lighted, and the quadratic factor multiplied by the square of the same distance. The default attenuation factors are (1,0,0), resulting in no attenuation.
    /// </summary>
    GL_CONSTANT_ATTENUATION = 0x1207,

    /// <summary>
    /// The params parameter is a single floating-point value that specifies one of the three light attenuation factors. Integer and floating-point values are mapped directly. Only nonnegative values are accepted. If the light is positional, rather than directional, its intensity is attenuated by the reciprocal of the sum of: the constant factor, the linear factor multiplied by the distance between the light and the vertex being lighted, and the quadratic factor multiplied by the square of the same distance. The default attenuation factors are (1,0,0), resulting in no attenuation.
    /// </summary>
    GL_LINEAR_ATTENUATION = 0x1208,

    /// <summary>
    /// The params parameter is a single floating-point value that specifies one of the three light attenuation factors. Integer and floating-point values are mapped directly. Only nonnegative values are accepted. If the light is positional, rather than directional, its intensity is attenuated by the reciprocal of the sum of: the constant factor, the linear factor multiplied by the distance between the light and the vertex being lighted, and the quadratic factor multiplied by the square of the same distance. The default attenuation factors are (1,0,0), resulting in no attenuation.
    /// </summary>
    GL_QUADRATIC_ATTENUATION = 0x1209,
}

/// <summary>
/// Specifies which matrix stack is the target for subsequent matrix operations.
/// </summary>
public enum MATRIX_MODE : int
{
    /// <summary>
    /// Applies subsequent matrix operations to the modelview matrix stack.
    /// </summary>
    GL_MODELVIEW = 0x1700,

    /// <summary>
    /// Applies subsequent matrix operations to the projection matrix stack.
    /// </summary>
    GL_PROJECTION = 0x1701,

    /// <summary>
    /// Applies subsequent matrix operations to the texture matrix stack.
    /// </summary>
    GL_TEXTURE = 0x1702,

    /// <summary>
    /// Applies subsequent matrix operations to the color matrix stack. Only available if the ARB_imaging extension is supported.
    /// </summary>
    GL_COLOR = 0x1800,
}

/// <summary>
/// The material parameter of the face or faces being updated in <see cref="OpenGL32.glMaterialfv"/>.
/// </summary>
public enum MATERIAL_NAME : int
{
    /// <summary>
    /// The params parameter contains four floating-point values that specify the ambient RGBA reflectance of the material. Integer values are mapped linearly such that the most positive representable value maps to 1.0, and the most negative representable value maps to -1.0. Floating-point values are mapped directly. Neither integer nor floating-point values are clamped. The default ambient reflectance for both front-facing and back-facing materials is (0.2, 0.2, 0.2, 1.0).
    /// </summary>
    GL_AMBIENT = 0x1200,

    /// <summary>
    /// The params parameter contains four floating-point values that specify the diffuse RGBA reflectance of the material. Integer values are mapped linearly such that the most positive representable value maps to 1.0, and the most negative representable value maps to -1.0. Floating-point values are mapped directly. Neither integer nor floating-point values are clamped. The default diffuse reflectance for both front-facing and back-facing materials is (0.8, 0.8, 0.8, 1.0).
    /// </summary>
    GL_DIFFUSE = 0x1201,

    /// <summary>
    /// The params parameter contains four floating-point values that specify the specular RGBA reflectance of the material. Integer values are mapped linearly such that the most positive representable value maps to 1.0, and the most negative representable value maps to -1.0. Floating-point values are mapped directly. Neither integer nor floating-point values are clamped. The default specular reflectance for both front-facing and back-facing materials is (0.0, 0.0, 0.0, 1.0).
    /// </summary>
    GL_SPECULAR = 0x1202,

    /// <summary>
    /// The params parameter contains four floating-point values that specify the RGBA emitted light intensity of the material. Integer values are mapped linearly such that the most positive representable value maps to 1.0, and the most negative representable value maps to -1.0. Floating-point values are mapped directly. Neither integer nor floating-point values are clamped. The default emission intensity for both front-facing and back-facing materials is (0.0, 0.0, 0.0, 1.0).
    /// </summary>
    GL_EMISSION = 0x1600,

    /// <summary>
    /// The param parameter is a single integer value that specifies the RGBA specular exponent of the material. Integer values are mapped directly. Only values in the range [0, 128] are accepted. The default specular exponent for both front-facing and back-facing materials is 0.
    /// </summary>
    GL_SHININESS = 0x1601,

    /// <summary>
    /// Equivalent to calling glMaterial twice with the same parameter values, once with GL_AMBIENT and once with GL_DIFFUSE.
    /// </summary>
    GL_AMBIENT_AND_DIFFUSE = 0x1602,

    /// <summary>
    /// The params parameter contains three floating-point values specifying the color indexes for ambient, diffuse, and specular lighting. These three values, and GL_SHININESS, are the only material values used by the color-index mode lighting equation. Refer to glLightModel for a discussion of color-index lighting.
    /// </summary>
    GL_COLOR_INDEXES = 0x1603,
}

/// <summary>
/// The face of polygons to apply certain functions to
/// </summary>
public enum POLYGON_FACE : int
{
    /// <summary>
    /// front-facing polygons
    /// </summary>
    GL_FRONT = 0x0404,

    /// <summary>
    /// back-facing polygons
    /// </summary>
    GL_BACK = 0x0405,

    /// <summary>
    /// front- and back-facing polygons
    /// </summary>
    GL_FRONT_AND_BACK = 0x0408,
}

/// <summary>
/// The mode for use with <see cref="OpenGL32.glPolygonMode"/>
/// </summary>
public enum POLYGON_MODE : int
{
    /// <summary>
    /// Polygon vertices that are marked as the start of a boundary edge are drawn as points. Point attributes such as GL_POINT_SIZE and GL_POINT_SMOOTH control the rasterization of the points. Polygon rasterization attributes other than GL_POLYGON_MODE have no effect.
    /// </summary>
    GL_POINT = 0x1B00,

    /// <summary>
    /// Boundary edges of the polygon are drawn as line segments. They are treated as connected line segments for line stippling; the line stipple counter and pattern are not reset between segments (see glLineStipple). Line attributes such as GL_LINE_WIDTH and GL_LINE_SMOOTH control the rasterization of the lines. Polygon rasterization attributes other than GL_POLYGON_MODE have no effect.
    /// </summary>
    GL_LINE = 0x1B01,

    /// <summary>
    /// The interior of the polygon is filled. Polygon attributes such as GL_POLYGON_STIPPLE and GL_POLYGON_SMOOTH control the rasterization of the polygon.
    /// </summary>
    GL_FILL = 0x1B02,
}

/// <summary>
/// Specifies what kind of primitives to render in <see cref="OpenGL32.glDrawArrays"/>.
/// </summary>
public enum PRIMITIVE_TYPE : int
{
    /// <summary>
    /// Treats each vertex as a single point. Vertex n defines point n. N points are drawn.
    /// </summary>
    GL_POINTS = 0,

    /// <summary>
    /// Treats each pair of vertices as an independent line segment. Vertices 2 ⁢ n - 1 and 2 ⁢ n define line n. N 2 lines are drawn.
    /// </summary>
    GL_LINES = 1,

    /// <summary>
    /// Draws a connected group of line segments from the first vertex to the last. Vertices n and n + 1 define line n. N - 1 lines are drawn.
    /// </summary>
    GL_LINE_STRIP = 3,

    /// <summary>
    /// Draws a connected group of line segments from the first vertex to the last, then back to the first. Vertices n and n + 1 define line n. The last line, however, is defined by vertices N and 1 . N lines are drawn.
    /// </summary>
    GL_LINE_LOOP = 2,

    /// <summary>
    /// Treats each triplet of vertices as an independent triangle. Vertices 3 ⁢ n - 2 , 3 ⁢ n - 1 , and 3 ⁢ n define triangle n. N 3 triangles are drawn.
    /// </summary>
    GL_TRIANGLES = 4,

    /// <summary>
    /// Draws a connected group of triangles. One triangle is defined for each vertex presented after the first two vertices. For odd n, vertices n, n + 1 , and n + 2 define triangle n. For even n, vertices n + 1 , n, and n + 2 define triangle n. N - 2 triangles are drawn.
    /// </summary>
    GL_TRIANGLE_STRIP = 5,

    /// <summary>
    /// Draws a connected group of triangles. One triangle is defined for each vertex presented after the first two vertices. Vertices 1 , n + 1 , and n + 2 define triangle n. N - 2 triangles are drawn.
    /// </summary>
    GL_TRIANGLE_FAN = 6,

    /// <summary>
    /// Treats each group of four vertices as an independent quadrilateral. Vertices 4 ⁢ n - 3 , 4 ⁢ n - 2 , 4 ⁢ n - 1 , and 4 ⁢ n define quadrilateral n. N 4 quadrilaterals are drawn.
    /// </summary>
    GL_QUADS = 7,

    /// <summary>
    /// Draws a connected group of quadrilaterals. One quadrilateral is defined for each pair of vertices presented after the first pair. Vertices 2 ⁢ n - 1 , 2 ⁢ n , 2 ⁢ n + 2 , and 2 ⁢ n + 1 define quadrilateral n. N 2 - 1 quadrilaterals are drawn. Note that the order in which vertices are used to construct a quadrilateral from strip data is different from that used with independent data.
    /// </summary>
    GL_QUAD_STRIP = 8,

    /// <summary>
    /// Draws a single, convex polygon. Vertices 1 through N define this polygon.
    /// </summary>
    GL_POLYGON = 9,

    GL_LINE_STRIP_ADJACENCY = 11,

    GL_LINES_ADJACENCY = 10,

    GL_TRIANGLE_STRIP_ADJACENCY = 13,

    GL_TRIANGLES_ADJACENCY = 12,

    GL_PATCHES = 14,
}

/// <summary>
/// Flat and smooth shading are specified by <see cref="OpenGL32.glShadeModel"/> with mode set to GL_FLAT and GL_SMOOTH, respectively.
/// </summary>
public enum SHADE_TECHNIQUE : int
{
    /// <summary>
    /// Flat shading selects the computed color of just one vertex and assigns it to all the pixel fragments generated by rasterizing a single primitive.
    /// </summary>
    GL_FLAT = 0x1D00,

    /// <summary>
    /// Smooth shading, the default, causes the computed colors of vertices to be interpolated as the primitive is rasterized, typically assigning different colors to each resulting pixel fragment. 
    /// </summary>
    GL_SMOOTH = 0x1D01,
}

/// <summary>
/// The symbolic name of a single valued texture parameter.
/// </summary>
public enum TEXPARAMETER_NAME : int
{
    /// <summary>
    /// Specifies the mode used to read from depth-stencil format textures. params must be one of GL_DEPTH_COMPONENT or GL_STENCIL_INDEX. If the depth stencil mode is GL_DEPTH_COMPONENT, then reads from depth-stencil format textures will return the depth component of the texel in Rt and the stencil component will be discarded. If the depth stencil mode is GL_STENCIL_INDEX then the stencil component is returned in Rt and the depth component is discarded. The initial value is GL_DEPTH_COMPONENT.
    /// </summary>
    GL_DEPTH_STENCIL_TEXTURE_MODE = 0x90EA,

    /// <summary>
    /// Specifies the index of the lowest defined mipmap level. This is an integer value. The initial value is 0.
    /// </summary>
    GL_TEXTURE_BASE_LEVEL = 0x813C,

    /// <summary>
    /// Specifies the comparison operator used when GL_TEXTURE_COMPARE_MODE is set to GL_COMPARE_REF_TO_TEXTURE.
    /// </summary>
    GL_TEXTURE_COMPARE_FUNC = 0x884D,

    /// <summary>
    /// Specifies the texture comparison mode for currently bound depth textures.
    /// </summary>
    GL_TEXTURE_COMPARE_MODE = 0x884C,

    /// <summary>
    /// params specifies a fixed bias value that is to be added to the level-of-detail parameter for the texture before texture sampling. The specified value is added to the shader-supplied bias value (if any) and subsequently clamped into the implementation-defined range [−biasmax,biasmax], where biasmax is the value of the implementation defined constant GL_MAX_TEXTURE_LOD_BIAS. The initial value is 0.0.
    /// </summary>
    GL_TEXTURE_LOD_BIAS = 0x8501,

    /// <summary>
    /// The texture minifying function is used whenever the pixel being textured maps to an area greater than one texture element. There are six defined minifying functions. Two of them use the nearest one or nearest four texture elements to compute the texture value. The other four use mipmaps.
    /// </summary>
    GL_TEXTURE_MIN_FILTER = 0x2801,

    /// <summary>
    /// The texture magnification function is used when the pixel being textured maps to an area less than or equal to one texture element. It sets the texture magnification function to either GL_NEAREST or GL_LINEAR.
    /// </summary>
    GL_TEXTURE_MAG_FILTER = 0x2800,

    /// <summary>
    /// Sets the minimum level-of-detail parameter. This floating-point value limits the selection of highest resolution mipmap (lowest mipmap level). The initial value is -1000.
    /// </summary>
    GL_TEXTURE_MIN_LOD = 0x813A,

    /// <summary>
    /// Sets the maximum level-of-detail parameter. This floating-point value limits the selection of the lowest resolution mipmap (highest mipmap level). The initial value is 1000.
    /// </summary>
    GL_TEXTURE_MAX_LOD = 0x813B,

    /// <summary>
    /// Sets the index of the highest defined mipmap level. This is an integer value. The initial value is 1000.
    /// </summary>
    GL_TEXTURE_MAX_LEVEL = 0x813D,

    /// <summary>
    /// Sets the swizzle that will be applied to the r component of a texel before it is returned to the shader. The initial value is GL_RED.
    /// </summary>
    GL_TEXTURE_SWIZZLE_R = 0x8E42,

    /// <summary>
    /// Sets the swizzle that will be applied to the g component of a texel before it is returned to the shader. The initial value is GL_GREEN.
    /// </summary>
    GL_TEXTURE_SWIZZLE_G = 0x8E43,

    /// <summary>
    /// Sets the swizzle that will be applied to the b component of a texel before it is returned to the shader. The initial value is GL_BLUE.
    /// </summary>
    GL_TEXTURE_SWIZZLE_B = 0x8E44,

    /// <summary>
    /// Sets the swizzle that will be applied to the a component of a texel before it is returned to the shader. The initial value is GL_ALPHA.
    /// </summary>
    GL_TEXTURE_SWIZZLE_A = 0x8E45,

    /// <summary>
    /// Sets the wrap parameter for texture coordinate s to either GL_CLAMP or GL_REPEAT. GL_CLAMP causes s coordinates to be clamped to the range [0,1] and is useful for preventing wrapping artifacts when mapping a single image onto an object. GL_REPEAT causes the integer part of the s coordinate to be ignored; OpenGL uses only the fractional part, thereby creating a repeating pattern. Border texture elements are accessed only if wrapping is set to GL_CLAMP. Initially, GL_TEXTURE_WRAP_S is set to GL_REPEAT.
    /// </summary>
    GL_TEXTURE_WRAP_S = 0x2802,

    /// <summary>
    /// Sets the wrap parameter for texture coordinate t to either GL_CLAMP or GL_REPEAT. See the discussion under GL_TEXTURE_WRAP_S. Initially, GL_TEXTURE_WRAP_T is set to GL_REPEAT.
    /// </summary>
    GL_TEXTURE_WRAP_T = 0x2803,

    /// <summary>
    /// Sets the wrap parameter for texture coordinate r to either GL_CLAMP_TO_EDGE, GL_MIRRORED_REPEAT, or GL_REPEAT. Initially, GL_TEXTURE_WRAP_R is set to GL_REPEAT.
    /// </summary>
    GL_TEXTURE_WRAP_R = 0x8072,

    /// <summary>
    /// Valid in glTexParameter*v. Sets a border color. The params parameter contains four values that comprise the RGBA color of the texture border.
    /// </summary>
    GL_TEXTURE_BORDER_COLOR = 0x1004,

    /// <summary>
    /// Valid in glTexParameter*v. 
    /// </summary>
    GL_TEXTURE_SWIZZLE_RGBA = 0x8E46,

    /// <summary>
    /// Specifies the texture residence priority of the currently bound texture. Permissible values are in the range [0, 1].
    /// </summary>
    GL_TEXTURE_PRIORITY = 0x8066,
}

/// <summary>
/// Specifies a target for texture binding. 
/// </summary>
public enum TEXTURE_TARGET : int
{
    /// <summary>
    /// one-dimensional texture
    /// </summary>
    GL_TEXTURE_1D = 0x0DE0,

    /// <summary>
    /// two-dimensional texture
    /// </summary>
    GL_TEXTURE_2D = 0x0DE1,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_1D = 0x8063,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_1D_EXT = 0x8063,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_2D = 0x8064,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_2D_EXT = 0x8064,

    /// <summary>
    /// three-dimensional texture
    /// </summary>
    GL_TEXTURE_3D = 0x806F,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_3D_EXT = 0x806F,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_3D_OES = 0x806F,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_3D = 0x8070,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_3D_EXT = 0x8070,

    /// <summary>
    /// 
    /// </summary>
    GL_DETAIL_TEXTURE_2D_SGIS = 0x8095,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_4D_SGIS = 0x8134,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_4D_SGIS = 0x8135,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_MIN_LOD = 0x813A,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_MIN_LOD_SGIS = 0x813A,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_MAX_LOD = 0x813B,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_MAX_LOD_SGIS = 0x813B,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_BASE_LEVEL = 0x813C,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_BASE_LEVEL_SGIS = 0x813C,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_MAX_LEVEL = 0x813D,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_MAX_LEVEL_SGIS = 0x813D,

    /// <summary>
    /// rectangle texture
    /// </summary>
    GL_TEXTURE_RECTANGLE = 0x84F5,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_RECTANGLE_ARB = 0x84F5,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_RECTANGLE_NV = 0x84F5,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_RECTANGLE = 0x84F7,

    /// <summary>
    /// cube-mapped texture
    /// </summary>
    GL_TEXTURE_CUBE_MAP = 0x8513,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_BINDING_CUBE_MAP = 0x8514,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519,

    /// <summary>
    /// 
    /// </summary>
    GL_TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_CUBE_MAP = 0x851B,

    /// <summary>
    /// one-dimensional array texture
    /// </summary>
    GL_TEXTURE_1D_ARRAY = 0x8C18,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_1D_ARRAY = 0x8C19,

    /// <summary>
    /// two-dimensional array texture
    /// </summary>
    GL_TEXTURE_2D_ARRAY = 0x8C1A,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_2D_ARRAY = 0x8C1B,

    /// <summary>
    /// buffer texture
    /// </summary>
    GL_TEXTURE_BUFFER = 0x8C2A,

    /// <summary>
    /// cube-mapped array texture
    /// </summary>
    GL_TEXTURE_CUBE_MAP_ARRAY = 0x9009,

    /// <summary>
    /// 
    /// </summary>
    GL_PROXY_TEXTURE_CUBE_MAP_ARRAY = 0x900B,

    /// <summary>
    /// two-dimensional multisampled texture, use with glTexImage2DMultisample 
    /// </summary>
    GL_TEXTURE_2D_MULTISAMPLE = 0x9100,

    /// <summary>
    /// Use with glTexImage2DMultisample 
    /// </summary>
    GL_PROXY_TEXTURE_2D_MULTISAMPLE = 0x9101,

    /// <summary>
    /// Use with glTexImage3DMultisample
    /// </summary>
    GL_TEXTURE_2D_MULTISAMPLE_ARRAY = 0x9102,

    /// <summary>
    /// Use with glTexImage3DMultisample
    /// </summary>
    GL_PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY = 0x9103
}

//-----Enums finalised above this line-----

public enum GLCAP : int
{
    GL_CURRENT_COLOR = 0x0B00,
    GL_CURRENT_INDEX = 0x0B01,
    GL_CURRENT_NORMAL = 0x0B02,
    GL_CURRENT_TEXTURE_COORDS = 0x0B03,
    GL_CURRENT_RASTER_COLOR = 0x0B04,
    GL_CURRENT_RASTER_INDEX = 0x0B05,
    GL_CURRENT_RASTER_TEXTURE_COORDS = 0x0B06,
    GL_CURRENT_RASTER_POSITION = 0x0B07,
    GL_CURRENT_RASTER_POSITION_VALID = 0x0B08,
    GL_CURRENT_RASTER_DISTANCE = 0x0B09,
    GL_POINT_SMOOTH = 0x0B10,
    GL_POINT_SIZE = 0x0B11,
    GL_POINT_SIZE_RANGE = 0x0B12,
    GL_POINT_SIZE_GRANULARITY = 0x0B13,
    GL_LINE_SMOOTH = 0x0B20,
    GL_LINE_WIDTH = 0x0B21,
    GL_LINE_WIDTH_RANGE = 0x0B22,
    GL_LINE_WIDTH_GRANULARITY = 0x0B23,
    GL_LINE_STIPPLE = 0x0B24,
    GL_LINE_STIPPLE_PATTERN = 0x0B25,
    GL_LINE_STIPPLE_REPEAT = 0x0B26,
    GL_LIST_MODE = 0x0B30,
    GL_MAX_LIST_NESTING = 0x0B31,
    GL_LIST_BASE = 0x0B32,
    GL_LIST_INDEX = 0x0B33,
    GL_POLYGON_MODE = 0x0B40,
    GL_POLYGON_SMOOTH = 0x0B41,
    GL_POLYGON_STIPPLE = 0x0B42,
    GL_EDGE_FLAG = 0x0B43,
    GL_CULL_FACE = 0x0B44,
    GL_CULL_FACE_MODE = 0x0B45,
    GL_FRONT_FACE = 0x0B46,
    GL_LIGHTING = 0x0B50,
    GL_LIGHT_MODEL_LOCAL_VIEWER = 0x0B51,
    GL_LIGHT_MODEL_TWO_SIDE = 0x0B52,
    GL_LIGHT_MODEL_AMBIENT = 0x0B53,
    GL_SHADE_MODEL = 0x0B54,
    GL_COLOR_MATERIAL_FACE = 0x0B55,
    GL_COLOR_MATERIAL_PARAMETER = 0x0B56,
    GL_COLOR_MATERIAL = 0x0B57,
    GL_FOG = 0x0B60,
    GL_FOG_INDEX = 0x0B61,
    GL_FOG_DENSITY = 0x0B62,
    GL_FOG_START = 0x0B63,
    GL_FOG_END = 0x0B64,
    GL_FOG_MODE = 0x0B65,
    GL_FOG_COLOR = 0x0B66,
    GL_DEPTH_RANGE = 0x0B70,
    GL_DEPTH_TEST = 0x0B71,
    GL_DEPTH_WRITEMASK = 0x0B72,
    GL_DEPTH_CLEAR_VALUE = 0x0B73,
    GL_DEPTH_FUNC = 0x0B74,
    GL_ACCUM_CLEAR_VALUE = 0x0B80,
    GL_STENCIL_TEST = 0x0B90,
    GL_STENCIL_CLEAR_VALUE = 0x0B91,
    GL_STENCIL_FUNC = 0x0B92,
    GL_STENCIL_VALUE_MASK = 0x0B93,
    GL_STENCIL_FAIL = 0x0B94,
    GL_STENCIL_PASS_DEPTH_FAIL = 0x0B95,
    GL_STENCIL_PASS_DEPTH_PASS = 0x0B96,
    GL_STENCIL_REF = 0x0B97,
    GL_STENCIL_WRITEMASK = 0x0B98,
    GL_MATRIX_MODE = 0x0BA0,
    GL_NORMALIZE = 0x0BA1,
    GL_VIEWPORT = 0x0BA2,
    GL_MODELVIEW_STACK_DEPTH = 0x0BA3,
    GL_PROJECTION_STACK_DEPTH = 0x0BA4,
    GL_TEXTURE_STACK_DEPTH = 0x0BA5,
    GL_MODELVIEW_MATRIX = 0x0BA6,
    GL_PROJECTION_MATRIX = 0x0BA7,
    GL_TEXTURE_MATRIX = 0x0BA8,
    GL_ATTRIB_STACK_DEPTH = 0x0BB0,
    GL_CLIENT_ATTRIB_STACK_DEPTH = 0x0BB1,
    GL_ALPHA_TEST = 0x0BC0,
    GL_ALPHA_TEST_FUNC = 0x0BC1,
    GL_ALPHA_TEST_REF = 0x0BC2,
    GL_DITHER = 0x0BD0,
    GL_BLEND_DST = 0x0BE0,
    GL_BLEND_SRC = 0x0BE1,
    GL_BLEND = 0x0BE2,
    GL_LOGIC_OP_MODE = 0x0BF0,
    GL_INDEX_LOGIC_OP = 0x0BF1,
    GL_COLOR_LOGIC_OP = 0x0BF2,
    GL_AUX_BUFFERS = 0x0C00,
    GL_DRAW_BUFFER = 0x0C01,
    GL_READ_BUFFER = 0x0C02,
    GL_SCISSOR_BOX = 0x0C10,
    GL_SCISSOR_TEST = 0x0C11,
    GL_INDEX_CLEAR_VALUE = 0x0C20,
    GL_INDEX_WRITEMASK = 0x0C21,
    GL_COLOR_CLEAR_VALUE = 0x0C22,
    GL_COLOR_WRITEMASK = 0x0C23,
    GL_INDEX_MODE = 0x0C30,
    GL_RGBA_MODE = 0x0C31,
    GL_DOUBLEBUFFER = 0x0C32,
    GL_STEREO = 0x0C33,
    GL_RENDER_MODE = 0x0C40,
    GL_PERSPECTIVE_CORRECTION_HINT = 0x0C50,
    GL_POINT_SMOOTH_HINT = 0x0C51,
    GL_LINE_SMOOTH_HINT = 0x0C52,
    GL_POLYGON_SMOOTH_HINT = 0x0C53,
    GL_FOG_HINT = 0x0C54,
    GL_TEXTURE_GEN_S = 0x0C60,
    GL_TEXTURE_GEN_T = 0x0C61,
    GL_TEXTURE_GEN_R = 0x0C62,
    GL_TEXTURE_GEN_Q = 0x0C63,
    GL_PIXEL_MAP_I_TO_I = 0x0C70,
    GL_PIXEL_MAP_S_TO_S = 0x0C71,
    GL_PIXEL_MAP_I_TO_R = 0x0C72,
    GL_PIXEL_MAP_I_TO_G = 0x0C73,
    GL_PIXEL_MAP_I_TO_B = 0x0C74,
    GL_PIXEL_MAP_I_TO_A = 0x0C75,
    GL_PIXEL_MAP_R_TO_R = 0x0C76,
    GL_PIXEL_MAP_G_TO_G = 0x0C77,
    GL_PIXEL_MAP_B_TO_B = 0x0C78,
    GL_PIXEL_MAP_A_TO_A = 0x0C79,
    GL_PIXEL_MAP_I_TO_I_SIZE = 0x0CB0,
    GL_PIXEL_MAP_S_TO_S_SIZE = 0x0CB1,
    GL_PIXEL_MAP_I_TO_R_SIZE = 0x0CB2,
    GL_PIXEL_MAP_I_TO_G_SIZE = 0x0CB3,
    GL_PIXEL_MAP_I_TO_B_SIZE = 0x0CB4,
    GL_PIXEL_MAP_I_TO_A_SIZE = 0x0CB5,
    GL_PIXEL_MAP_R_TO_R_SIZE = 0x0CB6,
    GL_PIXEL_MAP_G_TO_G_SIZE = 0x0CB7,
    GL_PIXEL_MAP_B_TO_B_SIZE = 0x0CB8,
    GL_PIXEL_MAP_A_TO_A_SIZE = 0x0CB9,
    GL_UNPACK_SWAP_BYTES = 0x0CF0,
    GL_UNPACK_LSB_FIRST = 0x0CF1,
    GL_UNPACK_ROW_LENGTH = 0x0CF2,
    GL_UNPACK_SKIP_ROWS = 0x0CF3,
    GL_UNPACK_SKIP_PIXELS = 0x0CF4,
    GL_UNPACK_ALIGNMENT = 0x0CF5,
    GL_PACK_SWAP_BYTES = 0x0D00,
    GL_PACK_LSB_FIRST = 0x0D01,
    GL_PACK_ROW_LENGTH = 0x0D02,
    GL_PACK_SKIP_ROWS = 0x0D03,
    GL_PACK_SKIP_PIXELS = 0x0D04,
    GL_PACK_ALIGNMENT = 0x0D05,
    GL_MAP_COLOR = 0x0D10,
    GL_MAP_STENCIL = 0x0D11,
    GL_INDEX_SHIFT = 0x0D12,
    GL_INDEX_OFFSET = 0x0D13,
    GL_RED_SCALE = 0x0D14,
    GL_RED_BIAS = 0x0D15,
    GL_ZOOM_X = 0x0D16,
    GL_ZOOM_Y = 0x0D17,
    GL_GREEN_SCALE = 0x0D18,
    GL_GREEN_BIAS = 0x0D19,
    GL_BLUE_SCALE = 0x0D1A,
    GL_BLUE_BIAS = 0x0D1B,
    GL_ALPHA_SCALE = 0x0D1C,
    GL_ALPHA_BIAS = 0x0D1D,
    GL_DEPTH_SCALE = 0x0D1E,
    GL_DEPTH_BIAS = 0x0D1F,
    GL_MAX_EVAL_ORDER = 0x0D30,
    GL_MAX_LIGHTS = 0x0D31,
    GL_MAX_CLIP_PLANES = 0x0D32,
    GL_MAX_TEXTURE_SIZE = 0x0D33,
    GL_MAX_PIXEL_MAP_TABLE = 0x0D34,
    GL_MAX_ATTRIB_STACK_DEPTH = 0x0D35,
    GL_MAX_MODELVIEW_STACK_DEPTH = 0x0D36,
    GL_MAX_NAME_STACK_DEPTH = 0x0D37,
    GL_MAX_PROJECTION_STACK_DEPTH = 0x0D38,
    GL_MAX_TEXTURE_STACK_DEPTH = 0x0D39,
    GL_MAX_VIEWPORT_DIMS = 0x0D3A,
    GL_MAX_CLIENT_ATTRIB_STACK_DEPTH = 0x0D3B,
    GL_SUBPIXEL_BITS = 0x0D50,
    GL_INDEX_BITS = 0x0D51,
    GL_RED_BITS = 0x0D52,
    GL_GREEN_BITS = 0x0D53,
    GL_BLUE_BITS = 0x0D54,
    GL_ALPHA_BITS = 0x0D55,
    GL_DEPTH_BITS = 0x0D56,
    GL_STENCIL_BITS = 0x0D57,
    GL_ACCUM_RED_BITS = 0x0D58,
    GL_ACCUM_GREEN_BITS = 0x0D59,
    GL_ACCUM_BLUE_BITS = 0x0D5A,
    GL_ACCUM_ALPHA_BITS = 0x0D5B,
    GL_NAME_STACK_DEPTH = 0x0D70,
    GL_AUTO_NORMAL = 0x0D80,
    GL_MAP1_COLOR_4 = 0x0D90,
    GL_MAP1_INDEX = 0x0D91,
    GL_MAP1_NORMAL = 0x0D92,
    GL_MAP1_TEXTURE_COORD_1 = 0x0D93,
    GL_MAP1_TEXTURE_COORD_2 = 0x0D94,
    GL_MAP1_TEXTURE_COORD_3 = 0x0D95,
    GL_MAP1_TEXTURE_COORD_4 = 0x0D96,
    GL_MAP1_VERTEX_3 = 0x0D97,
    GL_MAP1_VERTEX_4 = 0x0D98,
    GL_MAP2_COLOR_4 = 0x0DB0,
    GL_MAP2_INDEX = 0x0DB1,
    GL_MAP2_NORMAL = 0x0DB2,
    GL_MAP2_TEXTURE_COORD_1 = 0x0DB3,
    GL_MAP2_TEXTURE_COORD_2 = 0x0DB4,
    GL_MAP2_TEXTURE_COORD_3 = 0x0DB5,
    GL_MAP2_TEXTURE_COORD_4 = 0x0DB6,
    GL_MAP2_VERTEX_3 = 0x0DB7,
    GL_MAP2_VERTEX_4 = 0x0DB8,
    GL_MAP1_GRID_DOMAIN = 0x0DD0,
    GL_MAP1_GRID_SEGMENTS = 0x0DD1,
    GL_MAP2_GRID_DOMAIN = 0x0DD2,
    GL_MAP2_GRID_SEGMENTS = 0x0DD3,
    GL_TEXTURE_1D = 0x0DE0,
    GL_TEXTURE_2D = 0x0DE1,
    GL_FEEDBACK_BUFFER_POINTER = 0x0DF0,
    GL_FEEDBACK_BUFFER_SIZE = 0x0DF1,
    GL_FEEDBACK_BUFFER_TYPE = 0x0DF2,
    GL_SELECTION_BUFFER_POINTER = 0x0DF3,
    GL_SELECTION_BUFFER_SIZE = 0x0DF4,
    GL_DEBUG_OUTPUT = 0x92E0,
    GL_MULTISAMPLE = 0x809D,
}

public enum PIXEL_STORE_MODE : int
{
    GL_UNPACK_SWAP_BYTES = 0x0CF0,
    GL_UNPACK_LSB_FIRST = 0x0CF1,
    GL_UNPACK_ROW_LENGTH = 0x0CF2,
    GL_UNPACK_ROW_LENGTH_EXT = 0x0CF2,
    GL_UNPACK_SKIP_ROWS = 0x0CF3,
    GL_UNPACK_SKIP_ROWS_EXT = 0x0CF3,
    GL_UNPACK_SKIP_PIXELS = 0x0CF4,
    GL_UNPACK_SKIP_PIXELS_EXT = 0x0CF4,
    GL_UNPACK_ALIGNMENT = 0x0CF5,
    GL_PACK_SWAP_BYTES = 0x0D00,
    GL_PACK_LSB_FIRST = 0x0D01,
    GL_PACK_ROW_LENGTH = 0x0D02,
    GL_PACK_SKIP_ROWS = 0x0D03,
    GL_PACK_SKIP_PIXELS = 0x0D04,
    GL_PACK_ALIGNMENT = 0x0D05,
    GL_PACK_SKIP_IMAGES = 0x806B,
    GL_PACK_SKIP_IMAGES_EXT = 0x806B,
    GL_PACK_IMAGE_HEIGHT = 0x806C,
    GL_PACK_IMAGE_HEIGHT_EXT = 0x806C,
    GL_UNPACK_SKIP_IMAGES = 0x806D,
    GL_UNPACK_SKIP_IMAGES_EXT = 0x806D,
    GL_UNPACK_IMAGE_HEIGHT = 0x806E,
    GL_UNPACK_IMAGE_HEIGHT_EXT = 0x806E,
    GL_PACK_SKIP_VOLUMES_SGIS = 0x8130,
    GL_PACK_IMAGE_DEPTH_SGIS = 0x8131,
    GL_UNPACK_SKIP_VOLUMES_SGIS = 0x8132,
    GL_UNPACK_IMAGE_DEPTH_SGIS = 0x8133,
    GL_PIXEL_TILE_WIDTH_SGIX = 0x8140,
    GL_PIXEL_TILE_HEIGHT_SGIX = 0x8141,
    GL_PIXEL_TILE_GRID_WIDTH_SGIX = 0x8142,
    GL_PIXEL_TILE_GRID_HEIGHT_SGIX = 0x8143,
    GL_PIXEL_TILE_GRID_DEPTH_SGIX = 0x8144,
    GL_PIXEL_TILE_CACHE_SIZE_SGIX = 0x8145,
    GL_PACK_RESAMPLE_SGIX = 0x842C,
    GL_UNPACK_RESAMPLE_SGIX = 0x842D,
    GL_PACK_SUBSAMPLE_RATE_SGIX = 0x85A0,
    GL_UNPACK_SUBSAMPLE_RATE_SGIX = 0x85A1,
    GL_PACK_RESAMPLE_OML = 0x8984,
    GL_UNPACK_RESAMPLE_OML = 0x8985,
    GL_UNPACK_COMPRESSED_BLOCK_WIDTH = 0x9127,
    GL_UNPACK_COMPRESSED_BLOCK_HEIGHT = 0x9128,
    GL_UNPACK_COMPRESSED_BLOCK_DEPTH = 0x9129,
    GL_UNPACK_COMPRESSED_BLOCK_SIZE = 0x912A,
    GL_PACK_COMPRESSED_BLOCK_WIDTH = 0x912B,
    GL_PACK_COMPRESSED_BLOCK_HEIGHT = 0x912C,
    GL_PACK_COMPRESSED_BLOCK_DEPTH = 0x912D,
    GL_PACK_COMPRESSED_BLOCK_SIZE = 0x912E
}

public enum PIXEL_FORMAT : int
{
    GL_STENCIL_INDEX = 0X1901,
    GL_DEPTH_COMPONENT = 0X1902,
    GL_DEPTH_STENCIL = 0X84f9,
    GL_RED = 0X1903,
    GL_GREEN = 0X1904,
    GL_BLUE = 0X1905,
    GL_RG = 0X8227,
    GL_RGB = 0X1907,
    GL_RGBA = 0X1908,
    GL_BGR = 0X80e0,
    GL_BGRA = 0X80e1,
    GL_RED_INTEGER = 0X8d94,
    GL_GREEN_INTEGER = 0X8d95,
    GL_BLUE_INTEGER = 0X8d96,
    GL_RGB_INTEGER = 0X8d98,
    GL_RGBA_INTEGER = 0X8d99,
    GL_BGR_INTEGER = 0X8d9a,
    GL_BGRA_INTEGER = 0X8d9b,

    ColorIndex = 0X1900,
    Alpha = 0X1906,
    Luminance = 0X1909,
    LuminanceAlpha = 0X190a,
    AbgrExt = 0X8000,
    CmykExt = 0X800c,
    CmykaExt = 0X800D,
    Ycrcb422Sgix = 0X81bb,
    Ycrcb444Sgix = 0X81bc,
    RgInteger = 0X8228,
    AlphaInteger = 0X8d97,
}

public enum PIXEL_TYPE : int
{
    GL_UNSIGNED_BYTE = 0X1401,
    GL_BYTE = 0X1400,
    GL_UNSIGNED_SHORT = 0X1403,
    GL_SHORT = 0X1402,
    GL_UNSIGNED_INT = 0X1405,
    GL_INT = 0X1404,
    GL_HALF_FLOAT = 0X140b,
    GL_FLOAT = 0X1406,
    GL_UNSIGNED_BYTE_3_3_2 = 0X8032,
    GL_UNSIGNED_BYTE_2_3_3_REV = 0X8362,
    GL_UNSIGNED_SHORT_5_6_5 = 0X8363,
    GL_UNSIGNED_SHORT_5_6_5_REV = 0X8364,
    GL_UNSIGNED_SHORT_4_4_4_4 = 0X8033,
    GL_UNSIGNED_SHORT_4_4_4_4_REV = 0X8365,
    GL_UNSIGNED_SHORT_5_5_5_1 = 0X8034,
    GL_UNSIGNED_SHORT_1_5_5_5_REV = 0X8366,
    GL_UNSIGNED_INT_8_8_8_8 = 0X8035,
    GL_UNSIGNED_INT_8_8_8_8_REV = 0X8367,
    GL_UNSIGNED_INT_10_10_10_2 = 0X8036,
    GL_UNSIGNED_INT_2_10_10_10_REV = 0X8368,
    GL_UNSIGNED_INT_24_8 = 0X84fa,
    GL_UNSIGNED_INT_10F_11F_11F_REV = 0X8c3b,
    GL_UNSIGNED_INT_5_9_9_9_REV = 0X8c3e,
    GL_FLOAT_32_UNSIGNED_INT_24_8_REV = 0X8Dad,
}

public enum SHADER_TYPE : int
{
    GL_COMPUTE_SHADER = 0x91B9,
    GL_VERTEX_SHADER = 0x8B31,
    GL_TESS_CONTROL_SHADER = 0x8E88,
    GL_TESS_EVALUATION_SHADER = 0x8E87,
    GL_GEOMETRY_SHADER = 0x8DD9,
    GL_FRAGMENT_SHADER = 0x8B30,
}

[Flags]
public enum STATE_CAP : uint
{
    GL_VERTEX_ARRAY = 0x8074,
    GL_NORMAL_ARRAY = 0x8075,
    GL_COLOR_ARRAY = 0x8076,
    GL_INDEX_ARRAY = 0x8077,
    GL_TEXTURE_COORD_ARRAY = 0x8078,
    GL_EDGE_FLAG_ARRAY = 0x8079,
    GL_VERTEX_ARRAY_SIZE = 0x807A,
    GL_VERTEX_ARRAY_TYPE = 0x807B,
    GL_VERTEX_ARRAY_STRIDE = 0x807C,
    GL_NORMAL_ARRAY_TYPE = 0x807E,
    GL_NORMAL_ARRAY_STRIDE = 0x807F,
    GL_COLOR_ARRAY_SIZE = 0x8081,
    GL_COLOR_ARRAY_TYPE = 0x8082,
    GL_COLOR_ARRAY_STRIDE = 0x8083,
    GL_INDEX_ARRAY_TYPE = 0x8085,
    GL_INDEX_ARRAY_STRIDE = 0x8086,
    GL_TEXTURE_COORD_ARRAY_SIZE = 0x8088,
    GL_TEXTURE_COORD_ARRAY_TYPE = 0x8089,
    GL_TEXTURE_COORD_ARRAY_STRIDE = 0x808A,
    GL_EDGE_FLAG_ARRAY_STRIDE = 0x808C,
    GL_VERTEX_ARRAY_POINTER = 0x808E,
    GL_NORMAL_ARRAY_POINTER = 0x808F,
    GL_COLOR_ARRAY_POINTER = 0x8090,
    GL_INDEX_ARRAY_POINTER = 0x8091,
    GL_TEXTURE_COORD_ARRAY_POINTER = 0x8092,
    GL_EDGE_FLAG_ARRAY_POINTER = 0x8093,
    GL_V2F = 0x2A20,
    GL_V3F = 0x2A21,
    GL_C4UB_V2F = 0x2A22,
    GL_C4UB_V3F = 0x2A23,
    GL_C3F_V3F = 0x2A24,
    GL_N3F_V3F = 0x2A25,
    GL_C4F_N3F_V3F = 0x2A26,
    GL_T2F_V3F = 0x2A27,
    GL_T4F_V4F = 0x2A28,
    GL_T2F_C4UB_V3F = 0x2A29,
    GL_T2F_C3F_V3F = 0x2A2A,
    GL_T2F_N3F_V3F = 0x2A2B,
    GL_T2F_C4F_N3F_V3F = 0x2A2C,
    GL_T4F_C4F_N3F_V4F = 0x2A2D,
}

public enum TEXPARAMETER_VALUE : int
{
    //https://registry.khronos.org/OpenGL-Refpages/es3.0/html/glTexParameter.xhtml
    //https://registry.khronos.org/OpenGL-Refpages/gl4/html/glTexParameter.xhtml
    GL_REPEAT = 0x2901,

    GL_CLAMP_TO_BORDER = 0x812D,

    GL_CLAMP_TO_BORDER_ARB = 0x812D,

    GL_CLAMP_TO_BORDER_NV = 0x812D,

    GL_CLAMP_TO_BORDER_SGIS = 0x812D,

    GL_CLAMP_TO_EDGE = 0x812F,

    GL_CLAMP_TO_EDGE_SGIS = 0x812F,

    GL_MIRRORED_REPEAT = 0x8370,

    GL_NEAREST = 0x2600,

    GL_LINEAR = 0x2601,

    GL_LINEAR_DETAIL_SGIS = 0x8097,

    GL_LINEAR_DETAIL_ALPHA_SGIS = 0x8098,

    GL_LINEAR_DETAIL_COLOR_SGIS = 0x8099,

    GL_LINEAR_SHARPEN_SGIS = 0x80AD,

    GL_LINEAR_SHARPEN_ALPHA_SGIS = 0x80AE,

    GL_LINEAR_SHARPEN_COLOR_SGIS = 0x80AF,

    GL_FILTER4_SGIS = 0x8146,

    GL_PIXEL_TEX_GEN_Q_CEILING_SGIX = 0x8184,

    GL_PIXEL_TEX_GEN_Q_ROUND_SGIX = 0x8185,

    GL_PIXEL_TEX_GEN_Q_FLOOR_SGIX = 0x8186,

    GL_NEAREST_MIPMAP_NEAREST = 0x2700,

    GL_LINEAR_MIPMAP_NEAREST = 0x2701,

    GL_NEAREST_MIPMAP_LINEAR = 0x2702,

    GL_LINEAR_MIPMAP_LINEAR = 0x2703,

    GL_LINEAR_CLIPMAP_LINEAR_SGIX = 0x8170,

    GL_NEAREST_CLIPMAP_NEAREST_SGIX = 0x844D,

    GL_NEAREST_CLIPMAP_LINEAR_SGIX = 0x844E,

    GL_LINEAR_CLIPMAP_NEAREST_SGIX = 0x844F,
}

public enum TEXTURE_INTERNALFORMAT : int
{
    GL_DEPTH_COMPONENT = 0x1902,

    GL_ALPHA = 0x1906,

    GL_RGB = 0x1907,

    GL_RGBA = 0x1908,

    GL_LUMINANCE = 0x1909,

    GL_LUMINANCE_ALPHA = 0x190A,

    GL_R3_G3_B2 = 0x2A10,

    GL_ALPHA4 = 0x803B,

    GL_ALPHA8 = 0x803C,

    GL_ALPHA12 = 0x803D,

    GL_ALPHA16 = 0x803E,

    GL_LUMINANCE4 = 0x803F,

    GL_LUMINANCE8 = 0x8040,

    GL_LUMINANCE12 = 0x8041,

    GL_LUMINANCE16 = 0x8042,

    GL_LUMINANCE4_ALPHA4 = 0x8043,

    GL_LUMINANCE6_ALPHA2 = 0x8044,

    GL_LUMINANCE8_ALPHA8 = 0x8045,

    GL_LUMINANCE12_ALPHA4 = 0x8046,

    GL_LUMINANCE12_ALPHA12 = 0x8047,

    GL_LUMINANCE16_ALPHA16 = 0x8048,

    GL_INTENSITY = 0x8049,

    GL_INTENSITY4 = 0x804A,

    GL_INTENSITY8 = 0x804B,

    GL_INTENSITY12 = 0x804C,

    GL_INTENSITY16 = 0x804D,

    GL_RGB2_EXT = 0x804E,

    GL_RGB4 = 0x804F,

    GL_RGB5 = 0x8050,

    GL_RGB8 = 0x8051,

    GL_RGB10 = 0x8052,

    GL_RGB12 = 0x8053,

    GL_RGB16 = 0x8054,

    GL_RGBA2 = 0x8055,

    GL_RGBA4 = 0x8056,

    GL_RGB5_A1 = 0x8057,

    GL_RGBA8 = 0x8058,

    GL_RGB10_A2 = 0x8059,

    GL_RGBA12 = 0x805A,

    GL_RGBA16 = 0x805B,

    GL_DUAL_ALPHA4_SGIS = 0x8110,

    GL_DUAL_ALPHA8_SGIS = 0x8111,

    GL_DUAL_ALPHA12_SGIS = 0x8112,

    GL_DUAL_ALPHA16_SGIS = 0x8113,

    GL_DUAL_LUMINANCE4_SGIS = 0x8114,

    GL_DUAL_LUMINANCE8_SGIS = 0x8115,

    GL_DUAL_LUMINANCE12_SGIS = 0x8116,

    GL_DUAL_LUMINANCE16_SGIS = 0x8117,

    GL_DUAL_INTENSITY4_SGIS = 0x8118,

    GL_DUAL_INTENSITY8_SGIS = 0x8119,

    GL_DUAL_INTENSITY12_SGIS = 0x811A,

    GL_DUAL_INTENSITY16_SGIS = 0x811B,

    GL_DUAL_LUMINANCE_ALPHA4_SGIS = 0x811C,

    GL_DUAL_LUMINANCE_ALPHA8_SGIS = 0x811D,

    GL_QUAD_ALPHA4_SGIS = 0x811E,

    GL_QUAD_ALPHA8_SGIS = 0x811F,

    GL_QUAD_LUMINANCE4_SGIS = 0x8120,

    GL_QUAD_LUMINANCE8_SGIS = 0x8121,

    GL_QUAD_INTENSITY4_SGIS = 0x8122,

    GL_QUAD_INTENSITY8_SGIS = 0x8123,

    GL_DEPTH_COMPONENT16 = 0x81a5,

    GL_DEPTH_COMPONENT16_SGIX = 0x81A5,

    GL_DEPTH_COMPONENT24 = 0x81a6,

    GL_DEPTH_COMPONENT24_SGIX = 0x81A6,

    GL_DEPTH_COMPONENT32 = 0x81a7,

    GL_DEPTH_COMPONENT32_SGIX = 0x81A7,

    GL_COMPRESSED_RED = 0x8225,

    GL_COMPRESSED_RG = 0x8226,

    GL_R8 = 0x8229,

    GL_R16 = 0x822A,

    GL_RG8 = 0x822B,

    GL_RG16 = 0x822C,

    GL_R16F = 0x822D,

    GL_R32F = 0x822E,

    GL_RG16F = 0x822F,

    GL_RG32F = 0x8230,

    GL_R8I = 0x8231,

    GL_R8UI = 0x8232,

    GL_R16I = 0x8233,

    GL_R16UI = 0x8234,

    GL_R32I = 0x8235,

    GL_R32UI = 0x8236,

    GL_RG8I = 0x8237,

    GL_RG8UI = 0x8238,

    GL_RG16I = 0x8239,

    GL_RG16UI = 0x823A,

    GL_RG32I = 0x823B,

    GL_RG32UI = 0x823C,

    GL_COMPRESSED_RGB_S3TC_DXT1_EXT = 0x83F0,

    GL_COMPRESSED_RGBA_S3TC_DXT1_EXT = 0x83F1,

    GL_COMPRESSED_RGBA_S3TC_DXT3_EXT = 0x83F2,

    GL_COMPRESSED_RGBA_S3TC_DXT5_EXT = 0x83F3,

    GL_RGB_ICC_SGIX = 0x8460,

    GL_RGBA_ICC_SGIX = 0x8461,

    GL_ALPHA_ICC_SGIX = 0x8462,

    GL_LUMINANCE_ICC_SGIX = 0x8463,

    GL_INTENSITY_ICC_SGIX = 0x8464,

    GL_LUMINANCE_ALPHA_ICC_SGIX = 0x8465,

    GL_R5_G6_B5_ICC_SGIX = 0x8466,

    GL_R5_G6_B5_A8_ICC_SGIX = 0x8467,

    GL_ALPHA16_ICC_SGIX = 0x8468,

    GL_LUMINANCE16_ICC_SGIX = 0x8469,

    GL_INTENSITY16_ICC_SGIX = 0x846A,

    GL_LUMINANCE16_ALPHA8_ICC_SGIX = 0x846B,

    GL_COMPRESSED_ALPHA = 0x84E9,

    GL_COMPRESSED_LUMINANCE = 0x84EA,

    GL_COMPRESSED_LUMINANCE_ALPHA = 0x84EB,

    GL_COMPRESSED_INTENSITY = 0x84EC,

    GL_COMPRESSED_RGB = 0x84ED,

    GL_COMPRESSED_RGBA = 0x84EE,

    GL_DEPTH_STENCIL = 0x84F9,

    GL_RGBA32F = 0x8814,

    GL_RGB32F = 0x8815,

    GL_RGBA16F = 0x881A,

    GL_RGB16F = 0x881B,

    GL_DEPTH24_STENCIL8 = 0x88F0,

    GL_R11F_G11F_B10F = 0x8C3A,

    GL_RGB9_E5 = 0x8C3D,

    GL_SRGB = 0x8C40,

    GL_SRGB8 = 0x8C41,

    GL_SRGB_ALPHA = 0x8C42,

    GL_SRGB8_ALPHA8 = 0x8C43,

    GL_SLUMINANCE_ALPHA = 0x8C44,

    GL_SLUMINANCE8_ALPHA8 = 0x8C45,

    GL_SLUMINANCE = 0x8C46,

    GL_SLUMINANCE8 = 0x8C47,

    GL_COMPRESSED_SRGB = 0x8C48,

    GL_COMPRESSED_SRGB_ALPHA = 0x8C49,

    GL_COMPRESSED_SLUMINANCE = 0x8C4A,

    GL_COMPRESSED_SLUMINANCE_ALPHA = 0x8C4B,

    GL_COMPRESSED_SRGB_S3TC_DXT1_EXT = 0x8C4C,

    GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT = 0x8C4D,

    GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT = 0x8C4E,

    GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT = 0x8C4F,

    GL_DEPTH_COMPONENT32F = 0x8CAC,

    GL_DEPTH32F_STENCIL8 = 0x8CAD,

    GL_RGBA32UI = 0x8D70,

    GL_RGB32UI = 0x8D71,

    GL_RGBA16UI = 0x8D76,

    GL_RGB16UI = 0x8D77,

    GL_RGBA8UI = 0x8D7C,

    GL_RGB8UI = 0x8D7D,

    GL_RGBA32I = 0x8D82,

    GL_RGB32I = 0x8D83,

    GL_RGBA16I = 0x8D88,

    GL_RGB16I = 0x8D89,

    GL_RGBA8I = 0x8D8E,

    GL_RGB8I = 0x8D8F,

    GL_FLOAT_32_UNSIGNED_INT_24_8_REV = 0x8DAD,

    GL_COMPRESSED_RED_RGTC1 = 0x8DBB,

    GL_COMPRESSED_SIGNED_RED_RGTC1 = 0x8DBC,

    GL_COMPRESSED_RG_RGTC2 = 0x8DBD,

    GL_COMPRESSED_SIGNED_RG_RGTC2 = 0x8DBE,

    GL_COMPRESSED_RGBA_BPTC_UNORM = 0x8E8C,

    GL_COMPRESSED_RGB_BPTC_SIGNED_FLOAT = 0x8E8E,

    GL_COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT = 0x8E8F,

    GL_R8_SNORM = 0x8F94,

    GL_RG8_SNORM = 0x8F95,

    GL_RGB8_SNORM = 0x8F96,

    GL_RGBA8_SNORM = 0x8F97,

    GL_R16_SNORM = 0x8F98,

    GL_RG16_SNORM = 0x8F99,

    GL_RGB16_SNORM = 0x8F9A,

    GL_RGBA16_SNORM = 0x8F9B,

    GL_RGB10_A2UI = 0x906F,

    GL_ONE = 1,

    GL_TWO = 2,

    GL_THREE = 3,

    GL_FOUR = 4,
}

public enum TEXTURE_UNIT
{
    GL_TEXTURE0 = 33984,

    GL_TEXTURE1 = 33985,

    GL_TEXTURE2 = 33986,

    GL_TEXTURE3 = 33987,

    GL_TEXTURE4 = 33988,

    GL_TEXTURE5 = 33989,

    GL_TEXTURE6 = 33990,

    GL_TEXTURE7 = 33991,

    GL_TEXTURE8 = 33992,

    GL_TEXTURE9 = 33993,

    GL_TEXTURE10 = 33994,

    GL_TEXTURE11 = 33995,

    GL_TEXTURE12 = 33996,

    GL_TEXTURE13 = 33997,

    GL_TEXTURE14 = 33998,

    GL_TEXTURE15 = 33999,

    GL_TEXTURE16 = 34000,

    GL_TEXTURE17 = 34001,

    GL_TEXTURE18 = 34002,

    GL_TEXTURE19 = 34003,

    GL_TEXTURE20 = 34004,

    GL_TEXTURE21 = 34005,

    GL_TEXTURE22 = 34006,

    GL_TEXTURE23 = 34007,

    GL_TEXTURE24 = 34008,

    GL_TEXTURE25 = 34009,

    GL_TEXTURE26 = 34010,

    GL_TEXTURE27 = 34011,

    GL_TEXTURE28 = 34012,

    GL_TEXTURE29 = 34013,

    GL_TEXTURE30 = 34014,

    GL_TEXTURE31 = 34015,
}

public enum USAGE_PATTERN : int
{
    GL_STREAM_DRAW = 0x88E0,
    GL_STREAM_READ = 0x88E1,
    GL_STREAM_COPY = 0x88E2,
    GL_STATIC_DRAW = 0x88E4,
    GL_STATIC_READ = 0x88E5,
    GL_STATIC_COPY = 0x88E6,
    GL_DYNAMIC_DRAW = 0x88E8,
    GL_DYNAMIC_READ = 0x88E9,
    GL_DYNAMIC_COPY = 0x88EA,
}

public enum VERTEX_DATA_TYPE : int
{
    GL_BYTE = 0x1400,

    GL_UNSIGNED_BYTE = 0x1401,

    GL_SHORT = 0x1402,

    GL_UNSIGNED_SHORT = 0x1403,

    GL_INT = 0x1404,

    GL_UNSIGNED_INT = 0x1405,

    GL_HALF_FLOAT = 0x140B,

    GL_FLOAT = 0x1406,

    GL_FIXED = 0x140C,

    GL_INT_2_10_10_10_REV = 0x8D9F,

    GL_UNSIGNED_INT_2_10_10_10_REV = 0x8368,

    GL_UNSIGNED_INT_10F_11F_11F_REV = 0x8C3B,

    GL_DOUBLE = 0x140A,
}
#endregion

/// <summary>
/// Access to OpenGL capabilities via the Opengl32.dll library.
/// </summary>
[SuppressUnmanagedCodeSecurity]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "OpenGL functions have dumb naming conventions but I'm keeping these APIs pure.")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible", Justification = "This is a P/Invoke library...")]
public static partial class OpenGL32
{
    private const string Library = "Opengl32.dll";

    /// <summary>
    /// Specify the alpha test function. Alpha testing is performed only in RGBA mode.
    /// </summary>
    /// <param name="func">Specifies the alpha comparison function. Symbolic constants GL_NEVER, GL_LESS, GL_EQUAL, GL_LEQUAL, GL_GREATER, GL_NOTEQUAL, GL_GEQUAL, and GL_ALWAYS are accepted. The initial value is GL_ALWAYS.</param>
    /// <param name="ref">Specifies the reference value that incoming alpha values are compared to. This value is clamped to the range 0 1 , where 0 represents the lowest possible alpha value and 1 the highest possible value. The initial reference value is 0.</param>
    [DllImport(Library)]
    public static extern void glAlphaFunc(ALPHA_COMPARISON_FUNCTION func, float @ref);

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
    public static extern void glBindTexture(TEXTURE_TARGET target, int texture);

    /// <summary>
    /// Specify pixel arithmetic
    /// </summary>
    /// <param name="sfactor">Specifies how the red, green, blue, and alpha source blending factors are computed. The initial value is GL_ONE.</param>
    /// <param name="dfactor">Specifies how the red, green, blue, and alpha destination blending factors are computed. The following symbolic constants are accepted: GL_ZERO, GL_ONE, GL_SRC_COLOR, GL_ONE_MINUS_SRC_COLOR, GL_DST_COLOR, GL_ONE_MINUS_DST_COLOR, GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA, GL_DST_ALPHA, GL_ONE_MINUS_DST_ALPHA. GL_CONSTANT_COLOR, GL_ONE_MINUS_CONSTANT_COLOR, GL_CONSTANT_ALPHA, and GL_ONE_MINUS_CONSTANT_ALPHA. The initial value is GL_ZERO.</param>
    [DllImport(Library)]
    public static extern void glBlendFunc(BLEND_SCALE_FACTOR sfactor, BLEND_SCALE_FACTOR dfactor);

    /// <summary>
    /// Clear buffers to preset values
    /// </summary>
    /// <param name="mask">Bitwise OR of masks that indicate the buffers to be cleared. The three masks are GL_COLOR_BUFFER_BIT, GL_DEPTH_BUFFER_BIT, and GL_STENCIL_BUFFER_BIT.</param>
    [DllImport(Library)]
    public static extern void glClear(BUFFER_MASK mask);

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
    public static extern void glDeleteTextures(int n, int[] textures);

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
    public static extern void glDisableClientState(STATE_CAP array);

    /// <summary>
    /// Render primitives from array data.
    /// </summary>
    /// <param name="mode">Specifies what kind of primitives to render.</param>
    /// <param name="first">Specifies the starting index in the enabled arrays.</param>
    /// <param name="count">Specifies the number of indices to be rendered.</param>
    [DllImport(Library)]
    public static extern void glDrawArrays(PRIMITIVE_TYPE mode, int first, int count);

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
    public static extern void glEnableClientState(STATE_CAP array);

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
    public static extern void glFrontFace(FRONTFACE_MODE mode);

    /// <summary>
    /// The glGetBooleanv function returns the value or values of a selected parameter.
    /// </summary>
    /// <param name="pname">The parameter value to be returned.</param>
    /// <param name="data">Returns the value or values of the specified parameter.</param>
    [DllImport(Library)]
    public static extern void glGetBooleanv(GLCAP pname, [Out] out bool[] data);

    /// <summary>
    /// Return error information. Each detectable error is assigned a numeric code and symbolic name. When an error occurs, the error flag is set to the appropriate error code value. No other errors are recorded until glGetError is called, the error code is returned, and the flag is reset to GL_NO_ERROR. If a call to glGetError returns GL_NO_ERROR, there has been no detectable error since the last call to glGetError, or since the GL was initialized.
    /// </summary>
    /// <returns>Returns the value of the error flag. </returns>
    [DllImport(Library)]
    public static extern GL_ERROR glGetError();

    /// <summary>
    /// return the value or values of a selected parameter
    /// </summary>
    /// <param name="pname">Specifies the parameter value to be returned for non-indexed versions of glGet.</param>
    /// <param name="result">Returns the value or values of the specified parameter.</param>
    [DllImport(Library)]
    public static extern void glGetIntegerv(GLCAP pname, out int result);

    /// <summary>
    /// Return the value or values of a selected parameter
    /// </summary>
    /// <param name="pname">Specifies the parameter value to be returned for non-indexed versions of glGet</param>
    /// <param name="result">Returns the values of the specified parameter.</param>
    [DllImport(Library)]
    public static extern void glGetIntegerv(GLCAP pname, int[] result);

    /// <summary>
    /// Returns a pointer to a static string describing some aspect of the current GL connection.
    /// </summary>
    /// <param name="name">Specifies a symbolic constant</param>
    /// <returns>Returns a pointer to a static string describing some aspect of the current GL connection</returns>
    [DllImport(Library, CharSet = CharSet.Unicode)]
    public unsafe static extern sbyte* glGetString(GETSTRING_NAME name);

    /// <summary>
    /// return a texture image
    /// </summary>
    /// <param name="target">Specifies the target to which the texture is bound for glGetTexImage and glGetnTexImage functions.</param>
    /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
    /// <param name="format">Specifies a pixel format for the returned data.</param>
    /// <param name="type">Specifies a pixel type for the returned data.</param>
    /// <param name="pixels">Returns the texture image. Should be a pointer to an array of the type specified by type.</param>
    [DllImport(Library)]
    public static extern void glGetTexImage(TEXTURE_TARGET target, int level, PIXEL_FORMAT format, PIXEL_TYPE type, IntPtr pixels);

    /// <summary>
    /// returns n texture names in textures. There is no guarantee that the names form a contiguous set of integers; however, it is guaranteed that none of the returned names was in use immediately before the call to glGenTextures. The generated textures have no dimensionality; they assume the dimensionality of the texture target to which they are first bound(see glBindTexture). Texture names returned by a call to glGenTextures are not returned by subsequent calls, unless they are first deleted with glDeleteTextures.
    /// </summary>
    /// <param name="n">Specifies the number of texture names to be generated.</param>
    /// <param name="textures">Specifies an array in which the generated texture names are stored.</param>
    [DllImport(Library)]
    public static extern void glGenTextures(int n, int[] textures);

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
    public static extern void glLightfv(LIGHT light, LIGHTSOURCE_NAME pname, float[] @params);

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
    public static extern void glMaterialfv(POLYGON_FACE face, MATERIAL_NAME pname, float[] @params);

    /// <summary>
    /// Specifies which matrix stack is the target for subsequent matrix operations. The initial value is GL_MODELVIEW.
    /// </summary>
    /// <param name="mode">Specifies which matrix stack is the target for subsequent matrix operations.</param>
    [DllImport(Library)]
    public static extern void glMatrixMode(MATRIX_MODE mode);

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
    public static extern void glReadPixels(int x, int y, int width, int height, PIXEL_FORMAT format, PIXEL_TYPE type, [Out] IntPtr data);

    /// <summary>
    /// glPixelStorei sets pixel storage modes that affect the operation of subsequent glReadPixels as well as the unpacking of texture patterns (see glTexImage2D and glTexSubImage2D).
    /// </summary>
    /// <param name="pname">a symbolic constant indicating the parameter to be set</param>
    /// <param name="param">the new value</param>
    [DllImport(Library)]
    public static extern void glPixelStorei(PIXEL_STORE_MODE pname, int param);

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
    public static extern void glPolygonMode(POLYGON_FACE face, POLYGON_MODE mode);

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
    public static extern void glShadeModel(SHADE_TECHNIQUE mode);

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
    public static extern void glTexImage1D(TEXTURE_TARGET target, int level, TEXTURE_INTERNALFORMAT internalFormat, int width, int border, PIXEL_FORMAT format, PIXEL_TYPE type, IntPtr data);

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
    public static extern void glTexImage2D(TEXTURE_TARGET target, int level, TEXTURE_INTERNALFORMAT internalFormat, int width, int height, int border, PIXEL_FORMAT format, PIXEL_TYPE type, IntPtr data);

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
    public static extern void glTexSubImage2D(TEXTURE_TARGET target, int level, int xOffset, int yOffset, int width, int height, PIXEL_FORMAT format, PIXEL_TYPE type, IntPtr pixels);

    /// <summary>
    /// set texture parameters
    /// </summary>
    /// <param name="target">Specifies the target to which the texture is bound for glTexParameter functions.</param>
    /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter.</param>
    /// <param name="param">Specifies the value of pname.</param>
    [DllImport(Library)]
    public static extern void glTexParameteri(TEXTURE_TARGET target, TEXPARAMETER_NAME pname, TEXPARAMETER_VALUE param);

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
    public static extern void glVertexPointer(int size, VERTEX_DATA_TYPE type, int stride, float[] pointer);

    /// <summary>
    /// Specifies the affine transformation of x and y from normalized device coordinates to window coordinates.
    /// </summary>
    /// <param name="x">Specify the lower left corner of the viewport rectangle, in pixels. The initial value is (0,0).</param>
    /// <param name="y">Specify the lower left corner of the viewport rectangle, in pixels. The initial value is (0,0).</param>
    /// <param name="width">Specify the width and height of the viewport. When a GL context is first attached to a window, width and height are set to the dimensions of that window.</param>
    /// <param name="height">Specify the width and height of the viewport. When a GL context is first attached to a window, width and height are set to the dimensions of that window.</param>
    [DllImport(Library)]
    public static extern void glViewport(int x, int y, int width, int height);

    /// <summary>
    /// The wglCreateContext function creates a new OpenGL rendering context, which is suitable for drawing on the device referenced by hdc. The rendering context has the same pixel format as the device context.
    /// </summary>
    /// <param name="unnamedParam1">Handle to a device context for which the function creates a suitable OpenGL rendering context.</param>
    /// <returns>If the function succeeds, the return value is a valid handle to an OpenGL rendering context. If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr wglCreateContext(IntPtr unnamedParam1);

    /// <summary>
    /// The wglDeleteContext function deletes a specified OpenGL rendering context.
    /// </summary>
    /// <param name="unnamedParam1">Handle to an OpenGL rendering context that the function will delete.</param>
    /// <returns>If the function succeeds, the return value is TRUE. If the function fails, the return value is FALSE.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool wglDeleteContext(IntPtr unnamedParam1);

    /// <summary>
    /// The wglGetCurrentContext function obtains a handle to the current OpenGL rendering context of the calling thread.
    /// </summary>
    /// <returns>If the calling thread has a current OpenGL rendering context, wglGetCurrentContext returns a handle to that rendering context. Otherwise, the return value is NULL.</returns>
    [DllImport(Library)]
    public static extern IntPtr wglGetCurrentContext();

    /// <summary>
    /// The wglGetProcAddress function returns the address of an OpenGL extension function for use with the current OpenGL rendering context.
    /// </summary>
    /// <param name="unnamedParam1">Points to a null-terminated string that is the name of the extension function. The name of the extension function must be identical to a corresponding function implemented by OpenGL.</param>
    /// <returns>When the function succeeds, the return value is the address of the extension function. When no current rendering context exists or the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr wglGetProcAddress(string unnamedParam1);

    /// <summary>
    /// The wglMakeCurrent function makes a specified OpenGL rendering context the calling thread's current rendering context. All subsequent OpenGL calls made by the thread are drawn on the device identified by hdc. You can also use wglMakeCurrent to change the calling thread's current rendering context so it's no longer current.
    /// </summary>
    /// <param name="unnamedParam1">Handle to a device context. Subsequent OpenGL calls made by the calling thread are drawn on the device identified by hdc.</param>
    /// <param name="unnamedParam2">Handle to an OpenGL rendering context that the function sets as the calling thread's rendering context. If hglrc is NULL, the function makes the calling thread's current rendering context no longer current, and releases the device context that is used by the rendering context. In this case, hdc is ignored.</param>
    /// <returns>When the wglMakeCurrent function succeeds, the return value is TRUE; otherwise the return value is FALSE. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool wglMakeCurrent(IntPtr unnamedParam1, IntPtr unnamedParam2);

    /// <summary>
    /// The wglShareLists function enables multiple OpenGL rendering contexts to share a single display-list space.
    /// </summary>
    /// <param name="unnamedParam1">Specifies the OpenGL rendering context with which to share display lists.</param>
    /// <param name="unnamedParam2">Specifies the OpenGL rendering context to share display lists with hglrc1. The hglrc2 parameter should not contain any existing display lists when wglShareLists is called.</param>
    /// <returns>When the function succeeds, the return value is TRUE. When the function fails, the return value is FALSE and the display lists are not shared.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool wglShareLists(IntPtr unnamedParam1, IntPtr unnamedParam2);
}
