using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearsEngine.Worlds
{
    public interface ICollapsable
    {
        void Collapse();
        void Expand();
    }
}
