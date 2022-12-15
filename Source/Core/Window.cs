using HaighFramework.Input;
using HaighFramework.Window;

namespace BearsEngine;

/// <summary>
/// Provides access to Window information and features
/// </summary>
public static class Window
{
    private static IWindow? _instance;

    internal static IWindow Instance
    {
        get
        {
            if (_instance is null)
                throw new InvalidOperationException($"You must call {nameof(GameEngine)}.{nameof(GameEngine.Run)} before accessing functions within {nameof(Window)}.");

            return _instance;
        }
        set => _instance = value;
    }

    public static BorderStyle Border { get => Instance.Border; set => Instance.Border = value; }

    public static Point ClientSize { get => Instance.ClientSize; set => Instance.ClientSize = value; }

    public static Cursor Cursor { get => Instance.Cursor; set => Instance.Cursor = value; }

    public static bool CursorLockedToWindow { get => Instance.CursorLockedToWindow; set => Instance.CursorLockedToWindow = value; }

    public static bool CursorVisible { get => Instance.CursorVisible; set => Instance.CursorVisible = value; }

    public static bool ExitOnClose { get => Instance.ExitOnClose; set => Instance.ExitOnClose = value; }

    public static bool Focussed => Instance.Focussed;

    public static int Height { get => Instance.Height; set => Instance.Height = value; }
   
    public static Icon Icon { get => Instance.Icon; set => Instance.Icon = value; }
    
    public static Point MinClientSize { get => Instance.MinClientSize; set => Instance.MinClientSize = value; }
    
    public static Point MaxClientSize { get => Instance.MaxClientSize; set => Instance.MaxClientSize = value; }

    public static Rect Position { get => Instance.Position; set => Instance.Position = value; }
    
    public static WindowState State { get => Instance.State; set => Instance.State = value; }
    
    public static string Title { get => Instance.Title; set => Instance.Title = value; }

    public static bool Visible { get => Instance.Visible; set => Instance.Visible = value; }
    
    public static int Width { get => Instance.Width; set => Instance.Width = value; }
    
    public static int X { get => Instance.X; set => Instance.X = value; }
    
    public static int Y { get => Instance.Y; set => Instance.Y = value; }

    public static event EventHandler<KeyboardCharEventArgs>? CharEntered
    {
        add => Instance.CharEntered += value;

        remove => Instance.CharEntered -= value;
    }

    public static event EventHandler? CloseAttempted
    {
        add => Instance.CloseAttempted += value;

        remove => Instance.CloseAttempted -= value;
    }

    public static event EventHandler? Closed
    {
        add => Instance.Closed += value;

        remove => Instance.Closed -= value;
    }

    public static event EventHandler<FocusChangedEventArgs>? FocusChanged
    {
        add => Instance.FocusChanged += value;

        remove => Instance.FocusChanged -= value;
    }

    public static event EventHandler<KeyboardKeyEventArgs>? KeyDown
    {
        add => Instance.KeyDown += value;

        remove => Instance.KeyDown -= value;
    }

    public static event EventHandler<KeyboardKeyEventArgs>? KeyUp
    {
        add => Instance.KeyUp += value;

        remove => Instance.KeyUp -= value;
    }

    public static event EventHandler? Moved
    {
        add => Instance.Moved += value;

        remove => Instance.Moved -= value;
    }

    public static event EventHandler? Resized
    {
        add => Instance.Resized += value;

        remove => Instance.Resized -= value;
    }

    public static void Centre() => Instance.Centre();

    public static void Close() => Instance.Close();

    public static void Exit() => Instance.Exit();

    public static void MakeFocussed() => Instance.MakeFocussed();

    public static Point ScreenToClient(Point screenPosition) => Instance.ScreenToClient(screenPosition);
}