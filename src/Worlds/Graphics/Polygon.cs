using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using HaighFramework.Input;
using HaighFramework.OpenGL4;
using BearsEngine;
using BearsEngine.Worlds;
using BearsEngine.Graphics;

namespace BearsEngine.Worlds
{
    public class Polygon : IGraphic
    {
        #region Fields
        private uint _ID;
        private IShader _shader;
        private Vertex[] _vertices;
        private int _layer = 0;
        private List<Point> _points;
        #endregion

        #region Constructors
        public Polygon(Colour colour, params Point[] points)
        {
            _ID = OpenGL.GenBuffer();
            _shader = new SolidColourShader();

            Colour = colour;
            Points = points.ToList();
        }
        #endregion

        #region IAddable
        public IContainer Parent { get; set; }

        public virtual void Remove() => Parent.Remove(this);

        public virtual void OnAdded() => Added(this, EventArgs.Empty);

        public virtual void OnRemoved() => Removed(this, EventArgs.Empty);

        public event EventHandler Added = delegate { };

        public event EventHandler Removed = delegate { };
        #endregion

        #region IRenderable
        public bool Visible { get; set; } = true;

        #region Render
        public void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            Bind();
            var mv = Matrix4.Translate(ref modelView, OffsetX, OffsetY, 0);

            _vertices = Points.Select(p => new Vertex(p, Colour, Point.Zero)).ToArray();

            OpenGL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * Vertex.STRIDE, _vertices, BufferUsageHint.StreamDraw);

            _shader.Render(ref projection, ref mv, _vertices.Length, PrimitiveType.TriangleStrip);
            Unbind();
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

        #region IDisposable
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IGraphic
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
        #endregion

        #region Properties
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
        #endregion

        #region Methods
        #region Bind
        public void Bind()
        {
            OpenGL.BindBuffer(BufferTarget.ArrayBuffer, _ID);
            HV.LastBoundVertexBuffer = _ID;
        }
        #endregion

        #region Unbind
        public void Unbind()
        {
            OpenGL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            HV.LastBoundVertexBuffer = 0;
        }
        #endregion
        #endregion
    }
}