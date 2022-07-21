namespace BearsEngine.Audio.OpenAL;

internal static class AudioDeviceEnumerator
{
    #region All device strings

    private static readonly List<string> available_playback_devices = new();
    private static readonly List<string> available_recording_devices = new();

    internal static IList<string> AvailablePlaybackDevices
    {
        get
        {
            return available_playback_devices.AsReadOnly();
        }
    }
    internal static IList<string> AvailableRecordingDevices
    {
        get
        {
            return available_recording_devices.AsReadOnly();
        }
    }

    #endregion All device strings

    #region Default device strings

    private static string default_playback_device;
    internal static string DefaultPlaybackDevice
    {
        get
        {
            return default_playback_device;
        }
    }      

    #endregion Default device strings

    #region Is OpenAL supported?

    private static bool openal_supported = true;
    internal static bool IsOpenALSupported
    {
        get
        {
            return openal_supported;
        }
    }

    #endregion Is OpenAL supported?

    #region Alc Version number

    internal enum AlcVersion
    {
        Alc1_0,
        Alc1_1
    }

    private static AlcVersion version;
    internal static AlcVersion Version
    {
        get
        {
            return version;
        }
    }

    #endregion Alc Version number

    #region Constructors

    // Loads all available audio devices into the available_*_devices lists.
    static AudioDeviceEnumerator()
    {
        IntPtr dummy_device = IntPtr.Zero;
        IntPtr dummy_context = IntPtr.Zero;

        try
        {
            if (AudioPlayer.VerboseLogging)
            {
                Console.WriteLine();
                Console.WriteLine("Enumerating audio devices.");
                Console.WriteLine();
            }

            // need a dummy context for correct results
            dummy_device = Alc.OpenDevice(null);
            dummy_context = Alc.CreateContext(dummy_device, null);   //this line prints error message

            bool dummy_success = Alc.MakeContextCurrent(dummy_context);
            AlcError dummy_error = Alc.GetError(dummy_device);
            if (!dummy_success || dummy_error != AlcError.NoError)
                throw new HException("Failed to create dummy Context. Device (" + dummy_device.ToString() +") Context (" + dummy_context.ToString() + ") MakeContextCurrent " + (dummy_success ? "succeeded" : "failed") + ", Alc Error (" + dummy_error.ToString() + ") " + Alc.GetString(IntPtr.Zero, (AlcGetString)dummy_error));
          
            // Get a list of all known playback devices, using best extension available
            if (Alc.IsExtensionPresent(IntPtr.Zero, "ALC_ENUMERATION_EXT"))
            {
                version = AlcVersion.Alc1_1;
                if (Alc.IsExtensionPresent(IntPtr.Zero, "ALC_ENUMERATE_ALL_EXT"))
                {
                    available_playback_devices.AddRange(Alc.GetString(IntPtr.Zero, AlcGetStringList.AllDevicesSpecifier));
                    default_playback_device = Alc.GetString(IntPtr.Zero, AlcGetString.DefaultAllDevicesSpecifier);
                }
                else
                {
                    available_playback_devices.AddRange(Alc.GetString(IntPtr.Zero, AlcGetStringList.DeviceSpecifier));
                    default_playback_device = Alc.GetString(IntPtr.Zero, AlcGetString.DefaultDeviceSpecifier);
                }
            }
            else
            {
                version = AlcVersion.Alc1_0;
                if (AudioPlayer.VerboseLogging)
                    Console.WriteLine("Device enumeration extension not available. Failed to enumerate playback devices.");
            }

            //Check for OpenAL errors
            AlcError playback_err = Alc.GetError(dummy_device);
            if (playback_err != AlcError.NoError)
                throw new HException("Alc Error occured when querying available playback devices. " + playback_err.ToString());

            
            if (AudioPlayer.VerboseLogging)
            {
                Console.WriteLine("Found playback devices:");
                foreach (string s in available_playback_devices)
                    Console.WriteLine(s);

                Console.WriteLine("Default playback device: " + default_playback_device);                   
            }
        }
        catch (Exception ace)
        {
            AudioPlayer.OpenALFoundNotSupported(ace);
            openal_supported = false;
        }
        finally
        {
            // clean up the dummy context
            Alc.MakeContextCurrent(IntPtr.Zero);
            if (dummy_context != IntPtr.Zero)
                Alc.DestroyContext(dummy_context);
            if (dummy_device != IntPtr.Zero)
                Alc.CloseDevice(dummy_device);
        }
    }

    #endregion
}
