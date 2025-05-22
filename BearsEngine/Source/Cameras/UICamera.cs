using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using BearsEngine.Graphics.Shaders;
using BearsEngine.Input;
using BearsEngine.OpenGL;

namespace BearsEngine.Worlds.Cameras;

public class UICamera : EntityBase //todo: disposable - remove resize event handlers?
{
    private int _frameBufferMSAAID;
    private Texture _frameBufferMSAATexture;
    private int _frameBufferShaderPassID;
    private Texture _frameBufferShaderPassTexture;
    private readonly CameraMSAAShader _mSAAShader;
    private readonly IMouse _mouse;
    private Matrix3 _ortho;

    public UICamera(float layer, Rect position, MSAA_SAMPLES samples)
        : base(layer, position)
    {
        _mouse = Mouse.Instance;

        Shader = new DefaultShader();

        MSAASamples = samples;

        _mSAAShader = new CameraMSAAShader() { Samples = samples };

        VertexBuffer = OpenGLHelper.GenBuffer();

        var _colour = Colour.White;
        Vertices = new Vertex[4]
        {
            new Vertex(0, 0, _colour, 0f, 0f),
            new Vertex(W, 0, _colour, 1f,  0f),
            new Vertex(0, H, _colour, 0f,  1f),
            new Vertex(W, H, _colour, 1f,  1f)
        };

        OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, VertexBuffer);
        OpenGLHelper.BufferData(BUFFER_TARGET.GL_ARRAY_BUFFER, Vertices.Length * Vertex.STRIDE, Vertices, USAGE_PATTERN.GL_DYNAMIC_DRAW);
        OpenGLHelper.LastBoundVertexBuffer = VertexBuffer;

        SetUpFBOTex((int)W, (int)H);

        _ortho = Matrix3.CreateFBOOrtho(W, H);
    }

    protected bool MSAAEnabled { get => MSAASamples != MSAA_SAMPLES.Disabled; }

    protected int VertexBuffer { get; private set; }

    protected Vertex[] Vertices { get; set; }

    /// <summary>
    /// Angle in Degrees
    /// </summary>
    public float Angle { get; set; }

    public Colour BackgroundColour { get; set; } = Colour.White;

    public override Point LocalMousePosition => GetLocalPosition(_mouse.ClientPosition);

    public bool MouseIntersecting => R.Contains(LocalMousePosition);

    public MSAA_SAMPLES MSAASamples { get; set; } = MSAA_SAMPLES.Disabled; //todo: trigger resize if this changes?

    public IShader Shader { get; set; }

    [MemberNotNull(nameof(_frameBufferMSAAID), nameof(_frameBufferMSAATexture), nameof(_frameBufferShaderPassID), nameof(_frameBufferShaderPassTexture))]
    /// <summary>
    /// Initialise the textures that the Frame Buffet Object render target the camera draws to uses and the intermediate Multisample texture used in addition if MultiSample AntiAliasing is enabled.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    private void SetUpFBOTex(int width, int height)
    {
        if (width <= 0 || height <= 0)
            throw new Exception("fbo tex received 0/0 size");

        if (MSAASamples != MSAA_SAMPLES.Disabled)
        {
            OpenGLHelper.CreateMSAAFramebuffer(width, height, MSAASamples, out _frameBufferMSAAID, out _frameBufferMSAATexture);
        }

        //Generate FBO and texture to use for the final pass, where the camera's shader will be applied to the result of the MSAA pass
        _frameBufferShaderPassTexture = new Texture(OpenGLHelper.GenTexture(), width, height);

        OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, _frameBufferShaderPassTexture.ID);
        OpenGL32.glTexStorage2D(TEXTURE_TARGET.GL_TEXTURE_2D, 1, TEXTURE_INTERNALFORMAT.GL_RGBA8, width, height);

        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_S, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_T, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);

        OpenGLHelper.UnbindTexture();

        _frameBufferShaderPassID = OpenGLHelper.GenFramebuffer();

        //Check for OpenGL errors
        var err = OpenGL32.glGetError();
        if (err != GL_ERROR.GL_NO_ERROR)
            Log.Warning($"OpenGL error! (Camera.SetUpFBOTex) {err}");
    }

    public override Point GetLocalPosition(Point windowCoords)
    {
        var p1 = Parent.GetLocalPosition(windowCoords);
        var p2 = new Point(p1.X - X, p1.Y - Y);

        return p2;
    }

    public override Rect GetLocalPosition(Rect windowCoords)
    {
        Point tl = GetLocalPosition(windowCoords.TopLeft);
        Point br = GetLocalPosition(windowCoords.BottomRight);
        return new Rect(tl, br.X - tl.X, br.Y - tl.Y);
    }

    public override Point GetWindowPosition(Point localCoords)
    {
        return Parent.GetWindowPosition(new Point(X + localCoords.X, Y + localCoords.Y));
    }

    public override Rect GetWindowPosition(Rect localCoords)
    {
        Point tl = GetWindowPosition(localCoords.TopLeft);
        Point br = GetWindowPosition(localCoords.BottomRight);
        return new Rect(tl, br.X - tl.X, br.Y - tl.Y);
    }

    public override void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        //Bind vertex buffer - optimise this later            
        OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, VertexBuffer);
        OpenGLHelper.BufferData(BUFFER_TARGET.GL_ARRAY_BUFFER, Vertices.Length * Vertex.STRIDE, Vertices, USAGE_PATTERN.GL_DYNAMIC_DRAW);
        OpenGLHelper.LastBoundVertexBuffer = VertexBuffer;

        if (MSAAEnabled)
        {
            //Bind MSAA FBO to be the draw destination and clear it
            OpenGL32.glBindFramebuffer(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, _frameBufferMSAAID);
            OpenGL32.glFramebufferTexture2D(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, FRAMEBUFFER_ATTACHMENT_POINT.GL_COLOR_ATTACHMENT0, TEXTURE_TARGET.GL_TEXTURE_2D_MULTISAMPLE, _frameBufferMSAATexture.ID, 0);
        }
        else
        {
            //Bind Shader Pass FBO to be the draw destination and clear it
            OpenGL32.glBindFramebuffer(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, _frameBufferShaderPassID);
            OpenGL32.glFramebufferTexture2D(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, FRAMEBUFFER_ATTACHMENT_POINT.GL_COLOR_ATTACHMENT0, TEXTURE_TARGET.GL_TEXTURE_2D, _frameBufferShaderPassTexture.ID, 0);
        }

        //Clear the FBO
        OpenGL32.glClearColor(BackgroundColour.R / 255f, BackgroundColour.G / 255f, BackgroundColour.B / 255f, BackgroundColour.A / 255f);
        OpenGL32.glClear(BUFFER_MASK.GL_COLOR_BUFFER_BIT);

        //Set normal blend function for within the layers
        OpenGL32.glBlendFunc(BLEND_SCALE_FACTOR.GL_ONE, BLEND_SCALE_FACTOR.GL_ONE_MINUS_SRC_ALPHA);

        //Save the previous viewport and set the viewport to match the size of the texture we are now drawing to - the FBO
        Rect prevVP = OpenGLHelper.GetViewport();
        OpenGL32.glViewport(0, 0, (int)base.W, (int)base.H);

        //Locally save the current render target, we will then set this camera as the current render target for child cameras, then put it back
        int tempFBID = OpenGLHelper.LastBoundFrameBuffer;
        OpenGLHelper.LastBoundFrameBuffer = MSAAEnabled ? _frameBufferMSAAID : _frameBufferShaderPassID;

        Matrix3 identity = Matrix3.Identity;

        //draw stuff here 

        base.Render(ref _ortho, ref identity);

        OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, VertexBuffer);//this MUST be called after base render

        //Revert the render target 
        OpenGLHelper.LastBoundFrameBuffer = tempFBID;

        if (MSAAEnabled)
        {
            //Bind the 2nd pass FBO and draw from the first to do MSAA sampling
            OpenGL32.glBindFramebuffer(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, _frameBufferShaderPassID);
            OpenGL32.glFramebufferTexture2D(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, FRAMEBUFFER_ATTACHMENT_POINT.GL_COLOR_ATTACHMENT0, TEXTURE_TARGET.GL_TEXTURE_2D, _frameBufferShaderPassTexture.ID, 0);
            OpenGL32.glClearColor(0, 0, 0, 0);
            OpenGL32.glClear(BUFFER_MASK.GL_COLOR_BUFFER_BIT);

            //Bind the FBO to be drawn

            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D_MULTISAMPLE, _frameBufferMSAATexture.ID);

            //Do the MSAA render pass, drawing to the MSAATexture FBO
            _mSAAShader.Render(ref _ortho, ref identity, Vertices.Length, PRIMITIVE_TYPE.GL_TRIANGLE_STRIP);

            //Unbind the FBO 
            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D_MULTISAMPLE, 0);
        }

        //Bind the render target back to either the screen, or a camera higher up the heirachy, depending on what called this
        OpenGL32.glBindFramebuffer(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, OpenGLHelper.LastBoundFrameBuffer);

        //Bind the FBO to be drawn
        OpenGLHelper.BindTexture(_frameBufferShaderPassTexture);

        //Set some other blend fucntion when render the FBO texture which apparantly lets the layer alpha blend with the one beneath?
        OpenGL32.glBlendFunc(BLEND_SCALE_FACTOR.GL_ONE, BLEND_SCALE_FACTOR.GL_ONE_MINUS_SRC_ALPHA);

        //reset viewport
        OpenGLHelper.Viewport(prevVP);

        Matrix3 mv = modelView;
        if (Angle != 0)
            mv = Matrix3.RotateAroundPoint(ref mv, Angle, R.Centre.X, R.Centre.Y);
        mv = Matrix3.Translate(ref mv, X, Y);

        //Render with assigned shader
        Shader.Render(ref projection, ref mv, Vertices.Length, PRIMITIVE_TYPE.GL_TRIANGLE_STRIP);

        OpenGLHelper.UnbindTexture();
    }

    public void Resize(Point newSize) => Resize(newSize.X, newSize.Y);

    public void Resize(float newW, float newH)
    {
        W = newW;
        H = newH;

        var colour = Colour.White;

        Vertices =
        [
            new Vertex(0, 0, colour, 0f, 0f),
            new Vertex(W, 0, colour, 1f,  0f),
            new Vertex(0, H, colour, 0f,  1f),
            new Vertex(W, H, colour, 1f,  1f)
        ];

        SetUpFBOTex((int)W, (int)H);

        _ortho = Matrix3.CreateFBOOrtho(W, H);
    }
}
