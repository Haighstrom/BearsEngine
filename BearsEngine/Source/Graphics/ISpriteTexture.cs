namespace BearsEngine.Graphics;

public interface ISpriteTexture : ITexture
{
    /// <summary>
    /// The number of frames across
    /// </summary>
    int SpriteSheetColumns { get; }

    /// <summary>
    /// The number of frames down
    /// </summary>
    int SpriteSheetRows { get; }

    /// <summary>
    /// The total number of frames
    /// </summary>
    int Frames { get; }

    /// <summary>
    /// The width of one frame (normalised coordinates)
    /// </summary>
    float FrameWidth { get; }

    /// <summary>
    /// The height of one frame (normalised coordinates)
    /// </summary>
    float FrameHeight { get; }

    /// <summary>
    /// The added padding width between sprite frames (normalised coordinates)
    /// </summary>
    float PaddingWidth { get; }

    /// <summary>
    /// The added padding height between sprite frames (normalised coordinates)
    /// </summary>
    float PaddingHeight { get; }

    (Point UV1, Point UV2, Point UV3, Point UV4) GetUVCoordinates(int frame);
}
