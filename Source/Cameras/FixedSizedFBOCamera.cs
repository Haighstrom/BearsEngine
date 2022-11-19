using BearsEngine.Graphics.Shaders;
using BearsEngine.Win32API;

namespace BearsEngine.Worlds.Cameras
{
    public class FixedSizedFBOCamera : EntityBase, ICamera //todo: disposable - remove resize event handlers?
    {
        private Texture _frameBufferShaderPassTexture;
        private Texture _frameBufferMSAATexture;
        private uint _frameBufferMSAAID;
        private Matrix4 _ortho;
        private readonly CameraMSAAShader _mSAAShader;
        private uint _frameBufferShaderPassID;

        public FixedSizedFBOCamera(int layer, Rect position, Point FBOSize)
            : base(layer, position)
        {
            View = FBOSize.ToRect();
            Shader = new DefaultShader();

            _mSAAShader = new CameraMSAAShader() { Samples = MSAASamples };

            VertexBuffer = OpenGL32.GenBuffer();

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
            SetUpFBOTex();

            //HF.Graphics.CreateFramebuffer(W, H, out _frameBufferShaderPassID, out _frameBufferShaderPassTexture);
            //_graphic = new Image(_frameBufferShaderPassTexture);
            _ortho = Matrix4.CreateFBOOrtho(View.W, View.H);

            //_frameBufferShaderPassID = OpenGL.GenFramebuffer();
        }
        
        protected bool MSAAEnabled { get => MSAASamples != MSAA_Samples.Disabled; }

        protected uint VertexBuffer { get; private set; }

        protected Vertex[] Vertices { get; set; }

        /// <summary>
        /// Angle in Degrees
        /// </summary>
        public float Angle { get; set; }

        public Colour BackgroundColour { get; set; } = Colour.White;

        public override Point LocalMousePosition => GetLocalPosition(HI.MouseWindowP);

        public MSAA_Samples MSAASamples { get; set; } = MSAA_Samples.Disabled; //todo: trigger resize if this changes?

        public bool MouseIntersecting => View.Contains(LocalMousePosition);

        public IShader Shader { get; set; }

        public virtual Rect View { get; }

        /// <summary>
        /// Initialise the textures that the Frame Buffet Object render target the camera draws to uses and the intermediate Multisample texture used in addition if MultiSample AntiAliasing is enabled.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void SetUpFBOTex()
        {
            if (View.W <= 0 || View.H <= 0)
                return;

            if (MSAAEnabled)
            {
                //Generate FBO and texture to use with the MSAA antialising pass
                _frameBufferMSAATexture = new Texture(OpenGL32.GenTexture(), (int)View.W, (int)View.H);

                OpenGL32.glBindTexture(TextureTarget.Texture2DMultisample, _frameBufferMSAATexture.ID);
                OpenGL32.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, MSAASamples, PixelInternalFormat.Rgba8, (int)View.W, (int)View.H, false);

                OpenGL32.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
                OpenGL32.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
                OpenGL32.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
                OpenGL32.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);

                OpenGL32.glBindTexture(TextureTarget.Texture2DMultisample, 0);

                _frameBufferMSAAID = OpenGL32.GenFramebuffer();
            }

            //Generate FBO and texture to use for the final pass, where the camera's shader will be applied to the result of the MSAA pass
            _frameBufferShaderPassTexture = new Texture(OpenGL32.GenTexture(), (int)View.W, (int)View.H);

            OpenGL32.glBindTexture(TextureTarget.Texture2D, _frameBufferShaderPassTexture.ID);
            OpenGL32.TexStorage2D(TextureTarget.Texture2D, 1, TexInternalFormat.RGBA8, (int)View.W, (int)View.H);

            OpenGL32.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
            OpenGL32.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
            OpenGL32.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
            OpenGL32.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);

            OpenGL32.glBindTexture(TextureTarget.Texture2D, 0);
            BE.LastBoundTexture = 0;

            _frameBufferShaderPassID = OpenGL32.GenFramebuffer();

            //Check for OpenGL errors
            var err = OpenGL32.GetError();
            if (err != OpenGLErrorCode.NO_ERROR)
                HConsole.Warning($"OpenGL error! (Camera.Render) {err}");
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

        public bool MouseIsNearEdge(Direction edgeDirection, int maxDistance) => edgeDirection switch
        {
            Direction.Up => LocalMousePosition.Y >= -1 && LocalMousePosition.Y < View.Y + maxDistance,//todo: -1 because locked mice can go a bit out of bounds? Is this a wider issue with locked mice?
            Direction.Right => LocalMousePosition.X > View.Right - maxDistance && LocalMousePosition.X <= View.Right,
            Direction.Down => LocalMousePosition.Y > View.Bottom - maxDistance && LocalMousePosition.Y <= View.Bottom,
            Direction.Left => LocalMousePosition.X >= -1 && LocalMousePosition.X < View.X + maxDistance,
            _ => throw new ArgumentException(string.Format("edgeDirection value not handled: {0}", edgeDirection), "edgeDirection"),
        };

        public override void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            if (MSAAEnabled)
            {
                //Bind MSAA FBO to be the draw destination and clear it
                OpenGL32.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBufferMSAAID);
                OpenGL32.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColourAttachment0, TextureTarget.Texture2DMultisample, _frameBufferMSAATexture.ID, 0);
            }
            else
            {
                //Bind Shader Pass FBO to be the draw destination and clear it
                OpenGL32.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBufferShaderPassID);
                OpenGL32.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColourAttachment0, TextureTarget.Texture2D, _frameBufferShaderPassTexture.ID, 0);
            }

            //Clear the FBO
            OpenGL32.glClearColour(BackgroundColour);
            OpenGL32.glClear(ClearMask.GL_COLOR_BUFFER_BIT);

            //Set normal blend function for within the layers
            OpenGL32.glBlendFunc(BlendScaleFactor.GL_ONE, BlendScaleFactor.GL_ONE_MINUS_SRC_ALPHA);

            //Save the previous viewport and set the viewport to match the size of the texture we are now drawing to - the FBO
            Rect prevVP = OpenGL32.GetViewport();
            OpenGL32.Viewport(0, 0, (int)View.W, (int)View.H); //not W,H??

            //Locally save the current render target, we will then set this camera as the current render target for child cameras, then put it back
            uint tempFBID = BE.LastBoundFrameBuffer;
            BE.LastBoundFrameBuffer = MSAAEnabled ? _frameBufferMSAAID : _frameBufferShaderPassID;

            Matrix4 identity = Matrix4.Identity;
            Matrix4 MV = Matrix4.ScaleAroundOrigin(ref identity, W / View.W, H / View.H, 0);
            MV = Matrix4.Translate(ref MV, -View.X, -View.Y, 0);

            //draw stuff here 
            base.Render(ref _ortho, ref MV);

            //Revert the render target 
            BE.LastBoundFrameBuffer = tempFBID;

            //Bind vertex buffer - optimise this later            
            OpenGL32.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
            OpenGL32.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * Vertex.STRIDE, Vertices, BufferUsageHint.DynamicDraw);
            BE.LastBoundVertexBuffer = VertexBuffer;

            if (MSAAEnabled)
            {
                //Bind the 2nd pass FBO and draw from the first to do MSAA sampling
                OpenGL32.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBufferShaderPassID);
                OpenGL32.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColourAttachment0, TextureTarget.Texture2D, _frameBufferShaderPassTexture.ID, 0);
                OpenGL32.glClearColour(new Colour(0, 0, 0, 0));
                OpenGL32.glClear(ClearMask.GL_COLOR_BUFFER_BIT);

                //Bind the FBO to be drawn
                OpenGL32.glBindTexture(TextureTarget.Texture2DMultisample, _frameBufferMSAATexture.ID);

                //Do the MSAA render pass, drawing to the MSAATexture FBO
                _mSAAShader.Render(ref _ortho, ref identity, Vertices.Length, PrimitiveType.GL_TRIANGLE_STRIP);

                //Unbind the FBO 
                OpenGL32.glBindTexture(TextureTarget.Texture2DMultisample, 0);
            }

            //Bind the render target back to either the screen, or a camera higher up the heirachy, depending on what called this
            OpenGL32.BindFramebuffer(FramebufferTarget.Framebuffer, BE.LastBoundFrameBuffer);

            //Bind the FBO to be drawn
            _frameBufferShaderPassTexture.Bind();

            //Set some other blend fucntion when render the FBO texture which apparantly lets the layer alpha blend with the one beneath?
            OpenGL32.glBlendFunc(BlendScaleFactor.GL_ONE, BlendScaleFactor.GL_ONE_MINUS_SRC_ALPHA);

            //reset viewport
            OpenGL32.Viewport(prevVP);

            Matrix4 mv = modelView;
            if (Angle != 0)
                mv = Matrix4.RotateAroundPoint(ref mv, Angle, R.Centre.X, R.Centre.Y);
            mv = Matrix4.Translate(ref mv, X, Y, 0);

            //Render with assigned shader
            Shader.Render(ref projection, ref mv, Vertices.Length, PrimitiveType.GL_TRIANGLE_STRIP);

            //Unbind textures            
            OpenGL32.glBindTexture(TextureTarget.Texture2D, 0);
            BE.LastBoundTexture = 0;
        }

        public void Resize(Point newSize) => Resize(newSize.X, newSize.Y);

        public void Resize(float newW, float newH)
        {
            Point oldSize = Size;
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

            //SetUpFBOTex(W, H); we don't do this for this type of camera

            _ortho = Matrix4.CreateFBOOrtho(W, H); //do we want this?

            OnSizeChanged(new ResizeEventArgs(oldSize, Size));
        }

        public event EventHandler? ViewChanged;
    }
}