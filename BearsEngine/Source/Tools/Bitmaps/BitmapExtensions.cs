namespace BearsEngine;

internal static class BitmapExtensions
{
    public static Rect NonZeroAlphaRegion(this System.Drawing.Bitmap b)
    {
        return OpenGLHelper.NonZeroAlphaRegion(b);
    }

    public static void WriteToFile(this System.Drawing.Bitmap b, string targetPath)
    {
        OpenGLHelper.WriteBitmapToFile(b, targetPath);
    }
}
