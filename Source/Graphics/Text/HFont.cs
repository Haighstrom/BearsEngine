using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using Point = HaighFramework.Point;

namespace BearsEngine.Worlds.Graphics.Text;

public enum FontStyle { Regular = 0, Bold = 1, Italic = 2, BoldItalic = 3 }

[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This is part of the Windows API which will only be invoked if on Windows platform.")]
public class HFont : IDisposable
{
    private struct FontSave
    {
        public string Name { get; set; }
        public float Size { get; set; }
        public FontStyle FontStyle { get; set; }
        public bool AntiAliased { get; set; }
        public int WidestChar { get; set; }
        public int HighestChar { get; set; }
        public int SpaceWidth { get; set; }
        public Dictionary<char, Rect> CharPositions { get; set; }
        public Dictionary<char, Rect> CharPositionsNormalised { get; set; }
    }

    public const string DEFAULT_FONT = "Times New Roman";
    public const float DEFAULT_SIZE = 12;
    public const FontStyle DEFAULT_FONTSTYLE = FontStyle.Regular;
    public const bool DEFAULT_AA = false;
    private const string DEFAULT_CHARS_TO_LOAD = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 !£$%^&*()-=_+[]{};'#:@~,./<>?|`¬¦€""\";
    private const string DEFAULT_FONT_FOLDER = "Assets/Fonts/";
    private const int BITMAP_CHARS_PER_ROW = 13;

    private static HFont _default;
    private static readonly Dictionary<string, HFont> _loadedFonts = new();

    /// <summary>
    /// Returns a basic Font
    /// </summary>
    public static HFont Default => _default ??= new HFont(DEFAULT_FONT, DEFAULT_SIZE);

    /// <summary>
    /// Loads a font from a string of format "name,size" or "name,size,(int)fontstyle" or "name,size,(int)fontstyle,(int)antialiased"
    /// </summary>
    public static HFont Load(string fontLongName)
    {
        var parts = fontLongName.Split(',');

        if (parts.Length < 2 || parts.Length > 4)
            throw new ArgumentException($"fontLongName should be 2-4 parts, actually ({parts.Length}), full request ({fontLongName})");

        string name = fontLongName.Split(',')[0];
        float size = fontLongName.Split(',')[1].ParseTo<float>();
        FontStyle fs = parts.Length >= 3 ? (FontStyle)fontLongName.Split(',')[2].ParseTo<int>() : DEFAULT_FONTSTYLE;
        bool aa = parts.Length == 4 ? Convert.ToBoolean(fontLongName.Split(',')[3].ParseTo<int>()) : DEFAULT_AA;

        return Load(name, size, fs, aa);
    }

    /// <summary>
    /// Main function to load an HFont.
    /// </summary>
    public static HFont Load(string fontName, float size, FontStyle fontStyle = DEFAULT_FONTSTYLE, bool antiAliased = DEFAULT_AA, string extraCharsToLoad = "")
    {
        var longName = string.Format("{0},{1},{2},{3}", fontName, size, (int)fontStyle, antiAliased ? 1 : 0);

        //if it's already in memory reuse it
        if (_loadedFonts.ContainsKey(longName))
            return _loadedFonts[longName];

        HFont hFont;

        //if it's been created and saved to disk load it
        if (Files.FileExists(DEFAULT_FONT_FOLDER + longName + ".details"))
        {
            try
            {
                var details = Files.ReadJsonFile<FontSave>(DEFAULT_FONT_FOLDER + longName + ".details");
                var bmp = BitmapTools.LoadBitmap(DEFAULT_FONT_FOLDER + longName + ".png");
                hFont = new HFont(details, bmp);
                if (hFont.LongName != longName)
                    throw new Exception($"Loaded font's details ({hFont.LongName}) don't match requested details ({longName})");
                _loadedFonts.Add(longName, hFont);
                return hFont;
            }
            catch (Exception e)
            {
                Log.Warning($".details file for font {longName} existed but couldn't successfully load due to exception ({e})");
            }
        }

        //we need to generate the font
        hFont = new HFont(fontName, size, fontStyle, antiAliased, extraCharsToLoad ?? "");
        _loadedFonts.Add(longName, hFont); //note that f.longname may be different due to getting replaced by TimesNewRoman if font name is bad. We'll save keys based on the requested value.
        return hFont;
    }

    private static Font LoadFontPreinstalled(string fontName, float size, FontStyle style)
    {
        var Font = new Font(fontName, size, (System.Drawing.FontStyle)style);

        if (Font.Name == fontName)
            return Font;
        else
            return null;
    }

    private static Font LoadFontCustom(string fontPath, float size, FontStyle style)
    {
        if (!Files.FileExists(fontPath))
            fontPath = DEFAULT_FONT_FOLDER + fontPath;

        if (!Files.FileExists(fontPath))
            fontPath = fontPath + ".ttf";

        if (!Files.FileExists(fontPath))
            return null;

        using var pfc = new PrivateFontCollection();

        pfc.AddFontFile(fontPath);

        return new Font(pfc.Families[0], size, (System.Drawing.FontStyle)style);
    }

    private readonly object _syncRoot = new();
    private bool _disposed = false;

    private readonly Dictionary<char, Rect> _charPositions = new();
    private readonly Dictionary<char, Rect> _charPositionsNormalised = new();

    public string FontName { get; }
    public float FontSize { get; }
    public FontStyle FontStyle { get; }
    public bool AntiAliased { get; }

    /// <summary>
    /// Name in the format FontName,FontSize,(int)FontStyle,(int)Antialiased
    /// </summary>
    public string LongName { get; }
    public Bitmap CharSpriteSheet { get; }
    public int WidestChar { get; }
    public int HighestChar { get; }
    public int SpaceWidth { get; }

    private HFont(string fontName, float size, FontStyle fontStyle = DEFAULT_FONTSTYLE, bool antiAliased = DEFAULT_AA, string extraCharsToLoad = "")
    {
        //if the application packaged it, use that version as priority
        Font font = LoadFontCustom(fontName, size, fontStyle);

        //if not, see if it's already installed on the user's machine
        if (font == null)
            font = LoadFontPreinstalled(fontName, size, fontStyle);

        //if not, use the default font
        if (font == null)
        {
            Log.Warning($"Font ({fontName}) not found. Reverting to {DEFAULT_FONT}.");
            font = LoadFontPreinstalled(DEFAULT_FONT, DEFAULT_SIZE, DEFAULT_FONTSTYLE);
        }

        //if even this doesn't work, time to crash the program!
        if (font == null)
            throw new ArgumentException($"Neither requested font ({fontName}), nor default font ({DEFAULT_FONT}) was available on the machine. Program terminating.");

        FontName = font.Name;
        FontSize = font.Size;
        FontStyle = (FontStyle)font.Style;
        AntiAliased = antiAliased;
        LongName = string.Format("{0},{1},{2},{3}", FontName, FontSize, (int)FontStyle, AntiAliased ? 1 : 0);

        (CharSpriteSheet, WidestChar, HighestChar) = GenerateCharSpritesheetAndPositions(font, DEFAULT_CHARS_TO_LOAD + string.Concat((extraCharsToLoad ?? "").Where(c => !DEFAULT_CHARS_TO_LOAD.Contains(c))));
        SpaceWidth = (int)_charPositions[' '].W;

        SaveFont();

        font.Dispose();
    }

    private HFont(FontSave fs, Bitmap fontBitmap)
    {
        FontName = fs.Name;
        FontSize = fs.Size;
        FontStyle = fs.FontStyle;
        AntiAliased = fs.AntiAliased;
        LongName = string.Format("{0},{1},{2},{3}", FontName, FontSize, (int)FontStyle, AntiAliased ? 1 : 0);
        CharSpriteSheet = fontBitmap;
        _charPositions = fs.CharPositions;
        _charPositionsNormalised = fs.CharPositionsNormalised;
        WidestChar = fs.WidestChar;
        HighestChar = fs.HighestChar;
        SpaceWidth = fs.SpaceWidth;
    }

    private Bitmap GenerateCharacterBitmap(char c, Font font, bool antiAliased)
    {
        Bitmap image = new((int)font.Size * 2, (int)font.Size * 2, PixelFormat.Format32bppArgb);
        var g = System.Drawing.Graphics.FromImage(image);
        g.TextRenderingHint = antiAliased ? TextRenderingHint.AntiAlias : TextRenderingHint.AntiAliasGridFit;
        g.SmoothingMode = antiAliased ? SmoothingMode.AntiAlias : SmoothingMode.Default;

        var stringFormat = new StringFormat(StringFormat.GenericTypographic);

        g.DrawString(c.ToString(), font, new SolidBrush(Color.White), 0, 0, stringFormat);

        Rect r = OpenGL.NonZeroAlphaRegion(image);

        if (r.W == 0 || r.H == 0)
        {
            if (c != ' ')
                Log.Warning($"Character '{c}' is alleged to have size (W:{r.W},H:{r.H}) in font {FontName}, size {FontSize}. Reverting to trying character 't'.");

            g.DrawString("t", font, new SolidBrush(Color.White), 0, 0, stringFormat);

            r = OpenGL.NonZeroAlphaRegion(image);

            if (r.W == 0 || r.H == 0)
                throw new InvalidOperationException($"HFont.cs/GenerateCharacterBitmap: Font {FontName}, Size {FontSize}, character 't' is giving size (W:{r.W},H:{r.H})");
        }

        image = new Bitmap((int)r.X + (int)r.W, (int)r.Y + (int)r.H, PixelFormat.Format32bppArgb); //include alpha at left/top of image so positioning is preserved
        g = System.Drawing.Graphics.FromImage(image);
        g.TextRenderingHint = antiAliased ? TextRenderingHint.AntiAlias : TextRenderingHint.AntiAliasGridFit;
        g.SmoothingMode = antiAliased ? SmoothingMode.AntiAlias : SmoothingMode.Default;
        g.DrawString(c.ToString(), font, new SolidBrush(Color.White), 0, 0, stringFormat);

        return image;
    }

    private (Bitmap characterSS, int widestChar, int highestChar) GenerateCharSpritesheetAndPositions(Font font, string charsToLoad)
    {
        Bitmap characterSS;
        int widestChar = 0;
        int highestChar = 0;

        var characterBMPs = new List<Bitmap>();
        //find the biggest character and create the spritesheet based on this size
        foreach (char c in charsToLoad)
        {
            Bitmap b = GenerateCharacterBitmap(c, font, AntiAliased);
            if (b.Width > widestChar)
                widestChar = b.Width;
            if (b.Height > highestChar)
                highestChar = b.Height;
            characterBMPs.Add(b);
        }

        int spriteSheetWidth = BITMAP_CHARS_PER_ROW * widestChar;
        int spriteSheetHeight = (int)Math.Ceiling((float)characterBMPs.Count / BITMAP_CHARS_PER_ROW) * highestChar;

        characterSS = new Bitmap(spriteSheetWidth, spriteSheetHeight, PixelFormat.Format32bppArgb);
        var g = System.Drawing.Graphics.FromImage(characterSS);
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.TextRenderingHint = AntiAliased ? TextRenderingHint.AntiAlias : TextRenderingHint.AntiAliasGridFit;

        int j = 0, posX, posY;

        foreach (char c in charsToLoad)
        {
            posX = j % BITMAP_CHARS_PER_ROW * widestChar;
            posY = j / BITMAP_CHARS_PER_ROW * highestChar;

            var charRect = new Rect(posX, posY, characterBMPs[j].Width, highestChar);
            _charPositions.Add(c, charRect);
            //charRect = charRect.ScaleAround(spriteSheetWidth, spriteSheetHeight, 0, 0);
            var charRectNormalised = new Rect(charRect.X / spriteSheetWidth, charRect.Y / spriteSheetHeight, charRect.W / spriteSheetWidth, charRect.H / spriteSheetHeight);
            _charPositionsNormalised.Add(c, charRectNormalised);

            using (var ia = new ImageAttributes())
            {
                ia.SetWrapMode(WrapMode.TileFlipXY);
                g.DrawImage(characterBMPs[j], posX, posY);
            }

            j++;
        }

        foreach (var b in characterBMPs)
            b.Dispose();

        return (characterSS, widestChar, highestChar);
    }

    private void SaveFont()
    {
        if (!Directory.Exists(DEFAULT_FONT_FOLDER))
            Directory.CreateDirectory(DEFAULT_FONT_FOLDER);

        OpenGL.SaveBitmapToPNGFile(CharSpriteSheet, DEFAULT_FONT_FOLDER + LongName + ".png");

        var fs = new FontSave
        {
            Name = FontName,
            Size = FontSize,
            FontStyle = FontStyle,
            AntiAliased = AntiAliased,
            WidestChar = WidestChar,
            HighestChar = HighestChar,
            SpaceWidth = SpaceWidth,
            CharPositions = _charPositions,
            CharPositionsNormalised = _charPositionsNormalised,
        };
        Files.WriteJsonFile(DEFAULT_FONT_FOLDER + LongName + ".details", fs);
    }

    /// <summary>
    /// Position on the underlying bitmap of the character in pixels
    /// </summary>
    public Rect BitmapPosition(char c) => _charPositions[c];
    
    /// <summary>
    /// Position on the underlying bitmap of the character in range (0,1), for use with OpenGL textures
    /// </summary>
    public Rect BitmapPositionNormalised(char c) => _charPositionsNormalised[c];
    
    public Point MeasureString(char c) => new(_charPositions[c].W, HighestChar);

    public Point MeasureString(string s)
    {
        int length = 0;

        foreach (char c in s)
            length += (int)_charPositions[c].W;

        return new Point(length, HighestChar);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~HFont()
    {
        Dispose(false);
    }

    private void Dispose(bool manual)
    {
        if (!_disposed)
        {
            if (manual)
            {
                lock (_syncRoot)
                {
                    CharSpriteSheet.Dispose();
                }
            }
            else
            {
                Log.Warning($"{GetType().FullName} did not dispose correctly, did you forget to call Dispose()?");
            }
            _disposed = true;
        }
    }
    

    public override string ToString() => "HFont: " + LongName;
    
}