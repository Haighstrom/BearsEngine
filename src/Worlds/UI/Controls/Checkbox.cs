using HaighFramework;

namespace BearsEngine.Worlds
{
    public class Checkbox : Button
    {
        private IGraphic _tickGraphic;

        #region Constructors
        #region UITheme Based
        public Checkbox(int layer, IRect<float> r, string tickGraphic, UITheme theme, bool startTicked)
            : this(layer, r, theme.Button.DefaultColour, tickGraphic, theme, startTicked)
        {
        }

        public Checkbox(int layer, IRect<float> r, string boxGraphic, string tickGraphic, UITheme theme, bool startTicked)
            : this(layer, r, new Image(boxGraphic, r.Size), new Image(tickGraphic, r.Size), theme, startTicked)
        {
            DefaultColour = Colour.White; //ignore theme colour if we have an Image as graphic.
        }

        public Checkbox(int layer, IRect<float> r, Colour boxGraphic, string tickGraphic, UITheme theme, bool startTicked)
            : this(layer, r, new Image(theme.Button.DefaultColour, r.Size), new Image(tickGraphic, r.Size), theme, startTicked)
        {
            DefaultColour = boxGraphic; //ignore theme colour if graphic is a Colour
        }

        public Checkbox(int layer, IRect<float> r, IRectGraphic boxGraphic, IGraphic tickGraphic, UITheme theme, bool startTicked)
            : this(layer, r, boxGraphic, tickGraphic, startTicked)
        {
            DefaultColour = theme.Button.DefaultColour;
            UnclickableColour = theme.Button.UnclickableColour;
            HoverColour = theme.Button.HoverColour;
            PressedColour = theme.Button.PressedColour;
        }
        #endregion

        #region Standard
        public Checkbox(int layer, IRect<float> r, string boxGraphic, string tickGraphic, bool startTicked)
            : this(layer, r, new Image(boxGraphic, r.Size), new Image(tickGraphic, r.Size), startTicked)
        {
        }

        public Checkbox(int layer, IRect<float> r, Colour boxGraphic, string tickGraphic, bool startTicked)
            : this(layer, r, new Image(boxGraphic, r.Size), new Image(tickGraphic, r.Size), startTicked)
        {
            DefaultColour = boxGraphic; //ignore theme colour if colour is set
        }

        public Checkbox(int layer, IRect<float> r, IRectGraphic boxGraphic, IGraphic tickGraphic, bool startTicked)
            : base(layer, r.X, r.Y, r.W, r.H, boxGraphic)
        {
            Add(_tickGraphic = tickGraphic);
            _tickGraphic.Visible = startTicked;
        }
        #endregion
        #endregion

        #region Properties
        public bool IsChecked
        {
            get => _tickGraphic.Visible;
            set
            {
                if (_tickGraphic.Visible == value)
                    return;

                _tickGraphic.Visible = value;

                if (value)
                    Checked?.Invoke(this, EventArgs.Empty);
                else
                    Unchecked?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Methods
        public override void OnLeftDoubleClicked()
        {
            base.OnLeftDoubleClicked();
            OnLeftClicked();
        }
        public override void OnLeftClicked()
        {
            base.OnLeftClicked();
            IsChecked = !IsChecked;
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> Checked, Unchecked;
        #endregion
    }
}