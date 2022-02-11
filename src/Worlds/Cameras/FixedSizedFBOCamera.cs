using HaighFramework;
using HaighFramework.OpenGL4;
using BearsEngine.Graphics;

namespace BearsEngine.Worlds
{
    public class FixedSizedFBOCamera : AddableRectBase, ICamera //todo: disposable - remove resize event handlers?
    {
        #region Fields
        private Texture _frameBufferShaderPassTexture;
        private Texture _frameBufferMSAATexture;
        private uint _frameBufferMSAAID;
        private Matrix4 _ortho;
        private CameraMSAAShader _mSAAShader;

        //temporary mess of properties
        public HaighFramework.OpenGL4.MSAA_Samples MSAASamples { get; set; } = MSAA_Samples.Disabled; //todo: trigger resize if this changes?
        private uint _frameBufferShaderPassID;
        protected uint VertexBuffer { get; private set; }
        protected Vertex[] Vertices { get; set; }
        private int _layer;
        private IContainer _container;
        #endregion

        #region Constructors
        public FixedSizedFBOCamera(int layer, IRect<float> position, IPoint<float> FBOSize)
            : base(position)
        {
            View = FBOSize.ToRect();
            _container = new Container(this);
            Layer = layer;
            Shader = new DefaultShader();
        }
        #endregion

        #region IAddable
        public override void OnAdded()
        {
            _mSAAShader = new CameraMSAAShader() { Samples = MSAASamples };

            VertexBuffer = OpenGL.GenBuffer();

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

            base.OnAdded();
        }
        #endregion

        #region IUpdateable
        public bool Active { get; set; } = true;

        #region Update
        public virtual void Update(double elapsed) => _container.Update(elapsed);
        #endregion
        #endregion

        #region IRenderable
        public bool Visible { get; set; } = true;

        #region Render
        public virtual void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            if (MSAAEnabled)
            {
                //Bind MSAA FBO to be the draw destination and clear it
                OpenGL.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBufferMSAAID);
                OpenGL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColourAttachment0, TextureTarget.Texture2DMultisample, _frameBufferMSAATexture.ID, 0);
            }
            else
            {
                //Bind Shader Pass FBO to be the draw destination and clear it
                OpenGL.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBufferShaderPassID);
                OpenGL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColourAttachment0, TextureTarget.Texture2D, _frameBufferShaderPassTexture.ID, 0);
            }

            //Clear the FBO
            OpenGL.ClearColour(BackgroundColour);
            OpenGL.Clear(ClearBufferMask.ColourBufferBit);

            //Set normal blend function for within the layers
            OpenGL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.OneMinusSrcAlpha);

            //Save the previous viewport and set the viewport to match the size of the texture we are now drawing to - the FBO
            Rect<int> prevVP = OpenGL.GetViewport();
            OpenGL.Viewport(0, 0, (int)View.W, (int)View.H); //not W,H??

            //Locally save the current render target, we will then set this camera as the current render target for child cameras, then put it back
            uint tempFBID = HV.LastBoundFrameBuffer;
            HV.LastBoundFrameBuffer = MSAAEnabled ? _frameBufferMSAAID : _frameBufferShaderPassID;

            Matrix4 MV = Matrix4.ScaleAroundOrigin(ref Matrix4.Identity, W / View.W, H / View.H, 0);
            MV = Matrix4.Translate(ref MV, -View.X, -View.Y, 0);

            //draw stuff here 
            if (_container.Visible)
                _container.Render(ref _ortho, ref MV);

            //Revert the render target 
            HV.LastBoundFrameBuffer = tempFBID;

            //Bind vertex buffer - optimise this later            
            OpenGL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
            OpenGL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * Vertex.STRIDE, Vertices, BufferUsageHint.DynamicDraw);
            HV.LastBoundVertexBuffer = VertexBuffer;

            if (MSAAEnabled)
            {
                //Bind the 2nd pass FBO and draw from the first to do MSAA sampling
                OpenGL.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBufferShaderPassID);
                OpenGL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColourAttachment0, TextureTarget.Texture2D, _frameBufferShaderPassTexture.ID, 0);
                OpenGL.ClearColour(new Colour(0, 0, 0, 0));
                OpenGL.Clear(ClearBufferMask.ColourBufferBit);

                //Bind the FBO to be drawn
                OpenGL.BindTexture(TextureTarget.Texture2DMultisample, _frameBufferMSAATexture.ID);

                //Do the MSAA render pass, drawing to the MSAATexture FBO
                _mSAAShader.Render(ref _ortho, ref Matrix4.Identity, Vertices.Length, PrimitiveType.TriangleStrip);

                //Unbind the FBO 
                OpenGL.BindTexture(TextureTarget.Texture2DMultisample, 0);
            }

            //Bind the render target back to either the screen, or a camera higher up the heirachy, depending on what called this
            OpenGL.BindFramebuffer(FramebufferTarget.Framebuffer, HV.LastBoundFrameBuffer);

            //Bind the FBO to be drawn
            _frameBufferShaderPassTexture.Bind();

            //Set some other blend fucntion when render the FBO texture which apparantly lets the layer alpha blend with the one beneath?
            OpenGL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.OneMinusSrcAlpha);

            //reset viewport
            OpenGL.Viewport(prevVP);

            Matrix4 mv = modelView;
            if (Angle != 0)
                mv = Matrix4.RotateAroundPoint(ref mv, Angle, Centre.X, Centre.Y);
            mv = Matrix4.Translate(ref mv, X, Y, 0);

            //Render with assigned shader
            Shader.Render(ref projection, ref mv, Vertices.Length, PrimitiveType.TriangleStrip);

            //Unbind textures            
            OpenGL.BindTexture(TextureTarget.Texture2D, 0);
            HV.LastBoundTexture = 0;
        }
        #endregion
        #endregion

        #region IRenderableOnLayer
        #region Layer
        public int Layer
        {
            get => _layer;
            set
            {
                if (_layer == value)
                    return;

                LayerChanged(this, new LayerChangedArgs(_layer, value));

                _layer = value;
            }
        }
        #endregion

        public event EventHandler<LayerChangedArgs> LayerChanged = delegate { };
        #endregion

        #region IContainer
        public List<IAddable> Entities => _container.Entities;

        public int EntityCount => _container.EntityCount;

        public IPoint<float> GetWindowPosition(IPoint<float> localCoords)
        {
            return Parent.GetWindowPosition(new Point(
                X + W / View.W * (localCoords.X - View.X),
                Y + H / View.H * (localCoords.Y - View.Y)
                ));
        }

        public IRect<float> GetWindowPosition(IRect<float> localCoords)
        {
            IPoint<float> tl = GetWindowPosition(localCoords.TopLeft);
            IPoint<float> br = GetWindowPosition(localCoords.BottomRight);
            return new Rect<float>(tl, br.X - tl.X, br.Y - tl.Y);
        }

        public IPoint<float> GetLocalPosition(IPoint<float> windowCoords)
        {
            var p1 = Parent.GetLocalPosition(windowCoords);
            var p2 = new Point(
                (p1.X - X) / (W / View.W) + View.X,
                (p1.Y - Y) / (H / View.H) + View.Y
                );

            return p2;
        }

        public IRect<float> GetLocalPosition(IRect<float> windowCoords)
        {
            IPoint<float> tl = GetLocalPosition(windowCoords.TopLeft);
            IPoint<float> br = GetLocalPosition(windowCoords.BottomRight);
            return new Rect<float>(tl, br.X - tl.X, br.Y - tl.Y);
        }

        public IPoint<float> LocalMousePosition => GetLocalPosition(HI.MouseWindowP);

        public E Add<E>(E e) where E : IAddable => _container.Add(e);

        public void Add<E>(params E[] entities) where E : IAddable => _container.Add(entities);

        public E Remove<E>(E e) where E : IAddable => _container.Remove(e);

        public void RemoveAll(bool cascadeToChildren) => _container.RemoveAll(cascadeToChildren);

        public void RemoveAll<T>(bool cascadeToChildren = true) where T : IAddable => _container.RemoveAll<T>(cascadeToChildren);

        public void RemoveAll<T1, T2>(bool cascadeToChildren = true)
            where T1 : IAddable
            where T2 : IAddable
            => _container.RemoveAll<T1, T2>(cascadeToChildren);

        public void RemoveAllExcept<T>(bool cascadeToChildren = true) where T : IAddable => _container.RemoveAllExcept<T>(cascadeToChildren);

        public void RemoveAllExcept<T1, T2>(bool cascadeToChildren = true)
            where T1 : IAddable
            where T2 : IAddable
            => _container.RemoveAllExcept<T1, T2>(cascadeToChildren);

        public List<E> GetEntities<E>(bool considerChildren = true) => _container.GetEntities<E>(considerChildren);

        public E Collide<E>(IPoint<float> p, bool considerChildren = true) where E : ICollideable => _container.Collide<E>(p, considerChildren);

        public E Collide<E>(IRect<float> r, bool considerChildren = true) where E : ICollideable => _container.Collide<E>(r, considerChildren);

        public E Collide<E>(ICollideable i, bool considerChildren = true) where E : ICollideable => _container.Collide<E>(i, considerChildren);

        public List<E> CollideAll<E>(IPoint<float> p, bool considerChildren = true) where E : ICollideable => _container.CollideAll<E>(p, considerChildren);

        public List<E> CollideAll<E>(IRect<float> r, bool considerChildren = true) where E : ICollideable => _container.CollideAll<E>(r, considerChildren);

        public List<E> CollideAll<E>(ICollideable i, bool considerChildren = true) where E : ICollideable => _container.CollideAll<E>(i, considerChildren);
        #endregion

        #region ICamera
        #region View
        public virtual IRect<float> View { get; }
        #endregion
        public Colour BackgroundColour { get; set; } = Colour.White;
        public bool MouseIntersecting => View.Contains(LocalMousePosition);

        #region MouseIsNearEdge
        public bool MouseIsNearEdge(Direction edgeDirection, int maxDistance)
        {
            switch (edgeDirection)
            {
                case Direction.Up:
                    return LocalMousePosition.Y >= -1 && LocalMousePosition.Y < View.Y + maxDistance; //todo: -1 because locked mice can go a bit out of bounds? Is this a wider issue with locked mice?
                case Direction.Right:
                    return LocalMousePosition.X > View.Right - maxDistance && LocalMousePosition.X <= View.Right;
                case Direction.Down:
                    return LocalMousePosition.Y > View.Bottom - maxDistance && LocalMousePosition.Y <= View.Bottom;
                case Direction.Left:
                    return LocalMousePosition.X >= -1 && LocalMousePosition.X < View.X + maxDistance;
                case Direction.None:
                    return false;
                default:
                    throw new ArgumentException(string.Format("edgeDirection value not handled: {0}", edgeDirection), "edgeDirection");
            }
        }
        #endregion

        #region Resize
        public void Resize(IPoint<float> newSize) => Resize(newSize.X, newSize.Y);

        public void Resize(float newW, float newH)
        {
            IPoint<float> oldSize = Size;
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
        #endregion

        #region IsInBounds
        public virtual bool IsInBounds(Point p) => IsInBounds(p.X, p.Y);
        public virtual bool IsInBounds(float x, float y) => throw new NotImplementedException();
        #endregion

        #region IsOnEdge
        public virtual bool IsOnEdge(Point p) => IsOnEdge(p.X, p.Y);
        public virtual bool IsOnEdge(float x, float y) => throw new NotImplementedException();
        #endregion

        public event EventHandler ViewChanged;
        #endregion

        #region Properties
        #region Angle
        /// <summary>
        /// Angle in Degrees
        /// </summary>
        public float Angle { get; set; }
        #endregion

        protected bool MSAAEnabled { get => MSAASamples != MSAA_Samples.Disabled; }

        public IShader Shader { get; set; }

        #endregion

        #region Methods
        #region SetUpFBOTex
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
                _frameBufferMSAATexture = new Texture(OpenGL.GenTexture(), (int)View.W, (int)View.H);

                OpenGL.BindTexture(TextureTarget.Texture2DMultisample, _frameBufferMSAATexture.ID);
                OpenGL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, MSAASamples, PixelInternalFormat.Rgba8, (int)View.W, (int)View.H, false);

                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);

                OpenGL.BindTexture(TextureTarget.Texture2DMultisample, 0);

                _frameBufferMSAAID = OpenGL.GenFramebuffer();
            }

            //Generate FBO and texture to use for the final pass, where the camera's shader will be applied to the result of the MSAA pass
            _frameBufferShaderPassTexture = new Texture(OpenGL.GenTexture(), (int)View.W, (int)View.H);

            OpenGL.BindTexture(TextureTarget.Texture2D, _frameBufferShaderPassTexture.ID);
            OpenGL.TexStorage2D(TextureTarget.Texture2D, 1, TexInternalFormat.RGBA8, (int)View.W, (int)View.H);

            OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
            OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
            OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
            OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);

            OpenGL.BindTexture(TextureTarget.Texture2D, 0);
            HV.LastBoundTexture = 0;

            _frameBufferShaderPassID = OpenGL.GenFramebuffer();

            //Check for OpenGL errors
            var err = OpenGL.GetError();
            if (err != OpenGLErrorCode.NO_ERROR)
                HConsole.Warning("OpenGL error! (Camera.Render)", err);
        }
        #endregion

        protected void OnPositionChanged() => PositionChanged?.Invoke(this, new PositionChangedArgs(this));

        protected void OnSizeChanged() => SizeChanged?.Invoke(this, new SizeEventArgs(W, H));
        #endregion

        #region Events
        public event EventHandler<PositionChangedArgs> PositionChanged;

        public event EventHandler<SizeEventArgs> SizeChanged;
        #endregion
    }
}