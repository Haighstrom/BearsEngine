﻿using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source.PathfindingDemo;

internal class Square : Entity
{
    private bool _dragging = false;
    private readonly PathfindingDemoScreen _screen;

    public Square(PathfindingDemoScreen screen, float x, float y, string graphicsPath)
    : base(90, x, y, GP.Pathfinding.SquareSize, graphicsPath)
    {
        _screen = screen;
    }

    public int IndexX => (int)Maths.Clamp((X - GP.Pathfinding.GridTopLeft.X) / GP.Pathfinding.SquareSize.X, 0, _screen.Grid.Width - 1);

    public int IndexY => (int)Maths.Clamp((Y - GP.Pathfinding.GridTopLeft.Y) / GP.Pathfinding.SquareSize.Y, 0, _screen.Grid.Height - 1);

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();
        _dragging = true;
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        if (Mouse.LeftUp)
            _dragging = false;

        if (_dragging)
        {
            var newIndexX = (int)Maths.Clamp((Mouse.ClientX - GP.Pathfinding.GridTopLeft.X) / GP.Pathfinding.SquareSize.X, 0, _screen.Grid.Width - 1);
            var newIndexY = (int)Maths.Clamp((Mouse.ClientY - GP.Pathfinding.GridTopLeft.Y) / GP.Pathfinding.SquareSize.Y, 0, _screen.Grid.Height - 1);

            var startIsHere = _screen.StartSquare.IndexX == newIndexX && _screen.StartSquare.IndexY == newIndexY;
            var endIsHere = _screen.EndSquare.IndexX == newIndexX && _screen.EndSquare.IndexY == newIndexY;

            if (!startIsHere && !endIsHere) //only move if no moveable squares here already
            {
                X = GP.Pathfinding.GridTopLeft.X + newIndexX * GP.Pathfinding.SquareSize.X;
                Y = GP.Pathfinding.GridTopLeft.Y + newIndexY * GP.Pathfinding.SquareSize.Y;
            }
        }
    }
}