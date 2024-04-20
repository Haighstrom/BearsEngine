namespace BearsEngine.Input.Windows;

internal interface IKeyboardAPI
{
    KeyboardState GetAggregateState { get; }

    KeyboardState GetState(int index);

    void UpdateDevices();
}
