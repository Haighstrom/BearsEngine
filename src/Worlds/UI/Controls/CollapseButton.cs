using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine;
using BearsEngine.Worlds;

namespace BearsEngine.Worlds
{
    public class CollapseButton : Button
    {
        ICollapsable _target;
        bool _collapsed = false;
        IGraphic _collapseGraphic, _expandGraphic;

        public CollapseButton(int layer, IRect<float> position, string collapseGraphic, string expandGraphic, UITheme theme, ICollapsable parent)
            : base(layer, position, collapseGraphic, theme)
        {
            _target = parent;
            _collapseGraphic = BackgroundGraphic;
            _expandGraphic = new Image(expandGraphic, position.Size);
        }

        public override void OnLeftClicked()
        {
            base.OnLeftClicked();
            
            Remove(BackgroundGraphic);

            if (_collapsed = !_collapsed)
            {
                Add(BackgroundGraphic = _expandGraphic);
                _target.Collapse();
            }
            else
            {
                Add(BackgroundGraphic = _collapseGraphic);
                _target.Expand();
            }
        }
    }
}
