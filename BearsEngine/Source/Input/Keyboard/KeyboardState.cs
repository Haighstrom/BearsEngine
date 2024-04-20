namespace BearsEngine.Input;

/// <summary>
/// Represents the state of a keyboard.
/// </summary>
public struct KeyboardState
{
    //allocate enough ints to store all keys
    const int IntSize = sizeof(int);
    const int NumInts = ((int)Key.LastKey + IntSize - 1) / IntSize;

    private static void ValidateOffset(int offset)
    {
        if (offset < 0 || offset >= NumInts * IntSize)
            throw new ArgumentOutOfRangeException("offset");
    }

    //fixed sized buffer
    unsafe fixed int _keys[NumInts];
    
    internal bool IsConnected { get; set; }

    internal void SetKeyState(Key key, bool down)
    {
        if (down)
            EnableBit((int)key);
        else
            DisableBit((int)key);
    }
    
    internal bool ReadBit(int offset)
    {
        ValidateOffset(offset);
        int intOffset = offset / 32;
        int bitOffset = offset % 32;
        unsafe
        {
            fixed (int* k = _keys)
            {
                return (*(k + intOffset) & (1 << bitOffset)) != 0u;
            }
        }
    }

    internal void EnableBit(int offset)
    {
        ValidateOffset(offset);
        int intOffset = offset / 32;
        int bitOffset = offset % 32;
        unsafe
        {
            fixed (int* k = _keys)
            {
                *(k + intOffset) |= 1 << bitOffset;
            }
        }
    }

    internal void DisableBit(int offset)
    {
        ValidateOffset(offset);
        int intOffset = offset / 32;
        int bitOffset = offset % 32;
        unsafe
        {
            fixed (int* k = _keys)
            {
                *(k + intOffset) &= ~(1 << bitOffset);
            }
        }
    }

    internal void MergeBits(KeyboardState other)
    {
        unsafe
        {
            int* k2 = other._keys;
            fixed(int* k1 = _keys)
            {
                for(int i = 0; i < NumInts; i++)
                {
                    *(k1 + i) |= *(k2 + i);
                }
            }
            IsConnected |= other.IsConnected;
        }
    }

    /// <summary>
    /// Checks if a specified keyboard key is currently pressed down.
    /// </summary>
    /// <param name="key">The key to query.</param>
    /// <returns>Returns true if the key is currently pressed down, false if it is up.</returns>
    public bool IsDown(Key key)
    {
        return ReadBit((int)key);
    }
}
