using BearsEngine.Input;

namespace BearsEngine.UI;

public delegate void ValueChange(int change);

public class ValueDisplayWithButtons : ValueDisplay
{
    public ValueDisplayWithButtons(float layer, Rect position, string graphic, UITheme theme, string valueName, ValueGet valueToTrack, Rect plusButtonPos, string plusButtonGfx, Rect minusButtonPos, string minusButtonGfx, ValueChange changeFunction)
        : base(layer, position, graphic, theme, valueName, valueToTrack)
    {
        Entity plusButton = new Button(layer, plusButtonPos, plusButtonGfx, () => { changeFunction(1); UpdateValueText(); });
        Entity minusButton = new Button(layer, minusButtonPos, minusButtonGfx, () => { changeFunction(-1); UpdateValueText(); });

        //todo: what the fuck is this?
        //HV.Screen.Add(plusButton, minusButton);
        //me, several years later... I think the objection was about adding them directly to the screen?... soo
        //actual todo: build this as a parent object with the buttons and valuedisplay all as children added to it, not inheriting
        //some more months later... yeah but add these todos into git or it's never gonna happen is it
        //more time goes past: IS IT IN GIT YET OR WHAT?
    }
}
