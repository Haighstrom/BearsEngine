namespace BearsEngine.Input;

/// <summary>
/// Enumerates modifier keys.
/// </summary>
[Flags]
public enum KeyModifiers : byte
{
    Alt = 1 << 0,

    Control = 1 << 1,

    Shift = 1 << 2
}
