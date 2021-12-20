using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine.Worlds;
using HaighFramework.Input;

namespace BearsEngine.Worlds
{
    public struct PanelTheme
    {
        #region Default
        public static PanelTheme Default => new PanelTheme
        {
            BackgroundColour = Colour.LightBlue,
            Border = BorderTheme.Default,
        };
        #endregion

        public Colour BackgroundColour;
        public BorderTheme Border;
    }
}