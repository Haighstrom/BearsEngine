using System.Text;
using BearsEngine.Win32API;
using System.Diagnostics;
using System.Collections;
using System.IO;

namespace BearsEngine;

public static class HConsole
{
    private static readonly IntPtr InvalidHandleValue = new(-1);

    public static IntPtr Handle => Kernal32.GetConsoleWindow();

    public static bool IsOpen => Handle != IntPtr.Zero;

    private static string _logFilePath;
    private const string LOG_FILE_PATH = "Log.txt";

    //Timers dictionary
    private static Dictionary<string, Stopwatch> _timersDict = new();

    /// <summary>
    /// Set to false in eg release copies of programs to disable more verbose logging and blocking execution 
    /// </summary>
    public static bool DebugMode { get; set; } = true;

    /// <summary>
    /// Default false. Set to true to cause any Warning calls to throw an exception to allow stack tracing
    /// </summary>
    public static bool ThrowErrorsOnWarnings { get; set; } = false;

    #region HandleOpenGLOutput
    public static void HandleOpenGLOutput(DebugSource source, DebugType type, uint id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
    {
        Log("");
        Log("-------------------");
        Log("OpenGL Message: Source - {0}, Type - {1}", source, type);
        Log("Severity - {0}, ID - {1}", severity, id);

        if (message == null)
            Log("Message is null");
        else
        {
            string messageString = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(message);
            Log("Message - {0}", messageString);
        }
        Log("-------------------");
        Log("");
    }
    #endregion

    #region SizeInCharacters
    public static Point SizeInCharacters
    {
        get
        {
            CONSOLE_SCREEN_BUFFER_INFO info;
            Kernal32.GetConsoleScreenBufferInfo(Handle, out info);

            return new Point(info.dwSize.X, info.dwSize.Y);
        }
    }

    public static int WidthInCharacters => (int)SizeInCharacters.X;
    public static int HeightInCharacters => (int)SizeInCharacters.Y;
    #endregion

    #region LogToFile
    private static class Logger
    {
        private static TextWriter _current;

        private class OutputWriter : TextWriter
        {
            public override Encoding Encoding
            {
                get
                {
                    return _current.Encoding;
                }
            }

            public override void WriteLine(string value)
            {
                try
                {
                    _current.WriteLine(value);
                    File.AppendAllLines(_logFilePath, new string[] { value });
                }
                catch { }
            }
        }

        public static void Init()
        {
            _current = Console.Out;
            Console.SetOut(new OutputWriter());
        }
    }

    public static void EnableLoggingToFile()
    {
        EnableLoggingToFile(LOG_FILE_PATH);
    }

    public static void EnableLoggingToFile(string filePath)
    {
        EnableLoggingToFile(filePath, false);
    }

    public static void EnableLoggingToFile(string filePath, bool appendToExistingFile)
    {
        _logFilePath = filePath;

        if (!appendToExistingFile && File.Exists(filePath))
            File.Delete(filePath);

        Logger.Init();
    }

    #endregion

    #region Log
    public static void Log(object o, params object[] args)
    {
        if (o == null)
            Log("null");
        else if (o is string)
            Log((string)o, args);
        else if (o is IDictionary)
        {
            foreach (DictionaryEntry entry in o as IDictionary)
            {
                Log(entry.Key, args);
                Log(entry.Value, args);
            }
        }
        else if (o is IEnumerable)
        {
            string s = "[";
            foreach (var item in o as IEnumerable)
            {
                s += item.ToString() + ",";
            }
            s = s.Substring(0, s.Length - 1);
            s += "]";
            Log(s, args);
        }
        else
            Log(o.ToString(), args);
    }

    public static void Log(string message, params object[] args)
    {
        try
        {
            Console.WriteLine(string.Format(message, args));
        }
        catch
        {
            Console.WriteLine(message);
        }
    }
    #endregion

    #region LogException
    /// <summary>
    /// Pass in any thrown exceptions to make them be logged to console and/or log file, and then the log file closed so it is actually written to. The stream writer only actually writes to the file when it is closed.
    /// </summary>
    /// <param name="e">The exception that was caught</param>
    /// <param name="callerName">A string to identify where this was called from eg "World Render Start"</param>
    /// <param name="memberName">Details of the calling function and line - is passed automatically, do not enter these parameters</param>
    /// <param name="classPath">Details of the calling function and line - is passed automatically, do not enter these parameters</param>
    /// <param name="lineNumber">Details of the calling function and line - is passed automatically, do not enter these parameters</param>
    public static void LogException(Exception e, string callerName = null, [System.Runtime.CompilerServices.CallerMemberName]string memberName = "", [System.Runtime.CompilerServices.CallerFilePath] string classPath = "", [System.Runtime.CompilerServices.CallerLineNumberAttribute] int lineNumber = 0)
    {
        Console.WriteLine();
        Log("!! An exception has been thrown by " + callerName);
        Log(e.Message);
        Log(e);
        Log("Inner Exception", e.InnerException);
        Console.WriteLine();

        throw e;
    }
    #endregion

    #region CheckOpenGLError
    /// <summary>
    /// Perform 
    /// </summary>
    /// <param name="callerID"></param>
    /// <returns></returns>
    public static bool CheckOpenGLError(string callerID)
    {
        var err = OpenGL32.GetError();
        if (err != OpenGLErrorCode.NO_ERROR)
        {
            Warning("OpenGL error in {0} : {1}", callerID, err);
            return true;
        }

        return false;
    }
    #endregion

    #region StartTimer
    /// <summary>
    /// Start a Stopwatch timer for debugging diagnostics purposes.
    /// </summary>
    /// <param name="operationName">Name of operation being timed, this needs to be supplied to StopTimer method to retrieve the timer object.</param>
    public static void StartTimer(string operationName)
    {
        if (!DebugMode)
            return;

        Stopwatch t = new();
        t.Start();
        
        Log("--> " + operationName + " started..");

        _timersDict.Add(operationName, t);
    }
    #endregion

    #region StopTimer
    /// <summary>
    /// Stops a timer specified by operationName string matching one already started with the same name, and displays its information to the console.
    /// </summary>
    /// <param name="operationName">string describing the operation being timed, and acting as a dictionary ID to the desired timer</param>
    /// <param name="success">bool indicating whether the operation succesfully completed or was cancelled - for console display purposes only</param>
    /// <returns>Time elapsed in ms since StartTimer called</returns>
    public static double StopTimer(string operationName, bool success = true)
    {
        if (!DebugMode)
            return -1;

        Stopwatch t = _timersDict[operationName];

        double millisecs = t.ElapsedMilliseconds;
        t.Stop();
        _timersDict.Remove(operationName);

        if (success)
            Log(operationName + " completed in " + millisecs.ToString("F2") + " ms");
        else
            Log(operationName + " aborted after " + millisecs.ToString("F2") + " ms");
        
        return millisecs;
    }
    #endregion

    #region Warning
    public static void Warning(object o, params object[] args)
    {
        if (ThrowErrorsOnWarnings)
            throw new HException("Warning: " + o.ToString(), o, args);

        Console.Write("[Warning]:");
        Log(o, args);
    }
    public static void Warning(string message, params object[] args)
    {
        if (ThrowErrorsOnWarnings)
            throw new HException("Warning: " + message, args);

        Console.Write("[Warning]:");
        Log(message, args);
    }
    #endregion

    #region WarningPressKey
    /// <summary>
    /// A warning that will also block further execution using Console.Readkey() - if DebugMode is on.
    /// </summary>
    public static void WarningPressKey(object o, params object[] args)
    {
        if (ThrowErrorsOnWarnings)
            throw new HException("Warning: " + o.ToString(), o, args);

        Warning(o, args);

        if (!DebugMode)
            return;

        Console.WriteLine();
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    /// <summary>
    /// A warning that will also block further execution using Console.Readkey() - if DebugMode is on.
    /// </summary>
    public static void WarningPressKey(string message, params object[] args)
    {
        Warning(message, args);

        if (!DebugMode)
            return;

        Console.WriteLine();
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    #endregion

    #region Show
    public static void Show()
    {
        Kernal32.AllocConsole();

        //this shite is explained here:
        //https://developercommunity.visualstudio.com/content/problem/12166/console-output-is-gone-in-vs2017-works-fine-when-d.html
        UnredirectConsole();
    }

    public static void Show(int x, int y, int w, int h)
    {
        Show();
        MoveConsoleTo(x, y, w, h);
    }
    #endregion

    #region Hide
    public static void Hide()
    {
        Kernal32.FreeConsole();
    }
    #endregion

    #region Dispose
    public static void Dispose()
    {
        Hide();
    }
    #endregion

    #region MoveConsoleTo
    public static void MoveConsoleTo(int x, int y, int w, int h)
    {
        User32.MoveWindow(Handle, x, y, w, h, true);
    }
    #endregion

    #region Maximise
    public static void Maximize()
    {
        Process p = Process.GetCurrentProcess();
        User32.ShowWindow(p.MainWindowHandle, ShowWindowCommand.MAXIMIZE);
    }
    #endregion

    #region UnredirectConsole
    public static void UnredirectConsole()
    {
        Kernal32.SetStdHandle(HandleType.STD_OUTPUT_HANDLE, GetConsoleStandardOutput());
        Kernal32.SetStdHandle(HandleType.STD_INPUT_HANDLE, GetConsoleStandardInput());
        Kernal32.SetStdHandle(HandleType.STD_ERROR_HANDLE, GetConsoleStandardError());
    }
    #endregion

    #region GetConsoleStandardInput
    private static IntPtr GetConsoleStandardInput()
    {
        var handle = Kernal32.CreateFile
            ("CONIN$"
            , DesiredAccess.GenericRead | DesiredAccess.GenericWrite
            , FileShare.ReadWrite
            , IntPtr.Zero
            , FileMode.Open
            , FileAttributes.Normal
            , IntPtr.Zero
            );
        if (handle == InvalidHandleValue)
            return InvalidHandleValue;
        return handle;
    }
    #endregion

    #region GetConsoleStandardOutput
    private static IntPtr GetConsoleStandardOutput()
    {
        var handle = Kernal32.CreateFile
            ("CONOUT$"
            , DesiredAccess.GenericWrite | DesiredAccess.GenericWrite
            , FileShare.ReadWrite
            , IntPtr.Zero
            , FileMode.Open
            , FileAttributes.Normal
            , IntPtr.Zero
            );
        if (handle == InvalidHandleValue)
            return InvalidHandleValue;
        return handle;
    }
    #endregion

    #region GetConsoleStandardError
    private static IntPtr GetConsoleStandardError()
    {
        var handle = Kernal32.CreateFile
            ("CONERR$"
            , DesiredAccess.GenericWrite | DesiredAccess.GenericWrite
            , FileShare.ReadWrite
            , IntPtr.Zero
            , FileMode.Open
            , FileAttributes.Normal
            , IntPtr.Zero
            );
        if (handle == InvalidHandleValue)
            return InvalidHandleValue;
        return handle;
    }
    #endregion

    #region MaxWidth
    public static int MaxWidth
    {
        get
        {
            IntPtr monitor = User32.MonitorFromWindow(Handle, MonitorFrom.Nearest);

            var mInfo = new MonitorInfo() { Size = MonitorInfo.UnmanagedSize };

            User32.GetMonitorInfo(monitor, ref mInfo);

            return mInfo.Work.Width;
        }
    }
    #endregion

    #region MaxHeight
    public static int MaxHeight
    {
        get
        {
            IntPtr monitor = User32.MonitorFromWindow(Handle, MonitorFrom.Nearest);

            var mInfo = new MonitorInfo() { Size = MonitorInfo.UnmanagedSize };

            User32.GetMonitorInfo(monitor, ref mInfo);

            return mInfo.Work.Height;
        }
    }
    #endregion
}
