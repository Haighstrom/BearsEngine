namespace BearsEngine
{
    public interface ICollideable : IAddable, IRect
    {
        IRect WindowPosition { get; }
        bool Collideable { get; set; }

        bool Collides(Point p);
        bool Collides(IRect r);
        bool Collides(ICollideable i);
    }
}
