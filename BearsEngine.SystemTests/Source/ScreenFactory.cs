using BearsEngine.Console;

namespace BearsEngine.SystemTests.Source;

internal class ScreenFactory : IScreenFactory
{
    private readonly IApp _app;

    public ScreenFactory(IApp app)
    {
        _app = app;
    }

    public IScreen CreateMainMenuScreen()
    {
        return new MenuScreen(_app, this);
    }
}
