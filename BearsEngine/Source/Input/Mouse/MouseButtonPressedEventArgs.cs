namespace BearsEngine.Input;

public class MouseButtonPressedEventArgs : EventArgs
{
    public MouseButtonPressedEventArgs(MouseButton button)
    {
        Button = button;
    }

    public MouseButton Button { get; }
}
