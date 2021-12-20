using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine;
using BearsEngine.Worlds;

namespace BearsEngine.Tasks
{
    public class TaskController: AddableBase, IUpdatable
    {
        #region Fields
        private ITask _currentTask;
        #endregion

        #region Constructors
        public TaskController(Func<ITask> getNextTask = null) : this(null, getNextTask) { }

        public TaskController(ITask firstTask, Func<ITask> getNextTask = null)
        {
            CurrentTask = firstTask;
            GetNextTask = getNextTask;
        }
        #endregion

        #region IUpdateable
        public virtual bool Active { get; set; } = true;

        #region Update
        public virtual void Update(double elapsed)
        {
            if (Parent == null)
                return;

            if (CurrentTask != null && CurrentTask.Active)
            {
                CurrentTask.Update(elapsed);

                if (CurrentTask.IsComplete)
                {
                    CurrentTask.End();
                    CurrentTask = CurrentTask.NextTask;
                }
            }

            if (CurrentTask == null && GetNextTask != null)
                CurrentTask = GetNextTask();
        }
        #endregion
        #endregion

        public Func<ITask> GetNextTask { get; set; }

        #region CurrentTask
        public ITask CurrentTask
        {
            get => _currentTask;
            set
            {
                //logic for interrupt and whatnot?
                _currentTask = value;
            }
        }
        #endregion
    }
}