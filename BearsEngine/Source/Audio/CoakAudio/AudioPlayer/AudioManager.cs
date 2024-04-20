using BearsEngine.Audio.OpenAL.Wav;
using BearsEngine.Audio.OpenAL.OggVorbis;
using System.Threading;

namespace BearsEngine.Audio.OpenAL;

/// <summary>
/// A manager for audio clips and audio channels.  Puts audio clips into
/// empty channels when they're played.
/// </summary>
internal class AudioManager
{
    internal bool Paused { get { return _paused; } set { _paused = value; } }
    private bool _paused = false;
    internal int ChannelCount { get; private set; }
    internal int BuffersPerChannel { get; private set; }
    internal int BytesPerBuffer { get; private set; }

    internal AudioChannel MusicChannel { get; private set; }
    internal AudioChannel[] SFXChannels { get; private set; }

    internal Thread UpdateThread { get; private set; }

    internal bool RunUpdates { get; set; }

    private static AudioManager manager = null;

    /// <summary>
    /// The sole instance of the audio manager.
    /// </summary>
    internal static AudioManager Manager
    {
        get
        {
            if(manager == null)
                manager = new AudioManager();
            return manager;
        }

        set
        {
            manager = value;
        }
    }

    /// <summary>
    /// Disposes of all resources used by the audio manager.
    /// </summary>
    ~AudioManager()
    {
        Dispose();
    }

    /// <summary>
    /// Constructs a default audio manager with 16 channels.
    /// </summary>
    internal AudioManager()
    {
        //this 568 here represents the length of each buffer - was originally 4096. Make smaller to give snappier mroe responsive sound
        Init(16, 4 * 8, 568, true);
    }

    /// <summary>
    /// Initializes the audio manager.
    /// </summary>
    /// <param name="channels">The number of channels to use.</param>
    /// <param name="buffersPerChannel">The number of buffers each channel will contain.</param>
    /// <param name="bytesPerBuffer">The number of bytes in each buffer.</param>
    /// <param name="launchThread">If true, a separate thread will be launched to handle updating the sound manager.  
    ///                            Otherwise, a thread will not be launched and manual calls to Update() will be required.</param>
    internal AudioManager(int channels, int buffersPerChannel, int bytesPerBuffer, bool launchThread)
    {
        Init(channels, buffersPerChannel, bytesPerBuffer, launchThread);
    }

    /// <summary>
    /// Initializes the audio manager.
    /// </summary>
    /// <param name="channels">The number of channels to use.</param>
    /// <param name="buffersPerChannel">The number of buffers each channel will contain.</param>
    /// <param name="bytesPerBuffer">The number of bytes in each buffer.</param>
    /// <param name="launchThread"></param>
    private void Init(int channels, int buffersPerChannel, int bytesPerBuffer, bool launchThread)
    {
        RunUpdates = launchThread;
        //Leave one channel for the music channel
        ChannelCount = channels-1;

        MusicChannel = new AudioChannel(buffersPerChannel, bytesPerBuffer);

        SFXChannels = new AudioChannel[ChannelCount];
        for (int i = 0; i < ChannelCount; i++)
            SFXChannels[i] = new AudioChannel(buffersPerChannel, bytesPerBuffer);

        Manager = this;
        
        if(launchThread)
        {
            UpdateThread = new Thread(RunUpdateLoop);
            UpdateThread.Name = "Audio Manager";
            UpdateThread.IsBackground = true;
            UpdateThread.Start();
        }
        else
        {
            UpdateThread = null;
        }
    }

    internal void SetMusicVolume(float vol)
    {
        MusicChannel.SetVolume(vol);
    }
    internal void SetSFXVolume(float vol)
    {
        foreach (AudioChannel channel in SFXChannels)
            channel.SetVolume(vol);
    }

    /// <summary>
    /// Plays the audio clip on the dedicated single music channel.
    /// </summary>
    /// <param name="clip">The audio clip to play.</param>
    internal void PlayMusicClip(WavFile clip)
    {
        if (!MusicChannel.IsFree)
            MusicChannel.Stop();

        MusicChannel.Play(clip);
    }

    /// <summary>
    /// Plays the audio clip on the dedicated single music channel.
    /// </summary>
    /// <param name="clip">The audio clip to play.</param>
    internal void PlayMusicClip(VorbisFileInstance clip)
    {
        if (!MusicChannel.IsFree)
            MusicChannel.Stop();

        MusicChannel.Play(clip);
    }

    /// <summary>
    /// Plays the audio clip on the first free SFX channel - one channel is reserved for music.
    /// </summary>
    /// <param name="clip">The audio clip to play.</param>
    internal void PlaySFXClip(WavFile clip)
    {
        // If all channels are busy, the clip will be ignored.  
        foreach (AudioChannel channel in SFXChannels)
        {
                if (channel.IsFree)
                {
                    channel.Play(clip);
                    return;
                }
        }

        //if we reached here we didn't hit that return above, so all channels were occupied and the sound won't be played
        //let's throw a warning
        //DebugTools.Warning("All SFX channels are full, sound was not played");
    }

    /// <summary>
    /// Plays the audio clip on the first free SFX channel - one channel is reserved for music.
    /// </summary>
    /// <param name="clip">The audio clip to play.</param>
    internal void PlaySFXClip(VorbisFileInstance clip)
    {
        // If all channels are busy, the clip will be ignored.  
        foreach (AudioChannel channel in SFXChannels)
        {
            if (channel.IsFree)
            {
                channel.Play(clip);
                return;
            }

            //if we reached here we didn't hit that return above, so all channels were occupied and the sound won't be played
            //let's throw a warning
            // DebugTools.Warning("All SFX channels are full, sound was not played");
        }
    }

    internal void StopSFX()
    {
        foreach (AudioChannel channel in SFXChannels)
        {
            channel.Stop();
        }
    }

    /// <summary>
    /// Performs a single update by updating all channels.
    /// </summary>
    internal void Update()
    {
        if (_paused)
            return;

        lock (this)
        {
            MusicChannel.Update();
        }

        foreach (AudioChannel channel in SFXChannels)
        {
            lock (this)
            {
                channel.Update();
            }
        }
    }

    /// <summary>
    /// Continuously updates the audio manager.  This method will not return
    /// unless it's interrupted, so it's best to run it in a separate 
    /// thread.
    /// </summary>
    internal void RunUpdateLoop()
    {
        while (RunUpdates)
        {
            Update();
            // TODO: Is 1ms long enough to still have good performance outside
            // of the audio?
            Thread.Sleep(1);
        }
    }

    /// <summary>
    /// Dispose of the audio manager and frees its audio memory.
    /// </summary>
    internal void Dispose()
    {
        RunUpdates = false;
        UpdateThread.Join();

        foreach (AudioChannel channel in SFXChannels)
        {
            channel.Dispose();
        }

        MusicChannel.Dispose();
    }
}
