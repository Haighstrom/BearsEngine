using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace BearsEngine.Win32API;

[SuppressUnmanagedCodeSecurity]
internal static class Kernal32
{
    //https://docs.microsoft.com/en-us/windows/console/allocconsole
    /// <summary>
    /// Allocates a new console for the calling process.
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AllocConsole();

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getmodulehandlew
    /// Retrieves a module handle for the specified module. The module must have been loaded by the calling process.
    /// To avoid the race conditions described in the Remarks section, use the GetModuleHandleEx function.
    /// </summary>
    /// <param name="lpModuleName">The name of the loaded module (either a .dll or .exe file). If the file name extension is omitted, the default library extension .dll is appended. The file name string can include a trailing point character (.) to indicate that the module name has no extension. The string does not have to specify a path. When specifying a path, be sure to use backslashes (\), not forward slashes (/). The name is compared (case independently) to the names of modules currently mapped into the address space of the calling process.
    /// If this parameter is NULL, GetModuleHandle returns a handle to the file used to create the calling process (.exe file).
    /// The GetModuleHandle function does not retrieve handles for modules that were loaded using the LOAD_LIBRARY_AS_DATAFILE flag.For more information, see LoadLibraryEx.</param>
    /// <returns>If the function succeeds, the return value is a handle to the specified module.
    /// If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);
    

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibraryw
    /// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
    /// For additional load options, use the LoadLibraryEx function.
    /// </summary>
    /// <param name="lpLibFileName">The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file). The name specified is the file name of the module and is not related to the name stored in the library module itself, as specified by the LIBRARY keyword in the module-definition (.def) file.
    /// If the string specifies a relative path or a module name without a path, the function uses a standard search strategy to find the module; for more information, see the Remarks.
    /// If the function cannot find the module, the function fails.When specifying a path, be sure to use backslashes (\), not forward slashes(/). For more information about paths, see Naming a File or Directory.
    /// If the string specifies a module name without a path and the file name extension is omitted, the function appends the default library extension .dll to the module name.To prevent the function from appending .dll to the module name, include a trailing point character (.) in the module name string.</param>
    /// <returns>If the function succeeds, the return value is a handle to the module.
    /// If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    [DllImport("kernel32.dll")]
    public static extern IntPtr LoadLibrary(string lpLibFileName);
    

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/errhandlingapi/nf-errhandlingapi-setlasterror
    /// Sets the last-error code for the calling thread.
    /// </summary>
    /// <param name="dwErrCode">The last-error code for the thread.</param>
    [DllImport("kernel32.dll")]
    internal static extern void SetLastError(int dwErrCode);
    

    // * * * CLEANED UP ABOVE THIS LINE * * *

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr CreateFile(
         [MarshalAs(UnmanagedType.LPTStr)] string lpFilename,
         [MarshalAs(UnmanagedType.U4)] DesiredAccess dwDesiredAccess,
         [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode,
         IntPtr lpSecurityAttributes, // optional SECURITY_ATTRIBUTES struct or IntPtr.Zero
         [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition,
         [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes,
         IntPtr hTemplateFile);
    

    /// <summary>
    /// Detaches this application from the console (if this is the only application using it, it will close)
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAsAttribute(UnmanagedType.Bool)]
    public static extern bool FreeConsole();
    

    [DllImport("kernel32.dll")]
    internal static extern bool FreeLibrary(IntPtr handle);
    

    [DllImport("kernel32.dll")]
    internal static extern bool GetConsoleScreenBufferInfo(
        IntPtr hConsoleOutput,
        out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo
        );
    

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetConsoleWindow();
    

    [DllImport("kernel32.dll")]
    internal static extern IntPtr GetProcAddress(IntPtr handle, string funcname);

    [DllImport("kernel32.dll")]
    internal static extern IntPtr GetProcAddress(IntPtr handle, IntPtr funcname);
    

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetStdHandle(HandleType nStdHandle);
    

    [DllImport("kernel32.dll")]
    internal static extern bool SetConsoleScreenBufferSize(
      IntPtr hConsoleOutput,
      COORD size
    );
    

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetStdHandle(HandleType nStdHandle, IntPtr hHandle);
    
}