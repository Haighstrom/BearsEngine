namespace BearsEngine.Input;

public class MouseButtonReleasedEventArgs : EventArgs
{
    public MouseButtonReleasedEventArgs(MouseButton button)
    {
        Button = button;
    }

    public MouseButton Button { get; }
}
