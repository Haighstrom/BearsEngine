namespace BearsEngine.UI;

/// <summary>
/// Container Entity to add others too, where they will be neatly arranged in a grid. Add to a panel to use its scrollbars.
/// </summary>
public class GridLayout : Entity
{
    public bool IsFull => FindNextEmptyCell() == null;
    public int[] NextIndex => FindNextEmptyCell();
    public int NextRow => NextIndex[0];
    public int NextColumn => NextIndex[1];
    public IRectangular[,] Children { get; }
    public int Rows { get; }
    public int Columns { get; }

    public IRectangular this[int i, int j] => Children[i, j];

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
                throw new Exception("Row format must have same number of elements as the exisiting Row count");

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
                throw new Exception("Column format must have same number of elements as the exisiting Column count");

            _columnFormat = value;

            foreach (CellFormat c in _columnFormat)
                c.GridOrientation = GridOrientation.Column;

            UpdatePositions();
        }
    }

    public GridAlignment[,] GridAlignments { get; set; }
    

    private int _margin = 0;
    private int _columnSpacing = 0;
    private int _rowSpacing = 0;
    private List<CellFormat> _rowFormat = new();
    private List<CellFormat> _columnFormat = new();
    

    public GridLayout(float layer, Rect position, List<CellFormat> rowFormat, List<CellFormat> columnFormat)
        : base(layer, position)
    {
        Rows = rowFormat.Count;
        Columns = columnFormat.Count;

        _rowFormat = new List<CellFormat>(rowFormat);
        _columnFormat = new List<CellFormat>(columnFormat);

        Children = new IRectangular[Rows, Columns];
        GridAlignments = new GridAlignment[Rows, Columns];
    }
    public GridLayout(float layer, Rect position, int rows, int columns)
        : base(layer, position)
    {

        Rows = rows;
        Columns = columns;

        Children = new IRectangular[Rows, Columns];
        GridAlignments = new GridAlignment[Rows, Columns];

        SetDefaultFormats();
    }
    

    public E Add<E>(E e, int row, int column, GridAlignment gridAlignment = GridAlignment.Fill)
        where E : IRectAddable
    {
        Add(e);

        GridAlignments[row, column] = gridAlignment;

        Children[row, column] = e;
        var rect = GetEntityPosRect(row, column, gridAlignment, e.R);
        e.X = rect.X;
        e.Y = rect.Y;
        e.W = rect.W;
        e.H = rect.H;

        return e;
    }

    public void Add(IRectAddable e, GridAlignment gridAlignment)
    {
        int row = NextRow;
        int column = NextColumn;

        GridAlignments[row, column] = gridAlignment;

        Children[row, column] = e;
        var rect = GetEntityPosRect(row, column, gridAlignment, e.R);
        e.X = rect.X;
        e.Y = rect.Y;
        e.W = rect.W;
        e.H = rect.H;

        Add(e);
    }

    public void Add<E>(IRectAddable e) => Add(e, GridAlignment.Fill);
    

    private int[] FindNextEmptyCell()
    {
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
                if (Children[i, j] == null)
                    return new int[2] { i, j };

        //return null if there are not any spaces left..
        return null;
    }
    

    public List<IRectangular> GetEntitiesInColumn(int col)
    {
        var l = new List<IRectangular>();

        for (int i = 0; i < Rows; i++)
            if (Children[i, col] != null)
                l.Add(Children[i, col]);

        return l;
    }
    

    public List<IRectangular> GetEntitiesInRow(int row)
    {
        var l = new List<IRectangular>();

        for (int j = 0; j < Columns; j++)
            if (Children[row, j] != null)
                l.Add(Children[row, j]);

        return l;
    }
    

    /// <summary>
    /// Return GetPosRect, but also take into account the GridAlignment, for entities that do not fill the grid cell.
    /// </summary>
    private Rect GetEntityPosRect(int rowIdx, int colIdx, GridAlignment gridAlignment, Rect entRect)
    {
        Rect cell = GetPosRect(rowIdx, colIdx);
        switch (gridAlignment)
        {
            case GridAlignment.Fill:
                return cell;
            case GridAlignment.Centre:
                return new Rect
                    (
                        cell.X + (cell.W - entRect.W) / 2,
                        cell.Y + (cell.H - entRect.H) / 2,
                        entRect.W,
                        entRect.H
                    );
            case GridAlignment.BottomMiddle:
                return new Rect
                    (
                        cell.X + (cell.W - entRect.W) / 2,
                        cell.Y + (cell.H - entRect.H),
                        entRect.W,
                        entRect.H
                    );
            case GridAlignment.MiddleLeft:
                return new Rect
                    (
                        cell.X,
                        cell.Y + (cell.H - entRect.H) / 2,
                        entRect.W,
                        entRect.H
                    );
            case GridAlignment.MiddleRight:
                return new Rect
                    (
                        cell.X + (cell.W - entRect.W),
                        cell.Y + (cell.H - entRect.H) / 2,
                        entRect.W,
                        entRect.H
                    );
            case GridAlignment.TopMiddle:
                return new Rect
                    (
                        cell.X + (cell.W - entRect.W) / 2,
                        cell.Y,
                        entRect.W,
                        entRect.H
                    );
            case GridAlignment.BottomRight:
                return new Rect
                   (
                       cell.X + (cell.W - entRect.W),
                       cell.Y + (cell.H - entRect.H),
                       entRect.W,
                       entRect.H
                   );
            case GridAlignment.BottomLeft:
                return new Rect
                   (
                       cell.X,
                       cell.Y + (cell.H - entRect.H),
                       entRect.W,
                       entRect.H
                   );
            case GridAlignment.TopRight:
                return new Rect
                   (
                       cell.X + (cell.W - entRect.W),
                       cell.Y,
                       entRect.W,
                       entRect.H
                   );
            case GridAlignment.TopLeft:
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
    

    private Rect GetNextEntityPos(GridAlignment gridAlignment, Rect entRect) => GetEntityPosRect(NextRow, NextColumn, gridAlignment, entRect);
    

    private Rect GetNextPos() => GetPosRect(NextRow, NextColumn);
    

    private Rect GetPosRect(int rowIdx, int colIdx)
    {
        //Some error checking and verification
        if (rowIdx >= Rows)
            throw new Exception("Row index of GridLayout exceeds number of rows");
        if (colIdx >= Columns)
            throw new Exception("Column index of GridLayout exceeds number of columns");

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
    

    private int GetTotalFixedColSize()
    {
        int total = 0;
        for (int i = 0; i < _columnFormat.Count; i++)
            total += _columnFormat[i].GetFixedSize(GetEntitiesInColumn(i));

        return total;
    }
    

    private int GetTotalFixedRowSize()
    {
        int total = 0;
        for (int i = 0; i < _rowFormat.Count; i++)
            total += _rowFormat[i].GetFixedSize(GetEntitiesInRow(i));

        return total;
    }
    

    private float GetTotalWeight(List<CellFormat> formats)
    {
        float total = 0;
        for (int i = 0; i < formats.Count; i++)
            total += formats[i].Weight;

        return total;
    }
    

    protected override void OnSizeChanged(ResizeEventArgs args)
    {
        base.OnSizeChanged(args);

        UpdatePositions();
    }
                    

    private void SetDefaultFormats()
    {
        _rowFormat = new List<CellFormat>();
        _columnFormat = new List<CellFormat>();

        for (int i = 0; i < Rows; i++)
            _rowFormat.Add(CellFormat.Weighted(1f));
        for (int j = 0; j < Columns; j++)
            _columnFormat.Add(CellFormat.Weighted(1f));
    }
    

    /// <summary>
    /// After any change etc, Resize everything to meet updated params
    /// </summary>
    private void UpdatePositions()
    {
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
                if (Children[i, j] != null)
                {
                    var rect = GetEntityPosRect(i, j, GridAlignments[i, j], Children[i, j].R);
                    Children[i, j].X = rect.X;
                    Children[i, j].Y = rect.Y;
                    Children[i, j].W = rect.W;
                    Children[i, j].H = rect.H;
                }
    }
    
    
}
