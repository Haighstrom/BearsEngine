using System;
using System.Collections.Generic;
using HaighFramework;
using HaighFramework.Input;
using BearsEngine;
using BearsEngine.Worlds;

namespace BearsEngine.Worlds
{
    public interface IGraphic : IAddable, IRenderableOnLayer
    {
        Colour Colour { get; set; }
        byte Alpha { get; set; }
        bool ResizeWithParent { get; set; }
        void Resize(float xScale, float yScale);
        bool IsOnScreen { get; }
    }
}