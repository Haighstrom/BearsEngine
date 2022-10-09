using System.Collections.ObjectModel;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.UI
{
    public class Tab : Entity
    {
        private readonly IGraphic _activatedGraphic, _deactivatedGraphic;
        private readonly List<IAddable> _itemsOnPage = new();
        protected HText _title;
        

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
        

        public ReadOnlyCollection<IAddable> Items => _itemsOnPage.AsReadOnly();

        public TabbedPanel TPParent { get; internal set; }

        public void AddItem(IAddable a)
        {
            if (_itemsOnPage.Contains(a))
                throw new Exception("Already added {a} to this tab.");

            _itemsOnPage.Add(a);
            TPParent?.Panel.Add(a);
            a.Removed += (o, e) => { _itemsOnPage.Remove((IAddable)o); };
        }
        

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
        
        
    }
}