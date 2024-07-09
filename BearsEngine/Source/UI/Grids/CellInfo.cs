using BearsEngine.UI;

namespace BearsEngine.Source.UI.Grids;

internal class CellInfo
{
    public CellInfo()
    {
    }

    public IRectAddable? Contents { get; set; }

    public GridAlignment GridAlignment { get; set; } = GridAlignment.Fill;
}
