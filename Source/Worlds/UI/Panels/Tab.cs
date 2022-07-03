using System.Collections.ObjectModel;
using BearsEngine.Worlds.Graphics.Text;
using BearsEngine.Worlds.UI.UIThemes;

namespace BearsEngine.Worlds.UI.Panels
{
    public class Tab : Entity
    {
        #region Fields
        private readonly IGraphic _activatedGraphic, _deactivatedGraphic;
        private readonly List<IAddable> _itemsOnPage = new();
        protected HText _title;
        #endregion

        #region Constructors
        public Tab(Point size, string activatedGFX, string deactivatedGFX, UITheme theme, string tabText, int tabBorder = 0, HAlignment ha = HAlignment.Centred, VAlignment va = VAlignment.Centred)
            : this(size, activatedGFX, deactivatedGFX)
        {
            Add(_title = new HText(theme, new Rect(tabBorder, tabBorder, size.X - tabBorder * 2, size.Y - tabBorder * 2), tabText));
        }

        public Tab(Point size, string tabGFX, UITheme theme, string tabText, int tabBorder = 0, HAlignment ha = HAlignment.Centred, VAlignment va = VAlignment.Centred)
            : this(size, tabGFX)
        {
            Add(_title = new HText(theme, new Rect(tabBorder, tabBorder, size.X - tabBorder * 2, size.Y - tabBorder * 2), tabText));
        }

        public Tab(Point size, string tabGFX)
            : this(size, tabGFX, tabGFX)
        {
        }

        public Tab(Point size, string activatedGFX, string deactivatedGFX)
            : base(2, size)
        {
            Add(_activatedGraphic = new Image(activatedGFX, Size) { Visible = false });
            Add(_deactivatedGraphic = new Image(deactivatedGFX, Size));
        }
        #endregion

        public ReadOnlyCollection<IAddable> Items => _itemsOnPage.AsReadOnly();

        public TabbedPanel TPParent { get; internal set; }

        #region Methods
        #region AddItem
        public void AddItem(IAddable a)
        {
            if (_itemsOnPage.Contains(a))
                throw new HException("Already added {0} to this tab.", a);

            _itemsOnPage.Add(a);
            TPParent?.Panel.Add(a);
            a.Removed += (o, e) => { _itemsOnPage.Remove((IAddable)o); };
        }
        #endregion

        #region Activate
        protected internal virtual void Activate()
        {
            _activatedGraphic.Visible = true;
            _deactivatedGraphic.Visible = false;

            foreach (var a in _itemsOnPage)
            {
                if (a is IUpdatable u)
                    u.Active = true;

                if (a is IRenderable r)
                    r.Visible = true;
            }

            Layer = 0;
        }
        #endregion

        #region Deactivate
        protected internal void Deactivate()
        {
            _activatedGraphic.Visible = false;
            _deactivatedGraphic.Visible = true;

            foreach (var a in _itemsOnPage)
            {
                if (a is IUpdatable u)
                    u.Active = false;

                if (a is IRenderable r)
                    r.Visible = false;
            }

            Layer = 2;
        }
        #endregion
        #endregion
    }
}