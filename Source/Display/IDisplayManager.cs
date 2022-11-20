using BearsEngine.DisplayDevices;

namespace BearsEngine.Display;

public interface IDisplayManager
{
    IList<IDisplay> AvailableDisplays { get; }
    IDisplay MainDisplay { get; }
    IDisplay GetDisplay(int index);
}