namespace BearsEngine.UI;

/// <summary>
/// Info class to inform GridLayout how to stretch columns and rows.
/// </summary>
public class CellFormat
{
    public static CellFormat Fixed(int pixels) => new() { FormatMode = CellFormatMode.Fixed, FixedSize = pixels, Weight = 0 };
    public static CellFormat Weighted(float weight = 1f) => new() { FormatMode = CellFormatMode.Weighted, Weight = weight, FixedSize = 0 };
    public static CellFormat Fit => new() { FormatMode = CellFormatMode.Fit, Weight = 0, FixedSize = 0 };
    

    public CellFormatMode FormatMode { get; set; }
    public GridOrientation GridOrientation { get; set; }
    public int FixedSize { get; private set; }
    public float Weight { get; private set; }
    

    private CellFormat()
    {
    }
    

    public int GetSize(int totalSize, float totalWeights, List<IRectangular> childrenInRowOrCol)
    {
        switch (FormatMode)
        {
            case CellFormatMode.Fixed:
                return FixedSize;

            case CellFormatMode.Weighted:
                return HF.Maths.Round(totalSize * Weight / totalWeights);

            case CellFormatMode.Fit:
                int size = 0;

                switch (GridOrientation)
                {
                    case GridOrientation.Column:
                        foreach (Rect e in childrenInRowOrCol)
                            if (e.W > size)
                                size = (int)e.W;
                        break;

                    case GridOrientation.Row:
                        foreach (Rect e in childrenInRowOrCol)
                            if (e.H > size)
                                size = (int)e.H;
                        break;

                    default:
                        throw new NotImplementedException();
                }

                return size;

            default:
                throw new NotImplementedException();
        }
    }
    

    //Return Size allocated by FIt or Fixed pixel size - to calculate how much Weighted space is remaining...
    public int GetFixedSize(List<IRectangular> childrenInRowOrCol)
    {
        switch (FormatMode)
        {
            case CellFormatMode.Fixed:
                return FixedSize;

            case CellFormatMode.Weighted:
                return 0;

            case CellFormatMode.Fit:
                int size = 0;

                switch (GridOrientation)
                {
                    case GridOrientation.Column:
                        foreach (Rect e in childrenInRowOrCol)
                            if (e.W > size)
                                size = (int)e.W;
                        break;

                    case GridOrientation.Row:
                        foreach (Rect e in childrenInRowOrCol)
                            if (e.H > size)
                                size = (int)e.H;
                        break;

                    default:
                        throw new NotImplementedException();
                }

                return size;

            default:
                throw new NotImplementedException();
        }
    }
    
    
}
