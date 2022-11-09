using System.Text.RegularExpressions;
using BearsEngine.UI;
using BearsEngine.Worlds.Graphics.Text.Components;
using BearsEngine.Graphics.Shaders;
using Line = BearsEngine.Graphics.Line;

namespace BearsEngine.Worlds.Graphics.Text
{
    public class HText : RectGraphicBase, IDisposable
    {
        private static readonly Dictionary<string, TextCommandType> _textCommandTagKeys = new()
        {
            { "colour", TextCommandType.Colour },
            { "font", TextCommandType.Font },
            { "fontstyle", TextCommandType.FontStyle },
            { "style", TextCommandType.FontStyle },
            { "size", TextCommandType.Size },
            { "underline", TextCommandType.Underline },
            { "strikethrough", TextCommandType.Strikethrough },
        };
        

        private static readonly Dictionary<string, TextCommandTag> _textCommandTags = new()
        {
            { "r", new TextCommandTag(TextCommandType.FontStyle, FontStyle.Regular) },
            { "b", new TextCommandTag(TextCommandType.FontStyle, FontStyle.Bold) },
            { "i", new TextCommandTag(TextCommandType.FontStyle, FontStyle.Italic) },
            { "bi", new TextCommandTag(TextCommandType.FontStyle, FontStyle.BoldItalic) },
            { "u", new TextCommandTag(TextCommandType.Underline, true) },
            { "nu", new TextCommandTag(TextCommandType.Underline, false) },
            { "s", new TextCommandTag(TextCommandType.Strikethrough, true) },
            { "ns", new TextCommandTag(TextCommandType.Strikethrough, false) },
        };
        

        /// <summary>
        /// Creates a shortcut to a text command. See TextCommandTag for valid options. Do not include the enclosing brackets in the key.
        /// </summary>
        public static void AddTextCommandTag(string key, TextCommandTag tagOverride)
        {
            if (key.IndexOfAny("<>=,()".ToCharArray()) != -1)
                throw new Exception($"Illegal character <>=,() in key for AutoTag {key}");

            if (_textCommandTagKeys.ContainsKey(key.ToLower()))
                throw new Exception($"{key} is a reserved tag key and cannot also be a command tag.");

            _textCommandTags.Add(key.ToLower(), tagOverride);
        }
        

        public static void RemoveTextCommandTag(string key) => _textCommandTags.Remove(key.ToLower());
        

        private List<Line> _linesToDraw = new();
        private List<SimpleGraphic> _vertGroups = new();
        private bool _verticesChanged = true;
        private HFont _font;
        private string _text;
        private HAlignment _hAlignment = HAlignment.Left;
        private VAlignment _vAlignment = VAlignment.Top;
        private float _scaleX = 1;
        private float _scaleY = 1;
        private bool _multiline = true;
        private float _extraCharacterSpacing = 0;
        private float _extraSpaceWidth = 0;
        private float _extraLineSpacing = 0;
        private int _startCharToWrite = 0;
        private int _numCharsToWrite = 0;
        private bool _useCommandTags = true;
        private bool _underline = false;
        private float _underlineThickness = 0.5f;
        private int _underlineOffset = -1;
        private bool _strikethrough = false;
        private float _strikethroughThickness = 0.5f;
        private int _strikethroughOffset = 0;
        

        public HText(UITheme theme, Rect r, string text)
            : this(theme.Text, r, text)
        {
        }

        public HText(TextTheme theme, Rect r, string text)
            : this(theme.Font, r, text, theme.FontColour)
        {
            ScaleX = ScaleY = theme.FontScale;
            HAlignment = theme.HAlignment;
            VAlignment = theme.VAlignment;
        }

        public HText(HFont font, Rect r, string text, Colour textColour)
            : this(font, r, text)
        {
            Colour = textColour;
        }

        public HText(HFont font, Rect r, string text)
            : this(new DefaultShader(), font, r.X, r.Y, r.W, r.H, text)
        {
        }

        public HText(IShader shader, HFont font, float x, float y, float w, float h, string text)
            : base(shader, x, y, w, h)
        {
            Font = font;
            Text = text ?? "";
        }
        

        public override float W
        {
            set
            {
                base.W = value;
                _verticesChanged = true;
            }
        }
        

        public override float H
        {
            set
            {
                base.H = value;
                _verticesChanged = true;
            }
        }
        

        public override Colour Colour
        {
            set
            {
                base.Colour = value;
                _verticesChanged = true;
            }
        }
        

        public HFont Font
        {
            get => _font;
            set
            {
                _font = value;
                _verticesChanged = true;
            }
        }
        

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                FirstCharToDraw = 0;
                NumCharsToDraw = value.Length;
                _verticesChanged = true;
            }
        }
        

        public float Angle { get; set; }

        public HAlignment HAlignment
        {
            get => _hAlignment;
            set
            {
                _hAlignment = value;
                _verticesChanged = true;
            }
        }
        

        public VAlignment VAlignment
        {
            get => _vAlignment;
            set
            {
                _vAlignment = value;
                _verticesChanged = true;
            }
        }
        

        /// <summary>
        /// True: text will wrap when exceeding w (if text is too big it will overrun the bottom); False: forces the text to a single line (if text is too big it will overrun the right)
        /// </summary>
        public bool Multiline
        {
            get => _multiline;
            set
            {
                _multiline = value;
                _verticesChanged = true;
            }
        }
        

        public float ExtraSpaceWidth
        {
            get => _extraSpaceWidth;
            set
            {
                _extraSpaceWidth = value;
                _verticesChanged = true;
            }
        }
        

        public float ExtraLineSpacing
        {
            get => _extraLineSpacing;
            set
            {
                _extraLineSpacing = value;
                _verticesChanged = true;
            }
        }
        

        public float ExtraCharacterSpacing
        {
            get => _extraCharacterSpacing;
            set
            {
                _extraCharacterSpacing = value;
                _verticesChanged = true;
            }
        }
        

        public float ScaleX
        {
            get => _scaleX;
            set
            {
                _scaleX = value;
                _verticesChanged = true;
            }
        }
        

        public float ScaleY
        {
            get => _scaleY;
            set
            {
                _scaleY = value;
                _verticesChanged = true;
            }
        }
        

        public int FirstCharToDraw
        {
            get => _startCharToWrite;
            set
            {
                _startCharToWrite = Maths.Clamp(value, 0, Text.Length);

                if (value + NumCharsToDraw > Text.Length)
                    NumCharsToDraw = Text.Length - value;

                _verticesChanged = true;
            }
        }
        

        public int NumCharsToDraw
        {
            get => _numCharsToWrite;
            set
            {
                _numCharsToWrite = Maths.Clamp(value, 0, Text.Length - _startCharToWrite);

                _verticesChanged = true;
            }
        }
        

        /// <summary>
        /// If disabled, command tags will be ignored and simply written
        /// </summary>
        public bool UseCommandTags
        {
            get => _useCommandTags;
            set
            {
                _useCommandTags = value;
                _verticesChanged = true;
            }
        }
        

        public bool Underline
        {
            get => _underline;
            set
            {
                _underline = value;
                _verticesChanged = true;
            }
        }
        

        public float UnderlineThickness
        {
            get => _underlineThickness;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "cannot have a thickness of <0");

                _underlineThickness = value;
                _verticesChanged = true;
            }
        }
        

        public int UnderlineOffset
        {
            get => _underlineOffset;
            set
            {
                _underlineOffset = value;
                _verticesChanged = true;
            }
        }
        

        public bool Strikethrough
        {
            get => _strikethrough;
            set
            {
                _strikethrough = value;
                _verticesChanged = true;
            }
        }
        

        public float StrikethroughThickness
        {
            get => _strikethroughThickness;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "cannot have a thickness of <0");

                _strikethroughThickness = value;
                _verticesChanged = true;
            }
        }
        

        public int StrikethroughOffset
        {
            get => _strikethroughOffset;
            set
            {
                _strikethroughOffset = value;
                _verticesChanged = true;
            }
        }
        
        

        public Point MeasureString(int characterIndex) => MeasureString(Text[characterIndex]);
        public Point MeasureString(char c) => MeasureString(c.ToString());
        public Point MeasureString(int first, int length) => MeasureString(Text.Substring(first, length));
        public Point MeasureString(string s)
        {
            if (Multiline)
                HConsole.Warning("Trying to measure strings when Text is multiline. This function will not take into account line breaks.");

            if (UseCommandTags)
                HConsole.Warning("Trying to measure strings when UseCommandTags is true. This function only measures against the default font.");

            return Font.MeasureString(s);
        }
        

        private TextCommandTag ParseTag(string tag)
        {
            var tagWithoutBrackets = tag.Substring(1, tag.Length - 2);

            if (tag.Contains('=')) //key-value pair
            {
                var keyValue = tagWithoutBrackets.Split('=');

                if (keyValue.Length != 2)
                    throw new Exception($"tag {tag} should have had exactly one '=' sign but has more than one. Regex fail?");

                if (!_textCommandTagKeys.ContainsKey(keyValue[0].ToLower()))
                {
                    HConsole.Warning($"HText/ParseTag: ({keyValue[0]}) is not a valid tag value, fulltag: ({tag})");
                    return null;
                }
                var key = _textCommandTagKeys[keyValue[0].ToLower()];
                var valueString = keyValue[1];
                if (valueString[0] == '(' && valueString[valueString.Length - 1] == ')')
                    valueString = valueString.Substring(1, valueString.Length - 2);

                try
                {
                    return key switch
                    {
                        TextCommandType.Font => new TextCommandTag(key, HFont.Load(valueString)),
                        TextCommandType.Size => new TextCommandTag(key, valueString.ParseTo<float>()),
                        TextCommandType.FontStyle => new TextCommandTag(key, valueString.ParseTo<System.Drawing.FontStyle>()),
                        TextCommandType.Colour => new TextCommandTag(key, new Colour(valueString)),
                        TextCommandType.Strikethrough or TextCommandType.Underline => new TextCommandTag(key, bool.Parse(valueString)),
                        _ => throw new Exception($"HText/ParseTag: TagType not catered for: ({key})"),
                    };
                }
                catch
                {
                    HConsole.Warning($"HText/ParseTag: Value ({valueString}) is not valid with Key ({key}), fulltag: ({tag})");
                    return null;
                }
            }
            else //should be a full command tag
            {
                if (_textCommandTags.ContainsKey(tagWithoutBrackets.ToLower()))
                    return _textCommandTags[tagWithoutBrackets.ToLower()];
                else
                {
                    HConsole.Warning($"HText/ParseTag: ({tagWithoutBrackets}) is not a valid command value, fulltag: ({tag})");
                    return null;
                }
            }
        }
        

        private TextCommandType? ParseCloseTag(string tag)
        {
            var tagWithoutBrackets = tag.Substring(2, tag.Length - 3);

            if (tagWithoutBrackets.IsNullOrEmpty()) //<\> case
                return null;

            if (_textCommandTagKeys.ContainsKey(tagWithoutBrackets))
                return _textCommandTagKeys[tagWithoutBrackets];
            else if (_textCommandTags.ContainsKey(tagWithoutBrackets))
                return _textCommandTags[tagWithoutBrackets].Type;
            else
            {
                HConsole.Warning("HText/ParseCloseTag: ({tagWithoutBrackets}) is not a valid tag key, fulltag: ({tag})");
                return null;
            }
        }
        

        private (HFont font, Colour colour, bool underline, bool strikethrough) GetCurrentAttributes(List<TextCommandTag> overrides)
        {
            (HFont font, Colour colour, bool underline, bool strikethrough) result = (Font, Colour, Underline, Strikethrough);

            foreach (var t in overrides)
            {
                switch (t.Type)
                {
                    case TextCommandType.Font:
                        result.font = (HFont)t.Value;
                        break;
                    case TextCommandType.Size:
                        result.font = HFont.Load(result.font.FontName, (float)t.Value, result.font.FontStyle, result.font.AntiAliased);
                        break;
                    case TextCommandType.FontStyle:
                        result.font = HFont.Load(result.font.FontName, result.font.FontSize, (FontStyle)t.Value, result.font.AntiAliased);
                        break;
                    case TextCommandType.Colour:
                        result.colour = (Colour)t.Value;
                        break;
                    case TextCommandType.Underline:
                        result.underline = (bool)t.Value;
                        break;
                    case TextCommandType.Strikethrough:
                        result.strikethrough = (bool)t.Value;
                        break;
                    default:
                        throw new Exception($"HText/GetCurrentAttributes: TagType not catered for: ({t.Type})");
                }
            }

            return result;
        }
        

        private List<Components.Line> SplitTextToLines(string text)
        {
            text = text.Replace("\r\n", "\n");
            text = text.Replace("\r", "\n");

            if (!Multiline)
                text = text.Replace("\n", "");

            List<Components.Line> lines = new();
            Components.Line currentLine = new();
            List<TextCommandTag> activeOverrides = new();
            (HFont font, Colour colour, bool underline, bool strikethrough) currentAttributes = (Font, Colour, Underline, Strikethrough);
            float sizeOfLetterLongerThanWholeLine = 0;

            while (!text.IsNullOrEmpty())
            {
                if (text[0] == '\n')
                {
                    currentLine.Add(new LC_NewLine(currentAttributes.font, ScaleY));
                    currentLine.Finalise(true);
                    lines.Add(currentLine);
                    currentLine = new();
                    text = text.Remove(0, 1);
                }
                else if (text[0] == ' ')
                {
                    var s = new LC_Space(currentAttributes.font, currentAttributes.colour, ExtraSpaceWidth, ScaleX, ScaleY, currentAttributes.underline, currentAttributes.strikethrough);
                    if (Multiline && currentLine.Length + s.Length > W)
                    {
                        if (currentLine.IsEmpty) //case where one space is wider than W - so a space will take up a full line
                            currentLine.Add(s);

                        currentLine.Finalise(false);
                        lines.Add(currentLine);
                        currentLine = new();
                    }
                    else
                        currentLine.Add(s);

                    text = text.Remove(0, 1);
                }
                else //it's a string that needs figuring out
                {
                    var nextText = text.Split(new[] { '\n' })[0];
                    var nextSpace = nextText.IndexOf(' ');
                    var nextOpenTag = UseCommandTags ? Regex.Match(nextText, @"(?<![/])<\w+(=(\w+|[(]?\w+([ ]\w+)*(,\w+([ ]\w+)*)*[)]?))?>") : Match.Empty; //see regexr.com/51s1n
                    var nextCloseTag = UseCommandTags ? Regex.Match(nextText, @"(?<![/])<[/](\w+)?>") : Match.Empty; //see regexr.com/51s2i

                    if (nextOpenTag.Success && nextOpenTag.Index == 0)
                    {
                        var t = ParseTag(nextOpenTag.Value);
                        if (t != null)
                        {
                            activeOverrides.Add(t);
                            currentAttributes = GetCurrentAttributes(activeOverrides);
                        }

                        text = text.Remove(0, nextOpenTag.Length);
                    }
                    else if (nextCloseTag.Success && nextCloseTag.Index == 0)
                    {
                        var typeToRemove = ParseCloseTag(nextCloseTag.Value);

                        if (typeToRemove == null) //empty tags remove whatever was last
                        {
                            if (activeOverrides.Count > 0)
                            {
                                activeOverrides.RemoveAt(activeOverrides.Count - 1);
                                currentAttributes = GetCurrentAttributes(activeOverrides);
                            }
                            else
                                HConsole.Warning($"HText/SplitTextToLines: close tag ({nextCloseTag.Value}) was applied but nothing to close");
                        }
                        else //remove specified tag on LIFO basis
                        {
                            var lastEntry = activeOverrides.LastOrDefault(t => t.Type == typeToRemove.Value);
                            if (lastEntry == null)
                                HConsole.Warning($"HText/SplitTextToLines: close tag ({nextCloseTag.Value}) was applied but nothing to close");
                            else
                            {
                                activeOverrides.Remove(lastEntry);
                                currentAttributes = GetCurrentAttributes(activeOverrides);
                            }
                        }

                        text = text.Remove(0, nextCloseTag.Length);
                    }
                    else //it's a word next
                    {
                        int len = nextText.Length;
                        if (nextOpenTag.Success)
                            len = Math.Min(len, nextOpenTag.Index);
                        if (nextCloseTag.Success)
                            len = Math.Min(len, nextCloseTag.Index);
                        if (nextSpace > 0)
                            len = Math.Min(len, nextSpace);

                        nextText = nextText.Substring(0, len);

                        if (!Multiline || currentLine.Length + ScaleX * (currentAttributes.font.MeasureString(nextText).X + ExtraCharacterSpacing) <= W) //if it fits, add it to the current line
                        {
                            currentLine.Add(new LC_Word(nextText, currentAttributes.font, currentAttributes.colour, ExtraCharacterSpacing, ScaleX, ScaleY, currentAttributes.underline, currentAttributes.strikethrough));
                            text = text.Remove(0, len);
                        }
                        else if (currentLine.IsEmpty) //if doesn't fit, but it is the first word in a line, we will split and wrap it
                        {
                            int splitAt = 0;
                            float width = 0;

                            float nextCharLength;
                            while (width + (nextCharLength = ScaleX * (currentAttributes.font.MeasureString(nextText[splitAt]).X + ExtraCharacterSpacing)) <= W)
                            {
                                width += nextCharLength;
                                splitAt++;
                            }

                            if (splitAt == 0) //if one letter is too wide to fit, display it anyway
                            {
                                splitAt = 1;
                                sizeOfLetterLongerThanWholeLine = Math.Max((float)Math.Round(sizeOfLetterLongerThanWholeLine, 2), nextCharLength);
                            }

                            currentLine.Add(new LC_Word(nextText.Substring(0, splitAt), currentAttributes.font, currentAttributes.colour, ExtraCharacterSpacing, ScaleX, ScaleY, currentAttributes.underline, currentAttributes.strikethrough));
                            currentLine.Finalise(false);
                            lines.Add(currentLine);
                            currentLine = new();
                            text = text.Remove(0, splitAt);
                        }
                        else //push the word to the next line
                        {
                            currentLine.Finalise(false);
                            lines.Add(currentLine);
                            currentLine = new();
                            //we don't add the next word to the next line yet - it might not fit - just loop again
                        }
                    }
                }
            }

            if (!currentLine.IsEmpty)
            {
                currentLine.Finalise(true);
                lines.Add(currentLine);
            }

            float height = lines.Sum(l => l.Height);
            if (VAlignment != VAlignment.Full)
                height += Math.Max(lines.Count - 1, 0) * ScaleY * ExtraLineSpacing;
            if (height > H)
                HConsole.Warning($"HText/SplitTextToLines: lines total height ({height}) is bigger than text box height ({H})");

            if (lines.Count > 0 && lines.Max(l => l.Length > W))
                HConsole.Warning($"HText/SplitTextToLines: line is wider ({lines.Max(l => l.Length)}) than text box width ({W})");

            if (sizeOfLetterLongerThanWholeLine > 0)
                HConsole.Warning($"HText/SplitTextToLines: a single letter is wider ({sizeOfLetterLongerThanWholeLine}) than the text box width ({W})");
            

            return lines;
        }

        private void SetVertices()
        {
            var lines = SplitTextToLines(Text.Substring(FirstCharToDraw, NumCharsToDraw));

            _linesToDraw = new();
            foreach (var sg in _vertGroups)
                sg.Dispose();
            _vertGroups = new List<SimpleGraphic>();
            var vertices = new List<Vertex>();
            var lastFont = Font;

            Rect dest = new Rect();

            float fullAlignmentLineSpacing = float.NaN;

            switch (VAlignment)
            {
                case VAlignment.Top:
                    dest.Y = 0;
                    break;
                case VAlignment.Centred:
                    dest.Y = (H - (lines.Sum(l => l.Height) + Math.Max(lines.Count - 1, 0) * ExtraLineSpacing)) / 2;//todo: was 'int'ed before to avoid looking shit with no AA - but buggers up text in cameras - complex if statement?
                    break;
                case VAlignment.Bottom:
                    dest.Y = H - (lines.Sum(l => l.Height) + Math.Max(lines.Count - 1, 0) * ExtraLineSpacing);//todo: was 'int'ed before to avoid looking shit with no AA - but buggers up text in cameras - complex if statement?
                    break;
                case VAlignment.Full:
                    fullAlignmentLineSpacing = (H - lines.Sum(l => l.Height)) / Math.Max(lines.Count - 1, 1);
                    break;
                default:
                    throw new Exception("HText/SetVertices: alignment {VAlignment} was not catered for.");
            }

            float top = dest.Y;

            foreach (var line in lines)
            {
                float lineLen = line.Length;
                float fullAlignmentSpaceWidth = float.NaN;

                switch (HAlignment)
                {
                    case HAlignment.Left:
                        dest.X = 0;
                        break;
                    case HAlignment.Centred:
                        dest.X = (W - lineLen) / 2; //todo: was 'int'ed before to avoid looking shit with no AA - but buggers up text in cameras - complex if statement?
                        break;
                    case HAlignment.Right:
                        dest.X = W - lineLen;//todo: was 'int'ed before to avoid looking shit with no AA - but buggers up text in cameras - complex if statement?
                        break;
                    case HAlignment.Full:
                        dest.X = 0;
                        fullAlignmentSpaceWidth = (W - line.LengthExcludingSpaces) / line.Spaces;
                        break;
                    default:
                        throw new Exception($"HText/SetVertices: alignment {HAlignment} was not catered for.");
                }

                foreach (var com in line)
                {
                    if (com is LC_Space s)
                    {
                        dest.W = s.Font.BitmapPosition(' ').W * ScaleX;
                        dest.H = s.Font.BitmapPosition(' ').H * ScaleY;

                        if (s.IsUnderlined)
                            _linesToDraw.Add(new Line(s.Colour, UnderlineThickness, true, dest.BottomLeft.Shift(0, UnderlineOffset), dest.BottomRight.Shift(0, UnderlineOffset)));
                        if (s.IsStruckthrough)
                            _linesToDraw.Add(new Line(s.Colour, StrikethroughThickness, true, dest.CentreLeft.Shift(0, StrikethroughOffset), dest.CentreRight.Shift(0, StrikethroughOffset)));
                        if (HAlignment == HAlignment.Full && !line.EndsWithNewLine)
                            dest.X += fullAlignmentSpaceWidth;
                        else
                            dest.X += s.Length;
                    }
                    else if (com is LC_Word w) //if it's a newline nothing will happen
                    {
                        for (int i = 0; i < w.Text.Length; ++i)
                        {
                            if (w.Font != lastFont)
                            {
                                _vertGroups.Add(new SimpleGraphic(new DefaultShader(), HF.Graphics.LoadTexture(lastFont.CharSpriteSheet, lastFont.LongName), vertices.ToArray()));
                                vertices = new List<Vertex>();
                                lastFont = w.Font;
                            }

                            var c = w.Text[i];
                            Rect source = w.Font.BitmapPositionNormalised(c);
                            dest.W = w.Font.BitmapPosition(c).W * ScaleX;
                            dest.H = w.Font.BitmapPosition(c).H * ScaleY;
                            float sizeDifference = line.Height - dest.H;

                            if (w.IsUnderlined)
                                _linesToDraw.Add(new Line(w.Colour, UnderlineThickness, true, dest.BottomLeft.Shift(0, UnderlineOffset), dest.BottomRight.Shift(0, UnderlineOffset)));
                            if (w.IsStruckthrough)
                                _linesToDraw.Add(new Line(w.Colour, StrikethroughThickness, true, dest.CentreLeft.Shift(0, StrikethroughOffset), dest.CentreRight.Shift(0, StrikethroughOffset)));

                            vertices.Add(HF.Geom.QuadToTris(
                                new Vertex(dest.TopLeft.Shift(0, sizeDifference), w.Colour, source.TopLeft),
                                new Vertex(dest.TopRight.Shift(0, sizeDifference), w.Colour, source.TopRight),
                                new Vertex(dest.BottomLeft.Shift(0, sizeDifference), w.Colour, source.BottomLeft),
                                new Vertex(dest.BottomRight.Shift(0, sizeDifference), w.Colour, source.BottomRight)
                                ));

                            dest.X += dest.W;
                            if (i != w.Text.Length - 1)
                                dest.X += ExtraCharacterSpacing;
                        }
                    }
                }

                dest.Y += line.Height;
                if (VAlignment == VAlignment.Full)
                    dest.Y += fullAlignmentLineSpacing;
                else
                    dest.Y += ExtraLineSpacing;
            }

            if (vertices.Count > 0)
                _vertGroups.Add(new SimpleGraphic(new DefaultShader(), HF.Graphics.LoadTexture(lastFont.CharSpriteSheet, lastFont.LongName), vertices.ToArray()));
        }
        

        private void SetVerticesSimple()
        {
            _linesToDraw = new();

            foreach (var sg in _vertGroups)
                sg.Dispose();
            _vertGroups = new List<SimpleGraphic>();

            var vertices = new List<Vertex>();

            string text = Text.Substring(FirstCharToDraw, NumCharsToDraw);
            var len = ScaleX * Font.MeasureString(text).X;

            Rect dest = new Rect()
            {
                H = ScaleY * _font.HighestChar
            };

            dest.X = HAlignment switch //todo: was 'int'ed before to avoid looking shit with no AA - but buggers up text in cameras - complex if statement?
            {
                HAlignment.Left or HAlignment.Full => 0,
                HAlignment.Centred => (W - len) / 2,
                HAlignment.Right => W - len,
                _ => throw new Exception($"HText/SetVertices: alignment {HAlignment} was not catered for."),
            };
            dest.Y = VAlignment switch //todo: was 'int'ed before to avoid looking shit with no AA - but buggers up text in cameras - complex if statement?
            {
                VAlignment.Top or VAlignment.Full => 0,
                VAlignment.Centred => (H - dest.H) / 2,
                VAlignment.Bottom => H - dest.H,
                _ => throw new Exception($"HText/SetVertices: alignment {VAlignment} was not catered for."),
            };
            if (Underline)
                _linesToDraw.Add(new Line(Colour, UnderlineThickness, true, dest.BottomLeft.Shift(0, UnderlineOffset), dest.BottomLeft.Shift(len, UnderlineOffset)));
            if (Strikethrough)
                _linesToDraw.Add(new Line(Colour, StrikethroughThickness, true, dest.CentreLeft.Shift(0, StrikethroughOffset), dest.CentreLeft.Shift(len, StrikethroughOffset)));

            foreach (char c in text)
            {
                Rect source = Font.BitmapPositionNormalised(c);
                dest.W = Font.BitmapPosition(c).W * ScaleX;

                vertices.Add(HF.Geom.QuadToTris(
                    new Vertex(dest.TopLeft, Colour, source.TopLeft),
                    new Vertex(dest.TopRight, Colour, source.TopRight),
                    new Vertex(dest.BottomLeft, Colour, source.BottomLeft),
                    new Vertex(dest.BottomRight, Colour, source.BottomRight)
                    ));

                dest.X += dest.W;
                if (c == ' ')
                    dest.X += ExtraSpaceWidth;
                else
                    dest.X += ExtraCharacterSpacing;
            }

            if (vertices.Count > 0)
                _vertGroups.Add(new SimpleGraphic(new DefaultShader(), HF.Graphics.LoadTexture(Font.CharSpriteSheet, Font.LongName), vertices.ToArray()));

            if (Font.HighestChar > H)
                HConsole.Warning($"HText/SetVerticesSimple: line height ({Font.HighestChar}) is bigger than text box height ({H})");

            if (len > W)
                HConsole.Warning($"HText/SetVerticesSimple: line is longer ({len}) than text box width ({W})");
            
        }
        

        public override void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            if (W == 0 || H == 0)
                return;

            var mv = modelView;

            if (Angle != 0)
                mv = Matrix4.RotateAroundPoint(ref mv, Angle, R.Centre.X, R.Centre.Y);

            mv = Matrix4.Translate(ref mv, X, Y, 0);

            if (_verticesChanged)
            {
                if (Multiline || UseCommandTags)
                    SetVertices();
                else
                    SetVerticesSimple();

                _verticesChanged = false;
            }

            foreach (var g in _vertGroups)
                g.Render(ref projection, ref mv);

            foreach (var l in _linesToDraw)
                l.Render(ref projection, ref mv);
        }

        public void Dispose()
        {
            _font?.Dispose();
            foreach (var l in _linesToDraw)
                l.Dispose();
            foreach (var v in _vertGroups)
                v.Dispose();
        }
    }
}