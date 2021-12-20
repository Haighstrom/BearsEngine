using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;

namespace BearsEngine.Worlds
{
    public delegate void ValueChange(int change);

    public class ValueDisplayWithButtons : ValueDisplay
    {
        public ValueDisplayWithButtons(int layer, Rect position, string graphic, UITheme theme, string valueName, ValueGet valueToTrack, Rect plusButtonPos, string plusButtonGfx, Rect minusButtonPos, string minusButtonGfx, ValueChange changeFunction)
            : base(layer, position, graphic, theme, valueName, valueToTrack)
        {
            Entity plusButton = new Button(layer, plusButtonPos, plusButtonGfx, () => { changeFunction(1); UpdateValueText(); });
            Entity minusButton = new Button(layer, minusButtonPos, minusButtonGfx, () => { changeFunction(-1); UpdateValueText(); });

            HV.Screen.Add(plusButton, minusButton);
        }
    }
}