namespace BearsEngine.Worlds
{
    public class PositionChangedArgs : EventArgs
    {
        public IRect NewR;

        public PositionChangedArgs(IRect newR)
        {
            NewR = newR;
        }
    }
}