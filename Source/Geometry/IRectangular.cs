namespace BearsEngine;

public interface IRectangular : IPosition
{
    new float X { get; set; }
    new float Y { get; set; }
    float W { get; set; }
    float H { get; set; }
    Point P { get; set; }
    Point Size { get; set; }
    Rect R { get; set; }
}