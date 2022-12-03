namespace BearsEngine.Worlds;

public class LayerChangedEventArgs : EventArgs
{
    public int OldLayer, NewLayer;

    public LayerChangedEventArgs(int oldLayer, int newLayer)
    {
        OldLayer = oldLayer;
        NewLayer = newLayer;
    }
}
