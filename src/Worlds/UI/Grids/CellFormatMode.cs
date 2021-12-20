using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearsEngine.Worlds
{
    /// <summary>
    /// Resize behaviour specs for cells of GridLayout panels
    /// </summary>
    public enum CellFormatMode
    {
        Fixed,
        Weighted,
        Fit         //Match size of largest child entity
    }
}