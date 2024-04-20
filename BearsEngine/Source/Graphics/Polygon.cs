using BearsEngine.Graphics.Shaders;
using HaighFramework.OpenGL;

namespace BearsEngine.Graphics
{
    public class Polygon : IGraphic
    {
        private readonly int _ID;
        private readonly IShader _shader;
        private Vertex[] _vertices;
        private float _layer = 0;
        private List<Point> _points;
        

        public Polygon(Colour colour, params Point[] points)
        {
            _ID = OpenGL.GenBuffer();
            _shader = new SolidColourShader();

            Colour = colour;
            Points = points.ToList();
        }
        

        public IContainer Parent { get; set; }

        public virtual void Remove() => Parent.Remove(this);

        public virtual void OnAdded() => Added(this, EventArgs.Empty);

        public virtual void OnRemoved() => Removed(this, EventArgs.Empty);

        public event EventHandler Added = delegate { };

        public event EventHandler Removed = delegate { };
        

        public bool Visible { get; set; } = true;

        public void Render(ref Matrix3 projection, ref Matrix3 modelView)
        {
            Bind();
            var mv = Matrix3.Translate(ref modelView, OffsetX, OffsetY);

            _vertices = Points.Select(p => new Vertex(p, Colour, Point.Zero)).ToArray();

            OpenGL.BufferData(BUFFER_TARGET.GL_ARRAY_BUFFER, _vertices.Length * Vertex.STRIDE, _vertices, USAGE_PATTERN.GL_STREAM_DRAW);

            _shader.Render(ref projection, ref mv, _vertices.Length, PRIMITIVE_TYPE.GL_TRIANGLE_STRIP);
            Unbind();
        }

        
        

        public float Layer
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
        

        public List<Point> Points
        {
            get => _points;
            set
            {
                _points = value;

                //make a loop
                if (_points != null && _points.Count > 0 && _points[0] != _points[_points.Count - 1])
                    _points.Add(_points[0]);
            }
        }
        

        public void Bind()
        {
            OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, _ID);
            OpenGL.LastBoundVertexBuffer = _ID;
        }
        

        public void Unbind()
        {
            OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, 0);
            OpenGL.LastBoundVertexBuffer = 0;
        }
        
        
    }
}