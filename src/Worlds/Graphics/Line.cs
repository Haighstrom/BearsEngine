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
    public class Line : AddableBase, IGraphic
    {
        #region Fields
        private Vertex[] _vertices;
        private int _layer = 0;
        private uint _ID;
        private SmoothLinesShader _shader;
        #endregion

        #region Constructors
        public Line(Colour colour, float thickness, bool thicknessInPixels, IRect<float> rect)
            : this(colour, thickness, thicknessInPixels, rect.TopLeft, rect.TopRight, rect.BottomRight, rect.BottomLeft, rect.TopLeft)
        {
        }

        public Line(Colour colour, float thickness, bool thicknessInPixels, params IPoint<float>[] points)
        {
            _shader = new SmoothLinesShader(thickness, thicknessInPixels);

            _ID = OpenGL.GenBuffer();

            Colour = colour;
            Points = points.Select(p => new Point<float>(p.X, p.Y)).ToList();
        }
        #endregion

        #region IRenderable
        public bool Visible { get; set; } = true;

        #region Render
        public void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            if (Thickness == 0)
                return;

            Bind();

            var mv = Matrix4.Translate(ref modelView, OffsetX, OffsetY, 0);

            var n = Points.Count;
            _vertices = new Vertex[n + 2];

            for (int i = 0; i < n; ++i)
                _vertices[i + 1] = new Vertex(Points[i], Colour, Point.Zero);

            if (Points[0] == Points[n-1]) // closed loop
            {
                _vertices[0] = new Vertex(Points[n - 2], Colour, Point.Zero);
                _vertices[n + 1] = new Vertex(Points[1], Colour, Point.Zero);
            }
            else
            {
                _vertices[0] = new Vertex(2 * Points[0] - Points[1], Colour, Point.Zero); //append point in same direction back from p(0)
                _vertices[n + 1] = new Vertex(2 * Points[n - 1] - Points[n - 2], Colour, Point.Zero); //append forwards from p(n-1)
            }

            OpenGL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * Vertex.STRIDE, _vertices, BufferUsageHint.StreamDraw);

            _shader.Render(ref projection, ref mv, _vertices.Length, PrimitiveType.LineStripAdjacency);

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
        public float Thickness
        {
            get => _shader.Thickness;
            set => _shader.Thickness = value;
        }

        public List<Point<float>> Points { get; set; }
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