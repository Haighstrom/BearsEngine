namespace BearsEngine;

public class LayerChangedEventArgs : EventArgs
{
    public float OldLayer, NewLayer;

    public LayerChangedEventArgs(float oldLayer, float newLayer)
    {
        OldLayer = oldLayer;
        NewLayer = newLayer;
    }
}
