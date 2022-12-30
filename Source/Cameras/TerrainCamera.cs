namespace BearsEngine.Worlds.Cameras;

public class TerrainCamera : Camera
{
    protected SpriteMap SpriteMap { get; private set; }

    public TerrainCamera(int mapW, int mapH, int defaultIndex, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, float layer, Rect position, float tileSizeW, float tileSizeH)
        : this(HF.Arrays.FillArray(mapW, mapH, defaultIndex), defaultIndex, terrainSpriteSheetPath, spriteSheetW, spriteSheetH, layer, position, tileSizeW, tileSizeH)
    {
    }

    public TerrainCamera(int[,] map, int defaultIndex, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, float layer, Rect position, float tileSizeW, float tileSizeH)
        : base(layer, position, tileSizeW, tileSizeH)
    {
        Add(SpriteMap = new SpriteMap(map, defaultIndex, 1, 1, terrainSpriteSheetPath, spriteSheetW, spriteSheetH));
        ViewChanged += (o, s) => { SpriteMap.DrawArea = View; };
        MaxX = map.GetLength(0);
        MaxY = map.GetLength(1);
    }

    public TerrainCamera(int mapW, int mapH, int defaultIndex, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, float layer, Rect position, Rect viewport)
        : this(HF.Arrays.FillArray(mapW, mapH, defaultIndex), defaultIndex, terrainSpriteSheetPath, spriteSheetW, spriteSheetH, layer, position, viewport)
    {
    }

    public TerrainCamera(int[,] map, int defaultIndex, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, float layer, Rect position, Rect viewport)
        : base(layer, position, viewport)
    {
        Add(SpriteMap = new SpriteMap(map, defaultIndex, 1, 1, terrainSpriteSheetPath, spriteSheetW, spriteSheetH));
        ViewChanged += (o, s) => { SpriteMap.DrawArea = View; };
        MaxX = map.GetLength(0);
        MaxY = map.GetLength(1);
    }

    public virtual int this[int x, int y]
    {
        get => SpriteMap[x, y];
        set => SpriteMap[x, y] = value;
    }
    
    public int[,] MapValues => SpriteMap.MapValues;

    public int DefaultIndex => SpriteMap.DefaultIndex;

    public int MapW => SpriteMap.MapW;

    public int MapH => SpriteMap.MapH;

    public event EventHandler<SpriteMapIndexChangedEventArgs>? MapIndexChanged
    {
        add => SpriteMap.MapIndexChanged += value;

        remove => SpriteMap.MapIndexChanged -= value;
    }

    public override bool IsInBounds(Point p) => SpriteMap.IsInBounds(p);

    public override bool IsInBounds(float x, float y) => SpriteMap.IsInBounds(x, y);

    public override bool IsOnEdge(float x, float y) => SpriteMap.IsOnEdge(x, y);

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

    public void SetAllMapValue(int newValue) => SpriteMap.SetAllMapValue(newValue);
}