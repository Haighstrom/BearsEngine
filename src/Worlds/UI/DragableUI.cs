using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;

namespace BearsEngine.Worlds
{
    public class DragableUI : Entity
    {
        private int _dragStartX, _dragStartY;

        public DragableUI(int layer, IRect<float> pos, Colour colour)
            : base(layer, pos, colour)
        {
        }
        public DragableUI(int layer, IRect<float> pos, string gfx)
            : base(layer, pos, gfx)
        {
        }
        public DragableUI(int layer, IRect<float> pos, IGraphic gfx)
            : base(layer, pos, gfx)
        {
        }

        public bool Dragable { get; set; } = true;

        public bool Dragging { get; private set; } = false;

        protected virtual IRect<float> DragGrabArea => this;

        #region Update
        public override void Update(double elapsed)
        {
            base.Update(elapsed);

            if (!Visible)
                return;

            if (Dragable && HI.MouseLeftPressed && DragGrabArea.Contains(Parent.GetLocalPosition(HI.MouseWindowP)))
            {
                Dragging = true;
                OnStartedDragging();
                _dragStartX = HI.MouseWindowX - (int)X;
                _dragStartY = HI.MouseWindowY - (int)Y;
            }

            if (Dragging && (HI.MouseLeftUp || !Dragable))
            {
                Dragging = false;
                OnStoppedDragging();
            }

            if (Dragging)
            {
                X = HI.MouseWindowX - _dragStartX;
                Y = HI.MouseWindowY - _dragStartY;
            }
        }
        #endregion

        #region Events
        protected virtual void OnStartedDragging()
        {
            StartedDragging(this, EventArgs.Empty);
        }
        public event EventHandler StartedDragging = delegate { };
        protected virtual void OnStoppedDragging()
        {
            StoppedDragging(this, EventArgs.Empty);
        }
        public event EventHandler StoppedDragging = delegate { };
        #endregion
    }
}