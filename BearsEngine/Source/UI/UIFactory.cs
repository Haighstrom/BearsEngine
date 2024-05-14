using BearsEngine.Input;

namespace BearsEngine.UI;

public class UIFactory
{
    public UIFactory()
    {
    }

    public Button CreateButton(float layer, Rect position, string graphic, UITheme theme, string text = "", Action? actionOnClicked = null)
    {
        return new Button(layer, position, graphic, theme, text, actionOnClicked);
    }

    public Button CreateButton(float layer, Rect position, Colour graphic, UITheme theme, string text = "", Action? actionOnClicked = null)
    {
        return new Button(layer, position, graphic, theme, text, actionOnClicked);
    }
}
