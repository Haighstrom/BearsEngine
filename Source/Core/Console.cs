namespace BearsEngine;

/// <summary>
/// Provides a static entrypoint to Console information and features
/// </summary>
public static class Console
{
    private static IConsoleWindow? _instance;

    internal static IConsoleWindow Instance
    {
        get
        {
            if (_instance is null)
                throw new InvalidOperationException($"You must call {nameof(GameEngine)}.{nameof(GameEngine.Run)} before accessing functions within {nameof(Console)}.");

            return _instance;
        }
        set => _instance = value;
    }

    /// <summary>
    /// The maximum height the console can be without exceeding the screen height.
    /// </summary>
    public static int MaxHeight => Instance.MaxHeight;

    /// <summary>
    /// The maximum width the console can be without exceeding the screen width.
    /// </summary>
    public static int MaxWidth => Instance.MaxWidth;

    /// <summary>
    /// Returns true if the console is currently visible.
    /// </summary>
    public static bool Visible => Instance.Visible;

    /// <summary>
    /// Hides/closes the console.
    /// </summary>
    public static void HideConsole() => Instance.HideConsole();

    /// <summary>
    /// Moves the console to a specified location on screen.
    /// </summary>
    /// <param name="x">The new x-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="y">The new y-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="width">The new width of the console in pixels.</param>
    /// <param name="height">The new height of the console in pixels.</param>
    public static void MoveConsoleTo(int x, int y, int width, int height) => Instance.MoveConsoleTo(x, y, width, height);

    /// <summary>
    /// Shows/opens the console.
    /// </summary>
    public static void ShowConsole() => Instance.ShowConsole();

    /// <summary>
    /// Shows/opens the console at the location specified.
    /// </summary>
    /// <param name="x">The x-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="y">The y-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="width">The desired width of the console in pixels.</param>
    /// <param name="height">The desired height of the console in pixels.</param>
    public static void ShowConsole(int x, int y, int width, int height) => Instance.ShowConsole(x, y, width, height);
}