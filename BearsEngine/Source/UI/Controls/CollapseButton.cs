using BearsEngine.Input;
using BearsEngine.UI;

namespace BearsEngine.Worlds.UI.Controls;

public class CollapseButton : Button
{
    readonly ICollapsable _target;
    bool _collapsed = false;
    readonly IGraphic _collapseGraphic, _expandGraphic;

    public CollapseButton(IMouse mouse, float layer, Rect position, string collapseGraphic, string expandGraphic, UITheme theme, ICollapsable parent)
        : base(mouse, layer, position, collapseGraphic, theme)
    {
        _target = parent;
        _collapseGraphic = BackgroundGraphic;
        _expandGraphic = new Image(expandGraphic, position.Size);
    }

    protected override void OnLeftClicked()
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
