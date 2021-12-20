using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using HaighFramework.Input;
using BearsEngine;
using BearsEngine.Worlds;

namespace BearsEngine
{
    public interface IRenderableOnLayer : IRenderable
    {
        int Layer { get; set; }
        event EventHandler<LayerChangedArgs> LayerChanged;
    }
}