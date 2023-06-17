namespace BearsEngine.Source.UI;

public class ProgressBarTimer : ProgressBar
{
    private float _timeToFill;
    private float _remaining;

    public ProgressBarTimer(float layer, string graphicsPath, Rect position, float timeToFill)
        : base(layer, graphicsPath, position)
    {
        _remaining = _timeToFill = timeToFill;
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        _remaining = Math.Max(_remaining - elapsed, 0);
        
        AmountFilled = 1 - _remaining / _timeToFill;
    }
}
