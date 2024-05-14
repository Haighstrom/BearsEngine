using BearsEngine.Graphics.Shaders;
using BearsEngine.Input;
using BearsEngine.OpenGL;

namespace BearsEngine.Worlds.Cameras;

public class Camera : EntityBase, ICamera //todo: disposable - remove resize event handlers?
{
    private int _frameBufferMSAAID;
    private Texture _frameBufferMSAATexture;
    private int _frameBufferShaderPassID;
    private Texture _frameBufferShaderPassTexture;
    private readonly CameraMSAAShader _mSAAShader;
    private readonly IMouse _mouse;
    private Matrix3 _ortho;
    private float _tileWidth, _tileHeight;
    private Rect _view = new();

    private Camera(float layer, Rect position)
        : base(layer, position)
    {
        _mouse = Mouse.Instance;

        Shader = new DefaultShader();

        _mSAAShader = new CameraMSAAShader() { Samples = MSAASamples };

        VertexBuffer = OpenGLHelper.GenBuffer();

        var _colour = Colour.White;
        Vertices = new Vertex[4]
        {
            new Vertex(0, 0, _colour, 0f, 0f),
            new Vertex(W, 0, _colour, 1f,  0f),
            new Vertex(0, H, _colour, 0f,  1f),
            new Vertex(W, H, _colour, 1f,  1f)
        };

        //if (MSAASamples != MSAA_Samples.Disabled)
        //{
        //    HF.Graphics.CreateMSAAFramebuffer(W, H, MSAASamples, out _frameBufferMSAAID, out _frameBufferMSAATexture);
        //    _MSAAGraphic = new Image(_frameBufferMSAATexture)
        //    {
        //        Shader = new CameraMSAAShader(),
        //    };
        //}
        SetUpFBOTex((int)W, (int)H);

        //HF.Graphics.CreateFramebuffer(W, H, out _frameBufferShaderPassID, out _frameBufferShaderPassTexture);
        //_graphic = new Image(_frameBufferShaderPassTexture);
        _ortho = Matrix3.CreateFBOOrtho(W, H);

        //_frameBufferShaderPassID = OpenGL.GenFramebuffer();
    }

    public Camera(float layer, Rect position, Point tileSize)
        : this(layer, position, tileSize.X, tileSize.Y)
    {
    }

    public Camera(float layer, Rect position, float tileW, float tileH)
        : this(layer, position)
    {
        FixedTileSize = true;

        //View set automatically
        TileWidth = tileW;
        TileHeight = tileH;

    }

    public Camera(float layer, Rect position, Rect viewport)
        : this(layer, position)
    {
        FixedTileSize = false;

        //TileWidth/TileHeight set automatically
        View = new Rect(viewport); //don't keep reference to original viewport
    }

    protected bool MSAAEnabled { get => MSAASamples != MSAA_SAMPLES.Disabled; }

    protected int VertexBuffer { get; private set; }

    protected Vertex[] Vertices { get; set; }

    /// <summary>
    /// Angle in Degrees
    /// </summary>
    public float Angle { get; set; }

    public Colour BackgroundColour { get; set; } = Colour.White;

    public bool FixedTileSize { get; set; }

    public override Point LocalMousePosition => GetLocalPosition(_mouse.ClientPosition);

    public float MaxX { get; set; }

    public float MaxY { get; set; }

    public float MinX { get; set; }

    public float MinY { get; set; }

    public bool MouseIntersecting => View.Contains(LocalMousePosition);

    public MSAA_SAMPLES MSAASamples { get; set; } = MSAA_SAMPLES.Disabled; //todo: trigger resize if this changes?

    public IShader Shader { get; set; }

    public float TileHeight
    {
        get => _tileHeight;
        set
        {
            _tileHeight = value;

            if (FixedTileSize)
                View.H = H / TileHeight;
        }
    }

    public float TileWidth
    {
        get => _tileWidth;
        set
        {
            _tileWidth = value;

            if (FixedTileSize)
                View.W = W / value;
        }
    }

    public virtual Rect View
    {
        get => _view;
        set
        {
            if (!FixedTileSize)
            {
                TileWidth = W / value.W;
                TileHeight = H / value.H;
            }

            _view = value;

            ViewChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler? ViewChanged;

    /// <summary>
    /// Initialise the textures that the Frame Buffet Object render target the camera draws to uses and the intermediate Multisample texture used in addition if MultiSample AntiAliasing is enabled.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    private void SetUpFBOTex(int width, int height)
    {
        if (width <= 0 || height <= 0)
            return;

        if (MSAAEnabled)
        {
            //Generate FBO and texture to use with the MSAA antialising pass
            _frameBufferMSAATexture = new Texture(OpenGLHelper.GenTexture(), width, height);

            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_PROXY_TEXTURE_2D_MULTISAMPLE, _frameBufferMSAATexture.ID);
            OpenGL32.glTexImage2DMultisample(TEXTURE_TARGET.GL_TEXTURE_2D_MULTISAMPLE, (int)MSAASamples, TEXTURE_INTERNALFORMAT.GL_RGB8, width, height, false);

            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_S, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_T, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);

            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_PROXY_TEXTURE_2D_MULTISAMPLE, 0);

            _frameBufferMSAAID = OpenGLHelper.GenFramebuffer();
        }

        //Generate FBO and texture to use for the final pass, where the camera's shader will be applied to the result of the MSAA pass
        _frameBufferShaderPassTexture = new Texture(OpenGLHelper.GenTexture(), width, height);

        OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, _frameBufferShaderPassTexture.ID);
        OpenGL32.glTexStorage2D(TEXTURE_TARGET.GL_TEXTURE_2D, 1, TEXTURE_INTERNALFORMAT.GL_RGBA8, width, height);

        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_S, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_T, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);

        OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, 0);
        OpenGLHelper.LastBoundTexture = 0;

        _frameBufferShaderPassID = OpenGLHelper.GenFramebuffer();

        //Check for OpenGL errors
        var err = OpenGL32.glGetError();
        if (err != GL_ERROR.GL_NO_ERROR)
            Log.Warning($"OpenGL error! (Camera.Render) {err}");
    }

    public override Point GetLocalPosition(Point windowCoords)
    {
        var p1 = Parent.GetLocalPosition(windowCoords);
        var p2 = new Point(
            (p1.X - X) / (W / View.W) + View.X,
            (p1.Y - Y) / (H / View.H) + View.Y
            );

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
        return Parent.GetWindowPosition(new Point(
            X + W / View.W * (localCoords.X - View.X),
            Y + H / View.H * (localCoords.Y - View.Y)
            ));
    }

    public override Rect GetWindowPosition(Rect localCoords)
    {
        Point tl = GetWindowPosition(localCoords.TopLeft);
        Point br = GetWindowPosition(localCoords.BottomRight);
        return new Rect(tl, br.X - tl.X, br.Y - tl.Y);
    }

    public virtual bool IsInBounds(Point p) => IsInBounds(p.X, p.Y);

    public virtual bool IsInBounds(float x, float y) => x >= MinX && x < MaxX && y >= MinY && y < MaxY;

    public virtual bool IsOnEdge(Point p) => IsOnEdge(p.X, p.Y);

    public virtual bool IsOnEdge(float x, float y) => x == MinX || y == MinY || x == MaxX - 1 || y == MaxY - 1;

    public bool MouseIsNearEdge(Direction edgeDirection, int maxDistance) => edgeDirection switch
    {
        Direction.Up => LocalMousePosition.Y >= 0 && LocalMousePosition.Y < View.Y + maxDistance,
        Direction.Right => LocalMousePosition.X > View.Right - maxDistance && LocalMousePosition.X <= View.Right,
        Direction.Down => LocalMousePosition.Y > View.Bottom - maxDistance && LocalMousePosition.Y <= View.Bottom,
        Direction.Left => LocalMousePosition.X >= 0 && LocalMousePosition.X < View.X + maxDistance,
        _ => throw new ArgumentException(string.Format("edgeDirection value not handled: {0}", edgeDirection), nameof(edgeDirection)),
    };

    public override void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        if (MSAAEnabled)
        {
            //Bind MSAA FBO to be the draw destination and clear it
            OpenGL32.glBindFramebuffer(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, _frameBufferMSAAID);
            OpenGL32.glFramebufferTexture2D(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, FRAMEBUFFER_ATTACHMENT_POINT.GL_COLOR_ATTACHMENT0, TEXTURE_TARGET.GL_PROXY_TEXTURE_2D_MULTISAMPLE, _frameBufferMSAATexture.ID, 0);
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
        Matrix3 MV = Matrix3.ScaleAroundOrigin(ref identity, TileWidth, TileHeight);
        MV = Matrix3.Translate(ref MV, -View.X, -View.Y);

        //draw stuff here 
        base.Render(ref _ortho, ref MV);

        //Revert the render target 
        OpenGLHelper.LastBoundFrameBuffer = tempFBID;

        //Bind vertex buffer - optimise this later            
        OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, VertexBuffer);
        OpenGLHelper.BufferData(BUFFER_TARGET.GL_ARRAY_BUFFER, Vertices.Length * Vertex.STRIDE, Vertices, USAGE_PATTERN.GL_DYNAMIC_DRAW);
        OpenGLHelper.LastBoundVertexBuffer = VertexBuffer;

        if (MSAAEnabled)
        {
            //Bind the 2nd pass FBO and draw from the first to do MSAA sampling
            OpenGL32.glBindFramebuffer(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, _frameBufferShaderPassID);
            OpenGL32.glFramebufferTexture2D(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, FRAMEBUFFER_ATTACHMENT_POINT.GL_COLOR_ATTACHMENT0, TEXTURE_TARGET.GL_TEXTURE_2D, _frameBufferShaderPassTexture.ID, 0);
            OpenGL32.glClearColor(0, 0, 0, 0);
            OpenGL32.glClear(BUFFER_MASK.GL_COLOR_BUFFER_BIT);

            //Bind the FBO to be drawn

            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_PROXY_TEXTURE_2D_MULTISAMPLE, _frameBufferMSAATexture.ID);

            //Do the MSAA render pass, drawing to the MSAATexture FBO
            _mSAAShader.Render(ref _ortho, ref identity, Vertices.Length, PRIMITIVE_TYPE.GL_TRIANGLE_STRIP);

            //Unbind the FBO 
            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_PROXY_TEXTURE_2D_MULTISAMPLE, 0);
        }

        //Bind the render target back to either the screen, or a camera higher up the heirachy, depending on what called this
        OpenGL32.glBindFramebuffer(FRAMEBUFFER_TARGET.GL_FRAMEBUFFER, OpenGLHelper.LastBoundFrameBuffer);

        //Bind the FBO to be drawn
        if (OpenGLHelper.LastBoundTexture != _frameBufferShaderPassTexture.ID)
        {
            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, _frameBufferShaderPassTexture.ID);
            OpenGLHelper.LastBoundTexture = _frameBufferShaderPassTexture.ID;
        }

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

        //Unbind textures            
        OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, 0);
        OpenGLHelper.LastBoundTexture = 0;
    }

    public void Resize(Point newSize) => Resize(newSize.X, newSize.Y);

    public void Resize(float newW, float newH)
    {
        W = newW;
        H = newH;

        //if (MSAASamples != MSAA_Samples.Disabled)
        //    HF.Graphics.ResizeMSAAFramebuffer(_frameBufferMSAAID, ref _frameBufferMSAATexture, W, H, MSAASamples);

        //HF.Graphics.ResizeFramebuffer(_frameBufferShaderPassID, ref _frameBufferShaderPassTexture, W, H);

        var _colour = Colour.White;
        Vertices = new Vertex[4]
        {
            new Vertex(0, 0, _colour, 0f, 0f),
            new Vertex(W, 0, _colour, 1f,  0f),
            new Vertex(0, H, _colour, 0f,  1f),
            new Vertex(W, H, _colour, 1f,  1f)
        };

        SetUpFBOTex((int)W, (int)H);

        _ortho = Matrix3.CreateFBOOrtho(W, H);

        if (FixedTileSize)
        {
            View.W = W / TileWidth;
            View.H = H / TileHeight;
        }
        else
        {
            TileWidth = W / View.W;
            TileHeight = H / View.H;
        }
    }
}
