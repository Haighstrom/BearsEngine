using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using HaighFramework.Input;
using BearsEngine.Worlds;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.Worlds
{
    public class Button : Entity
    {
        #region Constructors
        #region UITheme Based
        public Button(int layer, IRect<float> position, string graphic, UITheme theme, string text, Action? actionOnClicked = null)
            : this(layer, position, new Image(graphic, position.Size), theme, text, actionOnClicked)
        {
            DefaultColour = Colour.White; //ignore theme colour if we have an Image as graphic.
        }

        public Button(int layer, IRect<float> position, Colour graphic, UITheme theme, string text, Action? actionOnClicked = null)
            : this(layer, position, new Image(graphic, position.Size), theme, text, actionOnClicked)
        {
            DefaultColour = Colour.White; //ignore theme colour if we have an Image as graphic.
        }

        public Button(int layer, IRect<float> position, string graphic, UITheme theme, Action? actionOnClicked = null)
            : this(layer, position, new Image(graphic, position.Size), theme, actionOnClicked)
        {
            DefaultColour = Colour.White; //ignore theme colour if we have an Image as graphic.
        }

        public Button(int layer, IRect<float> position, Colour graphic, UITheme theme, Action? actionOnClicked = null)
            : this(layer, position, new Image(graphic, position.Size), theme, actionOnClicked)
        {
            DefaultColour = graphic; //ignore theme colour if colour is set
        }

        public Button(int layer, IRect<float> position, UITheme theme, Action? actionOnClicked = null)
            : this(layer, position, new Image(theme.Button.DefaultColour, position.Size), theme, actionOnClicked)
        {
        }

        public Button(int layer, IRectGraphic g, UITheme theme, Action? actionOnClicked = null)
            : this(layer, g, g, theme, actionOnClicked)
        {
        }

        public Button(int layer, IRect<float> position, IGraphic graphic, UITheme theme, Action? actionOnClicked = null)
            : this(layer, position.X, position.Y, position.W, position.H, graphic, null, null, null, actionOnClicked)
        {
            DefaultColour = theme.Button.DefaultColour;
            UnclickableColour = theme.Button.UnclickableColour;
            HoverColour = theme.Button.HoverColour;
            PressedColour = theme.Button.PressedColour;
        }

        public Button(int layer, IRect<float> position, IGraphic graphic, UITheme theme, string text, Action? actionOnClicked = null)
            : this(layer, position.X, position.Y, position.W, position.H, graphic, theme.Button.Text.Font, theme.Button.Text.FontColour, text, actionOnClicked)
        {
            DefaultColour = theme.Button.DefaultColour;
            UnclickableColour = theme.Button.UnclickableColour;
            HoverColour = theme.Button.HoverColour;
            PressedColour = theme.Button.PressedColour;
        }
        #endregion

        #region Standard
        public Button(int layer, IRect<float> r, string graphic, Action? actionOnClicked = null)
            : this(layer, r, new Image(graphic, r.Size), actionOnClicked)
        {
        }

        public Button(int layer, IRect<float> r, Colour graphic, Action? actionOnClicked = null)
            : this(layer, r, new Image(graphic, r.Size), actionOnClicked)
        {
            DefaultColour = graphic; //ignore theme colour if colour is set
        }

        public Button(int layer, IRect<float> r, IGraphic? graphic = null, Action? actionOnClicked = null)
            : this(layer, r.X, r.Y, r.W, r.H, graphic, null, null, null, actionOnClicked)
        {
        }

        public Button(int layer = 0, float x = 0, float y = 0, float w = 0, float h = 0, IGraphic? graphic = null, HFont? font = null, Colour? colour = null, string? text = null, Action? actionOnClicked = null)
            : base(layer, x, y, w, h)
        {
            if (graphic != null)
                Add(BackgroundGraphic = graphic);

            if (font != null && colour != null && text != null)
                Add(new HText(font, Zeroed, text)
                {
                    Colour = colour.Value,
                    Text = text,
                    HAlignment = HAlignment.Centred,
                    VAlignment = VAlignment.Centred
                });

            ActionOnClicked = actionOnClicked;
        }
        #endregion
        #endregion

        #region Properties
        protected IGraphic BackgroundGraphic { get; set; }

        public Action? ActionOnClicked { get; set; }

        public Colour DefaultColour { get; set; } = Colour.White;

        public Colour UnclickableColour { get; set; } = Colour.White;

        public Colour HoverColour { get; set; } = Colour.White;

        public Colour PressedColour { get; set; } = Colour.White;
        #endregion

        #region Methods
        #region SetDefaultAutoShadingColours
        public void SetDefaultAutoShadingColours()
        {
            DefaultColour = Colour.White;
            UnclickableColour = Colour.DarkGray;
            HoverColour = new Colour(255, 255, 125, 255);
            PressedColour = Colour.Yellow;
        }
        #endregion

        #region OnLeftPressed
        public override void OnLeftPressed()
        {
            base.OnLeftPressed();
            BackgroundGraphic.Colour = PressedColour;
        }
        #endregion

        #region OnLeftClicked
        public override void OnLeftClicked()
        {
            base.OnLeftClicked();
            ActionOnClicked?.Invoke();
            BackgroundGraphic.Colour = HoverColour;
        }
        #endregion

        #region OnHover
        public override void OnHover()
        {
            base.OnHover();
            BackgroundGraphic.Colour = HoverColour;
        }
        #endregion

        #region OnNoMouseEvent
        public override void OnNoMouseEvent()
        {
            base.OnNoMouseEvent();
            if (Clickable)
                BackgroundGraphic.Colour = DefaultColour;
            else
                BackgroundGraphic.Colour = UnclickableColour;
        }
        #endregion

        public void Press() => OnLeftClicked();
        #endregion
    }
}