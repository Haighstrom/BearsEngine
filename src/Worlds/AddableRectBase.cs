using HaighFramework;

namespace BearsEngine.Worlds
{
    public class AddableRectBase : AddableBase, IRectAddable
    {
        private float _x, _y, _w, _h;

        #region Constructors
        public AddableRectBase(IRect<float> r)
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
        public IPoint<float> P
        {
            get => new Point<float>(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        #endregion

        #region R
        public IRect<float> R
        {
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
        public IPoint<float> Size => new Point(W, H);
        public float SmallestSide => Math.Min(W, H);
        public float BiggestSide => Math.Max(W, H);
        public IPoint<float> TopLeft => P;
        public IPoint<float> TopCentre => new Point(X + W * 0.5f, Y);
        public IPoint<float> TopRight => new Point(X + W, Y);
        public IPoint<float> CentreLeft => new Point(X, Y + H * 0.5f);
        public IPoint<float> Centre => new Point(X + W * 0.5f, Y + H * 0.5f);
        public IPoint<float> CentreRight => new Point(X + W, Y + H * 0.5f);
        public IPoint<float> BottomLeft => new Point(X, Y + H);
        public IPoint<float> BottomCentre => new Point(X + W * 0.5f, Y + H);
        public IPoint<float> BottomRight => new Point(X + W, Y + H);
        #endregion

        #region Zeroed/Shift/Scale/Resize/Grow
        public IRect<float> Zeroed => new Rect(0, 0, W, H);
        public IRect<float> Shift(IPoint<float> direction, float distance) => Shift(direction.Normal.Multiply(distance));
        public IRect<float> Shift(IPoint<float> direction) => Shift(direction.X, direction.Y);
        public IRect<float> Shift(float x, float y = 0, float w = 0, float h = 0) => new Rect(X + x, Y + y, W + w, H + h);
        public IRect<float> Scale(float scaleX, float scaleY) => new Rect(X, Y, W * scaleX, H * scaleY);
        public IRect<float> ScaleAroundCentre(float scale) => ScaleAroundCentre(scale, scale);
        public IRect<float> ScaleAroundCentre(float scaleX, float scaleY) => ScaleAround(scaleX, scaleY, Centre.X, Centre.Y);
        public IRect<float> ScaleAround(float scaleX, float scaleY, float originX, float originY) => ResizeAround(W * scaleX, H * scaleY, originX, originY);
        public virtual IRect<float> Resize(float newW, float newH) => new Rect(X, Y, newW, newH);

        #region ResizeAround

        public IRect<float> ResizeAround(float newW, float newH, IPoint<float> origin) => ResizeAround(newW, newH, origin.X, origin.Y);
        public IRect<float> ResizeAround(float newW, float newH, float originX, float originY)
            => new Rect(
                originX - newW * (originX - X) / W,
                originY - newH * (originY - Y) / H,
                newW, newH);
        #endregion

        public IRect<float> Grow(float margin) => Grow(margin, margin, margin, margin);
        public IRect<float> Grow(float left, float up, float right, float down) => new Rect(X - left, Y - up, W + left + right, H + up + down);
        #endregion

        #region Intersects/Intersection/Contains/IsContainedBy
        #region Intersects
        public bool Intersects(IRect<float> r, bool touchingCounts = false)
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
        public IRect<float> Intersection(IRect<float> r)
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
        public bool Contains(IRect<float> r) => X <= r.X && Y <= r.Y && Right >= r.Right && Bottom >= r.Bottom;

        public bool Contains(float x, float y, float w, float h) => X <= x && Y <= y && X + H >= x + w && Y + H >= y + h;

        public bool Contains(int x, int y, int w, int h) => X <= x && Y <= y && X + W >= x + w && Y + H >= y + h;

        public bool Contains(IPoint<float> p, bool onLeftAndTopEdgesCount = true, bool onRightAndBottomEdgesCount = false)
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
        public bool IsContainedBy(IRect<float> r) => X >= r.X && Y >= r.Y && X + W <= r.X + r.W && Y + H <= r.Y + r.H;

        public bool IsContainedBy(float x, float y, float w, float h) => X >= x && Y >= y && X + H <= x + h && Y + H <= y + h;
        #endregion
        #endregion

        #region ToVertices
        public List<IPoint<float>> ToVertices() => new List<IPoint<float>>() { TopLeft, TopRight, BottomRight, BottomLeft };
        #endregion

        #region IEquatable<IRect>
        public bool Equals(IRect<float>? other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            
            return X == other.X && Y == other.Y && W == other.W && H == other.H;
        }
        #endregion
        #endregion

        protected virtual void OnPositionChanged() => PositionChanged?.Invoke(this, new PositionChangedArgs(this));
        protected virtual void OnSizeChanged(ResizeEventArgs args) => SizeChanged?.Invoke(this, args);

        public event EventHandler<PositionChangedArgs>? PositionChanged;
        public event EventHandler<ResizeEventArgs>? SizeChanged;
    }
}