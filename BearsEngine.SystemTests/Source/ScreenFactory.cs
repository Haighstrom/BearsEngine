using BearsEngine.Console;

namespace BearsEngine.SystemTests.Source;

internal class ScreenFactory : IScreenFactory
{
    private readonly IGameEngine _app;

    public ScreenFactory(IGameEngine app)
    {
        _app = app;
    }

    public IScreen CreateMainMenuScreen()
    {
        return new MenuScreen(_app, this);
    }
}
