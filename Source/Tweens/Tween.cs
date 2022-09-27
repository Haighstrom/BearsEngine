namespace BearsEngine.Tweens
{
    public delegate float Easer(float t);

    public enum PersistType { OneShot, Persist, Looping }

    public class Tween : AddableBase, IUpdatable
    {
        #region Constructors
        public Tween(float duration, PersistType persistType, Action? actionOnCompleted = null, Easer? easer = null)
        {
            TotalDuration = duration;
            Persistence = persistType;
            ActionOnCompleted = actionOnCompleted;
            Easer = easer;
        }
        #endregion

        #region Properties
        public Action? ActionOnCompleted { get; set; }
        public bool Active { get; set; } = true;
        public Easer? Easer { get; set; }
        public float Elapsed { get; private set; }
        public float PercentComplete => Progress * 100;
        public PersistType Persistence { get; set; }
        public float Progress { get; private set; }
        public float TotalDuration { get; protected set; }
        #endregion

        #region Methods
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
                    throw new Exception($"Enum case {Persistence} not dealt with");
            }

            ActionOnCompleted?.Invoke();

            Completed?.Invoke(this, EventArgs.Empty);
        }
        #endregion

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
        #endregion

        #region Events
        public event EventHandler? Completed;
        #endregion
    }
}
