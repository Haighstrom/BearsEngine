namespace BearsEngine.Worlds
{
    public abstract class AddableRectBase : AddableBase, IRectAddable
    {
        private float _x, _y, _w, _h;

        public AddableRectBase(IRect r)
            : this(r.X, r.Y, r.W, r.H)
        {
        }

        public AddableRectBase(float x, float y, float w, float h)
        {
            _x = x;
            _y = y;
            _w = w;
            _h = h;
        }

        public virtual float X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPositionChanged();
                }
            }
        }

        public virtual float Y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPositionChanged();
                }
            }
        }

        public virtual float W
        {
            get => _w;
            set
            {
                if (_w != value)
                {
                    var oldSize = Size;
                    _w = value;
                    OnSizeChanged(new ResizeEventArgs(oldSize, Size));
                }
            }
        }

        public virtual float H
        {
            get => _h;
            set
            {
                if (_h != value)
                {
                    var oldSize = Size;
                    _h = value;
                    OnSizeChanged(new ResizeEventArgs(oldSize, Size));
                }
            }
        }

        public Point P
        {
            get => new(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public float Left => X;
        public float Right => X + W;
        public float Top => Y;
        public float Bottom => Y + H;
        public Point Size => new(W, H);
        public Point Centre => new(X + W * 0.5f, Y + H * 0.5f);

        public bool Equals(IRect? other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return X == other?.X && Y == other?.Y && W == other?.W && H == other?.H;
        }
        public void SetPosition(float newX, float newY, float newW, float newH)
        {
            X = newX;
            Y = newY;
            W = newW;
            H = newH;
        }
        public void SetPosition(IRect newPosition)
        {
            X = newPosition.X;
            Y = newPosition.Y;
            W = newPosition.W;
            H = newPosition.H;
        }

        protected virtual void OnPositionChanged() => PositionChanged?.Invoke(this, new PositionChangedArgs(new Rect(X, Y, W, H)));
        protected virtual void OnSizeChanged(ResizeEventArgs args) => SizeChanged?.Invoke(this, args);

        public event EventHandler<PositionChangedArgs>? PositionChanged;
        public event EventHandler<ResizeEventArgs>? SizeChanged;
    }
}