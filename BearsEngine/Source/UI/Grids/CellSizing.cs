namespace BearsEngine.UI;

/// <summary>
/// Info class to inform GridLayout how to stretch columns and rows.
/// </summary>
public class CellSizing
{
    /// <summary>
    /// Cell dimension length is a fixed number of pixels
    /// </summary>
    /// <param name="pixels">the length, in pixels</param>
    /// <returns></returns>
    public static CellSizing Fixed(int pixels) => new(CellSizingMode.Fixed, pixels, 0);

    /// <summary>
    /// Cell dimension length is a ratio of the total grid
    /// </summary>
    /// <param name="weight">The length of this cell in relation to other weighted cells</param>
    /// <returns></returns>
    public static CellSizing Weighted(float weight = 1f) => new(CellSizingMode.Weighted, 0, weight);

    /// <summary>
    /// Cell dimension length is determined by the biggest thing added to this cell's dimension
    /// For example if a row is set to Fit, the tallest item in that row defines the height of all cells in that row
    /// </summary>
    public static CellSizing Fit => new(CellSizingMode.Fit, 0, 0);
    
    private CellSizing(CellSizingMode cellFormatMode, int fixedSize, float weight)
    {
        SizingMode = cellFormatMode;
        FixedSize = fixedSize;
        Weight = weight;
    }

    public CellSizingMode SizingMode { get; }

    public int FixedSize { get; }

    public float Weight { get; }
}
