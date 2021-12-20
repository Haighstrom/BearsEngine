using System;
using System.Collections.Generic;
using HaighFramework;

namespace BearsEngine.Worlds
{
    public class AddableBase : IAddable
    {
        public IContainer Parent { get; set; }

        public virtual void Remove() => Parent.Remove(this);

        public virtual void OnAdded() => Added?.Invoke(this, EventArgs.Empty);

        public virtual void OnRemoved() => Removed?.Invoke(this, EventArgs.Empty);

        public event EventHandler Added;

        public event EventHandler Removed;
    }
}