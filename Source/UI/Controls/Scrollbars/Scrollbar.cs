namespace BearsEngine.UI;

public class Scrollbar : Entity
{
    private class Bar : Button
    {
        private readonly ScrollbarDirection _direction;
        private Rect _fullPosition;
        private float _amountFilled;
        private bool _dragging;
        private float _dragStart;
        

        public Bar(ScrollbarDirection direction, Rect position, int border, UITheme theme)
            : this(direction, position, border, theme.Scrollbar.Bar.DefaultColour, theme.Scrollbar.Bar.HoverColour, theme.Scrollbar.Bar.PressedColour, theme.Scrollbar.Bar.UnclickableColour)
        {
        }

        public Bar(ScrollbarDirection direction, Rect position, int border, Colour barColour, Colour hoverColour, Colour pressedColour, Colour unclickableColour)
            : base(1, position)
        {
            HoverColour = hoverColour;
            PressedColour = pressedColour;
            UnclickableColour = unclickableColour;

            _direction = direction;
            _fullPosition = position;

            Rect r = direction == ScrollbarDirection.Horizontal ?
                new Rect(0, border, W, H - 2 * border) :
                new Rect(border, 0, W - 2 * border, H);

            Add(BackgroundGraphic = new Image(barColour, r));
        }
        

        public float AmountFilled
        {
            get => _amountFilled;
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException($"value should be [0,1]:{value}");

                _amountFilled = value;

                RecalculateSizes();
            }
        }
        

        public float MinIncrement { get; set; } = 0;

        public float MinAmount => _direction == ScrollbarDirection.Horizontal ? (X - _fullPosition.X) / _fullPosition.W : (Y - _fullPosition.Y) / _fullPosition.H;

        public float MaxAmount => _direction == ScrollbarDirection.Horizontal ? (R.Right - _fullPosition.X) / _fullPosition.W : (R.Bottom - _fullPosition.Y) / _fullPosition.H;

        internal Rect FullPosition
        {
            get => _fullPosition;
            set
            {
                _fullPosition = value;
                X = _fullPosition.X;
                Y = _fullPosition.Y;
                W = _fullPosition.W;
                H = _fullPosition.H;

                RecalculateSizes();
            }
        }
        

        protected override void OnLeftPressed()
        {
            base.OnLeftPressed();

            _dragging = true;

            if (_direction == ScrollbarDirection.Horizontal)
                _dragStart = Mouse.ClientX - X;
            else
                _dragStart = Mouse.ClientY - Y;
        }

        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (_dragging && Mouse.LeftUp)
                _dragging = false;

            if (_dragging)
                if (_direction == ScrollbarDirection.Horizontal)
                {
                    var oldX = X;

                    X = Maths.Clamp(Mouse.ClientX - _dragStart, _fullPosition.X, _fullPosition.Right - W);

                    if (MinIncrement > 0 && R.Right != _fullPosition.Right)
                        X -= Maths.Mod(X - _fullPosition.Left, MinIncrement);

                    if (oldX != X)
                        BarPositionChanged?.Invoke(this, new ScrollbarPositionArgs(MinAmount, MaxAmount));
                }
                else
                {
                    var oldY = Y;

                    Y = Maths.Clamp(Mouse.ClientY - _dragStart, _fullPosition.Y, _fullPosition.Bottom - H);

                    if (MinIncrement > 0 && R.Bottom != _fullPosition.Bottom)
                        Y -= Maths.Mod(Y - _fullPosition.Top, MinIncrement);

                    if (oldY != Y)
                        BarPositionChanged?.Invoke(this, new ScrollbarPositionArgs(MinAmount, MaxAmount));
                }
        }
        

        internal void RecalculateSizes()
        {
            if (_direction == ScrollbarDirection.Horizontal)
            {
                ((IRectGraphic)BackgroundGraphic).W = W = _fullPosition.W * _amountFilled;
                X = Maths.Clamp(X, _fullPosition.X, _fullPosition.Right - W);
            }
            else
            {
                ((IRectGraphic)BackgroundGraphic).H = H = _fullPosition.H * _amountFilled;
                Y = Maths.Clamp(Y, _fullPosition.Y, _fullPosition.Bottom - H);
            }
        }
        

        public void MoveBy(float amount)
        {
            if (_direction == ScrollbarDirection.Horizontal)
            {
                var oldX = X;

                X = Maths.Clamp(X + amount, _fullPosition.X, _fullPosition.Right - W);

                if (MinIncrement > 0 && R.Right != _fullPosition.Right)
                    X -= Maths.Mod(X - _fullPosition.Left, MinIncrement);

                if (oldX != X)
                    BarPositionChanged?.Invoke(this, new ScrollbarPositionArgs(MinAmount, MaxAmount));
            }
            else
            {
                var oldY = Y;

                Y = Maths.Clamp(Y + amount, _fullPosition.Y, _fullPosition.Bottom - H);

                if (MinIncrement > 0 && R.Bottom != _fullPosition.Bottom)
                    Y -= Maths.Mod(Y - _fullPosition.Top, MinIncrement);

                if (oldY != Y)
                    BarPositionChanged?.Invoke(this, new ScrollbarPositionArgs(MinAmount, MaxAmount));
            }
        }
        
        

        public event EventHandler<ScrollbarPositionArgs> BarPositionChanged;
        
    }
    

    private readonly ScrollbarDirection _direction;
    private readonly Image _barBG;
    private readonly Bar _bar;
    private readonly Button _minus, _plus;
    private readonly Action<int> _actionOnMove;
    

    public Scrollbar(float layer, Rect r, ScrollbarDirection direction, UITheme theme)
        : this(layer,
              r,
              direction,
              theme.Scrollbar.BarBackgroundColour,
              theme.Scrollbar.Bar.DefaultColour,
              theme.Scrollbar.Bar.HoverColour,
              theme.Scrollbar.Bar.PressedColour,
              theme.Scrollbar.Bar.UnclickableColour,
              theme.Scrollbar.EdgeToBarSpace,
              theme.Scrollbar.ButtonBackgroundColour,
              OpenGL.GenTrianglePolygon(new Rect(r.SmallestSide, r.SmallestSide), theme.Scrollbar.EdgeToArrowSpace, direction == ScrollbarDirection.Horizontal ? Direction.Left : Direction.Up, theme.Scrollbar.Arrow.DefaultColour),
              OpenGL.GenTrianglePolygon(new Rect(r.SmallestSide, r.SmallestSide), theme.Scrollbar.EdgeToArrowSpace, direction == ScrollbarDirection.Horizontal ? Direction.Right : Direction.Down, theme.Scrollbar.Arrow.DefaultColour),
              theme.Scrollbar.Arrow.HoverColour,
              theme.Scrollbar.Arrow.PressedColour,
              theme.Scrollbar.Arrow.UnclickableColour)
    {
    }
    

    public Scrollbar(float layer, Rect fullPosition, ScrollbarDirection direction, Colour barBackgroundColour, Colour barDefaultColour, Colour barHoverColour, Colour barPressedColour, Colour barUnclickableButton, int edgeToBarSpace, Colour arrowBG, IGraphic minusArrow, IGraphic plusArrow, Colour arrowHoverColour, Colour arrowPressedColour, Colour arrowUnclickableColour)
        : base(layer, fullPosition)
    {
        _direction = direction;

        Rect sbBG, b1, b2;

        if (direction == ScrollbarDirection.Horizontal)
        {
            sbBG = new Rect(fullPosition.H, 0, fullPosition.W - 2 * fullPosition.H, fullPosition.H);
            b1 = new Rect(0, 0, H, H);
            b2 = new Rect(W - H, 0, H, H);
        }
        else
        {
            sbBG = new Rect(0, fullPosition.W, fullPosition.W, fullPosition.H - 2 * fullPosition.W);
            b1 = new Rect(0, 0, W, W);
            b2 = new Rect(0, H - W, W, W);
        }

        Add(_barBG = new Image(barBackgroundColour, sbBG));

        Add(_bar = new Bar(direction, sbBG, edgeToBarSpace, barDefaultColour, barHoverColour, barPressedColour, barUnclickableButton));
        _bar.BarPositionChanged += (sender, args) => BarPositionChanged(sender, args);

        Add(_minus = new Button(1, b1, minusArrow)
        {
            HoverColour = arrowHoverColour,
            PressedColour = arrowPressedColour,
            UnclickableColour = arrowUnclickableColour,
        });
        _minus.Add(new Entity(2, b1.Zeroed, arrowBG));

        Add(_plus = new Button(1, b2, plusArrow)
        {
            HoverColour = arrowHoverColour,
            PressedColour = arrowPressedColour,
            UnclickableColour = arrowUnclickableColour,
        });
        _plus.Add(new Entity(2, b2.Zeroed, arrowBG));
    }
    
    

    public float AmountFilled
    {
        get => _bar.AmountFilled;
        set => _bar.AmountFilled = value;
    }

    public float MinIncrement
    {
        get => _bar.MinIncrement;
        set => _bar.MinIncrement = value;
    }
    
    protected override void OnLeftPressed()
    {
        base.OnLeftClicked();

        //todo: clicking on background not bar moves bar

        //if (!_bar.MouseIntersecting && !_minus.MouseIntersecting && !_plus.MouseIntersecting)
        //{
        //    if (_direction == ScrollbarDirection.Horizontal)
        //        _bar.CurrentInterval += Math.Sign(HI.MouseWindowX - _bar.WindowPosition.Centre.X);
        //    else
        //        _bar.CurrentInterval += Math.Sign(HI.MouseWindowY - _bar.WindowPosition.Centre.Y);
        //}
    }

    public void Resize(float length)
    {
        if (_direction == ScrollbarDirection.Horizontal)
        {
            W = length;
            _barBG.W = W - 2 * _barBG.H;
            _plus.X = W - _plus.W;
            _bar.FullPosition = new Rect(_bar.FullPosition.H, 0, length - 2 * _bar.FullPosition.H, _bar.FullPosition.H);
        }
        else
        {
            H = length;
            _barBG.H = H - 2 * _barBG.W;
            _plus.Y = H - _plus.H;
            _bar.FullPosition = new Rect(0, _bar.FullPosition.W, _bar.FullPosition.W, length - 2 * _bar.FullPosition.W);
        }
    }
    

    /// <summary>
    /// Move the scrollbar by a specified amount.
    /// </summary>
    public void MoveBar(float amount)
    {
        _bar.MoveBy(amount);
    }
    

    public event EventHandler<ScrollbarPositionArgs> BarPositionChanged;
    
}
