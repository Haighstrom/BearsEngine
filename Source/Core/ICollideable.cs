namespace BearsEngine
{
    public interface ICollideable : IAddable, IRectangular
    {
        Rect WindowPosition { get; }
        bool Collideable { get; set; }

        bool Collides(Point p);
        bool Collides(Rect r);
        bool Collides(ICollideable i);
    }
}
