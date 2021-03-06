namespace BearsEngine.Worlds.UI.Grids
{
    /// <summary>
    /// Container Entity to add others too, where they will be neatly arranged in a grid. Add to a panel to use its scrollbars.
    /// </summary>
    public class GridLayout : Entity
    {
        #region Properties
        public bool IsFull => FindNextEmptyCell() == null;
        public int[] NextIndex => FindNextEmptyCell();
        public int NextRow => NextIndex[0];
        public int NextColumn => NextIndex[1];
        public IRect[,] Children { get; }
        public int Rows { get; }
        public int Columns { get; }

        public IRect this[int i, int j] => Children[i, j];

        public Point MinSize
            => new(
                GetTotalFixedColSize(),
                GetTotalFixedRowSize()
                );

        public int ColumnSpacing
        {
            get => _columnSpacing;
            set { _columnSpacing = value; UpdatePositions(); }
        }

        public int RowSpacing
        {
            get => _rowSpacing;
            set { _rowSpacing = value; UpdatePositions(); }
        }
        /// <summary>
        /// Border around the edge of the control
        /// </summary>
        public int Margin
        {
            get => _margin;
            set { _margin = value; UpdatePositions(); }
        }

        public List<CellFormat> RowFormat
        {
            get => _rowFormat;
            set
            {
                if (value.Count != Rows)
                    throw new HException("Row format must have same number of elements as the exisiting Row count");

                _rowFormat = value;

                foreach (CellFormat c in _rowFormat)
                    c.GridOrientation = GridOrientation.Row;

                UpdatePositions();
            }
        }
        public List<CellFormat> ColumnFormat
        {
            get => _columnFormat;
            set
            {
                if (value.Count != Columns)
                    throw new HException("Column format must have same number of elements as the exisiting Column count");

                _columnFormat = value;

                foreach (CellFormat c in _columnFormat)
                    c.GridOrientation = GridOrientation.Column;

                UpdatePositions();
            }
        }

        public DockPosition[,] GridAlignments { get; set; }
        #endregion

        #region Fields
        private int _margin = 0;
        private int _columnSpacing = 0;
        private int _rowSpacing = 0;
        private List<CellFormat> _rowFormat = new();
        private List<CellFormat> _columnFormat = new();
        #endregion

        #region Constructors
        public GridLayout(int layer, Rect position, List<CellFormat> rowFormat, List<CellFormat> columnFormat)
            : base(layer, position)
        {
            Rows = rowFormat.Count;
            Columns = columnFormat.Count;

            _rowFormat = new List<CellFormat>(rowFormat);
            _columnFormat = new List<CellFormat>(columnFormat);

            Children = new IRect[Rows, Columns];
            GridAlignments = new DockPosition[Rows, Columns];
        }
        public GridLayout(int layer, Rect position, int rows, int columns)
            : base(layer, position)
        {

            Rows = rows;
            Columns = columns;

            Children = new IRect[Rows, Columns];
            GridAlignments = new DockPosition[Rows, Columns];

            SetDefaultFormats();
        }
        #endregion

        #region Methods       

        #region Add
        public E Add<E>(E e, int row, int column, DockPosition gridAlignment = DockPosition.StretchToFill)
            where E : IRectAddable
        {
            Add(e);

            GridAlignments[row, column] = gridAlignment;

            Children[row, column] = e;
            e.R = GetEntityPosRect(row, column, gridAlignment, e);

            return e;
        }

        public void Add(IRectAddable e, DockPosition gridAlignment)
        {
            int row = NextRow;
            int column = NextColumn;

            GridAlignments[row, column] = gridAlignment;

            Children[row, column] = e;
            e.R = GetEntityPosRect(row, column, gridAlignment, e);

            Add(e);
        }

        public void Add<E>(IRectAddable e) => Add(e, DockPosition.StretchToFill);
        #endregion

        #region FindNextEmptyCell
        private int[] FindNextEmptyCell()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    if (Children[i, j] == null)
                        return new int[2] { i, j };

            //return null if there are not any spaces left..
            return null;
        }
        #endregion

        #region GetEntitiesInColumn
        public List<IRect> GetEntitiesInColumn(int col)
        {
            var l = new List<IRect>();

            for (int i = 0; i < Rows; i++)
                if (Children[i, col] != null)
                    l.Add(Children[i, col]);

            return l;
        }
        #endregion

        #region GetEntitiesInRow
        public List<IRect> GetEntitiesInRow(int row)
        {
            var l = new List<IRect>();

            for (int j = 0; j < Columns; j++)
                if (Children[row, j] != null)
                    l.Add(Children[row, j]);

            return l;
        }
        #endregion

        #region GetEntityPosRect
        /// <summary>
        /// Return GetPosRect, but also take into account the GridAlignment, for entities that do not fill the grid cell.
        /// </summary>
        private Rect GetEntityPosRect(int rowIdx, int colIdx, DockPosition gridAlignment, IRect entRect)
        {
            Rect cell = GetPosRect(rowIdx, colIdx);
            switch (gridAlignment)
            {
                case DockPosition.StretchToFill:
                    return cell;
                case DockPosition.Centre:
                    return new Rect
                        (
                            cell.X + (cell.W - entRect.W) / 2,
                            cell.Y + (cell.H - entRect.H) / 2,
                            entRect.W,
                            entRect.H
                        );
                case DockPosition.BottomMiddle:
                    return new Rect
                        (
                            cell.X + (cell.W - entRect.W) / 2,
                            cell.Y + (cell.H - entRect.H),
                            entRect.W,
                            entRect.H
                        );
                case DockPosition.MiddleLeft:
                    return new Rect
                        (
                            cell.X,
                            cell.Y + (cell.H - entRect.H) / 2,
                            entRect.W,
                            entRect.H
                        );
                case DockPosition.MiddleRight:
                    return new Rect
                        (
                            cell.X + (cell.W - entRect.W),
                            cell.Y + (cell.H - entRect.H) / 2,
                            entRect.W,
                            entRect.H
                        );
                case DockPosition.TopMiddle:
                    return new Rect
                        (
                            cell.X + (cell.W - entRect.W) / 2,
                            cell.Y,
                            entRect.W,
                            entRect.H
                        );
                case DockPosition.BottomRight:
                    return new Rect
                       (
                           cell.X + (cell.W - entRect.W),
                           cell.Y + (cell.H - entRect.H),
                           entRect.W,
                           entRect.H
                       );
                case DockPosition.BottomLeft:
                    return new Rect
                       (
                           cell.X,
                           cell.Y + (cell.H - entRect.H),
                           entRect.W,
                           entRect.H
                       );
                case DockPosition.TopRight:
                    return new Rect
                       (
                           cell.X + (cell.W - entRect.W),
                           cell.Y,
                           entRect.W,
                           entRect.H
                       );
                case DockPosition.TopLeft:
                    return new Rect
                       (
                           cell.X,
                           cell.Y,
                           entRect.W,
                           entRect.H
                       );
                default:
                    throw new NotImplementedException();
            }

        }
        #endregion

        #region GetNextEntityPos
        private Rect GetNextEntityPos(DockPosition gridAlignment, Rect entRect) => GetEntityPosRect(NextRow, NextColumn, gridAlignment, entRect);
        #endregion

        #region GetNextPos
        private Rect GetNextPos() => GetPosRect(NextRow, NextColumn);
        #endregion

        #region GetPosRect
        private Rect GetPosRect(int rowIdx, int colIdx)
        {
            //Some error checking and verification
            if (rowIdx >= Rows)
                throw new HException("Row index of GridLayout exceeds number of rows", rowIdx, Rows, this);
            if (colIdx >= Columns)
                throw new HException("Column index of GridLayout exceeds number of columns", colIdx, Columns, this);

            //Start with the total size of the control
            int availableWidth = (int)W;
            int availableHeight = (int)H;

            //Subtract the margins (twice!)
            availableWidth -= 2 * Margin;
            availableHeight -= 2 * Margin;

            //Subtract column and row spacings. N-1 times.
            availableWidth -= (Columns - 1) * ColumnSpacing;
            availableHeight -= (Rows - 1) * RowSpacing;

            //Subtract the size of the fixedSize rows and columns from the available W and H
            availableWidth -= GetTotalFixedColSize();
            availableHeight -= GetTotalFixedRowSize();

            //Get the total weights by summing all cell formats
            float totalWidthWeight = GetTotalWeight(_columnFormat);
            float totalHeightWeight = GetTotalWeight(_rowFormat);

            //Scan over columns up to this one for x position
            int xPos = Margin;
            for (int i = 0; i < colIdx; i++)
                xPos += _columnFormat[i].GetSize(availableWidth, totalWidthWeight, GetEntitiesInColumn(i)) + ColumnSpacing;

            //Same with rows
            int yPos = Margin;
            for (int i = 0; i < rowIdx; i++)
                yPos += _rowFormat[i].GetSize(availableHeight, totalHeightWeight, GetEntitiesInRow(i)) + RowSpacing;

            //Get width and height by evaluating selected cell
            int wdth = _columnFormat[colIdx].GetSize(availableWidth, totalWidthWeight, GetEntitiesInColumn(colIdx));
            int hgt = _rowFormat[rowIdx].GetSize(availableHeight, totalHeightWeight, GetEntitiesInRow(rowIdx));

            return new Rect(xPos, yPos, wdth, hgt);
        }
        #endregion

        #region GetTotalFixedColSize
        private int GetTotalFixedColSize()
        {
            int total = 0;
            for (int i = 0; i < _columnFormat.Count; i++)
                total += _columnFormat[i].GetFixedSize(GetEntitiesInColumn(i));

            return total;
        }
        #endregion

        #region GetTotalFixedRowSize
        private int GetTotalFixedRowSize()
        {
            int total = 0;
            for (int i = 0; i < _rowFormat.Count; i++)
                total += _rowFormat[i].GetFixedSize(GetEntitiesInRow(i));

            return total;
        }
        #endregion

        #region GetTotalWeight
        private float GetTotalWeight(List<CellFormat> formats)
        {
            float total = 0;
            for (int i = 0; i < formats.Count; i++)
                total += formats[i].Weight;

            return total;
        }
        #endregion

        #region OnSizeChanged
        protected override void OnSizeChanged(ResizeEventArgs args)
        {
            base.OnSizeChanged(args);

            UpdatePositions();
        }
        #endregion                

        #region SetDefaultFormats
        private void SetDefaultFormats()
        {
            _rowFormat = new List<CellFormat>();
            _columnFormat = new List<CellFormat>();

            for (int i = 0; i < Rows; i++)
                _rowFormat.Add(CellFormat.Weighted(1f));
            for (int j = 0; j < Columns; j++)
                _columnFormat.Add(CellFormat.Weighted(1f));
        }
        #endregion

        #region UpdatePositions
        /// <summary>
        /// After any change etc, Resize everything to meet updated params
        /// </summary>
        private void UpdatePositions()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    if (Children[i, j] != null)
                        Children[i, j].R = GetEntityPosRect(i, j, GridAlignments[i, j], Children[i, j]);
        }
        #endregion
        #endregion
    }
}
