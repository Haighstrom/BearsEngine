namespace BearsEngine;

public class LayerChangedEventArgs : EventArgs
{
    public LayerChangedEventArgs(float oldLayer, float newLayer)
    {
        OldLayer = oldLayer;
        NewLayer = newLayer;
    }

    public float NewLayer { get; }

    public float OldLayer { get; }
}
