using BearsEngine.Input;

namespace BearsEngine.UI;

public class UIFactory
{
    private readonly IMouse _mouse;

    public UIFactory(IMouse mouse)
    {
        _mouse = mouse;
    }

    public Button CreateButton(float layer, Rect position, string graphic, UITheme theme, string text = "", Action? actionOnClicked = null)
    {
        return new Button(_mouse, layer, position, graphic, theme, text, actionOnClicked);
    }

    public Button CreateButton(float layer, Rect position, Colour graphic, UITheme theme, string text = "", Action? actionOnClicked = null)
    {
        return new Button(_mouse, layer, position, graphic, theme, text, actionOnClicked);
    }
}
