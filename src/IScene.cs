namespace BearsEngine
{
    public interface IScene : IUpdatable, IRenderable
    {
        void Start();
        void End();
        void OnResize();
    }
}