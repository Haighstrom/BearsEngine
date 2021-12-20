using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using HaighFramework.Input;
using BearsEngine.Worlds;

namespace BearsEngine.Worlds
{
    public class ScrollingListPanel : Entity
    {
        private readonly List<IRectAddable> _itemsOnPage = new List<IRectAddable>();
        private Camera _camera;
        private Scrollbar _sb;

        public ScrollingListPanel(int layer, IRect<float> listPosition, int listToScrollbarGap, int scrollbarWidth, UITheme theme, int minIncrement = 0)
            : base(layer, new Rect(listPosition.X, listPosition.Y, listPosition.W + listToScrollbarGap + scrollbarWidth, listPosition.H))
        {
            Add(_camera = new Camera(1, listPosition.Zeroed, listPosition.Zeroed) { BackgroundColour = theme.ScrollingListPanel.PanelColour });
            Add(_sb = new Scrollbar(1, new Rect(listPosition.W + listToScrollbarGap, 0, scrollbarWidth, H), ScrollbarDirection.Vertical, theme));
            _sb.MinIncrement = minIncrement;
            _sb.BarPositionChanged += (s, a) => _camera.View.Y = a.MinAmount * _itemsOnPage.Last().Bottom;
        }

        #region AddItem
        public void AddItem(IRectAddable e)
        {
            if (_itemsOnPage.Contains(e))
                throw new HException("Already added {0} to this tab.", e);

            _itemsOnPage.Add(e);
            _camera.Add(e);

            e.Removed += (o, a) => RemoveItem((IRectAddable)o);

            ArrangeItems();
        }
        #endregion

        #region RemoveItem
        public void RemoveItem(IRectAddable e)
        {
            if (!_itemsOnPage.Contains(e))
                throw new HException("Entity {0} is not on ScrollingListPanel but was tried to be removed.", e);

            _itemsOnPage.Remove(e);

            if (e.Parent != null)
                e.Remove();

            ArrangeItems();
        }
        #endregion

        #region ClearItems
        public void ClearItems()
        {
            for (int i = _itemsOnPage.Count - 1; i >= 0; --i)
                _itemsOnPage[i].Remove();

            if (_itemsOnPage.Count > 0)
            {
                HConsole.Warning("ScrollingListPanel.ClearItems: items not all cleared successfully.");
                _itemsOnPage.Clear();
            }

            ArrangeItems();
        }
        #endregion

        #region ArrangeItems
        public void ArrangeItems()
        {
            float y = 0;
            foreach (IRectAddable e in _itemsOnPage)
            {
                e.Y = y;
                y += e.H;
            }
            _sb.AmountFilled = HF.Maths.Clamp(_camera.H / y, 0, 1);
        }
        #endregion
    }
}