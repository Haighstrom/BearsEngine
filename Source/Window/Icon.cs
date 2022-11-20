using BearsEngine.Win32API;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace BearsEngine.Window;

public class Icon : IDisposable
{
    private static readonly List<string> _bmpTypes = new() { ".bmp", ".gif", ".exif", ".jpg", ".png", ".tiff" };

    public static Icon Default { get; } = new(PredefinedIcons.IDI_APPLICATION);

    private readonly Bitmap? _bitmap;
    private bool _disposed = false;

    public Icon(PredefinedIcons icon)
    {
        HIcon = User32.LoadImage(icon);
    }

    public Icon(Bitmap icon, Point? size = null)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            throw new InvalidOperationException("Bitmap only valid for use in Windows");

        _bitmap = icon;

        if (size != null)
            _bitmap = new Bitmap(_bitmap, (int)size.Value.X, (int)size.Value.Y);

        HIcon = _bitmap.GetHicon();
    }

    public Icon(string filePath)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            throw new InvalidOperationException("Bitmap only valid for use in Windows");

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Could not find file at {filePath}");

        string fileExt = Path.GetExtension(filePath).ToLower();

        if (!_bmpTypes.Contains(fileExt))
            throw new ArgumentException($"filePath ({filePath})'s extension ({fileExt}) is not valid as a Bitmap", nameof(filePath));

        _bitmap = new(filePath);

        HIcon = _bitmap.GetHicon();
    }

    internal IntPtr HIcon { get; }

    protected virtual void Dispose(bool disposedCorrectly)
    {
        if (!_disposed)
        {
            if (disposedCorrectly)
            {
                // TODO: dispose managed state (managed objects)
            }
            else
                BE.Logging.Warning("Cursor was disposed by the finaliser.");

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _bitmap?.Dispose();

            _disposed = true;
        }
    }

    ~Icon()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposedCorrectly: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposedCorrectly: true);
        GC.SuppressFinalize(this);
    }
}