using BearsEngine.Graphics.Shaders;
using HaighFramework.OpenGL;
using HaighFramework.WinAPI;

namespace BearsEngine.Graphics
{
    public class Line : AddableBase, IGraphic
    {
        private Vertex[] _vertices;
        private int _layer = 0;
        private readonly uint _ID;
        private readonly SmoothLinesShader _shader;

        public Line(Colour colour, float thickness, bool thicknessInPixels, Rect rect)
            : this(colour, thickness, thicknessInPixels, rect.TopLeft, rect.TopRight, rect.BottomRight, rect.BottomLeft, rect.TopLeft)
        {
        }

        public Line(Colour colour, float thickness, bool thicknessInPixels, params Point[] points)
        {
            _shader = new SmoothLinesShader(thickness, thicknessInPixels);

            _ID = OpenGL32.GenBuffer();

            Colour = colour;
            Points = points.Select(p => new Point(p.X, p.Y)).ToList();
        }

        public bool Visible { get; set; } = true;

        public void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            if (Thickness == 0 || Points.Count <= 1)
                return;

            Bind();

            var mv = Matrix4.Translate(ref modelView, OffsetX, OffsetY, 0);

            var n = Points.Count;
            _vertices = new Vertex[n + 2];

            for (int i = 0; i < n; ++i)
                _vertices[i + 1] = new Vertex(Points[i], Colour, Point.Zero);

            if (Points[0] == Points[n - 1]) // closed loop
            {
                _vertices[0] = new Vertex(Points[n - 2], Colour, Point.Zero);
                _vertices[n + 1] = new Vertex(Points[1], Colour, Point.Zero);
            }
            else
            {
                _vertices[0] = new Vertex(2 * Points[0] - Points[1], Colour, Point.Zero); //append point in same direction back from p(0)
                _vertices[n + 1] = new Vertex(2 * Points[n - 1] - Points[n - 2], Colour, Point.Zero); //append forwards from p(n-1)
            }

            OpenGL32.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * Vertex.STRIDE, _vertices, BufferUsageHint.StreamDraw);

            _shader.Render(ref projection, ref mv, _vertices.Length, PRIMITIVEMODE.GL_LINE_STRIP_ADJACENCY);

            Unbind();
        }

        public int Layer
        {
            get => _layer;
            set
            {
                if (_layer == value)
                    return;

                LayerChanged(this, new LayerChangedEventArgs(_layer, value));

                _layer = value;
            }
        }

        public event EventHandler<LayerChangedEventArgs> LayerChanged = delegate { };

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Colour Colour { get; set; }

        public byte Alpha { get; set; }

        public float OffsetX { get; set; }

        public float OffsetY { get; set; }

        public bool ResizeWithParent { get; set; } = false;

        public void Resize(float xScale, float yScale)
        {
            throw new NotImplementedException();
        }

        public bool IsOnScreen => true;//todo: this thing
        
        public float Thickness
        {
            get => _shader.Thickness;
            set => _shader.Thickness = value;
        }

        public IList<Point> Points { get; set; }
        
        public void Bind()
        {
            OpenGL32.BindBuffer(BufferTarget.ArrayBuffer, _ID);
            BE.LastBoundVertexBuffer = _ID;
        }
        public void Unbind()
        {
            OpenGL32.BindBuffer(BufferTarget.ArrayBuffer, 0);
            BE.LastBoundVertexBuffer = 0;
        }
    }
}