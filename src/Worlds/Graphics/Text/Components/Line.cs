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
using System.Collections;

namespace BearsEngine.Worlds.Graphics.Text
{
    internal class Line
    {
        #region Fields
        private bool _finalised = false;
        private List<ILineComponent> _components = new List<ILineComponent>();
        #endregion

        #region AutoProperties
        public bool EndsWithNewLine { get; private set; }
        #endregion
        
        #region Properties
        public bool IsEmpty => _components.Count == 0;

        public int Spaces => _components.Where(c => c is LC_Space).Count();

        public float Length => _components.Sum(c => c.Length);

        public float LengthExcludingSpaces => _components.Where(c => c is LC_Word).Sum(w => w.Length);

        public float Height => _components.Max(c => c.Height);
        #endregion

        #region Methods
        public void Add(ILineComponent c)
        {
            if (_finalised)
                throw new HException("Line Finalise but trying to add {0} to it", c);

            _components.Add(c);
        }

        public void Finalise(bool lastCharIsNewline)
        {
            if (_finalised)
                throw new HException("Line Finalised Twice");

            EndsWithNewLine = lastCharIsNewline;

            if (_components.Count > 0)
                while (_components.Last() is LC_Space s)
                    _components.Remove(s);

            _finalised = true;
        }

        public IEnumerator<ILineComponent> GetEnumerator() => _components.GetEnumerator();
        #endregion
    }
}