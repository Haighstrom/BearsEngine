using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using HaighFramework.OpenGL4;
using Point = HaighFramework.Point;
using BearsEngine.Graphics;

namespace BearsEngine.Worlds.Graphics.Text
{
    internal class LC_Word : ILineComponent
    {
        public LC_Word(string text, HFont font, Colour colour, float extraCharSpacing, float scaleX, float scaleY, bool underline, bool strikethrough)
        {
            if (text.IsNullOrEmpty())
                throw new HException("LCWord/ctr: Tried to create an empty word.");

            Text = text;
            Font = font;
            Colour = colour;
            Length = scaleX * (font.MeasureString(text).X + extraCharSpacing * (text.Length - 1));
            Height = scaleY * font.HighestChar;
            IsUnderlined = underline;
            IsStruckthrough = strikethrough;
        }

        public float Length { get; }
        public float Height { get; }
        public HFont Font { get; }
        public string Text { get; }
        public Colour Colour { get; }
        public bool IsUnderlined { get; }
        public bool IsStruckthrough { get; }
    }
}