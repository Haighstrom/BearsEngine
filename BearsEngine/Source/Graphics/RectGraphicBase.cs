using BearsEngine.Graphics.Shaders;
using BearsEngine.OpenGL;

namespace BearsEngine.Graphics;

public abstract class RectGraphicBase : AddableRectBase, IRectGraphic
{
    private const float DefaultLayer = 999;

    private float _layer;

    public RectGraphicBase(IShader shader, float x, float y, float w, float h)
        : this(shader, DefaultLayer, x, y, w, h)
    {
    }

    public RectGraphicBase(IShader shader, float layer, float x, float y, float w, float h)
        : base(x, y, w, h)
    {
        _layer = layer;
        Shader = shader;
        VertexBuffer = OpenGLHelper.GenBuffer();
    }

    public virtual byte Alpha
    {
        get => Colour.A;
        set => Colour = new Colour(Colour, value);
    }

    public virtual Colour Colour { get; set; } = Colour.White;

    public bool IsOnScreen => true;//HV.Window.ClientZeroed.Intersects(Parent.GetWindowPosition(this));

    public float Layer
    {
        get => _layer;
        set
        {
            if (_layer != value)
            {
                float oldvalue = _layer;
                _layer = value;

                LayerChanged?.Invoke(this, new LayerChangedEventArgs(oldvalue, _layer));
            }
        }
    }

    public IShader Shader { get; set; }

    public int VertexBuffer { get; }

    public bool Visible { get; set; } = true;

    public event EventHandler<LayerChangedEventArgs>? LayerChanged;

    public void BindVertexBuffer()
    {
        if (OpenGLHelper.LastBoundVertexBuffer != VertexBuffer)
        {
            OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, VertexBuffer);
            OpenGLHelper.LastBoundVertexBuffer = VertexBuffer;
        }
    }

    public abstract void Render(ref Matrix3 projection, ref Matrix3 modelView);      

    public void UnbindVertexBuffer()
    {
        OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, 0);
        OpenGLHelper.LastBoundVertexBuffer = 0;
    }  
}
