using System;
using System.Collections.Generic;
using BearsEngine.Graphics;
using HaighFramework;
using HaighFramework.OpenGL4;

namespace BearsEngine.Worlds
{
    public abstract class RectGraphicBase : AddableRectBase, IRectGraphic
    {
        #region Fields
        private int _layer = 999;
        #endregion

        #region Constructors
        public RectGraphicBase(IShader shader, IRect<float> r)
            : this(shader, r.X, r.Y, r.W, r.H)
        {
        }

        public RectGraphicBase(IShader shader, float x, float y, float w, float h)
            : base(x, y, w, h)
        {
            Shader = shader;
            VertexBuffer = OpenGL.GenBuffer();
        }
        #endregion

        #region IRenderable
        public bool Visible { get; set; } = true;

        public abstract void Render(ref Matrix4 projection, ref Matrix4 modelView);
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

                LayerChanged?.Invoke(this, new LayerChangedArgs(_layer, value));

                _layer = value;
            }
        }
        #endregion

        public event EventHandler<LayerChangedArgs> LayerChanged;
        #endregion

        #region IGraphic
        public virtual Colour Colour { get; set; } = Colour.White;

        #region Alpha
        public virtual byte Alpha
        {
            get => Colour.A;
            set => Colour = new Colour(Colour, value);
        }
        #endregion

        public bool ResizeWithParent { get; set; } = true;

        #region Resize
        public void Resize(float xScale, float yScale)
        {
            W *= xScale;
            H *= yScale;
        }
        #endregion

        public bool IsOnScreen => HV.Window.ClientZeroed.Intersects(Parent.GetWindowPosition(this));
        #endregion

        #region Properties
        public IShader Shader { get; set; }

        public uint VertexBuffer { get; }
        #endregion

        #region Methods
        #region BindVertexBuffer
        public void BindVertexBuffer()
        {
            if (HV.LastBoundVertexBuffer != VertexBuffer)
            {
                OpenGL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
                HV.LastBoundVertexBuffer = VertexBuffer;
            }
        }
        #endregion

        #region UnbindVertexBuffer
        public void UnbindVertexBuffer()
        {
            OpenGL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            HV.LastBoundVertexBuffer = 0;
        }
        #endregion
        #endregion
    }
}