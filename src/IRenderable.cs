using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using HaighFramework.Input;
using BearsEngine;
using BearsEngine.Worlds;

namespace BearsEngine
{
    public interface IRenderable
    {
        bool Visible { get; set; }
        void Render(ref Matrix4 projection, ref Matrix4 modelView);
    }
}