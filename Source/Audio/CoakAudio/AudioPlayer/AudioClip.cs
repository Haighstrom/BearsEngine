using BearsEngine.Audio.OpenAL;
using BearsEngine.Audio.OpenAL.Wav;
using BearsEngine.Audio.OpenAL.OggVorbis;
using System.IO;

namespace BearsEngine.Audio;

/// <summary>
/// A container for audio.  Represents a single piece of audio that can
/// be repeatedly played.
/// </summary>
public abstract class AudioClip
{
    internal AudioFile rawClip;

    /// <summary>
    /// Constructs an audio clip from the given file.
    /// </summary>
    /// <param name="filePath">The file which to read from.</param>
    internal AudioClip(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);

        string ext = Path.GetExtension(filePath);

        switch (ext)
        {
            case (".ogg"):
                rawClip = new VorbisFile(filePath);
                Cache(64 * 1024);
                break;
            case(".wav"):
                rawClip = new WavFile(filePath);
                break;
            default:
                throw new NotSupportedException(ext + " filetype not supported");

        }
    }
            
    /// <summary>
    /// Caches the given number of bytes by reading them in and discarding
    /// them.  This is useful so that when the sound if first played,
    /// there's not a delay.
    /// </summary>
    /// <param name="bytes">Then number of PCM bytes to read.</param>
    protected void Cache(int bytes)
    {
        VorbisFileInstance instance = ((VorbisFile)rawClip).makeInstance();

        int totalBytes = 0;
        byte[] buffer = new byte[4096];

        while (totalBytes < bytes)
        {
            int bytesRead = instance.read(buffer, buffer.Length, 0, 2, 1, null);

            if (bytesRead <= 0)
                break;

            totalBytes += bytesRead;
        }
    }

    /// <summary>
    /// Plays the audio clip.
    /// </summary>
    internal abstract void Play();
}



