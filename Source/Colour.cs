using System.Runtime.InteropServices;

namespace BearsEngine;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct Colour : IEquatable<Colour>
{
    #region Static
    #region System Colours
    /// <summary>
    /// (255, 255, 255, 0)
    /// </summary>
    public static Colour Transparent => new(255, 255, 255, 0);
    /// <summary>
    /// (240, 248, 255, 255)
    /// </summary>
    public static Colour AliceBlue => new(240, 248, 255, 255);
    /// <summary>
    /// (250, 235, 215, 255)
    /// </summary>
    public static Colour AntiqueWhite => new(250, 235, 215, 255);
    /// <summary>
    /// (0, 255, 255, 255)
    /// </summary>
    public static Colour Aqua => new(0, 255, 255, 255);
    /// <summary>
    /// (127, 255, 212, 255)
    /// </summary>
    public static Colour Aquamarine => new(127, 255, 212, 255);
    /// <summary>
    /// (240, 255, 255, 255)
    /// </summary>
    public static Colour Azure => new(240, 255, 255, 255);
    /// <summary>
    /// (245, 245, 220, 255)
    /// </summary>
    public static Colour Beige => new(245, 245, 220, 255);
    /// <summary>
    /// (255, 228, 196, 255)
    /// </summary>
    public static Colour Bisque => new(255, 228, 196, 255);
    /// <summary>
    /// (0, 0, 0, 255)
    /// </summary>
    public static Colour Black => new(0, 0, 0, 255);
    /// <summary>
    /// (255, 235, 205, 255)
    /// </summary>
    public static Colour BlanchedAlmond => new(255, 235, 205, 255);
    /// <summary>
    /// (0, 0, 255, 255)
    /// </summary>
    public static Colour Blue => new(0, 0, 255, 255);
    /// <summary>
    /// (138, 43, 226, 255)
    /// </summary>
    public static Colour BlueViolet => new(138, 43, 226, 255);
    /// <summary>
    /// (165, 42, 42, 255)
    /// </summary>
    public static Colour Brown => new(165, 42, 42, 255);
    /// <summary>
    /// (222, 184, 135, 255)
    /// </summary>
    public static Colour BurlyWood => new(222, 184, 135, 255);
    /// <summary>
    /// (95, 158, 160, 255)
    /// </summary>
    public static Colour CadetBlue => new(95, 158, 160, 255);
    /// <summary>
    /// (127, 255, 0, 255)
    /// </summary>
    public static Colour Chartreuse => new(127, 255, 0, 255);
    /// <summary>
    /// (210, 105, 30, 255)
    /// </summary>
    public static Colour Chocolate => new(210, 105, 30, 255);
    /// <summary>
    /// (255, 127, 80, 255)
    /// </summary>
    public static Colour Coral => new(255, 127, 80, 255);
    /// <summary>
    /// (100, 149, 237, 255)
    /// </summary>
    public static Colour CornflowerBlue => new(100, 149, 237, 255);
    /// <summary>
    /// (255, 248, 220, 255)
    /// </summary>
    public static Colour Cornsilk => new(255, 248, 220, 255);
    /// <summary>
    /// (220, 20, 60, 255)
    /// </summary>
    public static Colour Crimson => new(220, 20, 60, 255);
    /// <summary>
    /// (0, 255, 255, 255)
    /// </summary>
    public static Colour Cyan => new(0, 255, 255, 255);
    /// <summary>
    /// (0, 0, 139, 255)
    /// </summary>
    public static Colour DarkBlue => new(0, 0, 139, 255);
    /// <summary>
    /// (0, 139, 139, 255)
    /// </summary>
    public static Colour DarkCyan => new(0, 139, 139, 255);
    /// <summary>
    /// (184, 134, 11, 255)
    /// </summary>
    public static Colour DarkGoldenrod => new(184, 134, 11, 255);
    /// <summary>
    /// (169, 169, 169, 255)
    /// </summary>
    public static Colour DarkGray => new(169, 169, 169, 255);
    /// <summary>
    /// (0, 100, 0, 255)
    /// </summary>
    public static Colour DarkGreen => new(0, 100, 0, 255);
    /// <summary>
    /// (189, 183, 107, 255)
    /// </summary>
    public static Colour DarkKhaki => new(189, 183, 107, 255);
    /// <summary>
    /// (139, 0, 139, 255)
    /// </summary>
    public static Colour DarkMagenta => new(139, 0, 139, 255);
    /// <summary>
    /// (85, 107, 47, 255)
    /// </summary>
    public static Colour DarkOliveGreen => new(85, 107, 47, 255);
    /// <summary>
    /// (255, 140, 0, 255)
    /// </summary>
    public static Colour DarkOrange => new(255, 140, 0, 255);
    /// <summary>
    /// (153, 50, 204, 255)
    /// </summary>
    public static Colour DarkOrchid => new(153, 50, 204, 255);
    /// <summary>
    /// (139, 0, 0, 255)
    /// </summary>
    public static Colour DarkRed => new(139, 0, 0, 255);
    /// <summary>
    /// (233, 150, 122, 255)
    /// </summary>
    public static Colour DarkSalmon => new(233, 150, 122, 255);
    /// <summary>
    /// (143, 188, 139, 255)
    /// </summary>
    public static Colour DarkSeaGreen => new(143, 188, 139, 255);
    /// <summary>
    /// (72, 61, 139, 255)
    /// </summary>
    public static Colour DarkSlateBlue => new(72, 61, 139, 255);
    /// <summary>
    /// (47, 79, 79, 255)
    /// </summary>
    public static Colour DarkSlateGray => new(47, 79, 79, 255);
    /// <summary>
    /// (0, 206, 209, 255)
    /// </summary>
    public static Colour DarkTurquoise => new(0, 206, 209, 255);
    /// <summary>
    /// (148, 0, 211, 255)
    /// </summary>
    public static Colour DarkViolet => new(148, 0, 211, 255);
    /// <summary>
    /// (255, 20, 147, 255)
    /// </summary>
    public static Colour DeepPink => new(255, 20, 147, 255);
    /// <summary>
    /// (0, 191, 255, 255)
    /// </summary>
    public static Colour DeepSkyBlue => new(0, 191, 255, 255);
    /// <summary>
    /// (105, 105, 105, 255)
    /// </summary>
    public static Colour DimGray => new(105, 105, 105, 255);
    /// <summary>
    /// (30, 144, 255, 255)
    /// </summary>
    public static Colour DodgerBlue => new(30, 144, 255, 255); 
    /// <summary>
    /// (178, 34, 34, 255)
    /// </summary>
    public static Colour Firebrick => new(178, 34, 34, 255); 
    /// <summary>
    /// (255, 250, 240, 255)
    /// </summary>
    public static Colour FloralWhite => new(255, 250, 240, 255); 
    /// <summary>
    /// (34, 139, 34, 255)
    /// </summary>
    public static Colour ForestGreen => new(34, 139, 34, 255); 
    /// <summary>
    /// (255, 0, 255, 255)
    /// </summary>
    public static Colour Fuchsia => new(255, 0, 255, 255); 
    /// <summary>
    /// (220, 220, 220, 255)
    /// </summary>
    public static Colour Gainsboro => new(220, 220, 220, 255); 
    /// <summary>
    /// (248, 248, 255, 255)
    /// </summary>
    public static Colour GhostWhite => new(248, 248, 255, 255); 
    /// <summary>
    /// (255, 215, 0, 255)
    /// </summary>
    public static Colour Gold => new(255, 215, 0, 255); 
    /// <summary>
    /// (218, 165, 32, 255)
    /// </summary>
    public static Colour Goldenrod => new(218, 165, 32, 255); 
    /// <summary>
    /// (128, 128, 128, 255)
    /// </summary>
    public static Colour Gray => new(128, 128, 128, 255); 
    /// <summary>
    /// (0, 128, 0, 255)
    /// </summary>
    public static Colour Green => new(0, 128, 0, 255); 
    /// <summary>
    /// (173, 255, 47, 255)
    /// </summary>
    public static Colour GreenYellow => new(173, 255, 47, 255); 
    /// <summary>
    /// (240, 255, 240, 255)
    /// </summary>
    public static Colour Honeydew => new(240, 255, 240, 255); 
    /// <summary>
    /// (255, 105, 180, 255)
    /// </summary>
    public static Colour HotPink => new(255, 105, 180, 255); 
    /// <summary>
    /// (205, 92, 92, 255)
    /// </summary>
    public static Colour IndianRed => new(205, 92, 92, 255); 
    /// <summary>
    /// (75, 0, 130, 255)
    /// </summary>
    public static Colour Indigo => new(75, 0, 130, 255); 
    /// <summary>
    /// (255, 255, 240, 255)
    /// </summary>
    public static Colour Ivory => new(255, 255, 240, 255); 
    /// <summary>
    /// (240, 230, 140, 255)
    /// </summary>
    public static Colour Khaki => new(240, 230, 140, 255); 
    /// <summary>
    /// (230, 230, 250, 255)
    /// </summary>
    public static Colour Lavender => new(230, 230, 250, 255); 
    /// <summary>
    /// (255, 240, 245, 255)
    /// </summary>
    public static Colour LavenderBlush => new(255, 240, 245, 255); 
    /// <summary>
    /// (124, 252, 0, 255)
    /// </summary>
    public static Colour LawnGreen => new(124, 252, 0, 255); 
    /// <summary>
    /// (255, 250, 205, 255)
    /// </summary>
    public static Colour LemonChiffon => new(255, 250, 205, 255); 
    /// <summary>
    /// (173, 216, 230, 255)
    /// </summary>
    public static Colour LightBlue => new(173, 216, 230, 255); 
    /// <summary>
    /// (240, 128, 128, 255)
    /// </summary>
    public static Colour LightCoral => new(240, 128, 128, 255); 
    /// <summary>
    /// (224, 255, 255, 255)
    /// </summary>
    public static Colour LightCyan => new(224, 255, 255, 255); 
    /// <summary>
    /// (250, 250, 210, 255)
    /// </summary>
    public static Colour LightGoldenrodYellow => new(250, 250, 210, 255); 
    /// <summary>
    /// (144, 238, 144, 255)
    /// </summary>
    public static Colour LightGreen => new(144, 238, 144, 255); 
    /// <summary>
    /// (211, 211, 211, 255)
    /// </summary>
    public static Colour LightGray => new(211, 211, 211, 255); 
    /// <summary>
    /// (255, 182, 193, 255)
    /// </summary>
    public static Colour LightPink => new(255, 182, 193, 255); 
    /// <summary>
    /// (255, 160, 122, 255)
    /// </summary>
    public static Colour LightSalmon => new(255, 160, 122, 255); 
    /// <summary>
    /// (32, 178, 170, 255)
    /// </summary>
    public static Colour LightSeaGreen => new(32, 178, 170, 255); 
    /// <summary>
    /// (135, 206, 250, 255)
    /// </summary>
    public static Colour LightSkyBlue => new(135, 206, 250, 255); 
    /// <summary>
    /// (119, 136, 153, 255)
    /// </summary>
    public static Colour LightSlateGray => new(119, 136, 153, 255); 
    /// <summary>
    /// (176, 196, 222, 255)
    /// </summary>
    public static Colour LightSteelBlue => new(176, 196, 222, 255); 
    /// <summary>
    /// (255, 255, 224, 255)
    /// </summary>
    public static Colour LightYellow => new(255, 255, 224, 255); 
    /// <summary>
    /// (0, 255, 0, 255)
    /// </summary>
    public static Colour Lime => new(0, 255, 0, 255); 
    /// <summary>
    /// (50, 205, 50, 255)
    /// </summary>
    public static Colour LimeGreen => new(50, 205, 50, 255); 
    /// <summary>
    /// (250, 240, 230, 255)
    /// </summary>
    public static Colour Linen => new(250, 240, 230, 255); 
    /// <summary>
    /// (255, 0, 255, 255)
    /// </summary>
    public static Colour Magenta => new(255, 0, 255, 255); 
    /// <summary>
    /// (128, 0, 0, 255)
    /// </summary>
    public static Colour Maroon => new(128, 0, 0, 255); 
    /// <summary>
    /// (102, 205, 170, 255)
    /// </summary>
    public static Colour MediumAquamarine => new(102, 205, 170, 255); 
    /// <summary>
    /// (0, 0, 205, 255)
    /// </summary>
    public static Colour MediumBlue => new(0, 0, 205, 255); 
    /// <summary>
    /// (186, 85, 211, 255)
    /// </summary>
    public static Colour MediumOrchid => new(186, 85, 211, 255); 
    /// <summary>
    /// (147, 112, 219, 255)
    /// </summary>
    public static Colour MediumPurple => new(147, 112, 219, 255); 
    /// <summary>
    /// (60, 179, 113, 255)
    /// </summary>
    public static Colour MediumSeaGreen => new(60, 179, 113, 255); 
    /// <summary>
    /// (123, 104, 238, 255)
    /// </summary>
    public static Colour MediumSlateBlue => new(123, 104, 238, 255); 
    /// <summary>
    /// (0, 250, 154, 255)
    /// </summary>
    public static Colour MediumSpringGreen => new(0, 250, 154, 255); 
    /// <summary>
    /// (72, 209, 204, 255)
    /// </summary>
    public static Colour MediumTurquoise => new(72, 209, 204, 255); 
    /// <summary>
    /// (199, 21, 133, 255)
    /// </summary>
    public static Colour MediumVioletRed => new(199, 21, 133, 255); 
    /// <summary>
    /// (25, 25, 112, 255)
    /// </summary>
    public static Colour MidnightBlue => new(25, 25, 112, 255); 
    /// <summary>
    /// (245, 255, 250, 255)
    /// </summary>
    public static Colour MintCream => new(245, 255, 250, 255); 
    /// <summary>
    /// (255, 228, 225, 255)
    /// </summary>
    public static Colour MistyRose => new(255, 228, 225, 255); 
    /// <summary>
    /// (255, 228, 181, 255)
    /// </summary>
    public static Colour Moccasin => new(255, 228, 181, 255); 
    /// <summary>
    /// (255, 222, 173, 255)
    /// </summary>
    public static Colour NavajoWhite => new(255, 222, 173, 255); 
    /// <summary>
    /// (0, 0, 128, 255)
    /// </summary>
    public static Colour Navy => new(0, 0, 128, 255); 
    /// <summary>
    /// (253, 245, 230, 255)
    /// </summary>
    public static Colour OldLace => new(253, 245, 230, 255); 
    /// <summary>
    /// (128, 128, 0, 255)
    /// </summary>
    public static Colour Olive => new(128, 128, 0, 255); 
    /// <summary>
    /// (107, 142, 35, 255)
    /// </summary>
    public static Colour OliveDrab => new(107, 142, 35, 255); 
    /// <summary>
    /// (255, 165, 0, 255)
    /// </summary>
    public static Colour Orange => new(255, 165, 0, 255); 
    /// <summary>
    /// (255, 69, 0, 255)
    /// </summary>
    public static Colour OrangeRed => new(255, 69, 0, 255); 
    /// <summary>
    /// (218, 112, 214, 255)
    /// </summary>
    public static Colour Orchid => new(218, 112, 214, 255); 
    /// <summary>
    /// (238, 232, 170, 255)
    /// </summary>
    public static Colour PaleGoldenrod => new(238, 232, 170, 255); 
    /// <summary>
    /// (152, 251, 152, 255)
    /// </summary>
    public static Colour PaleGreen => new(152, 251, 152, 255); 
    /// <summary>
    /// (175, 238, 238, 255)
    /// </summary>
    public static Colour PaleTurquoise => new(175, 238, 238, 255); 
    /// <summary>
    /// (219, 112, 147, 255)
    /// </summary>
    public static Colour PaleVioletRed => new(219, 112, 147, 255); 
    /// <summary>
    /// (255, 239, 213, 255)
    /// </summary>
    public static Colour PapayaWhip => new(255, 239, 213, 255); 
    /// <summary>
    /// (255, 218, 185, 255)
    /// </summary>
    public static Colour PeachPuff => new(255, 218, 185, 255); 
    /// <summary>
    /// (205, 133, 63, 255)
    /// </summary>
    public static Colour Peru => new(205, 133, 63, 255); 
    /// <summary>
    /// (255, 192, 203, 255)
    /// </summary>
    public static Colour Pink => new(255, 192, 203, 255); 
    /// <summary>
    /// (221, 160, 221, 255)
    /// </summary>
    public static Colour Plum => new(221, 160, 221, 255); 
    /// <summary>
    /// (176, 224, 230, 255)
    /// </summary>
    public static Colour PowderBlue => new(176, 224, 230, 255); 
    /// <summary>
    /// (128, 0, 128, 255)
    /// </summary>
    public static Colour Purple => new(128, 0, 128, 255); 
    /// <summary>
    /// (255, 0, 0, 255)
    /// </summary>
    public static Colour Red => new(255, 0, 0, 255); 
    /// <summary>
    /// (188, 143, 143, 255)
    /// </summary>
    public static Colour RosyBrown => new(188, 143, 143, 255); 
    /// <summary>
    /// (65, 105, 225, 255)
    /// </summary>
    public static Colour RoyalBlue => new(65, 105, 225, 255); 
    /// <summary>
    /// (139, 69, 19, 255)
    /// </summary>
    public static Colour SaddleBrown => new(139, 69, 19, 255); 
    /// <summary>
    /// (250, 128, 114, 255)
    /// </summary>
    public static Colour Salmon => new(250, 128, 114, 255); 
    /// <summary>
    /// (244, 164, 96, 255)
    /// </summary>
    public static Colour SandyBrown => new(244, 164, 96, 255); 
    /// <summary>
    /// (46, 139, 87, 255)
    /// </summary>
    public static Colour SeaGreen => new(46, 139, 87, 255); 
    /// <summary>
    /// (255, 245, 238, 255)
    /// </summary>
    public static Colour SeaShell => new(255, 245, 238, 255); 
    /// <summary>
    /// (160, 82, 45, 255)
    /// </summary>
    public static Colour Sienna => new(160, 82, 45, 255); 
    /// <summary>
    /// (192, 192, 192, 255)
    /// </summary>
    public static Colour Silver => new(192, 192, 192, 255); 
    /// <summary>
    /// (135, 206, 235, 255)
    /// </summary>
    public static Colour SkyBlue => new(135, 206, 235, 255); 
    /// <summary>
    /// (106, 90, 205, 255)
    /// </summary>
    public static Colour SlateBlue => new(106, 90, 205, 255); 
    /// <summary>
    /// (112, 128, 144, 255)
    /// </summary>
    public static Colour SlateGray => new(112, 128, 144, 255); 
    /// <summary>
    /// (255, 250, 250, 255)
    /// </summary>
    public static Colour Snow => new(255, 250, 250, 255); 
    /// <summary>
    /// (0, 255, 127, 255)
    /// </summary>
    public static Colour SpringGreen => new(0, 255, 127, 255); 
    /// <summary>
    /// (70, 130, 180, 255)
    /// </summary>
    public static Colour SteelBlue => new(70, 130, 180, 255); 
    /// <summary>
    /// (210, 180, 140, 255)
    /// </summary>
    public static Colour Tan => new(210, 180, 140, 255); 
    /// <summary>
    /// (0, 128, 128, 255)
    /// </summary>
    public static Colour Teal => new(0, 128, 128, 255); 
    /// <summary>
    /// (216, 191, 216, 255)
    /// </summary>
    public static Colour Thistle => new(216, 191, 216, 255); 
    /// <summary>
    /// (255, 99, 71, 255)
    /// </summary>
    public static Colour Tomato => new(255, 99, 71, 255); 
    /// <summary>
    /// (64, 224, 208, 255)
    /// </summary>
    public static Colour Turquoise => new(64, 224, 208, 255); 
    /// <summary>
    /// (238, 130, 238, 255)
    /// </summary>
    public static Colour Violet => new(238, 130, 238, 255); 
    /// <summary>
    /// (245, 222, 179, 255)
    /// </summary>
    public static Colour Wheat => new(245, 222, 179, 255); 
    /// <summary>
    /// (255, 255, 255, 255)
    /// </summary>
    public static Colour White => new(255, 255, 255, 255); 
    /// <summary>
    /// (245, 245, 245, 255)
    /// </summary>
    public static Colour WhiteSmoke => new(245, 245, 245, 255); 
    /// <summary>
    /// (255, 255, 0, 255)
    /// </summary>
    public static Colour Yellow => new(255, 255, 0, 255); 
    /// <summary>
    /// (154, 205, 50, 255)
    /// </summary>
    public static Colour YellowGreen => new(154, 205, 50, 255);
    #endregion
    #endregion

    #region Fields
    /// <summary>
    /// The Red colour component [0,255]
    /// </summary>
    public byte R;
    /// <summary>
    /// The Green colour component [0,255]
    /// </summary>
    public byte G;
    /// <summary>
    /// The Blue colour component [0,255]
    /// </summary>
    public byte B;
    /// <summary>
    /// The Alpha component [0,255]
    /// </summary>
    public byte A;
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a Colour from float components. The components should be between 0 and 1.
    /// </summary>
    public Colour(float r, float g, float b, float a = 1)
    {
        if (r < 0 || r > 1 || g < 0 || g > 1 || b < 0 || b > 1 || a < 0 || a > 1)
            throw new HException("Arguments should be between 0 & 1. You requested: R:{0}, G:{1}, B:{2}, A:{3}", r, g, b, a);

        R = (byte)(r * 255);
        G = (byte)(g * 255);
        B = (byte)(b * 255);
        A = (byte)(a * 255);
    }

    /// <summary>
    /// Builds a colour from a string in the format "r,g,b,a" with byte values (integers in [0,255])
    /// </summary>
    /// <param name="colourComponents"></param>
    public Colour(string colourComponents)
        : this((byte)Convert.ChangeType(colourComponents.Split(',')[0], typeof(byte)),
              (byte)Convert.ChangeType(colourComponents.Split(',')[1], typeof(byte)),
              (byte)Convert.ChangeType(colourComponents.Split(',')[2], typeof(byte)),
              (byte)Convert.ChangeType(colourComponents.Split(',')[3], typeof(byte)))
    {
    }

    /// <summary>
    /// Creates a Colour from components. The components should be between 0 and 255.
    /// </summary>
    public Colour(byte r, byte g, byte b, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Colour(Colour colour, byte alpha)
    {
        R = colour.R;
        G = colour.G;
        B = colour.B;
        A = alpha;
    }

    public Colour(Colour colour, float alpha)
    {
        if (alpha < 0 || alpha > 1)
            throw new HException("Arguments should be between 0 & 1. You requested: A:{0}", alpha);

        R = colour.R;
        G = colour.G;
        B = colour.B;
        A = (byte)(alpha * 255);
    }
    #endregion

    #region IEquatable
    #region Equals
    public bool Equals(Colour other)
    {
        return R == other.R &&
            G == other.G &&
            B == other.B &&
            A == other.A;
    }
    #endregion
    #endregion

    #region Methods
    /// <summary>
    /// Returns the same colour but with the specified alpha
    /// </summary>
    public Colour WithAlpha(byte alpha) => new(R, G, B, alpha);
    #endregion

    #region Overloads / Overrides
    #region +
    public static Colour operator +(Colour l, Colour r)
    {
        return new Colour
        {
            R = (byte)Math.Min(l.R + r.R, byte.MaxValue),
            G = (byte)Math.Min(l.G + r.G, byte.MaxValue),
            B = (byte)Math.Min(l.B + r.B, byte.MaxValue),
            A = (byte)Math.Min(l.A + r.A, byte.MaxValue),
        };
    }
    #endregion

    #region -
    public static Colour operator -(Colour l, Colour r)
    {
        return new Colour
        {
            R = (byte)Math.Max(l.R - r.R, 0),
            G = (byte)Math.Max(l.G - r.G, 0),
            B = (byte)Math.Max(l.B - r.B, 0),
            A = (byte)Math.Max(l.A - r.A, 0),
        };
    }
    #endregion

    #region *
    public static Colour operator *(Colour l, Colour r)
    {
        return new Colour
        {
            R = (byte)Math.Min(l.R * r.R / 255, byte.MaxValue),
            G = (byte)Math.Min(l.G * r.G / 255, byte.MaxValue),
            B = (byte)Math.Min(l.B * r.B / 255, byte.MaxValue),
            A = (byte)Math.Min(l.A * r.A / 255, byte.MaxValue),
        };
    }

    public static Colour operator *(Colour l, float r) => r * l;

    public static Colour operator *(float l, Colour r)
    {
        return new Colour
        {
            R = (byte)Math.Min(Math.Max(l * r.R, 0), byte.MaxValue),
            G = (byte)Math.Min(Math.Max(l * r.G, 0), byte.MaxValue),
            B = (byte)Math.Min(Math.Max(l * r.B, 0), byte.MaxValue),
            A = (byte)Math.Min(Math.Max(l * r.A, 0), byte.MaxValue),
        };
    }
    #endregion

    #region ==
    public static bool operator ==(Colour left, Colour right)
    {
        return left.Equals(right);
    }
    #endregion

    #region !=
    public static bool operator !=(Colour left, Colour right)
    {
        return !left.Equals(right);
    }
    #endregion

    #region Equals
    public override bool Equals(object obj)
    {
        if (!(obj is Colour))
            return false;

        return Equals((Colour)obj);
    }
    #endregion

    #region ToString
    public override string ToString()
    {
        return string.Format("(R:{0}, G:{1}, B:{2}, A:{3})", R, G, B, A);
    }
    #endregion

    #region GetHashCode
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion
    #endregion
}
