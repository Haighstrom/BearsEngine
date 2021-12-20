using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
