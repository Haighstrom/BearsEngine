using System.Diagnostics.CodeAnalysis;

namespace BearsEngine.Graphics;

public class SpriteTexture : Texture, ISpriteTexture
{
    [SetsRequiredMembers]
    public SpriteTexture(int id, int width, int height, int spriteSheetColumns, int spriteSheetRows, int padding)
        : base(id, width, height)
    {
        SpriteSheetColumns = spriteSheetColumns;
        SpriteSheetRows = spriteSheetRows;
        Frames = spriteSheetColumns * spriteSheetRows;
        PaddingWidth = (float)padding / width;
        PaddingHeight = (float)padding / height;
        FrameWidth = (1f - (1 + spriteSheetColumns) * PaddingWidth) / spriteSheetColumns;
        FrameHeight = (1f - (1 + spriteSheetRows) * PaddingHeight) / spriteSheetRows;
    }

    public int SpriteSheetColumns { get; }

    public int SpriteSheetRows { get; }

    public int Frames { get; }

    public float FrameWidth { get; }

    public float FrameHeight { get; }

    public float PaddingWidth { get; }

    public float PaddingHeight { get; }

    public (Point UV1, Point UV2, Point UV3, Point UV4) GetUVCoordinates(int frame)
    {
        Ensure.That(frame < Frames);

        int indexX = Maths.Mod(frame, SpriteSheetColumns);
        int indexY = frame / SpriteSheetColumns;

        Point uv1, uv2, uv3, uv4;

        uv1 = new()
        {
            X = PaddingWidth + indexX * (FrameWidth + PaddingWidth),
            Y = PaddingHeight + indexY * (FrameHeight + PaddingHeight),
        };

        uv2 = uv1 + new Point(FrameWidth, 0);
        uv3 = uv1 + new Point(0, FrameHeight);
        uv4 = uv1 + new Point(FrameWidth, FrameHeight);

        return (uv1,  uv2, uv3, uv4);
    }
}
