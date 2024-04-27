using BearsEngine.Input;
using BearsEngine.Pathfinding;
using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source.PathfindingDemo;

internal class Node : Entity, IPathfindNode<Node>
{
    private readonly Image _image;
    private bool _passable = true;
    private readonly Line _squareHighlight;

    public Node(IMouse mouse, float x, float y)
    : base(mouse, 50, x, y, GP.Pathfinding.SquareSize)
    {
        Add(_image = new Image(GA.GFX.Pathfinding.Wall, GP.Pathfinding.SquareSize) { Visible = false });
        Add(_squareHighlight = new(Colour.Orange, 1, true, R.Zeroed) { Visible = false });
    }

    public IList<Node> ConnectedNodes { get; set; } = new List<Node>();

    public float DistanceBetweenConnectedNodes => W;

    public object? GraphSearchData { get; set; }

    public Node? ParentNode { get; set; }

    public bool Passable
    {
        get => _passable;
        set
        {
            _passable = value;
            _image.Visible = !value;
        }
    }

    public bool YellowHighlight { set => _squareHighlight.Visible = value; }

    protected override void OnLeftClicked()
    {
        base.OnLeftClicked();
        Passable = !Passable;
    }

    public bool Equals(Node? other)
    {
        if (other is null)
            return false;

        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Node);
    }

    public override string ToString()
    {
        return $"X:{X},Y:{Y}";
    }
}