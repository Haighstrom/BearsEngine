using BearsEngine.WinAPI;

namespace BearsEngine.Input.Windows;

internal interface IMouseAPI
{
    MouseState GetAggregateState();

    bool ProcessInputData(RawInput data);

    void UpdateDevices();
}
