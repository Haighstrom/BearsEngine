using System.Runtime.InteropServices;
using System.Security;

namespace BearsEngine.Audio.OpenAL;

/// <summary>Alc = Audio Library Context PInvokes</summary>
internal static class Alc
{

    // * * * CLEANED UP ABOVE THIS LINE * * *

    private const string Lib = AL.Lib;
    private const CallingConvention Style = CallingConvention.Cdecl;

    #region CaptureCloseDevice
    /// <summary>This function closes the specified capture device.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <returns>Returns True if the close operation was successful, False on failure.</returns>
    [DllImport(Lib, EntryPoint = "alcCaptureCloseDevice", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern bool CaptureCloseDevice([In] IntPtr device);
    #endregion

    #region CaptureOpenDevice
    /// <summary>This function opens a capture device by name. </summary>
    /// <param name="devicename">a pointer to a device name string.</param>
    /// <param name="frequency">the frequency that the buffer should be captured at.</param>
    /// <param name="format">the requested capture buffer format.</param>
    /// <param name="buffersize">the size of the capture buffer in samples, not bytes.</param>
    /// <returns>Returns the capture device pointer, or NULL on failure.</returns>
    [DllImport(Lib, EntryPoint = "alcCaptureOpenDevice", ExactSpelling = true, CallingConvention = Style, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
    internal static extern IntPtr CaptureOpenDevice(string devicename, uint frequency, ALFormat format, int buffersize);
    #endregion

    #region CaptureSamples
    /// <summary>This function completes a capture operation, and does not block.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <param name="buffer">a pointer to a buffer, which must be large enough to accommodate the number of samples.</param>
    /// <param name="samples">the number of samples to be retrieved.</param>
    [DllImport(Lib, EntryPoint = "alcCaptureSamples", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void CaptureSamples(IntPtr device, IntPtr buffer, int samples);

    /// <summary>This function completes a capture operation, and does not block.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <param name="buffer">a reference to a buffer, which must be large enough to accommodate the number of samples.</param>
    /// <param name="samples">the number of samples to be retrieved.</param>
    internal static void CaptureSamples<T>(IntPtr device, ref T buffer, int samples)
        where T : struct
    {
        GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        try
        {
            CaptureSamples(device, handle.AddrOfPinnedObject(), samples);
        }
        finally
        {
            handle.Free();
        }
    }

    /// <summary>This function completes a capture operation, and does not block.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <param name="buffer">a buffer, which must be large enough to accommodate the number of samples.</param>
    /// <param name="samples">the number of samples to be retrieved.</param>
    internal static void CaptureSamples<T>(IntPtr device, T[] buffer, int samples)
        where T : struct
    {
        CaptureSamples(device, ref buffer[0], samples);
    }

    /// <summary>This function completes a capture operation, and does not block.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <param name="buffer">a buffer, which must be large enough to accommodate the number of samples.</param>
    /// <param name="samples">the number of samples to be retrieved.</param>
    internal static void CaptureSamples<T>(IntPtr device, T[,] buffer, int samples)
        where T : struct
    {
        CaptureSamples(device, ref buffer[0, 0], samples);
    }

    /// <summary>This function completes a capture operation, and does not block.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <param name="buffer">a buffer, which must be large enough to accommodate the number of samples.</param>
    /// <param name="samples">the number of samples to be retrieved.</param>
    internal static void CaptureSamples<T>(IntPtr device, T[,,] buffer, int samples)
        where T : struct
    {
        CaptureSamples(device, ref buffer[0, 0, 0], samples);
    }
    #endregion

    #region CaptureStart
    /// <summary>This function begins a capture operation.</summary>
    /// <remarks>alcCaptureStart will begin recording to an internal ring buffer of the size specified when opening the capture device. The application can then retrieve the number of samples currently available using the ALC_CAPTURE_SAPMPLES token with alcGetIntegerv. When the application determines that enough samples are available for processing, then it can obtain them with a call to alcCaptureSamples.</remarks>
    /// <param name="device">a pointer to a capture device.</param>
    [DllImport(Lib, EntryPoint = "alcCaptureStart", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void CaptureStart([In] IntPtr device);
    #endregion

    #region CaptureStop
    /// <summary>This function stops a capture operation.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    [DllImport(Lib, EntryPoint = "alcCaptureStop", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void CaptureStop([In] IntPtr device);
    #endregion

    #region CloseDevice
    /// <summary>This function closes a device by name.</summary>
    /// <param name="device">a pointer to an opened device</param>
    /// <returns>True will be returned on success or False on failure. Closing a device will fail if the device contains any contexts or buffers.</returns>
    [DllImport(Lib, EntryPoint = "alcCloseDevice", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern bool CloseDevice([In] IntPtr device);
    #endregion

    #region CreateContext
    [DllImport(Lib, EntryPoint = "alcCreateContext", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity]
    private unsafe static extern IntPtr CreateContext([In] IntPtr device, [In] int* attrlist);

    /// <summary>This function creates a context using a specified device.</summary>
    /// <param name="device">a pointer to a device</param>
    /// <param name="attriblist">an array of a set of attributes: ALC_FREQUENCY, ALC_MONO_SOURCES, ALC_REFRESH, ALC_STEREO_SOURCES, ALC_SYNC</param>
    /// <returns>Returns a pointer to the new context (NULL on failure).</returns>
    /// <remarks>The attribute list can be NULL, or a zero terminated list of integer pairs composed of valid ALC attribute tokens and requested values.</remarks>
    internal static IntPtr CreateContext(IntPtr device, int[] attriblist)
    {
        unsafe
        {
            fixed (int* attriblist_ptr = attriblist)
            {
                return CreateContext(device, attriblist_ptr);
            }
        }
    }
    #endregion

    #region DestroyContext
    /// <summary>
    /// This function destroys a context.
    /// </summary>
    /// <param name="context">A pointer to the new context.</param>
    [DllImport(Lib, EntryPoint = "alcDestroyContext", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void DestroyContext(IntPtr context);
    #endregion

    #region GetContextsDevice
    /// <summary>
    /// This function retrieves a context's device pointer.
    /// </summary>
    /// <param name="context">>A pointer to a context.</param>
    /// <returns>Returns a pointer to the specified context's device.</returns>
    [DllImport(Lib, EntryPoint = "alcGetContextsDevice", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    static extern IntPtr GetContextsDevice(IntPtr context);
    #endregion

    #region GetCurrentContext
    /// <summary>
    /// This function retrieves the current context.
    /// </summary>
    /// <returns>Returns a pointer to the current context.</returns>
    [DllImport(Lib, EntryPoint = "alcGetCurrentContext", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern IntPtr GetCurrentContext();
    #endregion

    #region GetEnumValue
    /// <summary>This function retrieves the enum value for a specified enumeration name.</summary>
    /// <param name="device">a pointer to the device to be queried.</param>
    /// <param name="enumname">a null terminated string describing the enum value.</param>
    /// <returns>Returns the enum value described by the enumName string. This is most often used for querying an enum value for an ALC extension.</returns>
    [DllImport(Lib, EntryPoint = "alcGetEnumValue", ExactSpelling = true, CallingConvention = Style, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
    internal static extern int GetEnumValue([In] IntPtr device, [In] string enumname);
    #endregion

    #region GetError
    /// <summary>This function retrieves the current context error state.</summary>
    /// <param name="device">a pointer to the device to retrieve the error state from</param>
    /// <returns>Errorcode Int32.</returns>
    [DllImport(Lib, EntryPoint = "alcGetError", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern AlcError GetError([In] IntPtr device);
    #endregion

    #region IsExtensionPresent
    /// <summary>This function queries if a specified context extension is available.</summary>
    /// <param name="device">a pointer to the device to be queried for an extension.</param>
    /// <param name="extname">a null-terminated string describing the extension.</param>
    /// <returns>Returns True if the extension is available, False if the extension is not available.</returns>
    [DllImport(Lib, EntryPoint = "alcIsExtensionPresent", ExactSpelling = true, CallingConvention = Style, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
    internal static extern bool IsExtensionPresent([In] IntPtr device, [In] string extname);
    #endregion

    #region GetInteger
    [DllImport(Lib, EntryPoint = "alcGetIntegerv", ExactSpelling = true, CallingConvention = Style, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
    private unsafe static extern void GetInteger(IntPtr device, AlcGetInteger param, int size, int* data);

    /// <summary>This function returns integers related to the context.</summary>
    /// <param name="device">a pointer to the device to be queried.</param>
    /// <param name="param">an attribute to be retrieved: ALC_MAJOR_VERSION, ALC_MINOR_VERSION, ALC_ATTRIBUTES_SIZE, ALC_ALL_ATTRIBUTES</param>
    /// <param name="size">the size of the destination buffer provided, in number of integers.</param>
    /// <param name="data">a pointer to the buffer to be returned</param>
    internal static void GetInteger(IntPtr device, AlcGetInteger param, int size, out int data)
    {
        unsafe
        {
            fixed (int* data_ptr = &data)
            {
                GetInteger(device, param, size, data_ptr);
            }
        }
    }

    /// <summary>This function returns integers related to the context.</summary>
    /// <param name="device">a pointer to the device to be queried.</param>
    /// <param name="param">an attribute to be retrieved: ALC_MAJOR_VERSION, ALC_MINOR_VERSION, ALC_ATTRIBUTES_SIZE, ALC_ALL_ATTRIBUTES</param>
    /// <param name="size">the size of the destination buffer provided, in number of integers.</param>
    /// <param name="data">a pointer to the buffer to be returned</param>
    internal static void GetInteger(IntPtr device, AlcGetInteger param, int size, int[] data)
    {
        unsafe
        {
            fixed (int* data_ptr = data)
            {
                GetInteger(device, param, size, data_ptr);
            }
        }
    }
    #endregion

    #region GetProcAddress
    /// <summary>This function retrieves the address of a specified context extension function.</summary>
    /// <param name="device">a pointer to the device to be queried for the function.</param>
    /// <param name="funcname">a null-terminated string describing the function.</param>
    /// <returns>Returns the address of the function, or NULL if it is not found.</returns>
    [DllImport(Lib, EntryPoint = "alcGetProcAddress", ExactSpelling = true, CallingConvention = Style, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
    internal static extern IntPtr GetProcAddress([In] IntPtr device, [In] string funcname);
    #endregion

    #region GetString
    [DllImport(Lib, EntryPoint = "alcGetString", ExactSpelling = true, CallingConvention = Style, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
    private static extern IntPtr GetStringPrivate([In] IntPtr device, AlcGetString param);

    /// <summary>This function returns pointers to strings related to the context.</summary>
    /// <remarks>
    /// ALC_DEFAULT_DEVICE_SPECIFIER will return the name of the default output device.
    /// ALC_CAPTURE_DEFAULT_DEVICE_SPECIFIER will return the name of the default capture device.
    /// ALC_DEVICE_SPECIFIER will return the name of the specified output device if a pointer is supplied, or will return a list of all available devices if a NULL device pointer is supplied. A list is a pointer to a series of strings separated by NULL characters, with the list terminated by two NULL characters. See Enumeration Extension for more details.
    /// ALC_CAPTURE_DEVICE_SPECIFIER will return the name of the specified capture device if a pointer is supplied, or will return a list of all available devices if a NULL device pointer is supplied.
    /// ALC_EXTENSIONS returns a list of available context extensions, with each extension separated by a space and the list terminated by a NULL character.
    /// </remarks>
    /// <param name="device">a pointer to the device to be queried.</param>
    /// <param name="param">an attribute to be retrieved: ALC_DEFAULT_DEVICE_SPECIFIER, ALC_CAPTURE_DEFAULT_DEVICE_SPECIFIER, ALC_DEVICE_SPECIFIER, ALC_CAPTURE_DEVICE_SPECIFIER, ALC_EXTENSIONS</param>
    /// <returns>A string containing the name of the Device.</returns>
    internal static string GetString(IntPtr device, AlcGetString param)
    {
        IntPtr pstr = GetStringPrivate(device, param);
        string str = string.Empty;
        if (pstr != IntPtr.Zero)
        {
            str = Marshal.PtrToStringAnsi(pstr);
        }
        return str;
    }

    /// <summary>This function returns a List of strings related to the context.</summary>
    /// <remarks>
    /// ALC_DEVICE_SPECIFIER will return the name of the specified output device if a pointer is supplied, or will return a list of all available devices if a NULL device pointer is supplied. A list is a pointer to a series of strings separated by NULL characters, with the list terminated by two NULL characters. See Enumeration Extension for more details.
    /// ALC_CAPTURE_DEVICE_SPECIFIER will return the name of the specified capture device if a pointer is supplied, or will return a list of all available devices if a NULL device pointer is supplied.
    /// ALC_EXTENSIONS returns a list of available context extensions, with each extension separated by a space and the list terminated by a NULL character.
    /// </remarks>
    /// <param name="device">a pointer to the device to be queried.</param>
    /// <param name="param">an attribute to be retrieved: ALC_DEVICE_SPECIFIER, ALC_CAPTURE_DEVICE_SPECIFIER, ALC_ALL_DEVICES_SPECIFIER</param>
    /// <returns>A List of strings containing the names of the Devices.</returns>
    internal static IList<string> GetString(IntPtr device, AlcGetStringList param)
    {
        List<string> result = new();

        // We cannot use Marshal.PtrToStringAnsi(),
        //  because alcGetString is defined to return either a nul-terminated string,
        //  or an array of nul-terminated strings terminated by an extra nul.
        // Marshal.PtrToStringAnsi() will fail in the latter case (it will only
        // return the very first string in the array.)
        // We'll have to marshal this ourselves.
        IntPtr t = GetStringPrivate(device, (AlcGetString)param);
        if (t != IntPtr.Zero)
        {
            System.Text.StringBuilder sb = new();
            byte b;
            int offset = 0;
            do
            {
                b = Marshal.ReadByte(t, offset++);
                if (b != 0)
                {
                    sb.Append((char)b);
                }
                else
                {
                    // One string from the array is complete
                    result.Add(sb.ToString());

                    // Check whether the array has finished
                    // Note: offset already been increased through offset++ above
                    if (Marshal.ReadByte(t, offset) == 0)
                    {
                        // 2x consecutive nuls, we've read the whole array
                        break;
                    }
                    else
                    {
                        // Another string is starting, clear the StringBuilder
                        sb.Remove(0, sb.Length);
                    }
                }
            }
            while (true);
        }
        else
        {
            HConsole.Log("[Audio] Alc.GetString({0}, {1}) returned null.",
                device, param);
        }

        return result;
    }
    #endregion

    #region MakeContextCurrent
    /// <summary>
    /// This function makes a specified context the current context.
    /// </summary>
    /// <param name="context">A pointer to the new context handle.</param>
    /// <returns>Returns True on success, or False on failure.</returns>
    [DllImport(Lib, EntryPoint = "alcMakeContextCurrent", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern bool MakeContextCurrent(IntPtr context);
    #endregion

    #region OpenDevice
    /// <summary>This function opens a device by name.</summary>
    /// <param name="devicename">a null-terminated string describing a device.</param>
    /// <returns>Returns a pointer to the opened device. The return value will be NULL if there is an error.</returns>
    [DllImport(Lib, EntryPoint = "alcOpenDevice", ExactSpelling = true, CallingConvention = Style, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
    internal static extern IntPtr OpenDevice([In] string devicename);
    #endregion

    #region ProcessContext
    /// <summary>
    /// This function tells a context to begin processing. When a context is suspended, changes in OpenAL state will be accepted but will not be processed. alcSuspendContext can be used to suspend a context, and then all the OpenAL state changes can be applied at once, followed by a call to alcProcessContext to apply all the state changes immediately. In some cases, this procedure may be more efficient than application of properties in a non-suspended state. In some implementations, process and suspend calls are each a NOP.
    /// </summary>
    /// <param name="context">A pointer to the new context handle.</param>
    [DllImport(Lib, EntryPoint = "alcProcessContext", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void ProcessContext(IntPtr context);
    #endregion

    #region SuspendContext
    /// <summary>
    /// This function suspends processing on a specified context. When a context is suspended, changes in OpenAL state will be accepted but will not be processed. A typical use of alcSuspendContext would be to suspend a context, apply all the OpenAL state changes at once, and then call alcProcessContext to apply all the state changes at once. In some cases, this procedure may be more efficient than application of properties in a non-suspended state. In some implementations, process and suspend calls are each a NOP. 
    /// </summary>
    /// <param name="context">A pointer to the context to be suspended.</param>
    [DllImport(Lib, EntryPoint = "alcSuspendContext", ExactSpelling = true, CallingConvention = Style), SuppressUnmanagedCodeSecurity()]
    internal static extern void SuspendContext(IntPtr context);
    #endregion
}
