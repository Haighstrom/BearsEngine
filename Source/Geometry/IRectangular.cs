namespace BearsEngine;

public interface IRectangular
{
    float X { get; set; }
    float Y { get; set; }
    float W { get; set; }
    float H { get; set; }
    Point P { get; set; }
    Point Size { get; set; }
    Rect R { get; set; }
}