using System.Runtime.InteropServices;
using System.Security;


namespace BearsEngine.Audio.OpenAL;

// * * * CLEANED UP ABOVE THIS LINE * * *

/// <summary>
/// PInvokes to provide access to the OpenAL 1.1 flat API.
/// </summary>
internal static class AL
{
    internal const string Lib = "openal32.dll";
    private const CallingConvention Style = CallingConvention.Cdecl;

    #region BufferData
    /// <summary>This function fills a buffer with audio buffer. All the pre-defined formats are PCM buffer, but this function may be used by extensions to load other buffer types as well.</summary>
    /// <param name="bid">buffer Handle/Name to be filled with buffer.</param>
    /// <param name="format">Format type from among the following: ALFormat.Mono8, ALFormat.Mono16, ALFormat.Stereo8, ALFormat.Stereo16.</param>
    /// <param name="buffer">Pointer to a pinned audio buffer.</param>
    /// <param name="size">The size of the audio buffer in bytes.</param>
    /// <param name="freq">The frequency of the audio buffer.</param>
    [DllImport(Lib, EntryPoint = "alBufferData", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void BufferData(uint bid, ALFormat format, IntPtr buffer, int size, int freq);

    /// <summary>This function fills a buffer with audio buffer. All the pre-defined formats are PCM buffer, but this function may be used by extensions to load other buffer types as well.</summary>
    /// <param name="bid">buffer Handle/Name to be filled with buffer.</param>
    /// <param name="format">Format type from among the following: ALFormat.Mono8, ALFormat.Mono16, ALFormat.Stereo8, ALFormat.Stereo16.</param>
    /// <param name="buffer">The audio buffer.</param>
    /// <param name="size">The size of the audio buffer in bytes.</param>
    /// <param name="freq">The frequency of the audio buffer.</param>
    internal static void BufferData<TBuffer>(uint bid, ALFormat format, TBuffer[] buffer, int size, int freq)
        where TBuffer : struct
    {
        GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        try { BufferData(bid, format, handle.AddrOfPinnedObject(), size, freq); }
        finally { handle.Free(); }
    }
    #endregion

    #region DeleteBuffer
    /// <summary>This function deletes one buffer only, freeing the resources used by the buffer. Buffers which are attached to a source can not be deleted. See AL.Source (ALSourcei) and AL.SourceUnqueueBuffers for information on how to detach a buffer from a source.</summary>
    /// <param name="buffer">Pointer to a buffer name identifying the buffer to be deleted.</param>
    internal static void DeleteBuffer(ref uint buffer)
    {
        DeleteBuffers(1, ref buffer);
    }

    /// <summary>This function deletes one buffer only, freeing the resources used by the buffer. Buffers which are attached to a source can not be deleted. See AL.Source (ALSourcei) and AL.SourceUnqueueBuffers for information on how to detach a buffer from a source.</summary>
    /// <param name="buffer">Pointer to a buffer name identifying the buffer to be deleted.</param>
    internal static void DeleteBuffer(int buffer)
    {
        DeleteBuffers(1, ref buffer);
    }
    #endregion

    #region DeleteBuffers
    /// <summary>This function deletes one or more buffers, freeing the resources used by the buffer. Buffers which are attached to a source can not be deleted. See AL.Source (ALSourcei) and AL.SourceUnqueueBuffers for information on how to detach a buffer from a source.</summary>
    /// <param name="n">The number of buffers to be deleted.</param>
    /// <param name="buffers">Pointer to an array of buffer names identifying the buffers to be deleted.</param>
    [DllImport(Lib, EntryPoint = "alDeleteBuffers", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    unsafe internal static extern void DeleteBuffers(int n, [In] uint* buffers);

    /// <summary>This function deletes one or more buffers, freeing the resources used by the buffer. Buffers which are attached to a source can not be deleted. See AL.Source (ALSourcei) and AL.SourceUnqueueBuffers for information on how to detach a buffer from a source.</summary>
    /// <param name="n">The number of buffers to be deleted.</param>
    /// <param name="buffers">Pointer to an array of buffer names identifying the buffers to be deleted.</param>
    [DllImport(Lib, EntryPoint = "alDeleteBuffers", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    unsafe internal static extern void DeleteBuffers(int n, [In] int* buffers);

    /// <summary>This function deletes one or more buffers, freeing the resources used by the buffer. Buffers which are attached to a source can not be deleted. See AL.Source (ALSourcei) and AL.SourceUnqueueBuffers for information on how to detach a buffer from a source.</summary>
    /// <param name="n">The number of buffers to be deleted.</param>
    /// <param name="buffers">Pointer to an array of buffer names identifying the buffers to be deleted.</param>
    internal static void DeleteBuffers(int n, [In] ref uint buffers)
    {
        unsafe
        {
            fixed (uint* pbuffers = &buffers)
            {
                DeleteBuffers(n, pbuffers);
            }
        }
    }

    /// <summary>This function deletes one or more buffers, freeing the resources used by the buffer. Buffers which are attached to a source can not be deleted. See AL.Source (ALSourcei) and AL.SourceUnqueueBuffers for information on how to detach a buffer from a source.</summary>
    /// <param name="n">The number of buffers to be deleted.</param>
    /// <param name="buffers">Pointer to an array of buffer names identifying the buffers to be deleted.</param>
    internal static void DeleteBuffers(int n, [In] ref int buffers)
    {
        unsafe
        {
            fixed (int* pbuffers = &buffers)
            {
                DeleteBuffers(n, pbuffers);
            }
        }
    }

    /// <summary>This function deletes one buffer only, freeing the resources used by the buffer. Buffers which are attached to a source can not be deleted. See AL.Source (ALSourcei) and AL.SourceUnqueueBuffers for information on how to detach a buffer from a source.</summary>
    /// <param name="buffers">Pointer to a buffer name identifying the buffer to be deleted.</param>
    internal static void DeleteBuffers(uint[] buffers)
    {
        if (buffers == null) throw new ArgumentNullException();
        if (buffers.Length == 0) throw new ArgumentOutOfRangeException();
        DeleteBuffers(buffers.Length, ref buffers[0]);
    }

    /// <summary>This function deletes one or more buffers, freeing the resources used by the buffer. Buffers which are attached to a source can not be deleted. See AL.Source (ALSourcei) and AL.SourceUnqueueBuffers for information on how to detach a buffer from a source.</summary>
    /// <param name="buffers">Pointer to an array of buffer names identifying the buffers to be deleted.</param>
    internal static void DeleteBuffers(int[] buffers)
    {
        if (buffers == null) throw new ArgumentNullException();
        if (buffers.Length == 0) throw new ArgumentOutOfRangeException();
        DeleteBuffers(buffers.Length, ref buffers[0]);
    }
    #endregion

    #region DeleteSources
    /// <summary>This function deletes one or more sources.</summary>
    /// <param name="n">The number of sources to be deleted.</param>
    /// <param name="sources">Pointer to an array of source names identifying the sources to be deleted.</param>
    [DllImport(Lib, EntryPoint = "alDeleteSources", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    unsafe internal static extern void DeleteSources(int n, [In] uint* sources); // Delete Source objects 

    /// <summary>This function deletes one or more sources.</summary>
    /// <param name="n">The number of sources to be deleted.</param>
    /// <param name="sources">Reference to a single source, or an array of source names identifying the sources to be deleted.</param>
    [DllImport(Lib, EntryPoint = "alDeleteSources", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void DeleteSources(int n, ref uint sources);

    /// <summary>This function deletes one or more sources.</summary>
    /// <param name="n">The number of sources to be deleted.</param>
    /// <param name="sources">Reference to a single source, or an array of source names identifying the sources to be deleted.</param>
    [DllImport(Lib, EntryPoint = "alDeleteSources", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void DeleteSources(int n, ref int sources);

    /// <summary>This function deletes one or more sources.</summary>
    /// <param name="sources">An array of source names identifying the sources to be deleted.</param>
    internal static void DeleteSources(uint[] sources)
    {
        if (sources == null) throw new ArgumentNullException();
        if (sources.Length == 0) throw new ArgumentOutOfRangeException();
        DeleteBuffers(sources.Length, ref sources[0]);
    }

    /// <summary>This function deletes one or more sources.</summary>
    /// <param name="sources">An array of source names identifying the sources to be deleted.</param>
    internal static void DeleteSources(int[] sources)
    {
        if (sources == null) throw new ArgumentNullException();
        if (sources.Length == 0) throw new ArgumentOutOfRangeException();
        DeleteBuffers(sources.Length, ref sources[0]);
    }

    /// <summary>This function deletes one source only.</summary>
    /// <param name="source">Pointer to a source name identifying the source to be deleted.</param>
    internal static void DeleteSource(ref uint source)
    {
        DeleteSources(1, ref source);
    }

    /// <summary>This function deletes one source only.</summary>
    /// <param name="source">Pointer to a source name identifying the source to be deleted.</param>
    internal static void DeleteSource(int source)
    {
        DeleteSources(1, ref source);
    }
    #endregion

    #region Disable
    /// <summary>This function disables a feature of the OpenAL driver.</summary>
    /// <param name="capability">The name of a capability to disable.</param>
    [DllImport(Lib, EntryPoint = "alDisable", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void Disable(ALCapability capability);
    #endregion

    #region DistanceModel
    /// <summary>This function selects the OpenAL distance model – ALDistanceModel.InverseDistance, ALDistanceModel.InverseDistanceClamped, ALDistanceModel.LinearDistance, ALDistanceModel.LinearDistanceClamped, ALDistanceModel.ExponentDistance, ALDistanceModel.ExponentDistanceClamped, or ALDistanceModel.None. The default distance model in OpenAL is ALDistanceModel.InverseDistanceClamped.</summary>
    /// <remarks>
    /// The ALDistanceModel .InverseDistance model works according to the following formula:
    /// gain = ALSourcef.ReferenceDistance / (ALSourcef.ReferenceDistance + ALSourcef.RolloffFactor * (distance – ALSourcef.ReferenceDistance));
    /// 
    /// The ALDistanceModel .InverseDistanceClamped model works according to the following formula:
    /// distance = max(distance,ALSourcef.ReferenceDistance);
    /// distance = min(distance,ALSourcef.MaxDistance);
    /// gain = ALSourcef.ReferenceDistance / (ALSourcef.ReferenceDistance + ALSourcef.RolloffFactor * (distance – ALSourcef.ReferenceDistance));
    /// 
    /// The ALDistanceModel.LinearDistance model works according to the following formula: 
    /// distance = min(distance, ALSourcef.MaxDistance) // avoid negative gain
    /// gain = (1 – ALSourcef.RolloffFactor * (distance – ALSourcef.ReferenceDistance) / (ALSourcef.MaxDistance – ALSourcef.ReferenceDistance))
    /// 
    /// The ALDistanceModel.LinearDistanceClamped model works according to the following formula:
    /// distance = max(distance, ALSourcef.ReferenceDistance)
    /// distance = min(distance, ALSourcef.MaxDistance)
    /// gain = (1 – ALSourcef.RolloffFactor * (distance – ALSourcef.ReferenceDistance) / (ALSourcef.MaxDistance – ALSourcef.ReferenceDistance))
    /// 
    /// The ALDistanceModel.ExponentDistance model works according to the following formula:
    /// gain = (distance / ALSourcef.ReferenceDistance) ^ (- ALSourcef.RolloffFactor) 
    /// 
    /// The ALDistanceModel.ExponentDistanceClamped model works according to the following formula:
    /// distance = max(distance, ALSourcef.ReferenceDistance)
    /// distance = min(distance, ALSourcef.MaxDistance)
    /// gain = (distance / ALSourcef.ReferenceDistance) ^ (- ALSourcef.RolloffFactor)
    /// 
    /// The ALDistanceModel.None model works according to the following formula:
    /// gain = 1f;
    /// </remarks>
    /// <param name="distancemodel"></param>
    [DllImport(Lib, EntryPoint = "alDistanceModel", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void DistanceModel(ALDistanceModel distancemodel);
    #endregion 

    #region DopplerFactor
    /// <summary>AL.DopplerFactor is a simple scaling of source and listener velocities to exaggerate or deemphasize the Doppler (pitch) shift resulting from the calculation.</summary>
    /// <param name="value">A negative value will result in an error, the command is then ignored. The default value is 1f. The current setting can be queried using AL.Get with parameter ALGetFloat.SpeedOfSound.</param>
    [DllImport(Lib, EntryPoint = "alDopplerFactor", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void DopplerFactor(float value);
    #endregion

    #region DopplerVelocity
    /// <summary>This function is deprecated and should not be used.</summary>
    /// <param name="value">The default is 1.0f.</param>
    [DllImport(Lib, EntryPoint = "alDopplerVelocity", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void DopplerVelocity(float value);
    #endregion

    #region Enable
    /// <summary>This function enables a feature of the OpenAL driver. There are no capabilities defined in OpenAL 1.1 to be used with this function, but it may be used by an extension.</summary>
    /// <param name="capability">The name of a capability to enable.</param>
    [DllImport(Lib, EntryPoint = "alEnable", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void Enable(ALCapability capability);
    #endregion

    #region GenBuffer
    /// <summary>This function generates one buffer only, which contain audio data (see AL.BufferData). References to buffers are uint values, which are used wherever a buffer reference is needed (in calls such as AL.DeleteBuffers, AL.Source with parameter ALSourcei, AL.SourceQueueBuffers, and AL.SourceUnqueueBuffers).</summary>
    /// <returns>Pointer to an uint value which will store the name of the new buffer.</returns>
    internal static uint GenBuffer()
    {
        uint temp;
        GenBuffers(1, out temp);
        return temp;
    }

    /// <summary>This function generates one buffer only, which contain audio data (see AL.BufferData). References to buffers are uint values, which are used wherever a buffer reference is needed (in calls such as AL.DeleteBuffers, AL.Source with parameter ALSourcei, AL.SourceQueueBuffers, and AL.SourceUnqueueBuffers).</summary>
    /// <param name="buffer">Pointer to an uint value which will store the names of the new buffer.</param>
    internal static void GenBuffer(out uint buffer)
    {
        GenBuffers(1, out buffer);
    }
    #endregion

    #region GenBuffers
    /// <summary>This function generates one or more buffers, which contain audio buffer (see AL.BufferData). References to buffers are uint values, which are used wherever a buffer reference is needed (in calls such as AL.DeleteBuffers, AL.Source with parameter ALSourcei, AL.SourceQueueBuffers, and AL.SourceUnqueueBuffers).</summary>
    /// <param name="n">The number of buffers to be generated.</param>
    /// <param name="buffers">Pointer to an array of uint values which will store the names of the new buffers.</param>
    [DllImport(Lib, EntryPoint = "alGenBuffers", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity]
    unsafe internal static extern void GenBuffers(int n, [Out] uint* buffers);

    /// <summary>This function generates one or more buffers, which contain audio buffer (see AL.BufferData). References to buffers are uint values, which are used wherever a buffer reference is needed (in calls such as AL.DeleteBuffers, AL.Source with parameter ALSourcei, AL.SourceQueueBuffers, and AL.SourceUnqueueBuffers).</summary>
    /// <param name="n">The number of buffers to be generated.</param>
    /// <param name="buffers">Pointer to an array of uint values which will store the names of the new buffers.</param>        
    [DllImport(Lib, EntryPoint = "alGenBuffers", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity]
    unsafe internal static extern void GenBuffers(int n, [Out] int* buffers);

    /// <summary>This function generates one or more buffers, which contain audio buffer (see AL.BufferData). References to buffers are uint values, which are used wherever a buffer reference is needed (in calls such as AL.DeleteBuffers, AL.Source with parameter ALSourcei, AL.SourceQueueBuffers, and AL.SourceUnqueueBuffers).</summary>
    /// <param name="n">The number of buffers to be generated.</param>
    /// <param name="buffers">Pointer to an array of uint values which will store the names of the new buffers.</param>
    internal static void GenBuffers(int n, out uint buffers)
    {
        unsafe
        {
            fixed (uint* pbuffers = &buffers)
            {
                GenBuffers(n, pbuffers);
            }
        }
    }

    /// <summary>This function generates one or more buffers, which contain audio buffer (see AL.BufferData). References to buffers are uint values, which are used wherever a buffer reference is needed (in calls such as AL.DeleteBuffers, AL.Source with parameter ALSourcei, AL.SourceQueueBuffers, and AL.SourceUnqueueBuffers).</summary>
    /// <param name="n">The number of buffers to be generated.</param>
    /// <param name="buffers">Pointer to an array of uint values which will store the names of the new buffers.</param>
    internal static void GenBuffers(int n, out int buffers)
    {
        unsafe
        {
            fixed (int* pbuffers = &buffers)
            {
                GenBuffers(n, pbuffers);
            }
        }
    }

    /// <summary>This function generates one or more buffers, which contain audio data (see AL.BufferData). References to buffers are uint values, which are used wherever a buffer reference is needed (in calls such as AL.DeleteBuffers, AL.Source with parameter ALSourcei, AL.SourceQueueBuffers, and AL.SourceUnqueueBuffers).</summary>
    /// <param name="n">The number of buffers to be generated.</param>
    /// <returns>Pointer to an array of uint values which will store the names of the new buffers.</returns>
    internal static int[] GenBuffers(int n)
    {
        int[] buffers = new int[n];
        GenBuffers(buffers.Length, out buffers[0]);
        return buffers;
    }
    #endregion

    #region GenSources
    [DllImport(Lib, EntryPoint = "alGenSources", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    unsafe private static extern void GenSourcesPrivate(int n, [Out] uint* sources);

    /// <summary>This function generates one or more sources. References to sources are uint values, which are used wherever a source reference is needed (in calls such as AL.DeleteSources and AL.Source with parameter ALSourcei).</summary>
    /// <param name="n">The number of sources to be generated.</param>
    /// <param name="sources">Pointer to an array of uint values which will store the names of the new sources.</param>
    internal static void GenSources(int n, out uint sources)
    {
        unsafe
        {
            fixed (uint* sources_ptr = &sources)
            {
                GenSourcesPrivate(n, sources_ptr);
            }
        }
    }

    /// <summary>This function generates one or more sources. References to sources are int values, which are used wherever a source reference is needed (in calls such as AL.DeleteSources and AL.Source with parameter ALSourcei).</summary>
    /// <param name="n">The number of sources to be generated.</param>
    /// <param name="sources">Pointer to an array of int values which will store the names of the new sources.</param>
    internal static void GenSources(int n, out int sources)
    {
        unsafe
        {
            fixed (int* sources_ptr = &sources)
            {
                GenSourcesPrivate(n, (uint*)sources_ptr);
            }
        }
    }

    /// <summary>This function generates one or more sources. References to sources are int values, which are used wherever a source reference is needed (in calls such as AL.DeleteSources and AL.Source with parameter ALSourcei).</summary>
    /// <param name="sources">Pointer to an array of int values which will store the names of the new sources.</param>
    internal static void GenSources(int[] sources)
    {
        uint[] temp = new uint[sources.Length];
        GenSources(temp.Length, out temp[0]);
        for (int i = 0; i < temp.Length; i++)
        {
            sources[i] = (int)temp[i];
        }
    }

    /// <summary>This function generates one or more sources. References to sources are int values, which are used wherever a source reference is needed (in calls such as AL.DeleteSources and AL.Source with parameter ALSourcei).</summary>
    /// <param name="n">The number of sources to be generated.</param>
    /// <returns>Pointer to an array of int values which will store the names of the new sources.</returns>
    internal static uint[] GenSources(int n)
    {
        uint[] temp = new uint[n];
        GenSources(temp.Length, out temp[0]);
        return temp;
    }

    /// <summary>This function generates one source only. References to sources are int values, which are used wherever a source reference is needed (in calls such as AL.DeleteSources and AL.Source with parameter ALSourcei).</summary>
    /// <returns>Pointer to an int value which will store the name of the new source.</returns>
    internal static uint GenSource()
    {
        uint temp;
        GenSources(1, out temp);
        return temp;
    }

    /// <summary>This function generates one source only. References to sources are uint values, which are used wherever a source reference is needed (in calls such as AL.DeleteSources and AL.Source with parameter ALSourcei).</summary>
    /// <param name="source">Pointer to an uint value which will store the name of the new source.</param>
    internal static void GenSource(out uint source)
    {
        GenSources(1, out source);
    }
    #endregion

    #region Get
    /// <summary>This function returns an integer OpenAL state.</summary>
    /// <param name="param">the state to be queried: DistanceModel.</param>
    /// <returns>The integer state described by param will be returned.</returns>
    [DllImport(Lib, EntryPoint = "alGetInteger", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern int Get(ALGetInteger param);

    /// <summary>This function returns a floating-point OpenAL state.</summary>
    /// <param name="param">the state to be queried: DopplerFactor, SpeedOfSound.</param>
    /// <returns>The floating-point state described by param will be returned.</returns>
    [DllImport(Lib, EntryPoint = "alGetFloat", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern float Get(ALGetFloat param);
    #endregion

    #region GetBuffer
    /// <summary>This function retrieves an integer property of a buffer.</summary>
    /// <param name="bid">Buffer name whose attribute is being retrieved</param>
    /// <param name="param">The name of the attribute to be retrieved: ALGetBufferi.Frequency, Bits, Channels, Size, and the currently hidden AL_DATA (dangerous).</param>
    /// <param name="value">A pointer to an int to hold the retrieved buffer</param>
    [DllImport(Lib, EntryPoint = "alGetBufferi", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void GetBuffer(uint bid, ALGetBufferi param, [Out] out int value);

    /// <summary>This function retrieves an integer property of a buffer.</summary>
    /// <param name="bid">Buffer name whose attribute is being retrieved</param>
    /// <param name="param">The name of the attribute to be retrieved: ALGetBufferi.Frequency, Bits, Channels, Size, and the currently hidden AL_DATA (dangerous).</param>
    /// <param name="value">A pointer to an int to hold the retrieved buffer</param>
    internal static void GetBuffer(int bid, ALGetBufferi param, out int value)
    {
        GetBuffer((uint)bid, param, out value);
    }
    #endregion

    #region GetEnumValue
    /// <summary>This function returns the enumeration value of an OpenAL token, described by a string.</summary>
    /// <param name="ename">A string describing an OpenAL token. Example "AL_DISTANCE_MODEL"</param>
    /// <returns>Returns the actual ALenum described by a string. Returns 0 if the string doesn’t describe a valid OpenAL token.</returns>
    [DllImport(Lib, EntryPoint = "alGetEnumValue", ExactSpelling = true, CallingConvention = Style, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
    internal static extern int GetEnumValue([In] string ename);
    #endregion

    #region GetError
    /// <summary>Error support. Obtain the most recent error generated in the AL state machine. When an error is detected by AL, a flag is set and the error code is recorded. Further errors, if they occur, do not affect this recorded code. When alGetError is called, the code is returned and the flag is cleared, so that a further error will again record its code.</summary>
    /// <returns>The first error that occured. can be used with AL.GetString. Returns an Alenum representing the error state. When an OpenAL error occurs, the error state is set and will not be changed until the error state is retrieved using alGetError. Whenever alGetError is called, the error state is cleared and the last state (the current state when the call was made) is returned. To isolate error detection to a specific portion of code, alGetError should be called before the isolated section to clear the current error state.</returns>
    [DllImport(Lib, EntryPoint = "alGetError", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern ALError GetError();
    #endregion

    #region GetListener
    /// <summary>This function retrieves a floating-point property of the listener.</summary>
    /// <param name="param">the name of the attribute to be retrieved: ALListenerf.Gain</param>
    /// <param name="value">a pointer to the floating-point value being retrieved.</param>
    [DllImport(Lib, EntryPoint = "alGetListenerf", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void GetListener(ALListenerf param, [Out] out float value);

    /// <summary>This function retrieves a set of three floating-point values from a property of the listener.</summary>
    /// <param name="param">The name of the attribute to be retrieved: ALListener3f.Position, ALListener3f.Velocity</param>
    /// <param name="value1">The first floating-point value being retrieved.</param>
    /// <param name="value2">The second floating-point value  being retrieved.</param>
    /// <param name="value3">The third floating-point value  being retrieved.</param>
    [DllImport(Lib, EntryPoint = "alGetListener3f", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void GetListener(ALListener3f param, [Out] out float value1, [Out] out float value2, [Out] out float value3);

    /// <summary>This function retrieves a floating-point vector property of the listener. You must pin it manually.</summary>
    /// <param name="param">the name of the attribute to be retrieved: ALListener3f.Position, ALListener3f.Velocity, ALListenerfv.Orientation</param>
    /// <param name="values">A pointer to the floating-point vector value being retrieved.</param>
    [DllImport(Lib, EntryPoint = "alGetListenerfv", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    unsafe internal static extern void GetListener(ALListenerfv param, float* values);
    #endregion

    #region GetProcAddress
    /// <summary>This function returns the address of an OpenAL extension function. Handle with care.</summary>
    /// <param name="fname">A string containing the function name.</param>
    /// <returns>The return value is a pointer to the specified function. The return value will be IntPtr.Zero if the function is not found.</returns>
    [DllImport(Lib, EntryPoint = "alGetProcAddress", ExactSpelling = true, CallingConvention = Style, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
    internal static extern IntPtr GetProcAddress([In] string fname);
    #endregion

    #region GetSource
    /// <summary>This function retrieves a floating-point property of a source.</summary>
    /// <param name="sid">Source name whose attribute is being retrieved.</param>
    /// <param name="param">The name of the attribute to set: ALSourcef.Pitch, Gain, MinGain, MaxGain, MaxDistance, RolloffFactor, ConeOuterGain, ConeInnerAngle, ConeOuterAngle, ReferenceDistance, EfxAirAbsorptionFactor, EfxRoomRolloffFactor, EfxConeOuterGainHighFrequency.</param>
    /// <param name="value">A pointer to the floating-point value being retrieved</param>
    [DllImport(Lib, EntryPoint = "alGetSourcef", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void GetSource(uint sid, ALSourcef param, [Out] out float value);


    /// <summary>This function retrieves three floating-point values representing a property of a source.</summary>
    /// <param name="sid">Source name whose attribute is being retrieved.</param>
    /// <param name="param">the name of the attribute being retrieved: ALSource3f.Position, Velocity, Direction.</param>
    /// <param name="value1">Pointer to the value to retrieve.</param>
    /// <param name="value2">Pointer to the value to retrieve.</param>
    /// <param name="value3">Pointer to the value to retrieve.</param>
    [DllImport(Lib, EntryPoint = "alGetSource3f", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void GetSource(uint sid, ALSource3f param, [Out] out float value1, [Out] out float value2, [Out] out float value3);


    /// <summary>This function retrieves an integer property of a source.</summary>
    /// <param name="sid">Source name whose attribute is being retrieved.</param>
    /// <param name="param">The name of the attribute to retrieve: ALSourcei.SourceRelative, Buffer, SourceState, BuffersQueued, BuffersProcessed.</param>
    /// <param name="value">A pointer to the integer value being retrieved.</param>
    [DllImport(Lib, EntryPoint = "alGetSourcei", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void GetSource(uint sid, ALGetSourcei param, [Out] out int value);


    /// <summary>This function retrieves a bool property of a source.</summary>
    /// <param name="sid">Source name whose attribute is being retrieved.</param>
    /// <param name="param">The name of the attribute to get: ALSourceb.SourceRelative, Looping.</param>
    /// <param name="value">A pointer to the bool value being retrieved.</param>
    internal static void GetSource(uint sid, ALSourceb param, out bool value)
    {
        int result;
        GetSource(sid, (ALGetSourcei)param, out result);
        value = result != 0;
    }
    #endregion

    #region GetSourceState     
    /// <summary>(Helper) Returns Source state information.</summary>
    /// <param name="sid">The source to be queried.</param>
    /// <returns>state information from OpenAL.</returns>
    public static ALSourceState GetSourceState(uint sid)
    {
        GetSource(sid, ALGetSourcei.SourceState, out int temp);
        return (ALSourceState)temp;
    }
    #endregion

    #region GetString
    [DllImport(Lib, EntryPoint = "alGetString", ExactSpelling = true, CallingConvention = Style, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
    private static extern IntPtr GetStringPrivate(ALGetString param); // accepts the enums AlError, AlContextString

    /// <summary>This function retrieves an OpenAL string property.</summary>
    /// <param name="param">The property to be returned: Vendor, Version, Renderer and Extensions</param>
    /// <returns>Returns a pointer to a null-terminated string.</returns>
    internal static string GetString(ALGetString param)
    {
        return Marshal.PtrToStringAnsi(GetStringPrivate(param));
    }

    /// <summary>This function retrieves an OpenAL string property.</summary>
    /// <param name="param">The human-readable errorstring to be returned.</param>
    /// <returns>Returns a pointer to a null-terminated string.</returns>
    internal static string GetString(ALError param)
    {
        return Marshal.PtrToStringAnsi(GetStringPrivate((ALGetString)param));
    }
    #endregion

    #region IsBuffer
    /// <summary>This function tests if a buffer name is valid, returning True if valid, False if not.</summary>
    /// <param name="bid">A buffer Handle previously allocated with <see cref="GenBuffers(int)"/>.</param>
    /// <returns>Success.</returns>
    [DllImport(Lib, EntryPoint = "alIsBuffer", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern bool IsBuffer(uint bid);

    /// <summary>This function tests if a buffer name is valid, returning True if valid, False if not.</summary>
    /// <param name="bid">A buffer Handle previously allocated with <see cref="GenBuffers(int)"/>.</param>
    /// <returns>Success.</returns>
    internal static bool IsBuffer(int bid)
    {
        uint temp = (uint)bid;
        return IsBuffer(temp);
    }
    #endregion

    #region IsEnabled
    /// <summary>This function returns a boolean indicating if a specific feature is enabled in the OpenAL driver.</summary>
    /// <param name="capability">The name of a capability to enable.</param>
    /// <returns>True if enabled, False if disabled.</returns>
    [DllImport(Lib, EntryPoint = "alIsEnabled", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern bool IsEnabled(ALCapability capability);
    #endregion

    #region IsExtensionPresent
    ///<summary>This function tests if a specific Extension is available for the OpenAL driver.</summary>
    /// <param name="extname">A string naming the desired extension. Example: "EAX-RAM"</param>
    /// <returns>Returns True if the Extension is available or False if not available.</returns>
    [DllImport(Lib, EntryPoint = "alIsExtensionPresent", ExactSpelling = true, CallingConvention = Style, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
    internal static extern bool IsExtensionPresent([In] string extname);
    #endregion

    #region IsSource
    /// <summary>This function tests if a source name is valid, returning True if valid and False if not.</summary>
    /// <param name="sid">A source name to be tested for validity</param>
    /// <returns>Success.</returns>
    [DllImport(Lib, EntryPoint = "alIsSource", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern bool IsSource(uint sid);

    /// <summary>This function tests if a source name is valid, returning True if valid and False if not.</summary>
    /// <param name="sid">A source name to be tested for validity</param>
    /// <returns>Success.</returns>
    internal static bool IsSource(int sid)
    {
        return IsSource((uint)sid);
    }
    #endregion

    #region Listener
    /// <summary>This function sets a floating-point property for the listener.</summary>
    /// <param name="param">The name of the attribute to be set: ALListenerf.Gain</param>
    /// <param name="value">The float value to set the attribute to.</param>
    [DllImport(Lib, EntryPoint = "alListenerf", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void Listener(ALListenerf param, float value);

    /// <summary>This function sets a floating-point property for the listener.</summary>
    /// <param name="param">The name of the attribute to set: ALListener3f.Position, ALListener3f.Velocity</param>
    /// <param name="value1">The value to set the attribute to.</param>
    /// <param name="value2">The value to set the attribute to.</param>
    /// <param name="value3">The value to set the attribute to.</param>
    [DllImport(Lib, EntryPoint = "alListener3f", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void Listener(ALListener3f param, float value1, float value2, float value3);
    
    [DllImport(Lib, EntryPoint = "alListenerfv", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    unsafe private static extern void ListenerPrivate(ALListenerfv param, float* values);

    /// <summary>This function sets a floating-point vector property of the listener.</summary>
    /// <param name="param">The name of the attribute to be set: ALListener3f.Position, ALListener3f.Velocity, ALListenerfv.Orientation</param>
    /// <param name="values">Pointer to floating-point vector values.</param>
    internal static void Listener(ALListenerfv param, ref float[] values)
    {
        unsafe
        {
            fixed (float* ptr = &values[0])
            {
                ListenerPrivate(param, ptr);
            }
        }
    }        
    #endregion       

    #region Source
    /// <summary>This function sets a floating-point property of a source.</summary>
    /// <param name="sid">Source name whose attribute is being set</param>
    /// <param name="param">The name of the attribute to set: ALSourcef.Pitch, Gain, MinGain, MaxGain, MaxDistance, RolloffFactor, ConeOuterGain, ConeInnerAngle, ConeOuterAngle, ReferenceDistance, EfxAirAbsorptionFactor, EfxRoomRolloffFactor, EfxConeOuterGainHighFrequency.</param>
    /// <param name="value">The value to set the attribute to.</param>
    [DllImport(Lib, EntryPoint = "alSourcef", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void Source(uint sid, ALSourcef param, float value);


    /// <summary>This function sets a source property requiring three floating-point values.</summary>
    /// <param name="sid">Source name whose attribute is being set.</param>
    /// <param name="param">The name of the attribute to set: ALSource3f.Position, Velocity, Direction.</param>
    /// <param name="value1">The three ALfloat values which the attribute will be set to.</param>
    /// <param name="value2">The three ALfloat values which the attribute will be set to.</param>
    /// <param name="value3">The three ALfloat values which the attribute will be set to.</param>
    [DllImport(Lib, EntryPoint = "alSource3f", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void Source(uint sid, ALSource3f param, float value1, float value2, float value3);
           
    /// <summary>This function sets an integer property of a source.</summary>
    /// <param name="sid">Source name whose attribute is being set.</param>
    /// <param name="param">The name of the attribute to set: ALSourcei.SourceRelative, ConeInnerAngle, ConeOuterAngle, Looping, Buffer, SourceState.</param>
    /// <param name="value">The value to set the attribute to.</param>
    [DllImport(Lib, EntryPoint = "alSourcei", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void Source(uint sid, ALSourcei param, int value);

    /// <summary>This function sets an bool property of a source.</summary>
    /// <param name="sid">Source name whose attribute is being set.</param>
    /// <param name="param">The name of the attribute to set: ALSourceb.SourceRelative, Looping.</param>
    /// <param name="value">The value to set the attribute to.</param>
    internal static void Source(uint sid, ALSourceb param, bool value)
    {
        Source(sid, (ALSourcei)param, (value) ? 1 : 0);
    }

    /// <summary>This function sets 3 integer properties of a source. This property is used to establish connections between Sources and Auxiliary Effect Slots.</summary>
    /// <param name="sid">Source name whose attribute is being set.</param>
    /// <param name="param">The name of the attribute to set: EfxAuxiliarySendFilter</param>
    /// <param name="value1">The value to set the attribute to. (EFX Extension) The destination Auxiliary Effect Slot ID</param>
    /// <param name="value2">The value to set the attribute to. (EFX Extension) The Auxiliary Send number.</param>
    ///<param name="value3">The value to set the attribute to. (EFX Extension) optional Filter ID.</param>
    [DllImport(Lib, EntryPoint = "alSource3i", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void Source(uint sid, ALSource3i param, int value1, int value2, int value3);
    #endregion

    #region SourcePause
    /// <summary>This function pauses a source. The paused source will have its state changed to ALSourceState.Paused.</summary>
    /// <param name="sid">The name of the source to be paused.</param>
    [DllImport(Lib, EntryPoint = "alSourcePause", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void SourcePause(uint sid);

    /// <summary>This function pauses a set of sources. The paused sources will have their state changed to ALSourceState.Paused.</summary>
    /// <param name="ns">The number of sources to be paused.</param>
    /// <param name="sids">A pointer to an array of sources to be paused.</param>
    [DllImport(Lib, EntryPoint = "alSourcePausev"), SuppressUnmanagedCodeSecurity]
    unsafe internal static extern void SourcePause(int ns, [In] uint* sids);

    /// <summary>This function pauses a set of sources. The paused sources will have their state changed to ALSourceState.Paused.</summary>
    /// <param name="ns">The number of sources to be paused.</param>
    /// <param name="sids">A pointer to an array of sources to be paused.</param>
    internal static void SourcePause(int ns, uint[] sids)
    {
        unsafe
        {
            fixed (uint* ptr = sids)
            {
                SourcePause(ns, ptr);
            }
        }
    }
    /// <summary>This function pauses a set of sources. The paused sources will have their state changed to ALSourceState.Paused.</summary>
    /// <param name="ns">The number of sources to be paused.</param>
    /// <param name="sids">A pointer to an array of sources to be paused.</param>
    internal static void SourcePause(int ns, int[] sids)
    {
        uint[] temp = new uint[ns];
        for (int i = 0; i < ns; i++)
        {
            temp[i] = (uint)sids[i];
        }
        SourcePause(ns, temp);
    }

    /// <summary>This function pauses a set of sources. The paused sources will have their state changed to ALSourceState.Paused.</summary>
    /// <param name="ns">The number of sources to be paused.</param>
    /// <param name="sids">A pointer to an array of sources to be paused.</param>
    internal static void SourcePause(int ns, ref uint sids)
    {
        unsafe
        {
            fixed (uint* ptr = &sids)
            {
                SourcePause(ns, ptr);
            }
        }
    }
    #endregion

    #region SourcePlay
    /// <summary>This function plays a set of sources. The playing sources will have their state changed to ALSourceState.Playing. When called on a source which is already playing, the source will restart at the beginning. When the attached buffer(s) are done playing, the source will progress to the ALSourceState.Stopped state.</summary>
    /// <param name="ns">The number of sources to be played.</param>
    /// <param name="sids">A pointer to an array of sources to be played.</param>
    [DllImport(Lib, EntryPoint = "alSourcePlayv"), SuppressUnmanagedCodeSecurity]
    unsafe internal static extern void SourcePlay(int ns, [In] uint* sids);

    /// <summary>This function plays a set of sources. The playing sources will have their state changed to ALSourceState.Playing. When called on a source which is already playing, the source will restart at the beginning. When the attached buffer(s) are done playing, the source will progress to the ALSourceState.Stopped state.</summary>
    /// <param name="ns">The number of sources to be played.</param>
    /// <param name="sids">A pointer to an array of sources to be played.</param>
    internal static void SourcePlay(int ns, uint[] sids)
    {
        unsafe
        {
            fixed (uint* ptr = sids)
            {
                SourcePlay(ns, ptr);
            }
        }
    }

    /// <summary>This function plays a set of sources. The playing sources will have their state changed to ALSourceState.Playing. When called on a source which is already playing, the source will restart at the beginning. When the attached buffer(s) are done playing, the source will progress to the ALSourceState.Stopped state.</summary>
    /// <param name="ns">The number of sources to be played.</param>
    /// <param name="sids">A pointer to an array of sources to be played.</param>
    internal static void SourcePlay(int ns, int[] sids)
    {
        uint[] temp = new uint[ns];
        for (int i = 0; i < ns; i++)
        {
            temp[i] = (uint)sids[i];
        }
        SourcePlay(ns, temp);
    }

    /// <summary>This function plays a set of sources. The playing sources will have their state changed to ALSourceState.Playing. When called on a source which is already playing, the source will restart at the beginning. When the attached buffer(s) are done playing, the source will progress to the ALSourceState.Stopped state.</summary>
    /// <param name="ns">The number of sources to be played.</param>
    /// <param name="sids">A pointer to an array of sources to be played.</param>
    internal static void SourcePlay(int ns, ref uint sids)
    {
        unsafe
        {
            fixed (uint* ptr = &sids)
            {
                SourcePlay(ns, ptr);
            }
        }
    }

    /// <summary>This function plays, replays or resumes a source. The playing source will have it's state changed to ALSourceState.Playing. When called on a source which is already playing, the source will restart at the beginning. When the attached buffer(s) are done playing, the source will progress to the ALSourceState.Stopped state.</summary>
    /// <param name="sid">The name of the source to be played.</param>
    [DllImport(Lib, EntryPoint = "alSourcePlay", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void SourcePlay(uint sid);
    #endregion

    #region SourceQueueBuffer
    /// <summary>This function queues a set of buffers on a source. All buffers attached to a source will be played in sequence, and the number of processed buffers can be detected using AL.GetSource with parameter ALGetSourcei.BuffersProcessed. When first created, a source will be of type ALSourceType.Undetermined. A successful AL.SourceQueueBuffers call will change the source type to ALSourceType.Streaming.</summary>
    /// <param name="source">The name of the source to queue buffers onto.</param>
    /// <param name="buffer">The name of the buffer to be queued.</param>
    internal static void SourceQueueBuffer(uint source, uint buffer)
    {
        unsafe { SourceQueueBuffers(source, 1, &buffer); }
    }
    #endregion

    #region SourceQueueBuffers
    /// <summary>This function queues a set of buffers on a source. All buffers attached to a source will be played in sequence, and the number of processed buffers can be detected using AL.GetSource with parameter ALGetSourcei.BuffersProcessed. When first created, a source will be of type ALSourceType.Undetermined. A successful AL.SourceQueueBuffers call will change the source type to ALSourceType.Streaming.</summary>
    /// <param name="sid">The name of the source to queue buffers onto.</param>
    /// <param name="numEntries">The number of buffers to be queued.</param>
    /// <param name="bids">A pointer to an array of buffer names to be queued.</param>
    [DllImport(Lib, EntryPoint = "alSourceQueueBuffers", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, ]
    unsafe internal static extern void SourceQueueBuffers(uint sid, int numEntries, [In] uint* bids);

    /// <summary>This function queues a set of buffers on a source. All buffers attached to a source will be played in sequence, and the number of processed buffers can be detected using AL.GetSource with parameter ALGetSourcei.BuffersProcessed. When first created, a source will be of type ALSourceType.Undetermined. A successful AL.SourceQueueBuffers call will change the source type to ALSourceType.Streaming.</summary>
    /// <param name="sid">The name of the source to queue buffers onto.</param>
    /// <param name="numEntries">The number of buffers to be queued.</param>
    /// <param name="bids">A pointer to an array of buffer names to be queued.</param>
    internal static void SourceQueueBuffers(uint sid, int numEntries, uint[] bids)
    {
        unsafe
        {
            fixed (uint* ptr = bids)
            {
                SourceQueueBuffers(sid, numEntries, ptr);
            }
        }
    }

    /// <summary>This function queues a set of buffers on a source. All buffers attached to a source will be played in sequence, and the number of processed buffers can be detected using AL.GetSource with parameter ALGetSourcei.BuffersProcessed. When first created, a source will be of type ALSourceType.Undetermined. A successful AL.SourceQueueBuffers call will change the source type to ALSourceType.Streaming.</summary>
    /// <param name="sid">The name of the source to queue buffers onto.</param>
    /// <param name="numEntries">The number of buffers to be queued.</param>
    /// <param name="bids">A pointer to an array of buffer names to be queued.</param>
    internal static void SourceQueueBuffers(uint sid, int numEntries, ref uint bids)
    {
        unsafe
        {
            fixed (uint* ptr = &bids)
            {
                SourceQueueBuffers(sid, numEntries, ptr);
            }
        }
    }

    /// <summary>This function unqueues a set of buffers attached to a source. The number of processed buffers can be detected using AL.GetSource with parameter ALGetSourcei.BuffersProcessed, which is the maximum number of buffers that can be unqueued using this call. The unqueue operation will only take place if all n buffers can be removed from the queue.</summary>
    /// <param name="sid">The name of the source to unqueue buffers from.</param>
    /// <param name="numEntries">The number of buffers to be unqueued.</param>
    /// <param name="bids">A pointer to an array of buffer names that were removed.</param>
    [DllImport(Lib, EntryPoint = "alSourceUnqueueBuffers", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    unsafe internal static extern void SourceUnqueueBuffers(uint sid, int numEntries, [In] uint* bids);

    /// <summary>This function unqueues a set of buffers attached to a source. The number of processed buffers can be detected using AL.GetSource with parameter ALGetSourcei.BuffersProcessed, which is the maximum number of buffers that can be unqueued using this call. The unqueue operation will only take place if all n buffers can be removed from the queue.</summary>
    /// <param name="sid">The name of the source to unqueue buffers from.</param>
    /// <param name="numEntries">The number of buffers to be unqueued.</param>
    /// <param name="bids">A pointer to an array of buffer names that were removed.</param>
    [DllImport(Lib, EntryPoint = "alSourceUnqueueBuffers", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    internal static extern void SourceUnqueueBuffers(uint sid, int numEntries, [Out] uint[] bids);
    #endregion

    #region SourceRewind
    /// <summary>This function stops the source and sets its state to ALSourceState.Initial.</summary>
    /// <param name="sid">The name of the source to be rewound.</param>
    [DllImport(Lib, EntryPoint = "alSourceRewind", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void SourceRewind(uint sid);

    /// <summary>This function stops a set of sources and sets all their states to ALSourceState.Initial.</summary>
    /// <param name="ns">The number of sources to be rewound.</param>
    /// <param name="sids">A pointer to an array of sources to be rewound.</param>
    [DllImport(Lib, EntryPoint = "alSourceRewindv"), SuppressUnmanagedCodeSecurity]
    unsafe internal static extern void SourceRewind(int ns, [In] uint* sids);

    /// <summary>This function stops a set of sources and sets all their states to ALSourceState.Initial.</summary>
    /// <param name="ns">The number of sources to be rewound.</param>
    /// <param name="sids">A pointer to an array of sources to be rewound.</param>
    internal static void SourceRewind(int ns, uint[] sids)
    {
        unsafe
        {
            fixed (uint* ptr = sids)
            {
                SourceRewind(ns, ptr);
            }
        }
    }

    /// <summary>This function stops a set of sources and sets all their states to ALSourceState.Initial.</summary>
    /// <param name="ns">The number of sources to be rewound.</param>
    /// <param name="sids">A pointer to an array of sources to be rewound.</param>
    internal static void SourceRewind(int ns, int[] sids)
    {
        uint[] temp = new uint[ns];
        for (int i = 0; i < ns; i++)
        {
            temp[i] = (uint)sids[i];
        }
        SourceRewind(ns, temp);
    }

    /// <summary>This function stops a set of sources and sets all their states to ALSourceState.Initial.</summary>
    /// <param name="ns">The number of sources to be rewound.</param>
    /// <param name="sids">A pointer to an array of sources to be rewound.</param>
    internal static void SourceRewind(int ns, ref uint sids)
    {
        unsafe
        {
            fixed (uint* ptr = &sids)
            {
                SourceRewind(ns, ptr);
            }
        }
    }
    #endregion

    #region SourceStop
    /// <summary>This function stops a set of sources. The stopped sources will have their state changed to ALSourceState.Stopped.</summary>
    /// <param name="ns">The number of sources to stop.</param>
    /// <param name="sids">A pointer to an array of sources to be stopped.</param>
    [DllImport(Lib, EntryPoint = "alSourceStopv"), SuppressUnmanagedCodeSecurity]
    unsafe internal static extern void SourceStop(int ns, [In] uint* sids);

    /// <summary>This function stops a source. The stopped source will have it's state changed to ALSourceState.Stopped.</summary>
    /// <param name="sid">The name of the source to be stopped.</param>
    [DllImport(Lib, EntryPoint = "alSourceStop", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void SourceStop(uint sid);

    /// <summary>This function stops a set of sources. The stopped sources will have their state changed to ALSourceState.Stopped.</summary>
    /// <param name="ns">The number of sources to stop.</param>
    /// <param name="sids">A pointer to an array of sources to be stopped.</param>
    internal static void SourceStop(int ns, uint[] sids)
    {
        unsafe
        {
            fixed (uint* ptr = sids)
            {
                SourceStop(ns, ptr);
            }
        }
    }

    /// <summary>This function stops a set of sources. The stopped sources will have their state changed to ALSourceState.Stopped.</summary>
    /// <param name="ns">The number of sources to stop.</param>
    /// <param name="sids">A pointer to an array of sources to be stopped.</param>
    internal static void SourceStop(int ns, int[] sids)
    {
        uint[] temp = new uint[ns];
        for (int i = 0; i < ns; i++)
        {
            temp[i] = (uint)sids[i];
        }
        SourceStop(ns, temp);
    }

    /// <summary>This function stops a set of sources. The stopped sources will have their state changed to ALSourceState.Stopped.</summary>
    /// <param name="ns">The number of sources to stop.</param>
    /// <param name="sids">A pointer to an array of sources to be stopped.</param>
    internal static void SourceStop(int ns, ref uint sids)
    {
        unsafe
        {
            fixed (uint* ptr = &sids)
            {
                SourceStop(ns, ptr);
            }
        }
    }
    #endregion

    #region SourceUnqueueBuffer
    /// <summary>This function unqueues a set of buffers attached to a source. The number of processed buffers can be detected using AL.GetSource with parameter ALGetSourcei.BuffersProcessed, which is the maximum number of buffers that can be unqueued using this call. The unqueue operation will only take place if all n buffers can be removed from the queue.</summary>
    /// <param name="sid">The name of the source to unqueue buffers from.</param>
    internal static int SourceUnqueueBuffer(int sid)
    {
        uint buf;
        unsafe { SourceUnqueueBuffers((uint)sid, 1, &buf); }
        return (int)buf;
    }
    #endregion

    #region SourceUnqueueBuffers
    /// <summary>This function unqueues a set of buffers attached to a source. The number of processed buffers can be detected using AL.GetSource with parameter ALGetSourcei.BuffersProcessed, which is the maximum number of buffers that can be unqueued using this call. The unqueue operation will only take place if all n buffers can be removed from the queue.</summary>
    /// <param name="sid">The name of the source to unqueue buffers from.</param>
    /// <param name="numEntries">The number of buffers to be unqueued.</param>
    /// <param name="bids">A pointer to an array of buffer names that were removed.</param>
    [DllImport(Lib, EntryPoint = "alSourceUnqueueBuffers", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    internal static extern void SourceUnqueueBuffers(int sid, int numEntries, [Out] int[] bids);

    /// <summary>This function unqueues a set of buffers attached to a source. The number of processed buffers can be detected using AL.GetSource with parameter ALGetSourcei.BuffersProcessed, which is the maximum number of buffers that can be unqueued using this call. The unqueue operation will only take place if all n buffers can be removed from the queue.</summary>
    /// <param name="sid">The name of the source to unqueue buffers from.</param>
    /// <param name="numEntries">The number of buffers to be unqueued.</param>
    /// <param name="bids">A pointer to an array of buffer names that were removed.</param>
    [DllImport(Lib, EntryPoint = "alSourceUnqueueBuffers", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    internal static extern void SourceUnqueueBuffers(uint sid, int numEntries, ref uint bids);

    /// <summary>This function unqueues a set of buffers attached to a source. The number of processed buffers can be detected using AL.GetSource with parameter ALGetSourcei.BuffersProcessed, which is the maximum number of buffers that can be unqueued using this call. The unqueue operation will only take place if all n buffers can be removed from the queue.</summary>
    /// <param name="sid">The name of the source to unqueue buffers from.</param>
    /// <param name="numEntries">The number of buffers to be unqueued.</param>
    /// <param name="bids">A pointer to an array of buffer names that were removed.</param>
    [DllImport(Lib, EntryPoint = "alSourceUnqueueBuffers", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    internal static extern void SourceUnqueueBuffers(int sid, int numEntries, ref int bids);

    /// <summary>This function unqueues a set of buffers attached to a source. The number of processed buffers can be detected using AL.GetSource with parameter ALGetSourcei.BuffersProcessed, which is the maximum number of buffers that can be unqueued using this call. The unqueue operation will only take place if all n buffers can be removed from the queue.</summary>
    /// <param name="sid">The name of the source to unqueue buffers from.</param>
    /// <param name="numEntries">The number of buffers to be unqueued.</param>
    internal static int[] SourceUnqueueBuffers(int sid, int numEntries)
    {
        if (numEntries <= 0) throw new ArgumentOutOfRangeException("numEntries", "Must be greater than zero.");
        int[] buf = new int[numEntries];
        SourceUnqueueBuffers(sid, numEntries, buf);
        return buf;
    }
    #endregion        

    #region SpeedOfSound
    /// <summary>AL.SpeedOfSound allows the application to change the reference (propagation) speed used in the Doppler calculation. The source and listener velocities should be expressed in the same units as the speed of sound.</summary>
    /// <param name="value">A negative or zero value will result in an error, and the command is ignored. Default: 343.3f (appropriate for velocity units of meters and air as the propagation medium). The current setting can be queried using AL.Get with parameter ALGetFloat.SpeedOfSound.</param>
    [DllImport(Lib, EntryPoint = "alSpeedOfSound", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void SpeedOfSound(float value);
    #endregion                        
}
