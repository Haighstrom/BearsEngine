using BearsEngine.Input;

namespace BearsEngine.Source.UI;

public class ProgressBarTimer : ProgressBar
{
    private float _timeToFill;
    private float _remaining;

    public ProgressBarTimer(IMouse mouse, float layer, string graphicsPath, Rect position, float timeToFill)
        : base(mouse, layer, graphicsPath, position)
    {
        _remaining = _timeToFill = timeToFill;
    }

    public void Restart()
    {
        _remaining = _timeToFill;
    }

    public void Restart(float newTimeToFill)
    {
        _remaining = _timeToFill = newTimeToFill;
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        _remaining = Math.Max(_remaining - elapsed, 0);
        
        AmountFilled = 1 - _remaining / _timeToFill;
    }
}
