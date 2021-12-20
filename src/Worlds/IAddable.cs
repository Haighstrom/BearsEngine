using System;
using System.Collections.Generic;
using HaighFramework;

namespace BearsEngine.Worlds
{
    public interface IAddable
    {
        IContainer Parent { get; set; }

        void Remove();

        void OnAdded();
        void OnRemoved();

        event EventHandler Added;
        event EventHandler Removed;
    }
}