using BearsEngine.Input;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.UI;

public delegate int ValueGet();

public class ValueDisplay : Entity
{
    protected ValueGet Value;
    private readonly string _valueName;
    private readonly TextGraphic _valueText;

    public ValueDisplay(float layer, Rect position, string graphic, UITheme theme, string valueName, ValueGet valueToTrack)
        : base(layer, position, graphic)
    {
        Value = valueToTrack;
        _valueName = valueName;
        Add(_valueText = new TextGraphic(theme, position.Zeroed, ""));

        UpdateValueText();
    }

    public void UpdateValueText()
    {
        _valueText.Text = _valueName + "\n" + Value().ToString(); //todo: interpolated strings is life
    }
}
