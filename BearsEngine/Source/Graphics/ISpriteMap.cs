
namespace BearsEngine.Graphics;

public interface ISpriteMap : IRectGraphic
{
    int this[int x, int y] { get; set; }

    int DefaultIndex { get; }
    Rect DrawArea { get; set; }
    int MapH { get; }
    int[,] MapValues { get; set; }
    int MapW { get; }

    event EventHandler<SpriteMapIndexChangedEventArgs>? MapIndexChanged;

    bool IsInBounds(float x, float y);
    bool IsInBounds(Point p);
    bool IsOnEdge(float x, float y);
    bool IsOnEdge(Point p);
    void SetAllMapValue(int newValue);
    void Resize(float xScale, float yScale);
    void Resize(int newW, int newH, int newIndex);
}
