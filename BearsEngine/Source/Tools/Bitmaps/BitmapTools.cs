using BearsEngine.Source.Tools;
using System.Drawing;
using System.Runtime.InteropServices;

namespace BearsEngine;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This is part of the Windows API which will only be invoked if on Windows platform.")]
internal static class BitmapTools
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct RgbaColor
    {
        // Sigh: PixelFormat.Format32bppArgb is actually laid out BGRA...
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
    }

    public static Bitmap LoadBitmap(string path)
    {
        Ensure.FileExists(path);

        return new Bitmap(path);
    }

    public unsafe static Bitmap PremultiplyAlpha(Bitmap bitmap)
    {
        //todo: put some tries here as this fucks up sometimes

        // Lock the entire bitmap for Read/Write access as we'll be reading the pixel
        // colour values and altering them in-place.
        var bmlock = Repeat.TryMethod(() => bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat), 5, new TimeSpan(5));

        // This code only works with 32bit argb images - assume no alpha if not this format
        if (bmlock.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            throw new InvalidOperationException($"Unsupported pixel format {bmlock.PixelFormat}. Should be Formar32bppArgb to use TexturePremultiplier");

        var ptr = (byte*)bmlock.Scan0.ToPointer();

        for (var y = 0; y < bmlock.Height; y++)
        {
            for (var x = 0; x < bmlock.Width; x++)
            {
                // Obtain the memory location where our pixel data resides and cast it
                // into a struct to improve sanity.
                var color = (RgbaColor*)(ptr + y * bmlock.Stride + x * sizeof(RgbaColor));

                var alphaFloat = (*color).Alpha / 255.0f;

                (*color).Red = Convert.ToByte(alphaFloat * (*color).Red);
                (*color).Green = Convert.ToByte(alphaFloat * (*color).Green);
                (*color).Blue = Convert.ToByte(alphaFloat * (*color).Blue);
            }
        }

        // The bitmap lock is freed here
        bitmap.UnlockBits(bmlock);

        return bitmap;
    }
}