using BearsEngine.Worlds.Cameras;

namespace BearsEngine.UI;

public class ScrollingListPanel : Entity
{
    private readonly List<IRectAddable> _itemsOnPage = new();
    private readonly Camera _camera;
    private readonly Scrollbar _sb;

    public ScrollingListPanel(float layer, Rect listPosition, int listToScrollbarGap, int scrollbarWidth, UITheme theme, int minIncrement = 0)
        : base(layer, new Rect(listPosition.X, listPosition.Y, listPosition.W + listToScrollbarGap + scrollbarWidth, listPosition.H))
    {
        Add(_camera = new Camera(1, listPosition.Zeroed, listPosition.Zeroed) { BackgroundColour = theme.ScrollingListPanel.PanelColour });
        Add(_sb = new Scrollbar(1, new Rect(listPosition.W + listToScrollbarGap, 0, scrollbarWidth, H), ScrollbarDirection.Vertical, theme));
        _sb.MinIncrement = minIncrement;
        _sb.BarPositionChanged += (s, a) => _camera.View.Y = a.MinAmount * _itemsOnPage.Last().R.Bottom;
    }

    public void AddItem(IRectAddable e)
    {
        if (_itemsOnPage.Contains(e))
            throw new InvalidOperationException($"Already added {e} to this tab.");

        _itemsOnPage.Add(e);
        _camera.Add(e);

        e.Removed += (o, a) => RemoveItem((IRectAddable)o);

        ArrangeItems();
    }
    

    public void RemoveItem(IRectAddable e)
    {
        if (!_itemsOnPage.Contains(e))
            throw new InvalidOperationException($"Entity {e} is not on ScrollingListPanel but was tried to be removed.");

        _itemsOnPage.Remove(e);

        if (e.Parent != null)
            e.Remove();

        ArrangeItems();
    }
    

    public void ClearItems()
    {
        for (int i = _itemsOnPage.Count - 1; i >= 0; --i)
            _itemsOnPage[i].Remove();

        if (_itemsOnPage.Count > 0)
        {
            Log.Warning("ScrollingListPanel.ClearItems: items not all cleared successfully.");
            _itemsOnPage.Clear();
        }

        ArrangeItems();
    }
    

    public void ArrangeItems()
    {
        float y = 0;
        foreach (IRectAddable e in _itemsOnPage)
        {
            e.Y = y;
            y += e.H;
        }
        _sb.AmountFilled = Maths.Clamp(_camera.H / y, 0, 1);
    }
    
}