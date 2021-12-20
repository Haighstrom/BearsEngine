using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearsEngine.Worlds
{
    public class ScrollbarPositionArgs : EventArgs
    {
        public ScrollbarPositionArgs(float minAmount, float maxAmount)
        {
            MinAmount = minAmount;
            MaxAmount = maxAmount;
        }

        public float MinAmount { get; set; }
        public float MaxAmount { get; set; }
    }
}
