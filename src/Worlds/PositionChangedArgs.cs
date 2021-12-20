using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;

namespace BearsEngine.Worlds
{
    public class PositionChangedArgs : EventArgs
    {
        public IRect<float> NewR;

        public PositionChangedArgs(IRect<float> newR)
        {
            NewR = newR;
        }
    }
}