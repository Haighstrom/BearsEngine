using System;
using System.Collections.Generic;
using HaighFramework;
using HaighFramework.Input;
using BearsEngine;
using BearsEngine.Worlds;

namespace BearsEngine.Tweens
{
    public delegate float Easer(float t);

    public enum PersistType { OneShot, Persist, Looping }

    public class Tween : AddableBase, IUpdatable
    {
        #region Constructors
        public Tween(float duration, PersistType persistType, Action actionOnCompleted = null, Easer easer = null)
        {
            TotalDuration = duration;
            Persistence = persistType;
            ActionOnCompleted = actionOnCompleted;
            Easer = easer;
        }
        #endregion

        #region IUpdateable
        public bool Active { get; set; } = true;

        #region Update
        public virtual void Update(double elapsed)
        {
            Elapsed += (float)HV.ElapsedGameTime;
            Progress = Elapsed / TotalDuration;

            if (Elapsed >= TotalDuration)
            {
                Progress = 1;
                OnCompleted();
            }
            else if (Easer != null && Progress > 0)
                Progress = Easer(Progress);
        }
        #endregion
        #endregion

        #region Properties
        public Easer Easer { get; set; }

        public PersistType Persistence { get; set; }

        public float Elapsed { get; private set; }

        public float TotalDuration { get; protected set; }

        public float Progress { get; private set; }

        public float PercentComplete => Progress * 100;

        public Action ActionOnCompleted { get; set; }
        #endregion

        #region Methods
        #region Start
        public virtual void Start()
        {
            Elapsed = 0;
            if (TotalDuration == 0)
                Active = false;
            else
                Active = true;
        }
        #endregion

        #region OnCompleted
        protected virtual void OnCompleted()
        {
            switch (Persistence)
            {
                case PersistType.OneShot:
                    Remove();
                    break;
                case PersistType.Persist:
                    Elapsed = TotalDuration;
                    Active = false;
                    break;
                case PersistType.Looping:
                    Elapsed %= TotalDuration;
                    Progress = Elapsed / TotalDuration;
                    if (Easer != null && Progress > 0 && Progress < 1)
                        Progress = Easer(Progress);
                    break;
                default:
                    throw new HException("Enum case {0} not dealt with in {1}", Persistence, "Tween.OnCompleted");
            }

            ActionOnCompleted?.Invoke();

            Completed?.Invoke(this, EventArgs.Empty);
        }
        #endregion
        #endregion

        #region Events
        public event EventHandler Completed;
        #endregion
    }
}
