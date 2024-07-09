using System.Data.Common;
using System.DirectoryServices.ActiveDirectory;
using BearsEngine.Input;
using BearsEngine.OpenGL;
using BearsEngine.Source.UI.Grids;

namespace BearsEngine.UI;

/// <summary>
/// Container Entity to add others to, where they will be neatly arranged in a grid. Add to a panel to use its scrollbars.
/// </summary>
public class GridLayout : Entity
{
    private readonly CellInfo[,] _cells;

    private int _margin = 0;
    private int _columnSpacing = 0;
    private int _rowSpacing = 0;
    private List<CellSizing> _rowFormats = new();
    private List<CellSizing> _columnFormats = new();

    public GridLayout(float layer, Rect position, List<CellSizing> rowFormat, List<CellSizing> columnFormat)
        : base(layer, position)
    {
        var rows = rowFormat.Count;
        var columns = columnFormat.Count;

        _cells = new CellInfo[columns, rows];

        for (var i = 0; i < rows; i++)
            for (var j = 0; j < columns; j++)
            {
                _cells[i, j] = new CellInfo();
            }

        _rowFormats = new List<CellSizing>(rowFormat);
        _columnFormats = new List<CellSizing>(columnFormat);
    }

    public GridLayout(float layer, Rect position, int rows, int columns)
        : this(layer, position, Enumerable.Repeat(CellSizing.Weighted(1), rows).ToList(), Enumerable.Repeat(CellSizing.Weighted(1), columns).ToList())
    {
    }

    public IRectangular? this[int i, int j] => _cells[i, j].Contents;

    /// <summary>
    /// Border around the edge of the control
    /// </summary>
    public int Margin
    {
        get => _margin;
        set { _margin = value; UpdatePositions(); }
    }

    public List<CellSizing> RowFormat
    {
        get => _rowFormats;
        set
        {
            if (value.Count != Rows)
                throw new Exception("Row format must have same number of elements as the exisiting Row count");

            _rowFormats = value;

            UpdatePositions();
        }
    }

    public List<CellSizing> ColumnFormat
    {
        get => _columnFormats;
        set
        {
            if (value.Count != Columns)
                throw new Exception("Column format must have same number of elements as the exisiting Column count");

            _columnFormats = value;

            UpdatePositions();
        }
    }

    public Point MinSize => new(GetTotalFixedWidth(), GetTotalFixedHeight());

    public int ColumnSpacing
    {
        get => _columnSpacing;

        set
        {
            _columnSpacing = value;
            UpdatePositions();
        }
    }

    public int RowSpacing
    {
        get => _rowSpacing;

        set
        {
            _rowSpacing = value;
            UpdatePositions();
        }
    }

    public bool IsFull => FindNextEmptyCell() == null;

    public (int Row, int Column)? NextAvailableCell => FindNextEmptyCell();

    public int Rows => _cells.GetLength(0);

    public int Columns => _cells.GetLength(1);

    public E Add<E>(E e, int row, int column, GridAlignment gridAlignment = GridAlignment.Fill)
        where E : IRectAddable
    {
        Add(e);

        _cells[row, column].GridAlignment = gridAlignment;

        _cells[row, column].Contents = e;

        e.R = GetEntityPosRect(row, column, gridAlignment, e.R);

        return e;
    }

    public void Add(IRectAddable e, GridAlignment gridAlignment)
    {
        var nextAvailableCell = NextAvailableCell;

        if (!nextAvailableCell.HasValue)
        {
            throw new InvalidOperationException("Tried to add an object to a gridlayout with no space");
        }

        Add(e, nextAvailableCell.Value.Row, nextAvailableCell.Value.Column, gridAlignment);
    }

    public void Add<E>(IRectAddable e) => Add(e, GridAlignment.Fill);

    private (int Row, int Column)? FindNextEmptyCell()
    {
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
            {
                if (_cells[j, i].Contents == null)
                {
                    return (j, i);
                }
            }

        //return null if there are not any spaces left..
        return null;
    }

    public List<IRectangular> GetEntitiesInColumn(int col)
    {
        var l = new List<IRectangular>();

        for (int i = 0; i < Rows; i++)
        {
            var entity = _cells[i, col].Contents;

            if (entity is not null)
            {
                l.Add(entity);
            }
        }

        return l;
    }

    public List<IRectangular> GetEntitiesInRow(int row)
    {
        var l = new List<IRectangular>();

        for (int j = 0; j < Columns; j++)
        {

            var entity = _cells[row, j].Contents;

            if (entity is not null)
            {
                l.Add(entity);
            }
        }

        return l;
    }

    /// <summary>
    /// Return GetPosRect, but also take into account the GridAlignment, for entities that do not fill the grid cell.
    /// </summary>
    private Rect GetEntityPosRect(int rowIdx, int colIdx, GridAlignment gridAlignment, Rect entRect)
    {
        Rect cell = GetPosRect(rowIdx, colIdx);

        return gridAlignment switch
        {
            GridAlignment.Fill => cell,
            GridAlignment.Centre => new Rect(cell.X + (cell.W - entRect.W) / 2, cell.Y + (cell.H - entRect.H) / 2, entRect.W, entRect.H),
            GridAlignment.BottomMiddle => new Rect(cell.X + (cell.W - entRect.W) / 2, cell.Y + (cell.H - entRect.H), entRect.W, entRect.H),
            GridAlignment.MiddleLeft => new Rect(cell.X, cell.Y + (cell.H - entRect.H) / 2, entRect.W, entRect.H),
            GridAlignment.MiddleRight => new Rect(cell.X + (cell.W - entRect.W), cell.Y + (cell.H - entRect.H) / 2, entRect.W, entRect.H),
            GridAlignment.TopMiddle => new Rect(cell.X + (cell.W - entRect.W) / 2, cell.Y, entRect.W, entRect.H),
            GridAlignment.BottomRight => new Rect(cell.X + (cell.W - entRect.W), cell.Y + (cell.H - entRect.H), entRect.W, entRect.H),
            GridAlignment.BottomLeft => new Rect(cell.X, cell.Y + (cell.H - entRect.H), entRect.W, entRect.H),
            GridAlignment.TopRight => new Rect(cell.X + (cell.W - entRect.W), cell.Y, entRect.W, entRect.H),
            GridAlignment.TopLeft => new Rect(cell.X, cell.Y, entRect.W, entRect.H),
            _ => throw new NotImplementedException(),
        };
    }

    private Rect GetPosRect(int rowIdx, int colIdx)
    {
        if (rowIdx >= Rows)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIdx), "Row index of GridLayout exceeds number of rows");
        }

        if (colIdx >= Columns)
        {
            throw new ArgumentOutOfRangeException(nameof(colIdx), "Column index of GridLayout exceeds number of columns");
        }

        //Get the total weights by summing all cell formats
        float totalWidthWeight = _columnFormats.Sum(c => c.Weight);
        float totalHeightWeight = _rowFormats.Sum(r => r.Weight);

        //Scan over columns up to this one for x position
        int xPos = Margin;
        for (int i = 0; i < colIdx; i++)
        {
            xPos += CalculateColumnWidth(i) + ColumnSpacing;
        }

        //Same with rows
        int yPos = Margin;
        for (int i = 0; i < rowIdx; i++)
        {
            yPos += CalculateRowHeight(i) + RowSpacing;
        }

        //Get width and height by evaluating selected cell
        int wdth = CalculateColumnWidth(colIdx);
        int hgt = CalculateRowHeight(rowIdx);

        return new Rect(xPos, yPos, wdth, hgt);
    }

    private int GetTotalWeightedWidth()
    {
        //Start with the total size of the control
        int availableWidth = (int)W;

        //Subtract the margins (twice!)
        availableWidth -= 2 * Margin;

        //Subtract column and row spacings. N-1 times.
        availableWidth -= (Columns - 1) * ColumnSpacing;

        //Subtract the size of the fixedSize rows and columns from the available W and H
        availableWidth -= GetTotalFixedWidth();

        return availableWidth;
    }

    private int GetTotalWeightedHeight()
    {
        //Start with the total size of the control
        int availableHeight = (int)H;

        //Subtract the margins (twice!)
        availableHeight -= 2 * Margin;

        //Subtract column and row spacings. N-1 times.
        availableHeight -= (Rows - 1) * RowSpacing;

        //Subtract the size of the fixedSize rows and columns from the available W and H
        availableHeight -= GetTotalFixedHeight();

        return availableHeight;
    }

    private int GetTotalFixedWidth()
    {
        int total = 0;

        for (int i = 0; i < _columnFormats.Count; i++)
        {
            var cellFormat = _columnFormats[i];

            total += cellFormat.SizingMode switch
            {
                CellSizingMode.Fixed => cellFormat.FixedSize,
                CellSizingMode.Weighted => 0,
                CellSizingMode.Fit => (int)Math.Ceiling(GetEntitiesInColumn(i).MaxBy(e => e.W)!.W),
                _ => throw new NotImplementedException(),
            };
        }

        return total;
    }

    private int GetTotalFixedHeight()
    {
        int total = 0;

        for (int i = 0; i < _rowFormats.Count; i++)
        {
            var cellFormat = _rowFormats[i];

            total += cellFormat.SizingMode switch
            {
                CellSizingMode.Fixed => cellFormat.FixedSize,
                CellSizingMode.Weighted => 0,
                CellSizingMode.Fit => (int)Math.Ceiling(GetEntitiesInRow(i).MaxBy(e => e.H)!.H),
                _ => throw new NotImplementedException(),
            };
        }

        return total;
    }

    private int CalculateColumnWidth(int column)
    {
        var cellFormat = _columnFormats[column];

        return cellFormat.SizingMode switch
        {
            CellSizingMode.Fixed => cellFormat.FixedSize,
            CellSizingMode.Weighted => Maths.Round(GetTotalWeightedWidth() * cellFormat.Weight / _columnFormats.Sum(c => c.Weight)),
            CellSizingMode.Fit => (int)Math.Ceiling(GetEntitiesInColumn(column).MaxBy(e => e.W)!.W),
            _ => throw new NotImplementedException(),
        };
    }

    private int CalculateRowHeight(int row)
    {
        var cellFormat = _rowFormats[row];

        return cellFormat.SizingMode switch
        {
            CellSizingMode.Fixed => cellFormat.FixedSize,
            CellSizingMode.Weighted => Maths.Round(GetTotalWeightedHeight() * cellFormat.Weight / _rowFormats.Sum(c => c.Weight)),
            CellSizingMode.Fit => (int)Math.Ceiling(GetEntitiesInRow(row).MaxBy(e => e.H)!.H),
            _ => throw new NotImplementedException(),
        };
    }

    protected override void OnSizeChanged(ResizeEventArgs args)
    {
        base.OnSizeChanged(args);

        UpdatePositions();
    }

    /// <summary>
    /// After any change etc, Resize everything to meet updated params
    /// </summary>
    private void UpdatePositions()
    {
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
            {
                var entity = _cells[i, j].Contents;

                if (entity is not null)
                {
                    var rect = GetEntityPosRect(i, j, _cells[i, j].GridAlignment, entity.R);
                    entity.R = new Rect(rect);
                }
            }
    }
}
