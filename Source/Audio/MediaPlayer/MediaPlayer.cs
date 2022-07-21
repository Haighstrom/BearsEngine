using System.Media;

namespace BearsEngine.Audio.MediaPlayer;

/// <summary>
/// Simple lightweight media player that uses built in Windows SoundPlayer - no OpenAL calls. WAV files only.
/// </summary>
public class MediaPlayer : IDisposable
{
    private Dictionary<string, SoundPlayer> _sounds;

    public MediaPlayer()
    {
        _sounds = new Dictionary<string, SoundPlayer>();
    }

    public void LoadSound(string soundFile)
    {
        if (!_sounds.ContainsKey(soundFile))
        {
            SoundPlayer sp = new(soundFile);
            sp.Load();
            _sounds.Add(soundFile, sp);
        }
    }

    public void PlaySound(string soundFile)
    {
        if (_sounds.ContainsKey(soundFile)) 
            _sounds[soundFile].Play();
        else
        {
            SoundPlayer sp = new(soundFile);
            sp.Load();
            _sounds.Add(soundFile, sp);
            sp.Play();
        }
    }

    public void Dispose()
    { 
        foreach (SoundPlayer sp in _sounds.Values)
            sp.Dispose();
    }
}
