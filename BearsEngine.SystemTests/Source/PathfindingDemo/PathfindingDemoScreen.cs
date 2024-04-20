using BearsEngine.Pathfinding;
using BearsEngine.SystemTests.Source.Globals;
using System.Linq;

namespace BearsEngine.SystemTests.Source.PathfindingDemo;

internal class PathfindingDemoScreen : Screen
{
    protected static readonly Func<Node, Node, float> PythagorusSquaredHeuristic = (n1, n2) => (n1.X - n2.X) * (n1.X - n2.X) + (n1.Y - n2.Y) * (n1.Y - n2.Y);
    protected static readonly Func<Node, Node, float> PythagorusHeuristic = (n1, n2) => (float)Math.Sqrt((n1.X - n2.X) * (n1.X - n2.X) + (n1.Y - n2.Y) * (n1.Y - n2.Y));
    protected static readonly Func<Node, Node, float> ManhattanHeuristic = (n1, n2) => Math.Abs(n1.X - n2.X) + Math.Abs(n1.Y - n2.Y);
    protected static readonly Func<Node, Node, float> FloodFillHeuristic = (n1, n2) => 0;

    public static float PositionXFromIndexX(int x) => GP.Pathfinding.GridTopLeft.X + x * GP.Pathfinding.SquareSize.X;

    public static float PositionYFromIndexY(int y) => GP.Pathfinding.GridTopLeft.Y + y * GP.Pathfinding.SquareSize.Y;

    public static int IndexXFromPositionX(float x) => (int)((x - GP.Pathfinding.GridTopLeft.X) / GP.Pathfinding.SquareSize.X);

    public static int IndexYFromPositionY(float y) => (int)((y - GP.Pathfinding.GridTopLeft.Y) / GP.Pathfinding.SquareSize.Y);

    private const float SolveStepTime = 0.25f;
    private const int GridW = 20, GridH = 15;

    private float _timeToNextSolveStep = SolveStepTime;
    private IPathSolver<Node>? _solver;
    private readonly Line _line;
    private readonly DropdownList<Func<Node, Node, float>> _dropDownList;

    public PathfindingDemoScreen()
    {
        BackgroundColour = Colour.LightGray;

        Add(new ReturnButton());
        Add(new SolveButton(this));

        Add(_line = new Line(Colour.Blue, GP.Pathfinding.SolveLineThickness, true) { Layer = GL.Camera.Debug });

        Grid = new GridPathfinder<Node>(GridW, GridH, (x, y) => new Node(PositionXFromIndexX(x), PositionYFromIndexY(y)));
        for (var i = 0; i < GridW; i++)
            for (var j = 0; j < GridH; j++)
                Add(Grid[i, j]);

        Add(new Image(Colour.DarkerGray, new Rect(GP.Pathfinding.GridTopLeft, GridW * GP.Pathfinding.SquareSize.X, GridH * GP.Pathfinding.SquareSize.Y)));

        Add(_dropDownList = new(GL.UI.Button, GP.Pathfinding.DropDownList, GP.Pathfinding.DropDownOptionSpacing, Colour.AntiqueWhite, GV.MainFont, Colour.Black));
        _dropDownList.AddOption("PythagSqrd", PythagorusSquaredHeuristic);
        _dropDownList.AddOption("Manhattan", ManhattanHeuristic);
        _dropDownList.AddOption("Clownbus", FloodFillHeuristic);
        _dropDownList.SetValue(0);

        Grid[5, 3].Passable = false;
        Grid[8, 3].Passable = false;
        Grid[5, 4].Passable = false;
        Grid[6, 4].Passable = false;
        Grid[7, 4].Passable = false;

        Add(StartSquare = new Square(this, PositionXFromIndexX(0), PositionYFromIndexY(0), GA.GFX.Pathfinding.StartSquare));
        Add(EndSquare = new Square(this, PositionXFromIndexX(19), PositionYFromIndexY(14), GA.GFX.Pathfinding.EndSquare));
    }

    public Square EndSquare { get; }

    public GridPathfinder<Node> Grid { get; }

    public Square StartSquare { get; }

    private void UpdateGraphicsForCurrentSolveState()
    {
        foreach (var square in _solver.State.ExploredNodes)
        {
            square.YellowHighlight = false;
        }
        foreach (var square in _solver.State.NodesToExplore)
        {
            square.YellowHighlight = true;
        }
        _line.Points = _solver.Path.Select(s => s.Centre).ToList();
        Log.Debug(_solver.Path);
    }

    public void StartSolve()
    {
        _solver = new AStarSolver<Node>(Grid[StartSquare.IndexX, StartSquare.IndexY], Grid[EndSquare.IndexX, EndSquare.IndexY], (s1, s2) => s2.Passable, _dropDownList.CurrentValue);
        //_solver = new RandomPathSolver<Node>(Grid[StartSquare.IndexX, StartSquare.IndexY], (s1, s2) => s2.Passable, 10, false);
        UpdateGraphicsForCurrentSolveState();
    }

    public override void Update(float elapsedTime)
    {
        base.Update(elapsedTime);

        if (_solver?.State.SolveStatus == SolveStatus.NotStarted || _solver?.State.SolveStatus == SolveStatus.InProgress)
        {
            _timeToNextSolveStep -= elapsedTime;
            if (_timeToNextSolveStep < 0)
            {
                _solver.Step();
                UpdateGraphicsForCurrentSolveState();
                _timeToNextSolveStep += SolveStepTime;
            }
        }
    }
}