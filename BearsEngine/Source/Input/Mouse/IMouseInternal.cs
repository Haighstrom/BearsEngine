namespace BearsEngine.Input;

internal interface IMouseInternal : IMouse
{
    void Update(MouseState newState);
}
