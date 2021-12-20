using System;
using System.Collections.Generic;
using HaighFramework;

namespace BearsEngine.Worlds.Graphics.Text
{
    internal class LC_NewLine : ILineComponent
    {
        public LC_NewLine(HFont font, float scaleY)
        {
            Height = scaleY * font.HighestChar;
        }

        public float Length => 0;
        public float Height { get; }
        public bool IsUnderlined => false;
        public bool IsStruckthrough => false;
    }
}