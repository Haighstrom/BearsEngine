namespace BearsEngine.Console;

/// <summary>
/// A type for managing the Console Window
/// </summary>
public interface IConsoleWindow
{
    /// <summary>
    /// Returns true if the console is currently visible/open.
    /// </summary>
    bool Visible { get; }

    /// <summary>
    /// The maximum height the console can be without exceeding the screen height.
    /// </summary>
    int MaxHeight { get; }

    /// <summary>
    /// The maximum width the console can be without exceeding the screen height.
    /// </summary>
    int MaxWidth { get; }

    /// <summary>
    /// Hides/closes the console.
    /// </summary>
    void HideConsole();

    /// <summary>
    /// Moves the console to a specified location on screen.
    /// </summary>
    /// <param name="x">The new x-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="y">The new y-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="width">The new width of the console in pixels.</param>
    /// <param name="height">The new height of the console in pixels.</param>
    void MoveConsoleTo(int x, int y, int w, int h);

    /// <summary>
    /// Shows/opens the console.
    /// </summary>
    void ShowConsole();

    /// <summary>
    /// Shows/opens the console at the location specified.
    /// </summary>
    /// <param name="x">The x-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="y">The y-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="width">The desired width of the console in pixels.</param>
    /// <param name="height">The desired height of the console in pixels.</param>
    void ShowConsole(int x, int y, int w, int h);
}
