namespace BearsEngine.Input;

public struct MouseState : IEquatable<MouseState>
{
    internal const int MaxButtons = 16;

    private static void ValidateOffset(int offset)
    {
        if (offset < 0 || offset >= MaxButtons)
            throw new ArgumentOutOfRangeException("offset");
    }
    

    private Point _positionAbsolute;
    private Point _positionScreen;
    private Point _scroll;
    ushort _buttons;
    
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
    
    public bool IsButtonDown(MouseButton button)
    {
        return ReadBit((int)button);
    }
    
    public bool IsButtonUp(MouseButton button)
    {
        return !ReadBit((int)button);
    }
    
    public int Wheel { get { return (int)Math.Round(_scroll.Y, MidpointRounding.AwayFromZero); } }
    
    public float WheelPrecise { get { return _scroll.Y; } }

    public int WheelX { get { return (int)Math.Round(_scroll.X, MidpointRounding.AwayFromZero); } }
    
    public Point Scroll { get { return _scroll; } }
    
    
    internal bool ReadBit(int offset)
    {
        ValidateOffset(offset);
        return (_buttons & (1 << offset)) != 0;
    }
    
    internal void EnableBit(int offset)
    {
        ValidateOffset(offset);
        _buttons |= unchecked((ushort)(1 << offset));
    }
    
    internal void DisableBit(int offset)
    {
        ValidateOffset(offset);
        _buttons &= unchecked((ushort)(~(1 << offset)));
    }
    
    internal void MergeBits(MouseState other)
    {
        _buttons |= other._buttons;
        _positionAbsolute += other._positionAbsolute;
        //_positionScreen = other._positionScreen;
        _scroll += other._scroll;
        IsConnected |= other.IsConnected;
    }

    internal void SetScrollAbsolute(float x, float y)
    {
        _scroll.X = x;
        _scroll.Y = y;
    }
    
    internal void SetScrollRelative(float x, float y)
    {
        _scroll.X += x;
        _scroll.Y += y;
    }
    
    public bool Equals(MouseState other)
    {
        return
            _buttons == other._buttons &&
            _positionAbsolute == other._positionAbsolute &&
            _scroll == other._scroll;
    }
    
    public static bool operator ==(MouseState left, MouseState right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(MouseState left, MouseState right)
    {
        return !left.Equals(right);
    }
    
    public override bool Equals(object obj)
    {
        if (obj is MouseState)
            return this == (MouseState)obj;
        else
            return false;
    }
    
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    
    public override string ToString()
    {
        string buttons = Convert.ToString(_buttons, 2).PadLeft(MaxButtons, '0');
        string scroll = string.Format("[X={0:0.00},Y={1:0.00}]", _scroll.X, _scroll.Y);
        string connected = IsConnected ? "Connected" : "Disconnected";
        return string.Format("[X={0},Y={1},Scroll:{2},Buttons={3},{4}]", ScreenX, ScreenY, scroll, buttons, connected);
    }
    
    
}
