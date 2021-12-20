using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine;
using BearsEngine.Worlds;

namespace BearsEngine
{
    public interface IAttachable<T>
    {
        void AttachTo(T t);
        void Dettach();
    }
}