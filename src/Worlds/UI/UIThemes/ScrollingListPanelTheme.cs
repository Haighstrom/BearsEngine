using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine.Worlds;
using HaighFramework.Input;

namespace BearsEngine.Worlds
{
    public struct ScrollingListPanelTheme
    {
        #region Default
        public static ScrollingListPanelTheme Default => new ScrollingListPanelTheme
        {
            PanelColour = Colour.White,
        };
        #endregion

        public Colour PanelColour;
    }
}