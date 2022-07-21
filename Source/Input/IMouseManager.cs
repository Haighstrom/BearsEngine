namespace BearsEngine.Input;

public interface IMouseManager
{
    MouseState State { get; }
    MouseState GetState(int index);
    void RefreshDevices();
}
