using BearsEngine.Win32API;
using System.Drawing;
using System.IO;

namespace BearsEngine.Window;

public class Cursor : IDisposable
{
    private static readonly List<string> _cursorTypes = new() { ".cur", ".ani" };
    private static readonly List<string> _bmpTypes = new() { ".bmp", ".gif", ".exif", ".jpg", ".png", ".tiff" };
    private static readonly Cursor _default = new(PredefinedCursors.IDC_ARROW);

    public static Cursor Default => _default;

    //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-destroycursor
    private bool _sharedCursor;
    private bool _disposed = false;

    public Cursor(PredefinedCursors cursor)
    {
        HCursor = User32.LoadImage(cursor);
        _sharedCursor = true;
    }
    public Cursor(Bitmap cursor, int xHotspot = 0, int yHotspot = 0, Point? size = null)
    {
        if (size != null)
            cursor = new Bitmap(cursor, (int)size.Value.X, (int)size.Value.Y);

        IntPtr imgHandle = cursor.GetHicon();

        IconInfo ii = new();
        User32.GetIconInfo(imgHandle, ref ii);

        ii.xHotspot = xHotspot;
        ii.yHotspot = yHotspot;
        ii.IsIcon = false;

        HCursor = User32.CreateIconIndirect(ref ii);
        _sharedCursor = false;
    }
    public Cursor(string filePath, int xHotspot = 0, int yHotspot = 0, Point? size = null)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Could not find file at {filePath}");

        string fileExt = Path.GetExtension(filePath).ToLower();

        if (_cursorTypes.Contains(fileExt))
        {
            HCursor = User32.LoadCursorFromFile(filePath);
            _sharedCursor = true;
        }
        else if (_bmpTypes.Contains(fileExt))
        {
            Bitmap img = new(filePath);

            if (size != null)
                img = new Bitmap(img, (int)size.Value.X, (int)size.Value.Y);

            IntPtr imgHandle = img.GetHicon();

            IconInfo ii = new();
            User32.GetIconInfo(imgHandle, ref ii);

            ii.xHotspot = xHotspot;
            ii.yHotspot = yHotspot;
            ii.IsIcon = false;

            HCursor = User32.CreateIconIndirect(ref ii);
            _sharedCursor = false;
        }
        else
            throw new ArgumentException($"filePath ({filePath})'s extension ({fileExt}) is neither valid as a cursor nor a BMP", nameof(filePath));
    }

    public int XHotspot { get; }
    public int YHotspot { get; }
    internal IntPtr HCursor { get; }

    #region IDisposable
    protected virtual void Dispose(bool disposedCorrectly)
    {
        if (!_disposed)
        {
            if (disposedCorrectly)
            {
                // TODO: dispose managed state (managed objects)
            }
            else
                HConsole.Warning("Cursor was disposed by the finaliser.");

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            if (!_sharedCursor)
                User32.DestroyCursor(HCursor);

            _disposed = true;
        }
    }

    ~Cursor()
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
    #endregion
}