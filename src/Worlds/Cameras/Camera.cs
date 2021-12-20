using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaighFramework;
using HaighFramework.Input;
using HaighFramework.OpenGL4;
using BearsEngine.Graphics;

namespace BearsEngine.Worlds
{
    public class Camera : AddableRectBase, ICamera //todo: disposable - remove resize event handlers?
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
        private IRect<float> _view = new Rect();
        private float _x, _y, _w, _h;
        private int _layer;
        private IContainer _container;
        private float _tileWidth, _tileHeight;
        #endregion

        #region Constructors
        public Camera(int layer, IRect<float> position, Point tileSize)
            : this(layer, position, tileSize.X, tileSize.Y)
        {
        }

        public Camera(int layer, IRect<float> position, float tileW, float tileH)
            : this(layer, position)
        {
            FixedTileSize = true;

            //View set automatically
            TileWidth = tileW;
            TileHeight = tileH;

        }

        public Camera(int layer, IRect<float> position, IRect<float> viewport)
            : this(layer, position)
        {
            FixedTileSize = false;

            //TileWidth/TileHeight set automatically
            View = viewport;
        }

        private Camera(int layer, IRect<float> position)
            : base(position)
        {
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
            SetUpFBOTex((int)W, (int)H);

            //HF.Graphics.CreateFramebuffer(W, H, out _frameBufferShaderPassID, out _frameBufferShaderPassTexture);
            //_graphic = new Image(_frameBufferShaderPassTexture);
            _ortho = Matrix4.CreateFBOOrtho(W, H);

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

            //Set the viewport to match the size of the texture we are now drawing to - the FBO
            OpenGL.Viewport(0, 0, (int)W, (int)H);

            //Locally save the current render target, we will then set this camera as the current render target for child cameras, then put it back
            uint tempFBID = HV.LastBoundFrameBuffer;
            HV.LastBoundFrameBuffer = MSAAEnabled ? _frameBufferMSAAID : _frameBufferShaderPassID;

            Matrix4 MV = Matrix4.ScaleAroundOrigin(ref Matrix4.Identity, TileWidth, TileHeight, 0);
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

            //This needs to be equal to the FBO of what's being drawn to. Usually the screen, but. . .
            OpenGL.Viewport(0, 0, ViewportForcedX ?? HV.Window.Width, ViewportForcedY ?? HV.Window.Height);

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
                X + TileWidth * (localCoords.X - View.X),
                Y + TileHeight * (localCoords.Y - View.Y)
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
                (p1.X - X) / TileWidth + View.X,
                (p1.Y - Y) / TileHeight + View.Y
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

        public E Collide<E>(Point p, bool considerChildren = true) where E : ICollideable => _container.Collide<E>(p, considerChildren);

        public E Collide<E>(IRect<float> r, bool considerChildren = true) where E : ICollideable => _container.Collide<E>(r, considerChildren);

        public E Collide<E>(ICollideable i, bool considerChildren = true) where E : ICollideable => _container.Collide<E>(i, considerChildren);

        public List<E> CollideAll<E>(Point p, bool considerChildren = true) where E : ICollideable => _container.CollideAll<E>(p, considerChildren);

        public List<E> CollideAll<E>(IRect<float> r, bool considerChildren = true) where E : ICollideable => _container.CollideAll<E>(r, considerChildren);

        public List<E> CollideAll<E>(ICollideable i, bool considerChildren = true) where E : ICollideable => _container.CollideAll<E>(i, considerChildren);
        #endregion

        #region ICamera
        #region View
        public virtual IRect<float> View
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
        #endregion

        public float MinX { get; set; }

        public float MinY { get; set; }

        public float MaxX { get; set; }

        public float MaxY { get; set; }

        #region TileWidth
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
        #endregion

        #region TileHeight
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
        #endregion

        public Colour BackgroundColour { get; set; } = Colour.White;

        public bool MouseIntersecting => View.Contains(LocalMousePosition);

        #region MouseIsNearEdge
        public bool MouseIsNearEdge(Direction edgeDirection, int maxDistance)
        {
            switch (edgeDirection)
            {
                case Direction.Up:
                    return LocalMousePosition.Y >= 0 && LocalMousePosition.Y < View.Y + maxDistance;
                case Direction.Right:
                    return LocalMousePosition.X > View.Right - maxDistance && LocalMousePosition.X <= View.Right;
                case Direction.Down:
                    return LocalMousePosition.Y > View.Bottom - maxDistance && LocalMousePosition.Y <= View.Bottom;
                case Direction.Left:
                    return LocalMousePosition.X >= 0 && LocalMousePosition.X < View.X + maxDistance;
                case Direction.None:
                    return false;
                default:
                    throw new ArgumentException(string.Format("edgeDirection value not handled: {0}", edgeDirection), "edgeDirection");
            }
        }
        #endregion

        #region Resize
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

            _ortho = Matrix4.CreateFBOOrtho(W, H);

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
        #endregion

        #region IsInBounds
        public virtual bool IsInBounds(Point p) => IsInBounds(p.X, p.Y);
        public virtual bool IsInBounds(float x, float y) => x >= MinX && x < MaxX && y >= MinY && y < MaxY;
        #endregion

        #region IsOnEdge
        public virtual bool IsOnEdge(Point p) => IsOnEdge(p.X, p.Y);
        public virtual bool IsOnEdge(float x, float y) => x == MinX || y == MinY || x == MaxX - 1 || y == MaxY - 1;
        #endregion

        public event EventHandler ViewChanged;
        #endregion

        #region Properties
        /// <summary>
        /// todo: horrible hacks to make this work, please fix
        /// </summary>
        public int? ViewportForcedX { get; set; }
        /// <summary>
        /// todo: horrible hacks to make this work, please fix
        /// </summary>
        public int? ViewportForcedY { get; set; }

        #region Angle
        /// <summary>
        /// Angle in Degrees
        /// </summary>
        public float Angle { get; set; }
        #endregion

        public bool FixedTileSize { get; set; }

        protected bool MSAAEnabled { get => MSAASamples != MSAA_Samples.Disabled; }

        public IShader Shader { get; set; }

        #endregion

        #region Methods
        #region OnPositionChanged
        #region SetUpFBOTex
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
                _frameBufferMSAATexture = new Texture(OpenGL.GenTexture(), width, height);

                OpenGL.BindTexture(TextureTarget.Texture2DMultisample, _frameBufferMSAATexture.ID);
                OpenGL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, MSAASamples, PixelInternalFormat.Rgba8, width, height, false);

                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);

                OpenGL.BindTexture(TextureTarget.Texture2DMultisample, 0);

                _frameBufferMSAAID = OpenGL.GenFramebuffer();
            }

            //Generate FBO and texture to use for the final pass, where the camera's shader will be applied to the result of the MSAA pass
            _frameBufferShaderPassTexture = new Texture(OpenGL.GenTexture(), width, height);

            OpenGL.BindTexture(TextureTarget.Texture2D, _frameBufferShaderPassTexture.ID);
            OpenGL.TexStorage2D(TextureTarget.Texture2D, 1, TexInternalFormat.RGBA8, width, height);

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
        #endregion

        #region OnSizeChanged
        protected void OnSizeChanged() => SizeChanged?.Invoke(this, new SizeEventArgs(W, H));
        #endregion
        #endregion

        #region Events
        public event EventHandler<PositionChangedArgs> PositionChanged;

        public event EventHandler<SizeEventArgs> SizeChanged;
        #endregion
    }
}