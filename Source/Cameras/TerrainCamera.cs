namespace BearsEngine.Worlds.Cameras
{
    public class TerrainCamera : Camera
    {
        protected SpriteMap SpriteMap { get; private set; }

        #region Constructors
        #region Variable Tile Size Cameras
        public TerrainCamera(int mapW, int mapH, int defaultIndex, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, int layer, IRect position, float tileSizeW, float tileSizeH)
            : this(HF.Arrays.FillArray(mapW, mapH, defaultIndex), defaultIndex, terrainSpriteSheetPath, spriteSheetW, spriteSheetH, layer, position, tileSizeW, tileSizeH)
        {
        }

        public TerrainCamera(int[,] map, int defaultIndex, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, int layer, IRect position, float tileSizeW, float tileSizeH)
            : base(layer, position, tileSizeW, tileSizeH)
        {
            Add(SpriteMap = new SpriteMap(map, defaultIndex, 1, 1, terrainSpriteSheetPath, spriteSheetW, spriteSheetH));
            ViewChanged += (o, s) => { SpriteMap.DrawArea = View; };
            MaxX = map.GetLength(0);
            MaxY = map.GetLength(1);
        }
        #endregion

        #region Fixed Tile Size Cameras
        public TerrainCamera(int mapW, int mapH, int defaultIndex, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, int layer, IRect position, IRect viewport)
            : this(HF.Arrays.FillArray(mapW, mapH, defaultIndex), defaultIndex, terrainSpriteSheetPath, spriteSheetW, spriteSheetH, layer, position, viewport)
        {
        }

        public TerrainCamera(int[,] map, int defaultIndex, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, int layer, IRect position, IRect viewport)
            : base(layer, position, viewport)
        {
            Add(SpriteMap = new SpriteMap(map, defaultIndex, 1, 1, terrainSpriteSheetPath, spriteSheetW, spriteSheetH));
            ViewChanged += (o, s) => { SpriteMap.DrawArea = View; };
            MaxX = map.GetLength(0);
            MaxY = map.GetLength(1);
        }
        #endregion
        #endregion

        #region Indexers
        public virtual int this[Point p]
        {
            get => SpriteMap[(int)p.X, (int)p.Y];
            set => SpriteMap[(int)p.X, (int)p.Y] = value;
        }

        public virtual int this[int x, int y]
        {
            get => SpriteMap[x, y];
            set => SpriteMap[x, y] = value;
        }
        #endregion

        #region Properties
        public int[,] MapValues => SpriteMap.MapValues;

        public int DefaultIndex
        {
            get => SpriteMap.DefaultIndex;
            set => SpriteMap.DefaultIndex = value;
        }

        public int MapW => SpriteMap.MapW;

        public int MapH => SpriteMap.MapH;
        #endregion

        #region Methods
        public override bool IsInBounds(Point p) => SpriteMap.IsInBounds(p);

        public override bool IsInBounds(float x, float y) => SpriteMap.IsInBounds(x, y);

        public override bool IsOnEdge(float x, float y) => SpriteMap.IsOnEdge(x, y);

        #region Resize
        public virtual void ResizeMap(int newW, int newH)
        {
            SpriteMap.Resize(newW, newH);
            SpriteMap.DrawArea = View;
            MaxX = newW;
            MaxY = newH;
        }

        public virtual void ResizeMap(int newW, int newH, int newIndex)
        {
            SpriteMap.Resize(newW, newH, newIndex);
            SpriteMap.DrawArea = View;
            MaxX = newW;
            MaxY = newH;
        }
        #endregion

        public void SetAllMapValue(int newValue) => SpriteMap.SetAllMapValue(newValue);
        #endregion
    }
}