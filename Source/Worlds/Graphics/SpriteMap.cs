using HaighFramework.OpenGL4;
using BearsEngine.Graphics;
using BearsEngine.Graphics.Shaders;

namespace BearsEngine.Worlds.Graphics
{
    public class SpriteMap : AddableRectBase, IRectGraphic
    {
        #region Fields
        private Colour _colour = Colour.White;
        private int _layer = 999;
        private readonly SpritesheetShader _shader;
        private Texture _texture;
        private readonly uint _ID;
        private Vertex[] _vertices;
        private readonly float _ssTileW;
        private readonly float _ssTileH;
        private readonly int _ssColumns;
        #endregion

        #region Constructors
        public SpriteMap(int mapW, int mapH, int defaultIndex, float tileW, float tileH, string spritesheetPath, int spriteSheetColumns, int spriteSheetRows)
            : this(HF.Arrays.FillArray(mapW, mapH, defaultIndex), defaultIndex, tileW, tileH, spritesheetPath, spriteSheetColumns, spriteSheetRows)
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

            _ID = OpenGL.GenBuffer();
            _texture = HF.Graphics.LoadTexture(spritesheetPath);
            _shader = new SpritesheetShader(_ssTileW, _ssTileH);

            UpdateVertices();
        }
        #endregion

        #region Indexers
        public int this[int x, int y]
        {
            get => MapValues[x, y];
            set => MapValues[x, y] = value;
        }
        #endregion

        #region IRenderable
        public bool Visible { get; set; } = true;

        #region Render
        public void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            var mv = Matrix4.Translate(ref modelView, X, Y, 0);

            if (HV.LastBoundTexture != _texture.ID)
            {
                OpenGL.BindTexture(TextureTarget.Texture2D, _texture.ID);
                HV.LastBoundTexture = _texture.ID;
            }

            if (HV.LastBoundVertexBuffer != _ID)
                Bind();

            Matrix4 tileMatrix = Matrix4.Identity;

            for (int i = (int)DrawArea.X; i < DrawArea.Right; ++i)
                for (int j = (int)DrawArea.Y; j < DrawArea.Bottom; ++j)
                {
                    if (!IsInBounds(i, j))
                        continue;

                    tileMatrix = Matrix4.Translate(ref mv, i * W, j * H, 0);
                    _shader.IndexX = HF.Maths.Mod(MapValues[i, j], _ssColumns);
                    _shader.IndexY = MapValues[i, j] / _ssColumns;
                    _shader.Render(ref projection, ref tileMatrix, _vertices.Length, PrimitiveType.TriangleStrip);
                }
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

        #region IGraphic
        #region Colour
        public Colour Colour
        {
            get => _colour;
            set => _colour = value;
        }
        #endregion

        #region Alpha
        public byte Alpha
        {
            get => _colour.A;
            set => _colour.A = value;
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

        public bool IsOnScreen => true; //todo: HV.Window.ClientZeroed.Intersects(Parent.GetWindowPosition(DrawArea));
        #endregion

        #region Properties
        public int[,] MapValues { get; set; }
        public int DefaultIndex { get; set; }

        public int MapW { get; private set; }

        public int MapH { get; private set; }

        public bool IsInBounds(Point p) => IsInBounds(p.X, p.Y);

        public bool IsInBounds(float x, float y) => x >= 0 && x < MapW && y >= 0 && y < MapH;

        public bool IsOnEdge(Point p) => IsInBounds(p.X, p.Y);

        public bool IsOnEdge(float x, float y) => x == 0 || y == 0 || x == MapW - 1 || y == MapH - 1;

        public IRect DrawArea { get; set; }
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

        #region UpdateVertices
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
            OpenGL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * Vertex.STRIDE, _vertices, BufferUsageHint.StreamDraw);
            Unbind();
        }
        #endregion

        #region SetAllMapValues
        public void SetAllMapValue(int newValue) => MapValues = HF.Arrays.FillArray(MapW, MapH, newValue);
        #endregion

        #region Resize
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
        #endregion
        #endregion
    }
}