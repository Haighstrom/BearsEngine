namespace BearsEngine.Audio.OpenAL;

/// <summary>
/// Provides methods to instantiate, use and destroy an audio context for playback.
/// Static methods are provided to list available devices known by the driver.
/// </summary>
internal sealed class AudioContext : IDisposable
{
    #region --- Fields ---

    bool disposed;
    bool is_processing;
    IntPtr device_handle;
    IntPtr context_handle;
    bool context_exists;

    string device_name;
    static object audio_context_lock = new();
    static Dictionary<IntPtr, AudioContext> available_contexts = new();

    #endregion

    #region --- Constructors ---

    #region static AudioContext()

    /// \internal
    /// <summary>
    /// Runs before the actual class constructor, to load available devices.
    /// </summary>
    static AudioContext()
    {
        if (AudioDeviceEnumerator.IsOpenALSupported) // forces enumeration
            System.Console.WriteLine("OpenAL successfully initialised\n");
        else
            System.Console.WriteLine("OpenAL was not found or not succesfully set up\n");
    }

    #endregion static AudioContext()
    
    /// <summary>Constructs a new AudioContext, using the default audio device.</summary>
    public AudioContext()
        : this(null)
    {
    }

    /// <summary> Constructs a new AudioContext using the named device </summary> <param name="device"></param>
    public AudioContext(string device)
    {
        CreateContext(device, 0, 0, true, true, 0);
    }
    #endregion --- Constructors ---

    #region --- Private Methods ---

    #region CreateContext       
           
    private void CreateContext(string device, int freq, int refresh, bool sync, bool enableEfx, int efxAuxiliarySends)
    {
        if (!AudioDeviceEnumerator.IsOpenALSupported)
            throw new Exception("AudioContext.CreateContext - IsOpenALSupported evaluated to false. This can mean the dll openal32.dll could not be found.");

        if (AudioDeviceEnumerator.Version == AudioDeviceEnumerator.AlcVersion.Alc1_1 && AudioDeviceEnumerator.AvailablePlaybackDevices.Count == 0)    // Alc 1.0 does not support device enumeration.
            throw new NotSupportedException("No audio hardware is available.");
        if (context_exists) throw new NotSupportedException("Multiple AudioContexts are not supported.");
        if (freq < 0) throw new ArgumentOutOfRangeException("freq", freq, "Should be greater than zero.");
        if (refresh < 0) throw new ArgumentOutOfRangeException("refresh", refresh, "Should be greater than zero.");


        if (!string.IsNullOrEmpty(device))
        {
            device_name = device;
            device_handle = Alc.OpenDevice(device); // try to open device by name
        }
        if (device_handle == IntPtr.Zero)
        {
            device_name = "IntPtr.Zero (null string)";
            device_handle = Alc.OpenDevice(null); // try to open unnamed default device
        }
        if (device_handle == IntPtr.Zero)
        {
            device_name = DefaultDevice;
            device_handle = Alc.OpenDevice(DefaultDevice); // try to open named default device
        }
        if (device_handle == IntPtr.Zero)
        {
            device_name = "None";
            throw new Exception(string.Format("Audio device '{0}' does not exist or is tied up by another application.", string.IsNullOrEmpty(device) ? "default" : device));
        }

        CheckErrors();

        // Build the attribute list
        List<int> attributes = new();

        if (freq != 0)
        {
            attributes.Add((int)AlcContextAttributes.Frequency);
            attributes.Add(freq);
        }

        if (refresh != 0)
        {
            attributes.Add((int)AlcContextAttributes.Refresh);
            attributes.Add(refresh);
        }

        attributes.Add((int)AlcContextAttributes.Sync);
        attributes.Add(sync ? 1 : 0);

        if (enableEfx && Alc.IsExtensionPresent(device_handle, "ALC_EXT_EFX"))
        {
            Alc.GetInteger(device_handle, AlcGetInteger.EfxMaxAuxiliarySends, 1, out int num_slots);

            attributes.Add((int)AlcContextAttributes.EfxMaxAuxiliarySends);
            attributes.Add(num_slots);
        }
        attributes.Add(0);

        context_handle = Alc.CreateContext(device_handle, attributes.ToArray());

        if (context_handle == IntPtr.Zero)
        {
            Alc.CloseDevice(device_handle);
            throw new Exception("The audio context could not be created with the specified parameters.");
        }

        CheckErrors();

        // HACK: OpenAL SI on Linux/ALSA crashes on MakeCurrent. This hack avoids calling MakeCurrent when
        // an old OpenAL version is detect - it may affect outdated OpenAL versions different than OpenAL SI,
        // but it looks like a good compromise for now.
        if (AudioDeviceEnumerator.AvailablePlaybackDevices.Count > 0)
            MakeCurrent();

        CheckErrors();

        device_name = Alc.GetString(device_handle, AlcGetString.DeviceSpecifier);


        lock (audio_context_lock)
        {
            available_contexts.Add(this.context_handle, this);
            context_exists = true;
        }            
    }

    #endregion --- Private Methods ---

    #region static void MakeCurrent(AudioContext context)

    /// \internal
    /// <summary>Makes the specified AudioContext current in the calling thread.</summary>
    /// <param name="context">The OpenTK.Audio.AudioContext to make current, or null.</param>
    /// <exception cref="ObjectDisposedException">
    /// Occurs if this function is called after the AudioContext has been disposed.
    /// </exception>        
    static void MakeCurrent(AudioContext context)
    {
        lock (audio_context_lock)
        {
            if (!Alc.MakeContextCurrent(context != null ? context.context_handle : IntPtr.Zero))
                throw new Exception(string.Format("ALC {0} error detected at {1}.", Alc.GetError(context != null ? context.context_handle : IntPtr.Zero).ToString(),  context != null ? context.ToString() : "null"));
        }
    }

    #endregion

    #region internal bool IsCurrent

    /// <summary>
    /// Gets or sets a System.Boolean indicating whether the AudioContext
    /// is current.
    /// </summary>
    /// <remarks>
    /// Only one AudioContext can be current in the application at any time,
    /// <b>regardless of the number of threads</b>.
    /// </remarks>
    internal bool IsCurrent
    {
        get
        {
            lock (audio_context_lock)
            {
                if (available_contexts.Count == 0)
                    return false;
                else
                {
                    return CurrentContext == this;
                }
            }
        }
        set
        {
            if (value) MakeCurrent(this);
            else MakeCurrent(null);
        }
    }

    #endregion

    #region IntPtr Device

    IntPtr Device { get { return device_handle; } }

    #endregion

    #endregion

    #region --- Public Members ---

    #region CheckErrors

    /// <summary>
    /// Checks for ALC error conditions.
    /// </summary>
    public void CheckErrors()
    {
        if (disposed)
            throw new ObjectDisposedException(this.GetType().FullName);

        new AudioDeviceErrorChecker(device_handle).Dispose();
    }

    #endregion

    #region CurrentError

    /// <summary>
    /// Returns the ALC error code for this instance.
    /// </summary>
    internal AlcError CurrentError
    {
        get
        {
            if (disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return Alc.GetError(device_handle);
        }
    }

    #endregion

    #region MakeCurrent

    /// <summary>Makes the AudioContext current in the calling thread.</summary>
    /// <remarks>
    /// Only one AudioContext can be current in the application at any time,
    /// <b>regardless of the number of threads</b>.
    /// </remarks>
    public void MakeCurrent()
    {
        if (disposed)
            throw new ObjectDisposedException(GetType().FullName);

        MakeCurrent(this);
    }

    #endregion

    #region IsProcessing

    /// <summary>
    /// Gets a System.Boolean indicating whether the AudioContext is
    /// currently processing audio events.
    /// </summary>
    /// <seealso cref="Process"/>
    public bool IsProcessing
    {
        get
        {
            if (disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return is_processing;
        }
        private set { is_processing = value; }
    }

    #endregion       

    #region public void Process

    /// <summary>
    /// Processes queued audio events.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If AudioContext.IsSynchronized is true, this function will resume
    /// the internal audio processing thread. If AudioContext.IsSynchronized is false,
    /// you will need to call this function multiple times per second to process
    /// audio events.
    /// </para>
    /// <para>
    /// In some implementations this function may have no effect.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Occurs when this function is called after the AudioContext had been disposed.</exception>       
    public void Process()
    {
        if (disposed)
            throw new ObjectDisposedException(this.GetType().FullName);

        Alc.ProcessContext(this.context_handle);
        IsProcessing = true;
    }

    #endregion
          

   

    #region CurrentDevice

    /// <summary>
    /// Gets a System.String with the name of the device used in this context.
    /// </summary>
    public string CurrentDevice
    {
        get
        {
            if (disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return device_name;
        }
    }

    #endregion

    #endregion --- Public Members ---

    #region --- Static Members ---

    #region public static AudioContext CurrentContext

    /// <summary>
    /// Gets the OpenTK.Audio.AudioContext which is current in the application.
    /// </summary>
    /// <remarks>
    /// Only one AudioContext can be current in the application at any time,
    /// <b>regardless of the number of threads</b>.
    /// </remarks>
    public static AudioContext CurrentContext
    {
        get
        {
            lock (audio_context_lock)
            {
                if (available_contexts.Count == 0)
                    return null;
                else
                {
                    available_contexts.TryGetValue(Alc.GetCurrentContext(), out AudioContext context);
                    return context;
                }
            }
        }
    }

    #endregion

    #region AvailableDevices

    /// <summary>
    /// Returns a list of strings containing all known playback devices.
    /// </summary>
    public static IList<string> AvailableDevices
    {
        get
        {
            return AudioDeviceEnumerator.AvailablePlaybackDevices;
        }
    }
    #endregion public static IList<string> AvailablePlaybackDevices

    #region DefaultDevice

    /// <summary>
    /// Returns the name of the device that will be used as playback default.
    /// </summary>
    public static string DefaultDevice
    {
        get
        {
            return AudioDeviceEnumerator.DefaultPlaybackDevice;
        }
    }

    #endregion

    #endregion

    #region --- IDisposable Members ---

    /// <summary>
    /// Disposes of the AudioContext, cleaning up all resources consumed by it.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    void Dispose(bool manual)
    {
        if (!disposed)
        {
            if (IsCurrent)
                IsCurrent = false;

            if (context_handle != IntPtr.Zero)
            {
                available_contexts.Remove(context_handle);
                Alc.DestroyContext(context_handle);
            }

            if (device_handle != IntPtr.Zero)
                Alc.CloseDevice(device_handle);

            if (manual)
            {
            }
            disposed = true;
        }
    }

    /// <summary>
    /// Finalizes this instance.
    /// </summary>
    ~AudioContext()
    {
        this.Dispose(false);
    }

    #endregion

    #region --- Overrides ---

    /// <summary>
    /// Calculates the hash code for this instance.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /// <summary>
    /// Compares this instance with another.
    /// </summary>
    /// <param name="obj">The instance to compare to.</param>
    /// <returns>True, if obj refers to this instance; false otherwise.</returns>
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that desrcibes this instance.
    /// </summary>
    /// <returns>A <see cref="string"/> that desrcibes this instance.</returns>
    public override string ToString()
    {
        return string.Format("{0} (handle: {1}, device: {2})",
                             this.device_name, this.context_handle, this.device_handle);
    }

    #endregion
}
