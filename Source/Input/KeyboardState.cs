namespace BearsEngine.Input;

public struct KeyboardState : IEquatable<KeyboardState>
{
    #region Constants
    //allocate enough ints to store all keys
    const int IntSize = sizeof(int);
    const int NumInts = ((int)Key.LastKey + IntSize - 1) / IntSize;
    #endregion

    #region Static
    private static void ValidateOffset(int offset)
    {
        if (offset < 0 || offset >= NumInts * IntSize)
            throw new ArgumentOutOfRangeException("offset");
    }
    #endregion

    #region Fields
    //fixed sized buffer
    unsafe fixed int _keys[NumInts];
    #endregion

    #region Indexers
    public bool this[Key key]
    {
        get { return IsKeyDown(key); }
        internal set { SetKeyState(key, value); }
    }
    public bool this[short code]
    {
        get { return IsKeyDown((Key)code); }
    }
    #endregion

    #region Properties
    public bool IsConnected { get; internal set; }
    #endregion

    #region Methods
    #region Public
    #region IsKeyDown
    public bool IsKeyDown(Key key)
    {
        return ReadBit((int)key);
    }
    public bool IsKeyDown(short code)
    {
        return code >= 0 && code < (short)Key.LastKey && ReadBit(code);
    }
    #endregion

    #region IsKeyUp
    public bool IsKeyUp(Key key)
    {
        return !ReadBit((int)key);
    }
    public bool IsKeyUp(short code)
    {
        return !IsKeyDown(code);
    }
    #endregion
    #endregion

    #region Internal
    #region SetKeyState
    internal void SetKeyState(Key key, bool down)
    {
        if (down)
            EnableBit((int)key);
        else
            DisableBit((int)key);
    }
    #endregion

    #region ReadBit
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
    #endregion

    #region EnableBit
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
    #endregion

    #region DisableBit
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
    #endregion

    #region MergeBits
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
    #endregion
    #endregion
    #endregion

    #region IEquatable<KeyboardState>
    public bool Equals(KeyboardState other)
    {
        bool equal = true;
        unsafe
        {
            int* k2 = other._keys;
            fixed(int* k1 = _keys)
            {
                for (int i = 0; equal && i < NumInts; i++)
                    equal &= *(k1 + i) == *(k2 + i);
            }
        }
        return equal;
    }
    #endregion

    #region Overloads / Overrides
    #region ==
    public static bool operator ==(KeyboardState left, KeyboardState right)
    {
        return left.Equals(right);
    }
    #endregion

    #region !=
    public static bool operator !=(KeyboardState left, KeyboardState right)
    {
        return !left.Equals(right);
    }
    #endregion

    #region Equals
    public override bool Equals(object obj)
    {
        if (obj is KeyboardState)
        {
            return this == (KeyboardState)obj;
        }
        else
            return false;
    }
    #endregion

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion
}
