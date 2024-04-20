using BearsEngine.Graphics.Shaders;
using BearsEngine.OpenGL;

namespace BearsEngine.Graphics;

public class SpriteMap : AddableRectBase, IRectGraphic
{
    private Colour _colour = Colour.White;
    private float _layer = 999;
    private readonly SpritesheetShader _shader;
    private Texture _texture;
    private readonly int _ID;
    private Vertex[] _vertices;
    private readonly float _ssTileW;
    private readonly float _ssTileH;
    private readonly int _ssColumns;
    

    public SpriteMap(int mapW, int mapH, int defaultIndex, float tileW, float tileH, string spritesheetPath, int spriteSheetColumns, int spriteSheetRows)
        : this(ArrayHelper.CreateFilledArray(mapW, mapH, defaultIndex), defaultIndex, tileW, tileH, spritesheetPath, spriteSheetColumns, spriteSheetRows)
    {
    }

    public SpriteMap(int[,] map, int defaultIndex, float tileW, float tileH, string spritesheetPath, int spriteSheetColumns, int spriteSheetRows)
        : base(0, 0, tileW, tileH)
    {
        MapW = map.GetLength(0);
        MapH = map.GetLength(1);
        _ssTileW = (float)1 / spriteSheetColumns;
        _ssTileH = (float)1 / spriteSheetRows;
        _ssColumns = spriteSheetColumns;
        DefaultIndex = defaultIndex;

        MapValues = map;

        DrawArea = new Rect(0, 0, MapW, MapH);

        _ID = OpenGLHelper.GenBuffer();
        _texture = OpenGLHelper.LoadTexture(spritesheetPath);
        _shader = new SpritesheetShader(_ssTileW, _ssTileH);

        UpdateVertices();
    }

    public int this[int x, int y]
    {
        get => MapValues[x, y];
        set
        {
            if (MapValues[x, y] != value)
            {
                MapValues[x, y] = value;
                MapIndexChanged?.Invoke(this, new(x, y, value));
            }
        }
    }
    
    public bool Visible { get; set; } = true;

    public void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        var mv = Matrix3.Translate(ref modelView, X, Y);

        if (OpenGLHelper.LastBoundTexture != _texture.ID)
        {
            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, _texture.ID);
            OpenGLHelper.LastBoundTexture = _texture.ID;
        }

        if (OpenGLHelper.LastBoundVertexBuffer != _ID)
            Bind();

        Matrix3 tileMatrix = Matrix3.Identity;

        for (int i = (int)DrawArea.X; i < DrawArea.Right; ++i)
            for (int j = (int)DrawArea.Y; j < DrawArea.Bottom; ++j)
            {
                if (!IsInBounds(i, j))
                    continue;

                tileMatrix = Matrix3.Translate(ref mv, i * W, j * H);
                _shader.IndexX = Maths.Mod(MapValues[i, j], _ssColumns);
                _shader.IndexY = MapValues[i, j] / _ssColumns;
                _shader.Render(ref projection, ref tileMatrix, _vertices.Length, PRIMITIVE_TYPE.GL_TRIANGLE_STRIP);
            }
    }
    
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

    public event EventHandler<LayerChangedEventArgs>? LayerChanged;

    public event EventHandler<SpriteMapIndexChangedEventArgs>? MapIndexChanged;
    

    public Colour Colour
    {
        get => _colour;
        set => _colour = value;
    }
    

    public byte Alpha
    {
        get => _colour.A;
        set => _colour.A = value;
    }
    

    public bool ResizeWithParent { get; set; } = true;

    public void Resize(float xScale, float yScale)
    {
        W *= xScale;
        H *= yScale;
    }
    

    public bool IsOnScreen => true; //todo: HV.Window.ClientZeroed.Intersects(Parent.GetWindowPosition(DrawArea));
    

    public int[,] MapValues { get; set; }

    public int DefaultIndex { get; }

    public int MapW { get; private set; }

    public int MapH { get; private set; }

    public bool IsInBounds(Point p) => IsInBounds(p.X, p.Y);

    public bool IsInBounds(float x, float y) => x >= 0 && x < MapW && y >= 0 && y < MapH;

    public bool IsOnEdge(Point p) => IsInBounds(p.X, p.Y);

    public bool IsOnEdge(float x, float y) => x == 0 || y == 0 || x == MapW - 1 || y == MapH - 1;

    public Rect DrawArea { get; set; }

    public void Bind()
    {
        OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, _ID);
        OpenGLHelper.LastBoundVertexBuffer = _ID;
    }
    

    public void Unbind()
    {
        OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, 0);
        OpenGLHelper.LastBoundVertexBuffer = 0;
    }
    

    private void UpdateVertices()
    {
        Bind();
        _vertices = new Vertex[4]
        {
            new Vertex(new Point(0, 0), Colour, new Point(0, 0)),
            new Vertex(new Point(W, 0), Colour, new Point(_ssTileW, 0)),
            new Vertex(new Point(0, H), Colour, new Point(0, _ssTileH)),
            new Vertex(new Point(W, H), Colour, new Point(_ssTileW, _ssTileH))
        };
        OpenGLHelper.BufferData(BUFFER_TARGET.GL_ARRAY_BUFFER, _vertices.Length * Vertex.STRIDE, _vertices, USAGE_PATTERN.GL_STREAM_DRAW);
        Unbind();
    }
    

    public void SetAllMapValue(int newValue) => MapValues = ArrayHelper.CreateFilledArray(MapW, MapH, newValue);
    

    public void Resize(int newW, int newH) => Resize(newW, newH, DefaultIndex);

    public void Resize(int newW, int newH, int newIndex)
    {
        int[,] newMap = new int[newW, newH];

        for (int i = 0; i < newW; ++i)
            for (int j = 0; j < newH; ++j)
                if (IsInBounds(i, j))
                    newMap[i, j] = MapValues[i, j];
                else
                    newMap[i, j] = newIndex;

        MapW = newW;
        MapH = newH;

        MapValues = newMap;
    }
    
    
}
