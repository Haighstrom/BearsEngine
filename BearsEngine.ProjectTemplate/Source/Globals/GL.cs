namespace BearsEngine.ProjectTemplate.Source.Globals;

/// <summary>
/// Single place to hold graphical layers. Higher is deeper, i.e. below. Separate between UI and 'entities' within cameras as layer sorting happens within a container/parent and cascades
/// </summary>
internal static class GL
{
    public static class Entities
    {
        public const int TopMost = 1;

        public const int BottomMost = 9999;
    }

    public static class UI
    {
        public const int HUD = 100;
        public const int Camera = 200;
    }
}