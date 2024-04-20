using NAudio.Wave;

namespace BearsEngine.SystemTests.Source.SoundTest;

internal class PlaySFXButton : Button, IDisposable
{
    private string _sfxPath;
    private WaveOutEvent _outputDevice;
    private bool _disposed;

    public PlaySFXButton(Rect position, string sfxPath)
    : base(0, position, Colour.LightGray)
    {
        _sfxPath = sfxPath;
        _outputDevice = new WaveOutEvent();
    }

    protected override void OnLeftClicked()
    {
        base.OnLeftClicked();

        AudioFileReader afr = new(_sfxPath);
        _outputDevice.Init(afr);
        _outputDevice.Play();
    }

    protected virtual void Dispose(bool disposedCorrectly)
    {
        if (!_disposed)
        {
            if (disposedCorrectly)
            {
                //_audioFile.Dispose();
                _outputDevice.Dispose();
            }
            else
                Log.Warning("Window was disposed by the finaliser.");

            _disposed = true;
        }
    }

    ~PlaySFXButton()
    {
        Dispose(disposedCorrectly: false);
    }

    public void Dispose()
    {
        Dispose(disposedCorrectly: true);
        GC.SuppressFinalize(this);
    }

}