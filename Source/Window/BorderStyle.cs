namespace BearsEngine.Window;

public enum BorderStyle
{
    /// <summary>
    /// Window without a border. Will be used when making an application full screen.
    /// </summary>
    NoBorder,
    /// <summary>
    /// Window with a border which cannot be resized
    /// </summary>
    Border,
    /// <summary>
    /// Window with a border that can be resized.
    /// </summary>
    SizingBorder,
}
