using System.Runtime.InteropServices;
using BearsEngine.Console.Windows;

namespace BearsEngine.Console;

/// <summary>
/// A class for managing the Console Window
/// </summary>
public class ConsoleWindow : IConsoleWindow
{
    private static ConsoleWindow? s_instance;

    internal static ConsoleWindow Instance 
    {
        get
        {
            if (s_instance is null)
            {
                throw new InvalidOperationException($"Tried to access {nameof(ConsoleWindow)}.{nameof(Instance)} before a {nameof(ConsoleWindow)} was created.");
            }

            return s_instance;
        }
    }

    private readonly IConsoleWindow _api;

    /// <summary>
    /// Create a default console, which is not visible
    /// </summary>
    public ConsoleWindow()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _api = new WinAPIConsoleWindow();
        }
        else
        {
            throw new NotImplementedException();
        }

        s_instance = this;
    }

    /// <summary>
    /// Create a console with the specified settings
    /// </summary>
    /// <param name="settings"></param>
    public ConsoleWindow(ConsoleSettings settings)
        : this()
    {
        if (settings.ShowConsoleWindow)
        {
            ShowConsole(settings.X, settings.Y, settings.Width, settings.Height);
        }
    }

    /// <summary>
    /// Whether the console is currently open/visible
    /// </summary>
    public bool Visible => _api.Visible;

    /// <summary>
    /// The maximum height the console can be without exceeding the screen height.
    /// </summary>
    public int MaxHeight => _api.MaxHeight;

    /// <summary>
    /// The maximum width the console can be without exceeding the screen height.
    /// </summary>
    public int MaxWidth => _api.MaxWidth;

    /// <summary>
    /// Hides/closes the console.
    /// </summary>
    public void HideConsole() => _api.HideConsole();

    /// <summary>
    /// Moves the console to a specified location on screen.
    /// </summary>
    /// <param name="x">The new x-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="y">The new y-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="width">The new width of the console in pixels.</param>
    /// <param name="height">The new height of the console in pixels.</param>
    public void MoveConsoleTo(int topLeftX, int topLeftY, int width, int height) => _api.MoveConsoleTo(topLeftX, topLeftY, width, height);

    /// <summary>
    /// Shows/opens the console.
    /// </summary>
    public void ShowConsole() => _api.ShowConsole();

    /// <summary>
    /// Shows/opens the console at the location specified.
    /// </summary>
    /// <param name="x">The x-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="y">The y-coordinate of the top left of the console, relative to the top left of the main display.</param>
    /// <param name="width">The desired width of the console in pixels.</param>
    /// <param name="height">The desired height of the console in pixels.</param>
    public void ShowConsole(int topLeftX, int topLeftY, int width, int height) => _api.ShowConsole(topLeftX, topLeftY, width, height);
}
