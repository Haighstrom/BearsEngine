using BearsEngine.Graphics.Shaders;
using BearsEngine.Win32API;

namespace BearsEngine.Graphics
{
    public class Image : RectGraphicBase
    {
        #region Fields
        private Vertex[] _vertices;
        private bool _verticesChanged = true;
        #endregion

        #region Constructors
        #region ImagePath-based
        public Image(string imgPath, IRect r)
            : this(imgPath, r.X, r.Y, r.W, r.H)
        {
        }

        public Image(string imgPath, Point size, Point pos)
            : this(imgPath, pos.X, pos.Y, size.X, size.Y)
        {
        }

        public Image(string imgPath, Point size, float x = 0, float y = 0)
            : this(imgPath, x, y, size.X, size.Y)
        {
        }
        public Image(string imgPath, float w, float h)
            : this(imgPath, 0, 0, w, h)
        {
        }

        public Image(string imgPath, float x, float y, float width, float height)
            : base(new DefaultShader(), x, y, width, height)
        {
            Texture = HF.Graphics.LoadTexture(imgPath);
        }
        #endregion

        #region Colour-based
        public Image(Colour colour, IRect r)
            : this(colour, r.X, r.Y, r.W, r.H)
        {
        }

        public Image(Colour colour, Point size, Point pos)
            : this(colour, pos.X, pos.Y, size.X, size.Y)
        {
        }

        public Image(Colour colour, Point size, float x = 0, float y = 0)
            : this(colour, x, y, size.X, size.Y)
        {
        }

        public Image(Colour colour, float w, float h)
            : this(colour, 0, 0, w, h)
        {
        }

        public Image(Colour colour, float x, float y, float width, float height)
            : base(new SolidColourShader(), x, y, width, height)
        {
            Colour = colour;
        }
        #endregion

        #region Texture-based
        /// <summary>
        /// Create an Image of a texture with the original image size
        /// </summary>
        public Image(Texture texture)
            : this(texture, 0, 0, texture.Width, texture.Height)
        {
        }
        /// <summary>
        /// Create an Image of a texture with a forced size
        /// </summary>
        public Image(Texture texture, IRect r)
            : this(texture, r.X, r.Y, r.W, r.H)
        {
        }
        /// <summary>
        /// Create an Image of a texture with a forced size
        /// </summary>
        public Image(Texture texture, float x, float y, float width, float height)
            : base(new DefaultShader(), x, y, width, height)
        {
            Texture = texture;
        }
        #endregion
        #endregion

        #region w
        public override float W
        {
            set
            {
                base.W = value;
                _verticesChanged = true;
            }
        }
        #endregion

        #region h
        public override float H
        {
            set
            {
                base.H = value;
                _verticesChanged = true;
            }
        }
        #endregion

        #region Colour
        public override Colour Colour
        {
            set
            {
                base.Colour = value;
                _verticesChanged = true;
            }
        }
        #endregion

        public Texture Texture { get; set; }

        public float Angle { get; set; }

        #region Render
        public override void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            if (W == 0 || H == 0)
                return;

            var mv = modelView;

            if (Angle != 0)
                mv = Matrix4.RotateAroundPoint(ref mv, Angle, Centre.X, Centre.Y);

            mv = Matrix4.Translate(ref mv, X, Y, 0);

            if (HV.LastBoundTexture != Texture.ID)
            {
                OpenGL32.glBindTexture(TextureTarget.Texture2D, Texture.ID);
                HV.LastBoundTexture = Texture.ID;
            }

            BindVertexBuffer();

            if (_verticesChanged)
            {
                _vertices = new Vertex[4]
                {
                    new Vertex(new Point(0, 0), Colour, new Point(0, 0)),
                    new Vertex(new Point(W, 0), Colour, new Point(1, 0)),
                    new Vertex(new Point(0, H), Colour, new Point(0, 1)),
                    new Vertex(new Point(W, H), Colour, new Point(1, 1))
                };

                OpenGL32.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * Vertex.STRIDE, _vertices, BufferUsageHint.StreamDraw);

                _verticesChanged = false;
            }

            Shader.Render(ref projection, ref mv, _vertices.Length, PrimitiveType.TriangleStrip);

            UnbindVertexBuffer();
        }
        #endregion
    }
}