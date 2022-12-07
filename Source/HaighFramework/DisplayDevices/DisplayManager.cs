namespace BearsEngine.DisplayDevices;

public class DisplayManager : IDisplayManager
{
    public IList<IDisplay> AvailableDisplays => throw new NotImplementedException();

    public IDisplay MainDisplay => throw new NotImplementedException();

    public IDisplay GetDisplay(int index)
    {
        throw new NotImplementedException();
    }
}