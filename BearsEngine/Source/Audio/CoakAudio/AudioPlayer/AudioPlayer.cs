using BearsEngine.Audio.OpenAL;


namespace BearsEngine.Audio;

public enum AudioState { Playing, Paused, Stopped }

public static class AudioPlayer
{
    /// <summary>
    /// Set this to true (before calling Initialise) to enable full stack traces and exception details to be logged to the console.
    /// </summary>
    public static bool VerboseLogging { get; set; } = false;

    /// <summary>
    /// Whether music clips/playlists will restart when they reach the end. Default is true.
    /// </summary>
    public static bool LoopMusic { get { return _loopMusic; } set { _loopMusic = value; } }
    private static bool _loopMusic = true;

    /// <summary>
    /// Whether music playlists should be shuffled (will occur when they restart after a loop too). Default is true.
    /// </summary>
    public static bool ShuffleMusic { get { return _shuffleMusic; } set { _shuffleMusic = value; } }
    private static bool _shuffleMusic = true;

    private static List<MusicClip> _playlist = new();
    private static int _playlistIndex = 0;
    private static MusicClip _lastPlayedMusicClip;

    private static  AudioState _state = AudioState.Stopped;

    /// <summary>
    /// Gets the current state (Playing/Paused/Stopped) of the AudioPlayer. Remember to call Update every frame to make sure these things work!
    /// </summary>
    public static AudioState State
    {
        get { return _state; }
        private set { _state = value; }
    }

    private static int _channels = 16;
    public static int Channels { get { return _channels; } set { _channels = value; } }
    private static int _buffersPerChannel = 32;
    public static int BuffersPerChannel { get { return _buffersPerChannel; } set { _buffersPerChannel = value; } }
    private static int _bytesPerBuffer = 568;
    public static int BytesPerBuffer { get { return _bytesPerBuffer; } set { _bytesPerBuffer = value; } }
    private static Random Rand { get; set; }

    /// <summary>
    /// If Initialise is called, and there are no errors (such as there not being any Audio Devices, or OpenAL not being installed etc, this will be set to true.
    /// Every call to Audio checks this and will immediately return if it is false.
    /// </summary>
    public static bool AudioInitialisedSuccessfully { get; private set; } = false;

    /// <summary>
    /// Boolean flag to specify if this PC has a working OpenAL setup installed. Assumed true to start with then flipped to false if OpenALFoundNotSupported() is called during initialisation
    /// </summary>
    public static bool OpenALSupported { get; private set; } = true;

    /// <summary>
    /// Call this static initialisation method before making any Audio calls to set up the OpenAL context and Audio Manager. It is slow, so reccomended to call during initial game load. Only needs to be called once - in fact I'm not sure what happens if you were to try to call it twice..
    /// </summary>
    public static void Initialise()
    {
        try
        {
            AudioContext ac = new();
            AudioManager.Manager = new AudioManager(Channels, BuffersPerChannel, BytesPerBuffer, true);
            Rand = new Random();
        }
        catch(Exception e)  //Catch any errors during initialisation - these will mean it failed, so return, keeping AudioInitialisedSuccessfully set to false.
        {
            Log.Warning("Audio Error (BearsEngine AudioPlayer.Initialise): " + e.Message);
            Log.Verbose(e);
            return;
        }

        //Check whether the OpenAL installed flag was thrown
        if (!OpenALSupported)
            return;

        //If we got here everything should have gone well and we are ready to play some audio!
        //Added MCoak 2018-01-01 - needs thourough testing
        AudioInitialisedSuccessfully = true;
    }

    //This is called by AudioDeviceEnumerator if it fails due to a working and correct version OpenAL not being found
    internal static void OpenALFoundNotSupported(Exception e)
    {
        //We should do something here later - like install OpenAL on the computer running this
        Log.Warning("OpenAL not installed correctly on this PC (BearsEngine AudioPlayer.OpenALFoundNotSupported) - " + e.GetType() + " : " + e.Message);
        
        Log.Verbose(e);

        OpenALSupported = false;
    }

    public static MusicClip LoadMusicFile(string filePath)
    {
        if (!AudioInitialisedSuccessfully)
            return null;

        MusicClip clip = new(filePath);
        return clip;
    }

    public static SFXClip LoadSFXFile(string filePath)
    {
        if (!AudioInitialisedSuccessfully)
            return null;

        SFXClip clip = new(filePath);
        return clip;
    }

    public static void Update()
    {
        if (!AudioInitialisedSuccessfully)
            return;

        if (AudioManager.Manager.MusicChannel.IsFree && State == AudioState.Playing)
            MusicStopped();
    }

    //Runs when we get to the end of a music track
    private static void MusicStopped()
    {
        if (!AudioInitialisedSuccessfully)
            return;

        if (_playlist == null || _playlist.Count == 0)
            return;

        _playlistIndex++;

        if (_playlistIndex >= _playlist.Count)  //if that was the last track in the playlist
        {
            if (!LoopMusic)
            {
                State = AudioState.Stopped;
                _playlist = new List<MusicClip>();
                _playlistIndex = 0;
                _lastPlayedMusicClip = null;
            }
            else
            {
                if (_shuffleMusic)
                {
                    _playlist = Shuffle(_playlist);

                    //Make sure same clip doesn't play twice when looping playlist
                    if (_lastPlayedMusicClip != null && _playlist.Count > 1)
                        while (_playlist[0] == _lastPlayedMusicClip)
                            _playlist = Shuffle(_playlist);
                }

                _playlistIndex = 0;
                _playlist[0].Play();
                _lastPlayedMusicClip = _playlist[0];
            }
        }
        else    //Just play next track in the playlist
        {
            _playlist[_playlistIndex].Play();
            _lastPlayedMusicClip = _playlist[_playlistIndex];
        }
    }

    public static void PlaySFX(SFXClip clip)
    {
        if (!AudioInitialisedSuccessfully)
            return;

        if(clip != null)
            clip.Play();            
    }
    public static void PlayMusic(MusicClip clip)
    {
        PlayMusic(new List<MusicClip>() { clip });
    }
    public static void PlayMusic(params MusicClip[] clips)
    {
        PlayMusic(clips.ToList());
    }
    public static void PlayMusic(List<MusicClip> clips)
    {
        if (!AudioInitialisedSuccessfully)
            return;

        _playlist = clips;

        if (_shuffleMusic)
        {
            _playlist = Shuffle(_playlist);

            //Make sure same clip doesn't play tiwce when looping playlist
            if (_lastPlayedMusicClip != null && _playlist.Count > 1)
                while (_playlist[0] == _lastPlayedMusicClip)
                    _playlist = Shuffle(_playlist);
        }

        _playlistIndex = 0;

        if(_playlist[0] != null)
            _playlist[0].Play();

        _lastPlayedMusicClip = _playlist[0];
        State = AudioState.Playing;
    }

    /// <summary>
    /// From 0f to 1.0f - will be clamped to these values anyway.
    /// </summary>
    /// <param name="vol"></param>
    public static void SetMusicVolume(float vol)
    {
        if (!AudioInitialisedSuccessfully)
            return;

        vol = Clamp(vol, 0f, 1f);
        AudioManager.Manager.SetMusicVolume(vol);
    }

    /// <summary>
    /// From 0f to 1.0f - will be clamped to these values anyway.
    /// </summary>
    /// <param name="vol"></param>
    public static void SetSFXVolume(float vol)
    {
        if (!AudioInitialisedSuccessfully)
            return;

        vol = Clamp(vol, 0f, 1f);
        AudioManager.Manager.SetSFXVolume(vol);
    }

    public static void PauseAudio()
    {
        if (!AudioInitialisedSuccessfully)
            return;

        if (State == AudioState.Stopped)
            return;
        AudioManager.Manager.Paused = true;
        State = AudioState.Paused;
    }
    public static void ResumeAudio()
    {
        if (!AudioInitialisedSuccessfully)
            return;

        AudioManager.Manager.Paused = false;
        if(State == AudioState.Paused)
            State = AudioState.Playing;
    }

    public static void StopAudio()
    {
        StopMusic();
        StopSFX();
    }
    public static void StopMusic()
    {
        if (!AudioInitialisedSuccessfully)
            return;

        AudioManager.Manager.MusicChannel.Stop();
        State = AudioState.Stopped;
        _playlist = new List<MusicClip>();
        
        _lastPlayedMusicClip = null;
        _playlistIndex = 0;
    }
    public static void StopSFX()
    {
        if (!AudioInitialisedSuccessfully)
            return;

        AudioManager.Manager.StopSFX();
    }

    private static List<T> Shuffle<T>(List<T> array)
    {
        for (int i = array.Count; i > 1; i--)
        {
            // pick random element 0 <= j < i
            int j = Rand.Next(i);
            // swap i and j
            T temp = array[j];
            array[j] = array[i - 1];
            array[i - 1] = temp;
        }
        return array;
    }

    private static float Clamp(float num, float min, float max)
    {
        return Math.Max(Math.Min(num, max), min);
    }

    public static void Dispose()
    {
        StopMusic();
        StopSFX();

        try
        {
            AudioManager.Manager.Dispose();
        }
        catch (Exception e)
        {
            Log.Warning("Audio dispose error: " + e.Message);
        }
    }
}
