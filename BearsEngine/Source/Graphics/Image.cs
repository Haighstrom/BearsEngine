using BearsEngine.Graphics.Shaders;
using BearsEngine.OpenGL;

namespace BearsEngine.Graphics;

public class Image : RectGraphicBase
{
    private const float DefaultLayer = 999;

    private Vertex[] _vertices;
    private bool _verticesChanged = true;

    public Image(float layer, string imgPath, Rect r)
        : this(layer, imgPath, r.X, r.Y, r.W, r.H)
    {
    }

    public Image(float layer, string imgPath, Point size)
        : this(layer, imgPath, 0, 0, size.X, size.Y)
    {
    }

    public Image(string imgPath, Rect r)
        : this(imgPath, r.X, r.Y, r.W, r.H)
    {
    }

    public Image(string imgPath, Point position, float width, float height)
        : this(imgPath, position.X, position.Y, width, height)
    {
    }

    public Image(string imgPath, Point pos, Point size)
        : this(imgPath, pos.X, pos.Y, size.X, size.Y)
    {
    }

    public Image(string imgPath, Point size)
        : this(imgPath, 0, 0, size.X, size.Y)
    {
    }
    public Image(float layer, string imgPath, float w, float h)
        : this(layer, imgPath, 0, 0, w, h)
    {
    }
    public Image(string imgPath, float w, float h)
        : this(imgPath, 0, 0, w, h)
    {
    }

    public Image(string imgPath, float x, float y, float width, float height)
        : this(DefaultLayer, imgPath, x, y, width, height)
    {
    }

    public Image(float layer, string imgPath, float x, float y, float width, float height)
        : base(new DefaultShader(), layer, x, y, width, height)
    {
        Texture = OpenGLHelper.LoadTexture(imgPath);
    }


    public Image(Colour colour, Rect r)
        : this(colour, r.X, r.Y, r.W, r.H)
    {
    }

    public Image(Colour colour, Point pos, Point size)
        : this(colour, pos.X, pos.Y, size.X, size.Y)
    {
    }

    public Image(Colour colour, Point size)
        : this(colour, 0, 0, size.X, size.Y)
    {
    }

    public Image(Colour colour, float w, float h)
        : this(colour, 0, 0, w, h)
    {
    }

    public Image(Colour colour, float x, float y, float width, float height)
        : this(DefaultLayer, colour, x, y, width, height)
    {
    }

    public Image(float layer, Colour colour, float x, float y, float width, float height)
        : base(new SolidColourShader(), layer, x, y, width, height)
    {
        Colour = colour;
    }


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
    public Image(Texture texture, Rect r)
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


    public Texture Texture { get; set; }

    public float Angle { get; set; }

    public override void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        if (W == 0 || H == 0)
            return;

        var mv = modelView;

        if (Angle != 0)
            mv = Matrix3.RotateAroundPoint(ref mv, Angle, R.Centre.X, R.Centre.Y);

        mv = Matrix3.Translate(ref mv, X, Y);

        if (OpenGLHelper.LastBoundTexture != Texture.ID)
        {
            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, Texture.ID);
            OpenGLHelper.LastBoundTexture = Texture.ID;
        }

        BindVertexBuffer();

        if (_verticesChanged)
            UpdateVertices();

        Shader.Render(ref projection, ref mv, _vertices.Length, PRIMITIVE_TYPE.GL_TRIANGLE_STRIP);

        UnbindVertexBuffer();
    }

    private void UpdateVertices()
    {
        _vertices = new Vertex[4]
        {
            new Vertex(new Point(0, 0), Colour, new Point(0, 0)),
            new Vertex(new Point(W, 0), Colour, new Point(1, 0)),
            new Vertex(new Point(0, H), Colour, new Point(0, 1)),
            new Vertex(new Point(W, H), Colour, new Point(1, 1))
        };

        OpenGLHelper.BufferData(BUFFER_TARGET.GL_ARRAY_BUFFER, _vertices.Length * Vertex.STRIDE, _vertices, USAGE_PATTERN.GL_STREAM_DRAW);

        _verticesChanged = false;
    }
}
