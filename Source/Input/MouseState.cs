namespace BearsEngine.Input;

public struct MouseState : IEquatable<MouseState>
{
    #region Constants
    internal const int MaxButtons = 16;
    
    #endregion

    #region Static
    private static void ValidateOffset(int offset)
    {
        if (offset < 0 || offset >= MaxButtons)
            throw new ArgumentOutOfRangeException("offset");
    }
    #endregion

    #region Fields
    private Point _positionAbsolute;
    private Point _positionScreen;
    private Point _scroll;
    ushort _buttons;
    #endregion

    #region Indexers
    public bool this[MouseButton button]
    {
        get { return IsButtonDown(button);}
        internal set
        {
            if (value)
                EnableBit((int)button);
            else
                DisableBit((int)button);
        }
    }
    #endregion

    #region Properties
    public bool IsConnected { get; internal set; }
    public int AbsX
    {
        get { return (int)Math.Round(_positionAbsolute.X);}
        set { _positionAbsolute.X = value; }
    }
    public int AbsY
    {
        get { return (int)Math.Round(_positionAbsolute.Y); }
        set { _positionAbsolute.Y = value; }
    }
    public int ScreenX
    {
        get { return (int)Math.Round(_positionScreen.X); }
        internal set { _positionScreen.X = value; }
    }
    public int ScreenY
    {
        get { return (int)Math.Round(_positionScreen.Y); }
        internal set { _positionScreen.Y = value; }
    }
    #endregion

    #region Methods
    #region Public
    #region IsButtonDown
    public bool IsButtonDown(MouseButton button)
    {
        return ReadBit((int)button);
    }
    #endregion

    #region IsButtonUp
    public bool IsButtonUp(MouseButton button)
    {
        return !ReadBit((int)button);
    }
    #endregion

    #region Wheel
    public int Wheel { get { return (int)Math.Round(_scroll.Y, MidpointRounding.AwayFromZero); } }
    #endregion

    #region WheelPrecise
    public float WheelPrecise { get { return _scroll.Y; } }
    #endregion

    #region WheelX
    public int WheelX { get { return (int)Math.Round(_scroll.X, MidpointRounding.AwayFromZero); } }
    #endregion

    #region Scroll
    public Point Scroll { get { return _scroll; } }
    #endregion
    #endregion

    #region Internal
    #region ReadBit
    internal bool ReadBit(int offset)
    {
        ValidateOffset(offset);
        return (_buttons & (1 << offset)) != 0;
    }
    #endregion

    #region EnableBit
    internal void EnableBit(int offset)
    {
        ValidateOffset(offset);
        _buttons |= unchecked((ushort)(1 << offset));
    }
    #endregion

    #region DisableBit
    internal void DisableBit(int offset)
    {
        ValidateOffset(offset);
        _buttons &= unchecked((ushort)(~(1 << offset)));
    }
    #endregion

    #region MergeBits
    internal void MergeBits(MouseState other)
    {
        _buttons |= other._buttons;
        _positionAbsolute += other._positionAbsolute;
        //_positionScreen = other._positionScreen;
        _scroll += other._scroll;
        IsConnected |= other.IsConnected;
    }
    #endregion

    #region SetScrollAbsolute
    internal void SetScrollAbsolute(float x, float y)
    {
        _scroll.X = x;
        _scroll.Y = y;
    }
    #endregion

    #region SetScrollRelative
    internal void SetScrollRelative(float x, float y)
    {
        _scroll.X += x;
        _scroll.Y += y;
    }
    #endregion
    #endregion
    #endregion

    #region IEquatable<MouseState>
    public bool Equals(MouseState other)
    {
        return
            _buttons == other._buttons &&
            _positionAbsolute == other._positionAbsolute &&
            _scroll == other._scroll;
    }
    #endregion

    #region Overloads/Overrides
    #region ==
    public static bool operator ==(MouseState left, MouseState right)
    {
        return left.Equals(right);
    }
    #endregion

    #region !=
    public static bool operator !=(MouseState left, MouseState right)
    {
        return !left.Equals(right);
    }
    #endregion

    #region Equals
    public override bool Equals(object obj)
    {
        if (obj is MouseState)
            return this == (MouseState)obj;
        else
            return false;
    }
    #endregion

    #region GetHashCode
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion

    #region ToString
    public override string ToString()
    {
        string buttons = Convert.ToString(_buttons, 2).PadLeft(MaxButtons, '0');
        string scroll = string.Format("[X={0:0.00},Y={1:0.00}]", _scroll.X, _scroll.Y);
        string connected = IsConnected ? "Connected" : "Disconnected";
        return string.Format("[X={0},Y={1},Scroll:{2},Buttons={3},{4}]", ScreenX, ScreenY, scroll, buttons, connected);
    }
    #endregion
    #endregion
}
