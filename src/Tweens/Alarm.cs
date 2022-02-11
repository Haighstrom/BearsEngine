namespace BearsEngine.Tweens
{
    /// <summary>
    /// Simple alarm, used for timed events etc
    /// </summary>
    public class Alarm : Tween
    {
        public Alarm(float alarmTime, Action actionOnComplete = null, PersistType persistence = PersistType.OneShot)
            : base(alarmTime, persistence, actionOnComplete, null)
        {
        }

        public void Reset(float duration)
        {
            TotalDuration = duration;
            Start();
        }

        public float Remaining => TotalDuration - Elapsed;
    }
}