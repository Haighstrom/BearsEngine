using BearsEngine.Input;
using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source.PathfindingDemo;

internal class SolveButton : Button
{
    PathfindingDemoScreen _parent;
    public SolveButton(PathfindingDemoScreen parent)
        : base(GL.UI.Button, GP.Pathfinding.SolveButton, Colour.LightBlue, GV.Theme, "SOLVE!")
    {
        _parent = parent;
    }

    protected override void OnLeftClicked()
    {
        base.OnLeftClicked();

        _parent.StartSolve();
    }
}
