using BearsEngine.Win32API;
using System.Diagnostics;

namespace BearsEngine;

public static class HConsole
{
    //Timers dictionary
    private static Dictionary<string, Stopwatch> _timersDict = new();

    private static void Log(object message, params object?[] args) { }

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

    public static void LogException(Exception e, string callerName = null, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "", [System.Runtime.CompilerServices.CallerFilePath] string classPath = "", [System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
    {
        Console.WriteLine();
        Log("!! An exception has been thrown by " + callerName);
        Log(e.Message);
        Log(e);
        Log("Inner Exception", e.InnerException);
        Console.WriteLine();

        throw e;
    }

    public static bool CheckOpenGLError(string callerID)
    {
        var err = OpenGL32.GetError();
        if (err != OpenGLErrorCode.NO_ERROR)
        {
            Log($"OpenGL error in {callerID} : {err}");
            return true;
        }

        return false;
    }

    public static void StartTimer(string operationName)
    {
        Stopwatch t = new();
        t.Start();

        Log("--> " + operationName + " started..");

        _timersDict.Add(operationName, t);
    }

    public static double StopTimer(string operationName, bool success = true)
    {
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
}
