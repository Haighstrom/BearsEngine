using BearsEngine.Input;
using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source.IODemo;

internal class CyclingNumberButton : Button
{
    public CyclingNumberButton(float layer, Rect position, Colour bgColour, int startNumber)
        : base(layer, position, bgColour, GV.Theme, "")
    {
        ButtonValue = startNumber;
    }

    public int ButtonValue
    {
        get
        {
            var success = int.TryParse(Text, out var n);

            if (!success)
                throw new Exception($"{typeof(CyclingNumberButton).Name}._number (value: {Text}) was not parseable to an int");

            return n;
        }
        set => Text = value.ToString();
    }

    protected override void OnLeftClicked()
    {
        base.OnLeftClicked();

        ButtonValue = (ButtonValue + 1) % 10;
    }
}
