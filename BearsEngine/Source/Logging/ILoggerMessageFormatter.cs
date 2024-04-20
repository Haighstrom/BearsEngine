namespace BearsEngine.Logging;

/// <summary>
/// Formatter for translating objects into strings for the purposes of logging.
/// </summary>
internal interface ILoggerMessageFormatter
{
    /// <summary>
    /// Translate an object into a logging string.
    /// </summary>
    /// <param name="o">the object to translate.</param>
    /// <returns>Returns a loggable string.</returns>
    public string FormatToLoggingString(object? o);
}
