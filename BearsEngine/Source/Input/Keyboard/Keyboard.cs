namespace BearsEngine.Input;

internal class Keyboard : IKeyboardInternal
{
    internal static Keyboard Instance { get; private set; } = null!;

    private KeyboardState _previousState = new();
    private KeyboardState _currentState = new();

    public Keyboard()
    {
        Instance = this;   
    }

    void IKeyboardInternal.Update(KeyboardState newState)
    {
        _previousState = _currentState;
        _currentState = newState;
    }

    /// <summary>
    /// Check if a key is currently held down.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>Returns true if the specified key is currently held down, false otherwise.</returns>
    public bool KeyDown(Key key)
    {
        return _currentState.IsDown(key);
    }

    /// <summary>
    /// Check if a key was pressed since the last frame (i.e. last frame it was up, and now it is down).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>Returns true if the specified key was pressed in the last frame, false otherwise.</returns>
    public bool KeyPressed(Key key)
    {
        return 
            !_previousState.IsDown(key) &&
            _currentState.IsDown(key);
    }

    /// <summary>
    /// Check if a key was released since the last frame (i.e. last frame it was down, and now it is up).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>Returns true if the specified key was released in the last frame, false otherwise.</returns>
    public bool KeyReleased(Key key)
    {
        return 
            _previousState.IsDown(key) &&
            !_currentState.IsDown(key);
    }

    /// <summary>
    /// Check if any of a list of keys is currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if any key provided is currently down, false otherwise.</returns>
    public bool AnyKeyDown(IEnumerable<Key> keys)
    {
        foreach (Key k in keys)
        {
            if (KeyDown(k))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Check if any of a list of keys is currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if any key provided is currently down, false otherwise.</returns>
    public bool AnyKeyDown(params Key[] keys)
    {
        foreach (Key k in keys)
        {
            if (KeyDown(k))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Check if all of a list of keys are currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if all keys provided are currently down, false otherwise.</returns>
    public bool AllKeysDown(IEnumerable<Key> keys)
    {
        foreach (Key k in keys)
        {
            if (!KeyDown(k))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Check if all of a list of keys are currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if all keys provided are currently down, false otherwise.</returns>
    public bool AllKeysDown(params Key[] keys)
    {
        foreach (Key k in keys)
        {
            if (!KeyDown(k))
            {
                return false;
            }
        }

        return true;
    }
}
