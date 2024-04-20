using BearsEngine.Audio.OpenAL;
using BearsEngine.Audio.OpenAL.Wav;
using BearsEngine.Audio.OpenAL.OggVorbis;

namespace BearsEngine.Audio;

/// <summary>
/// A container for audio. Represents a single piece of audio that can
/// be repeatedly played. 
/// </summary>
public class MusicClip : AudioClip
{
    public MusicClip(string fileName)
        : base(fileName)
    {
    }

    /// <summary>
    /// Plays the audio clip.
    /// </summary>
    internal override void Play()
    {
        lock (AudioManager.Manager)
        {
            if (rawClip is VorbisFile)
                AudioManager.Manager.PlayMusicClip(((VorbisFile)rawClip).makeInstance());
            else if (rawClip is WavFile)
                AudioManager.Manager.PlayMusicClip((WavFile)rawClip);
            else
                throw new NotImplementedException();
        }
    }
}
