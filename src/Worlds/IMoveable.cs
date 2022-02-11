using HaighFramework;

namespace BearsEngine.Worlds
{
    public interface IMoveable : IRect<float>
    {
        float Speed { get; }
    }
}