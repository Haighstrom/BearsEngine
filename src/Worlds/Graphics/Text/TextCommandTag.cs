namespace BearsEngine.Worlds.Graphics.Text
{
    #region public enum TextCommandType
    /// <summary>
    /// Type of TextCommand - indicates the aspect of text that will be amended
    /// </summary>
    public enum TextCommandType { Font, Size, FontStyle, Colour, Underline, Strikethrough }
    #endregion

    public class TextCommandTag
    {
        /// <summary>
        /// Creates a TextCommand, for use with HText.AddTextCommandTag. Possible options:
        /// 1. Font, for permitted values see HFont.Load(string fontLongName)
        /// 2. Size, with float value
        /// 3. FontStyle, with use of FontStyle enum
        /// 4. Colour, with format byte,byte,byte,byte or (byte,byte,byte,byte)
        /// 5. Underline, with bool value
        /// 6. Strikethrough, with bool value
        /// </summary>
        public TextCommandTag(TextCommandType type, object value)
        {
            Type = type;
            Value = value;
        }
        public TextCommandType Type { get; }
        public object Value { get; }
    }
}