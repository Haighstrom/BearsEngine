namespace BearsEngine;

public interface IRectangular
{
    public float X { get; set; }
    public float Y { get; set; }
    public float W { get; set; }
    public float H { get; set; }
    public Point P { get; set; }
    public Point Size { get; set; }
    public Rect R { get; set; }
}