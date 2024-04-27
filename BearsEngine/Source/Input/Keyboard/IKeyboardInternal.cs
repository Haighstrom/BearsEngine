namespace BearsEngine.Input;

internal interface IKeyboardInternal : IKeyboard
{
    void Update(KeyboardState newState);
}
