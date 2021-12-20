using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaighFramework;

namespace BearsEngine
{
    public interface IScene : IUpdatable, IRenderable
    {
        void Start();
        void End();
        void OnResize(object sender, SizeEventArgs e);
    }
}