using BearsEngine.Graphics.Shaders;
using BearsEngine.Win32API;

namespace BearsEngine.Graphics
{
    public class ProgressBar : RectGraphicBase
    {
        private Texture _texture;
        private Vertex[] _vertices;
        private bool _verticesChanged = true;
        private readonly Point _UV1, _UV2, _UV3, _UV4;
        private Point _UV5, _UV6, _UV7, _UV8;
        private float _amount;

        public ProgressBar(string graphicPath, Point size, Point pos, float initialPercentage = 0)
            : this(graphicPath, pos.X, pos.Y, size.X, size.Y, initialPercentage)
        {
        }

        public ProgressBar(string graphicPath, Point size, float initialPercentage = 0)
            : this(graphicPath, 0, 0, size.X, size.Y, initialPercentage)
        {
        }

        public ProgressBar(string graphicPath, float width, float height, float initialAmountFilled = 0)
            : this(graphicPath, 0, 0, width, height, initialAmountFilled)
        {
        }

        public ProgressBar(string graphicPath, Rect r, float initialAmountFilled = 0)
            : this(graphicPath, r.X, r.Y, r.W, r.H, initialAmountFilled)
        {
        }

        public ProgressBar(string graphicPath, float x, float y, float w, float h, float initialAmountFilled = 0)
            : base(new DefaultShader(), x, y, w, h)
        {
            _texture = HF.Graphics.LoadTexture(graphicPath);
            AmountFilled = initialAmountFilled;

            _UV1 = new Point(0, 0);
            _UV2 = new Point(1, 0);
            _UV3 = new Point(0, 0.5f);
            _UV4 = new Point(1, 0.5f);
        }
        

        public override float W
        {
            set
            {
                base.W = value;
                _verticesChanged = true;
            }
        }
        

        public override float H
        {
            set
            {
                base.H = value;
                _verticesChanged = true;
            }
        }
        

        public override Colour Colour
        {
            set
            {
                base.Colour = value;
                _verticesChanged = true;
            }
        }
        

        /// <summary>
        /// [0,1]
        /// </summary>
        public virtual float AmountFilled
        {
            get => _amount;
            set
            {
                if (value == _amount)
                    return;
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "AmountFilled must be between [0,1]");

                _amount = value;
                _verticesChanged = true;
            }
        }
        

        public override void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            if (W == 0 || H == 0)
                return;

            var mv = Matrix4.Translate(ref modelView, X, Y, 0);

            if (BE.LastBoundTexture != _texture.ID)
            {
                OpenGL32.glBindTexture(TextureTarget.Texture2D, _texture.ID);
                BE.LastBoundTexture = _texture.ID;
            }

            BindVertexBuffer();

            if (_verticesChanged)
            {
                _UV5 = new Point(0, 0.5f);
                _UV6 = new Point(AmountFilled, 0.5f);
                _UV7 = new Point(0, 1);
                _UV8 = new Point(AmountFilled, 1);

                _vertices = new Vertex[12]
                {
                    new Vertex(new Point(0, 0), Colour, _UV1),
                    new Vertex(new Point(W, 0), Colour, _UV2),
                    new Vertex(new Point(0, H), Colour, _UV3),

                    new Vertex(new Point(0, H), Colour, _UV3),
                    new Vertex(new Point(W, 0), Colour, _UV2),
                    new Vertex(new Point(W, H), Colour, _UV4),

                    new Vertex(new Point(0, 0), Colour, _UV5),
                    new Vertex(new Point(W * AmountFilled, 0), Colour, _UV6),
                    new Vertex(new Point(0, H), Colour, _UV7),

                    new Vertex(new Point(0, H), Colour, _UV7),
                    new Vertex(new Point(W * AmountFilled, 0), Colour, _UV6),
                    new Vertex(new Point(W * AmountFilled, H), Colour, _UV8)
                };

                OpenGL32.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * Vertex.STRIDE, _vertices, BufferUsageHint.StreamDraw);

                _verticesChanged = false;
            }

            Shader.Render(ref projection, ref mv, _vertices.Length, PrimitiveType.Triangles);

            UnbindVertexBuffer();
        }
        
    }
}