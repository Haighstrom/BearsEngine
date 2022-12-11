using HaighFramework.OpenGL;
using HaighFramework.Win32API;
using System.Diagnostics;

namespace BearsEngine;

public static class HConsole
{
    //Timers dictionary
    private static Dictionary<string, Stopwatch> _timersDict = new();

    public static bool CheckOpenGLError(string callerID)
    {
        var err = OpenGL32.glGetError();
        if (err != OpenGLErrorCode.NO_ERROR)
        {
            //Log($"OpenGL error in {callerID} : {err}");
            return true;
        }

        return false;
    }

    public static void StartTimer(string operationName)
    {
        Stopwatch t = new();
        t.Start();

        //Log("--> " + operationName + " started..");

        _timersDict.Add(operationName, t);
    }

    public static double StopTimer(string operationName, bool success = true)
    {
        Stopwatch t = _timersDict[operationName];

        double millisecs = t.ElapsedMilliseconds;
        t.Stop();
        _timersDict.Remove(operationName);

        //if (success)
        //    Log(operationName + " completed in " + millisecs.ToString("F2") + " ms");
        //else
        //    Log(operationName + " aborted after " + millisecs.ToString("F2") + " ms");

        return millisecs;
    }
}
