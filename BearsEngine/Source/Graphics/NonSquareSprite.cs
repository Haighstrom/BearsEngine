using BearsEngine.Graphics.Shaders;
using BearsEngine.OpenGL;
using System.Diagnostics.CodeAnalysis;

namespace BearsEngine.Graphics;

public class NonSquareSprite : RectGraphicBase
{
    private Texture _texture;
    private Vertex[] _vertices;
    private bool _verticesChanged = true;
    protected int _currentFrame = 0;
    private readonly IList<(Point OutputSize, Rect TextureSource)> _frames;
    
    public NonSquareSprite(float layer, string imgPath, float x, float y, IList<(Point OutputSize, Rect TextureSource)> frames, int initialFrame = 0)
        : base(new DefaultShader(), layer, x, y, frames[initialFrame].OutputSize.X, frames[initialFrame].OutputSize.Y)
    {
        _frames = frames;
        _texture = OpenGLHelper.LoadTexture(imgPath);

        Frame = initialFrame;

        BindVertexBuffer();
        SetVertices();
        UnbindVertexBuffer();
    }

    private Rect GetTextureSource() => _frames[_currentFrame].TextureSource;

    public override Colour Colour
    {
        set
        {
            base.Colour = value;
            _verticesChanged = true;
        }
    }   

    public int TotalFrames => _frames.Count;

    public int Frame
    {
        get => _currentFrame;
        set
        {
            if (value == _currentFrame)
                return;

            _currentFrame = Maths.Mod(value, TotalFrames);

            W = _frames[_currentFrame].OutputSize.X;
            H = _frames[_currentFrame].OutputSize.Y;

            _verticesChanged = true;
        }
    }

    [MemberNotNull(nameof(_vertices))]
    private void SetVertices()
    {
        Rect texSource = GetTextureSource();
        _vertices = new Vertex[4]
        {
            new Vertex(new Point(0, 0), Colour, texSource.TopLeft),
            new Vertex(new Point(W, 0), Colour, texSource.TopRight),
            new Vertex(new Point(0, H), Colour, texSource.BottomLeft),
            new Vertex(new Point(W, H), Colour, texSource.BottomRight)
        };

        OpenGLHelper.BufferData(BUFFER_TARGET.GL_ARRAY_BUFFER, _vertices.Length * Vertex.STRIDE, _vertices, USAGE_PATTERN.GL_STREAM_DRAW);

        _verticesChanged = false;
    }

    public override void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        if (W == 0 || H == 0)
            return;

        var mv = Matrix3.Translate(ref modelView, X, Y);

        if (OpenGLHelper.LastBoundTexture != _texture.ID)
        {
            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, _texture.ID);
            OpenGLHelper.LastBoundTexture = _texture.ID;
        }

        BindVertexBuffer();

        if (_verticesChanged)
        {
            SetVertices();
        }

        Shader.Render(ref projection, ref mv, _vertices.Length, PRIMITIVE_TYPE.GL_TRIANGLE_STRIP);

        UnbindVertexBuffer();
    }
    
}
