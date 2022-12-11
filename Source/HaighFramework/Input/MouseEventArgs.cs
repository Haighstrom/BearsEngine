using HaighFramework.Win32API;

namespace BearsEngine.Input;

public class MouseEventArgs : EventArgs
{
    protected MouseState _state;

    public MouseEventArgs()
    {
        _state.IsConnected = true;
    }

    public MouseEventArgs(int x, int y)
        : this()
    {
        POINT p = new();
        User32.GetCursorPos(ref p);
        _state.ScreenX = p.X;
        _state.ScreenY = p.Y;
        _state.AbsX = x;
        _state.AbsY = y;
    }

    public MouseEventArgs(int x, int y, int xDelta, int yDelta)
        : this(x, y)
    {
        DeltaX = xDelta;
        DeltaY = yDelta;
    }
    

    public MouseState State { get { return _state; } internal set { _state = value;} }
    public int ScreenX { get { return State.ScreenX; } }
    public int ScreenY { get { return State.ScreenY; } }
    public Point ScreenXY { get { return new Point(ScreenX, ScreenY); } }
    public int AbsX { get { return State.AbsX; } }
    public int AbsY { get { return State.AbsY; } }
    public Point AbsXY { get { return new Point(AbsX, AbsY); } }
    public int DeltaX { get; set; }
    public int DeltaY { get; set; }
    public Point DeltaXY { get { return new Point(DeltaX, DeltaY); } }
    public MouseButton Button { get; internal set; }
    public bool Pressed { get { return GetButton(Button) == ButtonState.Down; } set { SetButton(Button, value ? ButtonState.Down : ButtonState.Up); } }
    

    internal void SetButton(MouseButton button, ButtonState state)
    {
        if (button < 0 || button > MouseButton.LastButton)
            throw new ArgumentOutOfRangeException();

        switch (state)
        {
            case ButtonState.Down:
                State.EnableBit((int)button);
                break;
            case ButtonState.Up:
                State.DisableBit((int)button);
                break;
        }
    }
    internal void SetButton(MouseButton button, bool pressed)
    {
        if (button < 0 || button > MouseButton.LastButton)
            throw new ArgumentOutOfRangeException();

        if (pressed)
            State.EnableBit((int)button);
        else
            State.DisableBit((int)button);
    }
    internal ButtonState GetButton(MouseButton button)
    {
        if (button < 0 || button > MouseButton.LastButton)
            throw new ArgumentOutOfRangeException();

        return State.ReadBit((int)button) ? ButtonState.Down : ButtonState.Up;
    }
    

}

public class MouseWheelEventArgs : MouseEventArgs
{
    float _delta;

    public MouseWheelEventArgs() { }
    public MouseWheelEventArgs(int x, int y, int value, int delta)
        : base(x, y)
    {
        _state.SetScrollAbsolute(_state.Scroll.X, value);
        _delta = delta;
    }
    

    public int Value { get { return _state.Wheel; } }

    public int Delta { get { return (int)Math.Round(_delta, MidpointRounding.AwayFromZero); } }

    public float ValuePrecise { get { return _state.WheelPrecise; } }

    public float DeltaPrecise { get { return _delta; } internal set { _delta = value; } }
    
}
