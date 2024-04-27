namespace BearsEngine.Input;

public interface IKeyboard
{
    /// <summary>
    /// Check if a key is currently held down.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>Returns true if the specified key is currently held down, false otherwise.</returns>
    bool KeyDown(Key key);

    /// <summary>
    /// Check if a key was pressed since the last frame (i.e. last frame it was up, and now it is down).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>Returns true if the specified key was pressed in the last frame, false otherwise.</returns>
    bool KeyPressed(Key key);

    /// <summary>
    /// Check if a key was released since the last frame (i.e. last frame it was down, and now it is up).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>Returns true if the specified key was released in the last frame, false otherwise.</returns>
    bool KeyReleased(Key key);

    /// <summary>
    /// Check if any of a list of keys is currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if any key provided is currently down, false otherwise.</returns>
    bool AnyKeyDown(IEnumerable<Key> keys);

    /// <summary>
    /// Check if any of a list of keys is currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if any key provided is currently down, false otherwise.</returns>
    bool AnyKeyDown(params Key[] keys);

    /// <summary>
    /// Check if all of a list of keys are currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if all keys provided are currently down, false otherwise.</returns>
    bool AllKeysDown(IEnumerable<Key> keys);

    /// <summary>
    /// Check if all of a list of keys are currently held down.
    /// </summary>
    /// <param name="keys">The keys to check against.</param>
    /// <returns>Returns true if all keys provided are currently down, false otherwise.</returns>
    bool AllKeysDown(params Key[] keys);
}
