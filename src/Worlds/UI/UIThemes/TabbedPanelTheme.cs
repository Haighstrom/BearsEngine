using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine.Worlds;
using HaighFramework.Input;

namespace BearsEngine.Worlds
{
    public struct TabbedPanelTheme
    {
        #region Default
        public static TabbedPanelTheme Default => new TabbedPanelTheme
        {
            Panel = PanelTheme.Default,
        };
        #endregion

        public PanelTheme Panel;
    }
}