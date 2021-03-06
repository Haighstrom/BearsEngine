namespace BearsEngine.Worlds
{
    public class AddableRectBase : AddableBase, IRectAddable
    {
        private float _x, _y, _w, _h;

        #region Constructors
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
        #endregion

        #region IRect
        #region X/Y/W/H/P/R
        #region X
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
        #endregion

        #region Y
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
        #endregion

        #region W
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
        #endregion

        #region H
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
        #endregion

        #region P
        public Point P
        {
            get => new(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        #endregion

        #region R
        public IRect R
        {
            get => new Rect(X, Y, W, H);
            set
            {
                X = value.X;
                Y = value.Y;
                W = value.W;
                H = value.H;
            }
        }
        #endregion
        #endregion

        #region Left/../Bottom/Area/Size/SmallestSide/TopLeft/../BottomRight
        public float Left => X;
        public float Right => X + W;
        public float Top => Y;
        public float Bottom => Y + H;
        public float Area => W * H;
        public Point Size => new(W, H);
        public float SmallestSide => Math.Min(W, H);
        public float BiggestSide => Math.Max(W, H);
        public Point TopLeft => P;
        public Point TopCentre => new(X + W * 0.5f, Y);
        public Point TopRight => new(X + W, Y);
        public Point CentreLeft => new(X, Y + H * 0.5f);
        public Point Centre => new(X + W * 0.5f, Y + H * 0.5f);
        public Point CentreRight => new(X + W, Y + H * 0.5f);
        public Point BottomLeft => new(X, Y + H);
        public Point BottomCentre => new(X + W * 0.5f, Y + H);
        public Point BottomRight => new(X + W, Y + H);
        #endregion

        #region Zeroed/Shift/Scale/Resize/Grow
        public IRect Zeroed => new Rect(0, 0, W, H);
        public IRect Shift(Point direction, float distance) => Shift(direction.Normal * distance);
        public IRect Shift(Point direction) => Shift(direction.X, direction.Y);
        public IRect Shift(float x, float y = 0, float w = 0, float h = 0) => new Rect(X + x, Y + y, W + w, H + h);
        public IRect Scale(float scaleX, float scaleY) => new Rect(X, Y, W * scaleX, H * scaleY);
        public IRect ScaleAroundCentre(float scale) => ScaleAroundCentre(scale, scale);
        public IRect ScaleAroundCentre(float scaleX, float scaleY) => ScaleAround(scaleX, scaleY, Centre.X, Centre.Y);
        public IRect ScaleAround(float scaleX, float scaleY, float originX, float originY) => ResizeAround(W * scaleX, H * scaleY, originX, originY);
        public virtual IRect Resize(float newW, float newH) => new Rect(X, Y, newW, newH);

        #region ResizeAround

        public IRect ResizeAround(float newW, float newH, Point origin) => ResizeAround(newW, newH, origin.X, origin.Y);
        public IRect ResizeAround(float newW, float newH, float originX, float originY)
            => new Rect(
                originX - newW * (originX - X) / W,
                originY - newH * (originY - Y) / H,
                newW, newH);
        #endregion

        public IRect Grow(float margin) => Grow(margin, margin, margin, margin);
        public IRect Grow(float left, float up, float right, float down) => new Rect(X - left, Y - up, W + left + right, H + up + down);
        #endregion

        #region Intersects/Intersection/Contains/IsContainedBy
        #region Intersects
        public bool Intersects(IRect r, bool touchingCounts = false)
        {
            if (touchingCounts)
                return
                    Left <= r.Right &&
                    Right >= r.Left &&
                    Top <= r.Bottom &&
                    Bottom >= r.Top;
            else
                return
                    Left < r.Right &&
                    Right > r.Left &&
                    Top < r.Bottom &&
                    Bottom > r.Top;
        }
        #endregion

        #region Intersection
        public IRect Intersection(IRect r)
        {
            Rect answer = new();

            if (!Intersects(r))
                return answer;

            answer.X = Math.Max(Left, r.Left);
            answer.Y = Math.Max(Top, r.Top);
            answer.W = Math.Min(Right, r.Right) - answer.X;
            answer.H = Math.Min(Bottom, r.Bottom) - answer.Y;

            return answer;
        }
        #endregion

        #region Contains
        public bool Contains(IRect r) => X <= r.X && Y <= r.Y && Right >= r.Right && Bottom >= r.Bottom;

        public bool Contains(float x, float y, float w, float h) => X <= x && Y <= y && X + H >= x + w && Y + H >= y + h;

        public bool Contains(int x, int y, int w, int h) => X <= x && Y <= y && X + W >= x + w && Y + H >= y + h;

        public bool Contains(Point p, bool onLeftAndTopEdgesCount = true, bool onRightAndBottomEdgesCount = false)
        {
            if (onLeftAndTopEdgesCount && onRightAndBottomEdgesCount)
                return X <= p.X && Y <= p.Y && X + W >= p.X && Y + H >= p.Y;
            else if (onLeftAndTopEdgesCount && !onRightAndBottomEdgesCount)
                return X <= p.X && Y <= p.Y && X + W > p.X && Y + H > p.Y;
            else if (!onLeftAndTopEdgesCount && onRightAndBottomEdgesCount)
                return X < p.X && Y < p.Y && X + W >= p.X && Y + H >= p.Y;
            else
                return X < p.X && Y < p.Y && X + W > p.X && Y + H > p.Y;
        }

        public bool Contains(float x, float y, bool onLeftAndTopEdgesCount = true, bool onRightAndBottomEdgesCount = false)
        {
            if (onLeftAndTopEdgesCount && onRightAndBottomEdgesCount)
                return X <= x && Y <= y && X + W >= x && Y + H >= y;
            else if (onLeftAndTopEdgesCount && !onRightAndBottomEdgesCount)
                return X <= x && Y <= y && X + W > x && Y + H > y;
            else if (!onLeftAndTopEdgesCount && onRightAndBottomEdgesCount)
                return X < x && Y < y && X + W >= x && Y + H >= y;
            else
                return X < x && Y < y && X + W > x && Y + H > y;
        }
        #endregion

        #region IsContainedBy
        public bool IsContainedBy(IRect r) => X >= r.X && Y >= r.Y && X + W <= r.X + r.W && Y + H <= r.Y + r.H;

        public bool IsContainedBy(float x, float y, float w, float h) => X >= x && Y >= y && X + H <= x + h && Y + H <= y + h;
        #endregion
        #endregion

        #region ToVertices
        public List<Point> ToVertices() => new() { TopLeft, TopRight, BottomRight, BottomLeft };
        #endregion

        #region IEquatable<IRect>
        public bool Equals(IRect? other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return X == other?.X && Y == other?.Y && W == other?.W && H == other?.H;
        }
        #endregion
        #endregion

        protected virtual void OnPositionChanged() => PositionChanged?.Invoke(this, new PositionChangedArgs(this));
        protected virtual void OnSizeChanged(ResizeEventArgs args) => SizeChanged?.Invoke(this, args);

        public event EventHandler<PositionChangedArgs>? PositionChanged;
        public event EventHandler<ResizeEventArgs>? SizeChanged;
    }
}