namespace BearsEngine.Worlds
{
    public class PositionChangedArgs : EventArgs
    {
        public Rect NewR;

        public PositionChangedArgs(Rect newR)
        {
            NewR = newR;
        }
    }
}