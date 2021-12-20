using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using HaighFramework.Input;
using BearsEngine.Worlds;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.Worlds
{
    public class PagedPanel : Entity
    {
        #region Fields
        private List<Entity> _pages;
        private int _page;
        private HText _pageNumText;

        private Button _minusArrow, _plusArrow;
        #endregion

        #region Constructors
        public PagedPanel(int layer, UITheme uiTheme, IRect<float> textPosition, IRect<float> arrow1Pos, IRect<float> arrow2Pos, string arrow1GFX, string arrow2GFX, List<Entity> pages)
            : base(layer)
        {
            if (pages.Count == 0)
                throw new HException("Tried to craete a PagedPanel with no pages");

            Add(_pageNumText = new HText(uiTheme, textPosition, ""));

            _page = 0;
            _pages = pages;

            _pageNumText.Text = "1 / " + _pages.Count;

            for (int i = 0; i < pages.Count; ++i)
            {
                Add(pages[i]);
                if (i != 0)
                {
                    pages[i].Active = false;
                    pages[i].Visible = false;
                }
            }

            Add(_minusArrow = new Button(layer, arrow1Pos, arrow1GFX, () => ChangePage(-1)));
            _minusArrow.SetDefaultAutoShadingColours();
            Add(_plusArrow = new Button(layer, arrow2Pos, arrow2GFX, () => ChangePage(1)));
            _plusArrow.SetDefaultAutoShadingColours();
        }
        #endregion

        #region Methods
        #region ChangePage
        private void ChangePage(int delta)
        {
            int newpage = HF.Maths.Mod(_page + delta, _pages.Count);

            if (_page == newpage)
                return;

            _pages[_page].Active = false;
            _pages[_page].Visible = false;

            _pages[newpage].Active = true;
            _pages[newpage].Visible = true;
 
            _page = newpage;

            _pageNumText.Text = _page + 1 + " / " + _pages.Count;
        }
        #endregion
        #endregion
    }
}