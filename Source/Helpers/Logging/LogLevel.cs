namespace BearsEngine.Logging;

/// <summary>
/// Specifies the level of logging that a messages should write or that a logger should display.
/// </summary>
public enum LogLevel
{
    /// <summary>
    /// Specifies that a logger should not write any messages. Should not be used to write messages.
    /// </summary>
    None = 0,

    /// <summary>
    /// Logs that contain the most detailed messages, and may contain sensitive application data. These should not be made available to write or used in a production environment.
    /// </summary>
    Verbose = 1,

    /// <summary>
    /// Debug is used for internal information that may not be observable from the outside, but useful when determining how something happened.
    /// </summary>
    Debug = 2,

    /// <summary>
    /// Information logs describe things happening in the system, which are usually observable actions the system are performing.
    /// </summary>
    Information = 3,

    /// <summary>
    /// Warning logs are used to indicate that functionality is behaving slightly outside of what is expected.
    /// </summary>
    Warning = 4,

    /// <summary>
    /// An Error log should be used to indicate that functionality is unavailable or expectations broken.
    /// </summary>
    Error = 5,

    /// <summary>
    /// Fatal logs should be used for recording events which cause the application to close.
    /// </summary>
    Fatal = 6,
}
