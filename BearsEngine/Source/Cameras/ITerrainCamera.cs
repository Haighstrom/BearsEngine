namespace BearsEngine.Worlds.Cameras;

public interface ITerrainCamera : ICamera
{
    public int this[int x, int y] { get; set; }

    public int[,] MapValues { get; }

    public int DefaultIndex { get; }

    public int MapW { get; }

    public int MapH { get; }
}