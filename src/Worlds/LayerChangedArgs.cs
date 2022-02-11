namespace BearsEngine.Worlds
{
    public class LayerChangedArgs : EventArgs
    {
        public int OldLayer, NewLayer;

        public LayerChangedArgs(int oldLayer, int newLayer)
        {
            OldLayer = oldLayer;
            NewLayer = newLayer;
        }
    }
}
