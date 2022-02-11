using HaighFramework;

namespace BearsEngine.Worlds
{
    /// <summary>
    /// Info class to inform GridLayout how to stretch columns and rows.
    /// </summary>
    public class CellFormat 
    {
        #region Static Members
        public static CellFormat Fixed(int pixels) => new CellFormat() { FormatMode = CellFormatMode.Fixed, FixedSize = pixels, Weight = 0 };
        public static CellFormat Weighted(float weight = 1f) => new CellFormat() { FormatMode = CellFormatMode.Weighted, Weight = weight, FixedSize = 0 };
        public static CellFormat Fit => new CellFormat() { FormatMode = CellFormatMode.Fit, Weight = 0, FixedSize = 0 };
        #endregion

        #region Properties
        public CellFormatMode FormatMode { get; set; }
        public GridOrientation GridOrientation { get; set; } 
        public int FixedSize { get; private set; }
        public float Weight { get; private set; }
        #endregion

        #region Constructors
        private CellFormat()
        {
        }
        #endregion

        #region Methods
        #region GetSize
        public int GetSize(int totalSize, float totalWeights, List<IRect<float>> childrenInRowOrCol)
        {
            switch (FormatMode)
            {
                case CellFormatMode.Fixed:
                    return FixedSize;

                case CellFormatMode.Weighted:
                    return HF.Maths.Round(totalSize * Weight / totalWeights);

                case CellFormatMode.Fit:
                    int size = 0;

                    switch(GridOrientation)
                    {
                        case (GridOrientation.Column):
                            foreach (IRect<float> e in childrenInRowOrCol)
                                if (e.W > size)
                                    size = (int)e.W;
                            break;

                        case (GridOrientation.Row):
                            foreach (IRect<float> e in childrenInRowOrCol)
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
        #endregion

        #region GetFixedSize
        //Return Size allocated by FIt or Fixed pixel size - to calculate how much Weighted space is remaining...
        public int GetFixedSize(List<IRect<float>> childrenInRowOrCol)
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
                        case (GridOrientation.Column):
                            foreach (IRect<float> e in childrenInRowOrCol)
                                if (e.W > size)
                                    size = (int)e.W;
                            break;

                        case (GridOrientation.Row):
                            foreach (IRect<float> e in childrenInRowOrCol)
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
        #endregion
        #endregion
    }
}