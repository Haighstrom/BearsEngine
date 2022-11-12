namespace BearsEngine;

public interface IRectangular : IPosition
{
    public new float X { get; set; }
    public new float Y { get; set; }
    public float W { get; set; }
    public float H { get; set; }
    public Point P { get; set; }
    public Point Size { get; set; }
    public Rect R { get; set; }
}