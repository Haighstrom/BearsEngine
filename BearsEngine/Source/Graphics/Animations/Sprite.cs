using BearsEngine.OpenGL;

namespace BearsEngine.Graphics;

public class Sprite : RectGraphicBase, ISprite
{
    private const int Vertices = 4;
    private const float DefaultLayer = 999;

    private ISpriteTexture _texture;
    private bool _verticesNeedBuffering = true;
    private int _frame = 0;

    public Sprite(ISpriteTexture texture, float x, float y, float width, float height, float layer = DefaultLayer, int initialFrame = 0)
        : base(layer, x, y, width, height)
    {
        _texture = texture;

        _frame = initialFrame;
    }

    public Sprite(ISpriteTexture texture, Rect position, int initialFrame = 0, float layer = DefaultLayer)
        : this(texture, position.X, position.Y, position.W, position.H, layer, initialFrame)
    {
    }

    public Sprite(ISpriteTexture texture, Point size, int initialFrame = 0, float layer = DefaultLayer)
        : this(texture, 0, 0, size.X, size.Y, layer, initialFrame)
    {
    }

    public Sprite(ISpriteTexture texture, float width, float height, int initialFrame = 0, float layer = DefaultLayer)
        : this(texture, 0, 0, width, height, layer, initialFrame)
    {
    }

    public override float W
    {
        set
        {
            base.W = value;
            _verticesNeedBuffering = true;
        }
    }

    public override float H
    {
        set
        {
            base.H = value;
            _verticesNeedBuffering = true;
        }
    }

    public override Colour Colour
    {
        set
        {
            base.Colour = value;
            _verticesNeedBuffering = true;
        }
    }

    public ISpriteTexture Texture
    {
        get => _texture;
        set
        {
            _texture = value;
            _verticesNeedBuffering = true;
        }
    }

    public int Frame
    {
        get => _frame;
        set
        {
            if (_frame != value)
            {
                _frame = Maths.Mod(value, Frames);

                _verticesNeedBuffering = true;
            }
        }
    }

    public int Frames => _texture.Frames;

    public int LastFrame => Frames - 1;

    private (Point UV1, Point UV2, Point UV3, Point UV4) GetUVCoordinates()
    {
        return _texture.GetUVCoordinates(Frame);
    }

    private void BufferVertices()
    {
        var (uv1, uv2, uv3, uv4) = GetUVCoordinates();

        var vertices = new Vertex[Vertices]
        {
            new(new Point(0, 0), Colour, uv1),
            new(new Point(W, 0), Colour, uv2),
            new(new Point(0, H), Colour, uv3),
            new(new Point(W, H), Colour, uv4)
        };

        OpenGLHelper.BufferData(BUFFER_TARGET.GL_ARRAY_BUFFER, vertices.Length * Vertex.STRIDE, vertices, USAGE_PATTERN.GL_STREAM_DRAW);
    }

    public override void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        if (W == 0 || H == 0)
        {
            return;
        }

        var mv = Matrix3.Translate(ref modelView, X, Y);

        OpenGLHelper.BindTexture(_texture);

        OpenGLHelper.BindVertexBuffer(VertexBuffer);

        if (_verticesNeedBuffering)
        {
            BufferVertices();
            _verticesNeedBuffering = false;
        }

        Shader.Render(ref projection, ref mv, Vertices, PRIMITIVE_TYPE.GL_TRIANGLE_STRIP);

        OpenGLHelper.UnbindVertexBuffer();
    }
}
