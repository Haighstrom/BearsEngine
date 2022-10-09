namespace BearsEngine
{
    public interface IScene : IUpdatable, IRenderable, IDisposable
    {
        Colour BackgroundColour { get; }
        /// <summary>
        /// called once active
        /// </summary>
        void Start();
        /// <summary>
        /// only called if Active
        /// </summary>
        void End();
    }
}