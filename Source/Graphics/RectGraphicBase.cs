using BearsEngine.Graphics.Shaders;
using HaighFramework.OpenGL;
using HaighFramework.WinAPI;

namespace BearsEngine.Graphics
{
    public abstract class RectGraphicBase : AddableRectBase, IRectGraphic
    {
        private float _layer = 999;
        

        public RectGraphicBase(IShader shader, Rect r)
            : this(shader, r.X, r.Y, r.W, r.H)
        {
        }

        public RectGraphicBase(IShader shader, float x, float y, float w, float h)
            : base(x, y, w, h)
        {
            Shader = shader;
            VertexBuffer = OpenGL.GenBuffer();
        }
        

        public bool Visible { get; set; } = true;

        public abstract void Render(ref Matrix3 projection, ref Matrix3 modelView);
        

        public float Layer
        {
            get => _layer;
            set
            {
                if (_layer == value)
                    return;

                LayerChanged?.Invoke(this, new LayerChangedEventArgs(_layer, value));

                _layer = value;
            }
        }
        

        public event EventHandler<LayerChangedEventArgs> LayerChanged;
        

        public virtual Colour Colour { get; set; } = Colour.White;

        public virtual byte Alpha
        {
            get => Colour.A;
            set => Colour = new Colour(Colour, value);
        }
        

        public bool ResizeWithParent { get; set; } = true;

        public void Resize(float xScale, float yScale)
        {
            W *= xScale;
            H *= yScale;
        }
        

        public bool IsOnScreen => true;//HV.Window.ClientZeroed.Intersects(Parent.GetWindowPosition(this));
        

        public IShader Shader { get; set; }

        public int VertexBuffer { get; }
        

        public void BindVertexBuffer()
        {
            if (OpenGL.LastBoundVertexBuffer != VertexBuffer)
            {
                OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, VertexBuffer);
                OpenGL.LastBoundVertexBuffer = VertexBuffer;
            }
        }
        

        public void UnbindVertexBuffer()
        {
            OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, 0);
            OpenGL.LastBoundVertexBuffer = 0;
        }
        
        
    }
}