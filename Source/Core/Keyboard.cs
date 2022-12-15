using HaighFramework.Input;

namespace BearsEngine;

/// <summary>
/// Provides access to Keyboard information
/// </summary>
public static class Keyboard
{
    private static KeyboardState _previousState = new();
    
    internal static void Update(KeyboardState currentState)
    {
        _previousState = CurrentState;
        CurrentState = currentState;
    }

    /// <summary>
    /// Get the current state of the keyboard.
    /// </summary>
    public static KeyboardState CurrentState { get; private set; } = new();

    /// <summary>
    /// Check if a key is currently held down.
    /// </summary>
    /// <param name="k">The key to check.</param>
    /// <returns>Returns true if the specified key is currently held down, false otherwise.</returns>
    public static bool KeyDown(Key k)
    {
        return CurrentState.IsKeyDown(k);
    }

    /// <summary>
    /// Check if any of a list of keys is currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if any key provided is currently down, false otherwise.</returns>
    public static bool AnyKeyDown(IEnumerable<Key> keys)
    {
        foreach (Key k in keys)
            if (KeyDown(k))
                return true;

        return false;
    }

    /// <summary>
    /// Check if any of a list of keys is currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if any key provided is currently down, false otherwise.</returns>
    public static bool AnyKeyDown(params Key[] keys)
    {
        foreach (Key k in keys)
            if (KeyDown(k))
                return true;

        return false;
    }

    /// <summary>
    /// Check if all of a list of keys are currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if all keys provided are currently down, false otherwise.</returns>
    public static bool AllKeysDown(IEnumerable<Key> keys)
    {
        foreach (Key k in keys)
            if (!KeyDown(k))
                return false;

        return true;
    }

    /// <summary>
    /// Check if all of a list of keys are currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if all keys provided are currently down, false otherwise.</returns>
    public static bool AllKeysDown(params Key[] keys)
    {
        foreach (Key k in keys)
            if (!KeyDown(k))
                return false;

        return true;
    }

    /// <summary>
    /// Check if a key was pressed since the last frame (i.e. last frame it was up, and now it is down).
    /// </summary>
    /// <param name="k">The key to check.</param>
    /// <returns>Returns true if the specified key was pressed in the last frame, false otherwise.</returns>
    public static bool KeyPressed(Key k)
    {
        return _previousState.IsKeyUp(k) &&
            CurrentState.IsKeyDown(k);
    }

    /// <summary>
    /// Check if a key was released since the last frame (i.e. last frame it was down, and now it is up).
    /// </summary>
    /// <param name="k">The key to check.</param>
    /// <returns>Returns true if the specified key was released in the last frame, false otherwise.</returns>
    public static bool KeyReleased(Key k)
    {
        return _previousState.IsKeyDown(k) &&
            CurrentState.IsKeyUp(k);
    }
}