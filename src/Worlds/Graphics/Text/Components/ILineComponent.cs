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
    internal interface ILineComponent
    {
        float Length { get; }
        float Height { get; }
        bool IsUnderlined { get; }
        bool IsStruckthrough { get; }
    }
}