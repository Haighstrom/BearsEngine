using HaighFramework;
using BearsEngine.Worlds;

namespace BearsEngine
{
    public interface ICollideable : IAddable, IRect<float>
    {
        IRect<float> WindowPosition { get; }
        bool Collideable { get; set; }

        bool Collides(IPoint<float> p);
        bool Collides(IRect<float> r);
        bool Collides(ICollideable i);
    }
}
