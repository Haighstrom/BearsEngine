namespace BearsEngine.Graphics;

public interface ISprite
{
    int Frame { get; set; }

    int LastFrame { get; }

    int TotalFrames { get; }
}
