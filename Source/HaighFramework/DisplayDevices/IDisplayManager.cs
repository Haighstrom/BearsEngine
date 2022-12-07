namespace BearsEngine.DisplayDevices;

public interface IDisplayManager
{
    IList<IDisplay> AvailableDisplays { get; }
    IDisplay MainDisplay { get; }
    IDisplay GetDisplay(int index);
}