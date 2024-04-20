namespace BearsEngine.SystemTests.Source.Globals
{
    internal static class GP
    {
        public static Point DefaultClientSize = new(800, 600);

        public static Rect DefaultCameraViewport = new(100, 100, 400, 400);

        public static Point DefaultCameraSize = new(200, 200);

        public static Rect ReturnButton = new(10, 10, 60, 40);

        public static class ConsoleDemo
        {
            public static Rect ButtonConsoleHide = new(100, 10, 60, 60);
            public static Rect ButtonConsoleShow = new(170, 10, 60, 60);
            public static Rect ButtonConsoleMaximise = new(240, 10, 60, 60);
            public static Rect ButtonConsoleLeftSide = new(310, 10, 60, 60);
            public static Rect ButtonConsoleRightSide = new(380, 10, 60, 60);
        }

        public static class Pathfinding
        {
            public static Rect SolveButton = new(10, 300, 100, 40);
            public static Point SquareSize = new(30, 30);
            public static Point GridTopLeft = new(120, 65);
            public const float SolveLineThickness = 2;
            public static Rect DropDownList = new(10, 90, 100, 30);
            public static int DropDownOptionSpacing = 30;
        }
    }
}