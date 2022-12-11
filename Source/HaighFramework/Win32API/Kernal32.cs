using System.Runtime.InteropServices;
using System.Security;

namespace HaighFramework.Win32API;

/// <summary>
/// Low-level operating system functions for memory management and resource handling.
/// </summary>
[SuppressUnmanagedCodeSecurity]
internal static class Kernal32
{
    private const string Library = "Kernel32.dll";

    /// <summary>
    /// Allocates a new console for the calling process.
    /// </summary>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool AllocConsole();

    /// <summary>
    /// Detaches the calling process from its console.
    /// </summary>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool FreeConsole();

    /// <summary>
    /// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count. When the reference count reaches zero, the module is unloaded from the address space of the calling process and the handle is no longer valid.
    /// </summary>
    /// <param name="hLibModule">A handle to the loaded library module. The LoadLibrary, LoadLibraryEx, GetModuleHandle, or GetModuleHandleEx function returns this handle.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call the GetLastError function.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool FreeLibrary(IntPtr hLibModule);

    /// <summary>
    /// Retrieves information about the specified console screen buffer.
    /// </summary>
    /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the GENERIC_READ access right. For more information, see Console Buffer Security and Access Rights.</param>
    /// <param name="lpConsoleScreenBufferInfo">A pointer to a CONSOLE_SCREEN_BUFFER_INFO structure that receives the console screen buffer information.</param>
    /// <returns></returns>
    [DllImport(Library)]
    public static extern bool GetConsoleScreenBufferInfo(IntPtr hConsoleOutput, out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

    /// <summary>
    /// Retrieves the window handle used by the console associated with the calling process.
    /// </summary>
    /// <returns>The return value is a handle to the window used by the console associated with the calling process or NULL if there is no such associated console.</returns>
    [DllImport(Library)]
    public static extern IntPtr GetConsoleWindow();

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
    [DllImport(Library, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    /// <summary>
    /// Retrieves the address of an exported function (also known as a procedure) or variable from the specified dynamic-link library (DLL).
    /// </summary>
    /// <param name="hModule">A handle to the DLL module that contains the function or variable. The LoadLibrary, LoadLibraryEx, LoadPackagedLibrary, or GetModuleHandle function returns this handle. The GetProcAddress function does not retrieve addresses from modules that were loaded using the LOAD_LIBRARY_AS_DATAFILE flag. For more information, see LoadLibraryEx.</param>
    /// <param name="lpProcName">The function or variable name, or the function's ordinal value. If this parameter is an ordinal value, it must be in the low-order word; the high-order word must be zero.</param>
    /// <returns>If the function succeeds, the return value is the address of the exported function or variable. If the function fails, the return value is NULL. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    /// <summary>
    /// Retrieves the address of an exported function (also known as a procedure) or variable from the specified dynamic-link library (DLL).
    /// </summary>
    /// <param name="hModule">A handle to the DLL module that contains the function or variable. The LoadLibrary, LoadLibraryEx, LoadPackagedLibrary, or GetModuleHandle function returns this handle. The GetProcAddress function does not retrieve addresses from modules that were loaded using the LOAD_LIBRARY_AS_DATAFILE flag. For more information, see LoadLibraryEx.</param>
    /// <param name="lpProcName">The function or variable name, or the function's ordinal value. If this parameter is an ordinal value, it must be in the low-order word; the high-order word must be zero.</param>
    /// <returns>If the function succeeds, the return value is the address of the exported function or variable. If the function fails, the return value is NULL. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, IntPtr lpProcName);

    /// <summary>
    /// Retrieves a handle to the specified standard device (standard input, standard output, or standard error).
    /// </summary>
    /// <param name="nStdHandle">The standard device.</param>
    /// <returns>If the function succeeds, the return value is a handle to the specified device, or a redirected handle set by a previous call to SetStdHandle. The handle has GENERIC_READ and GENERIC_WRITE access rights, unless the application has used SetStdHandle to set a standard handle with lesser access. If the function fails, the return value is INVALID_HANDLE_VALUE.To get extended error information, call GetLastError. If an application does not have associated standard handles, such as a service running on an interactive desktop, and has not redirected them, the return value is NULL.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr GetStdHandle(STDHANDLE nStdHandle);

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
    [DllImport(Library)]
    public static extern IntPtr LoadLibrary(string lpLibFileName);

    /// <summary>
    /// Changes the size of the specified console screen buffer.
    /// </summary>
    /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the GENERIC_READ access right. For more information, see Console Buffer Security and Access Rights.</param>
    /// <param name="dwSize">A COORD structure that specifies the new size of the console screen buffer, in character rows and columns. The specified width and height cannot be less than the width and height of the console screen buffer's window. The specified dimensions also cannot be less than the minimum size allowed by the system. This minimum depends on the current font size for the console (selected by the user) and the SM_CXMIN and SM_CYMIN values returned by the GetSystemMetrics function.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD dwSize);

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/errhandlingapi/nf-errhandlingapi-setlasterror
    /// Sets the last-error code for the calling thread.
    /// </summary>
    /// <param name="dwErrCode">The last-error code for the thread.</param>
    [DllImport(Library)]
    public static extern void SetLastError(int dwErrCode);

    /// <summary>
    /// Sets the handle for the specified standard device (standard input, standard output, or standard error).
    /// </summary>
    /// <param name="nStdHandle">The standard device for which the handle is to be set.</param>
    /// <param name="hHandle">The handle for the standard device.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool SetStdHandle(STDHANDLE nStdHandle, IntPtr hHandle);
}