using System.Runtime.InteropServices;

namespace BearsEngine;

internal sealed class PointerHandler : IDisposable
{
    private bool _disposed;
    private GCHandle _GCHandle;

    public PointerHandler(object o)
    {
        _GCHandle = GCHandle.Alloc(o, GCHandleType.Pinned);

        Handle = _GCHandle.AddrOfPinnedObject();
    }

    private IntPtr Handle { get; }

    private void Dispose(bool disposedCorrectly)
    {
        if (!_disposed)
        {
            if (!disposedCorrectly)
            {
                Log.Warning($"Did not dispose {nameof(PointerHandler)} correctly.");
            }

            _GCHandle.Free();
            _disposed = true;
        }
    }

    ~PointerHandler()
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
