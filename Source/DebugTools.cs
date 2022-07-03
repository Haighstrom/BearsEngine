namespace BearsEngine;

public enum ErrorType { Error, Warning, Ignore }

public enum LogLocation { Console /*, Screen, File*/ }

public class DebugTools
{
    public bool VerboseLogging { get; set; } = false;
    public LogLocation LogLocation { get; set; } = LogLocation.Console;
}
