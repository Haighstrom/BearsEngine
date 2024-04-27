namespace BearsEngine.Input;

internal class MouseWheelMovedEventArgs : EventArgs
{
    public MouseWheelMovedEventArgs(float amountMoved, float position)
    {
        AmountMoved = amountMoved;
        Position = position;
    }

    /// <summary>
    /// The change of the mouse wheel scroll since the last frame. A positive value indicates the wheel was rolled forward, and a negative one, back.
    /// </summary>
    public float AmountMoved { get; }

    /// <summary>
    /// The mouse wheel's overall position
    /// </summary>
    public float Position { get; }
}
