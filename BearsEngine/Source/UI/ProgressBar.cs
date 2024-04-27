using BearsEngine.Input;

namespace BearsEngine.Source.UI;

public class ProgressBar : Entity
{
    private readonly FillableBar _fillableBar;

    public ProgressBar(IMouse mouse, float layer, string graphicsPath, Rect position)
        : base(mouse, layer, position)
    {
        _fillableBar = new FillableBar(graphicsPath, position.Size, 0);
        Add(_fillableBar);
    }

    /// <summary>
    /// [0,1]
    /// </summary>
    public virtual float AmountFilled
    {
        get => _fillableBar.AmountFilled;
        set
        {
            Ensure.IsInRange(value, 0, 1);

            if (value != AmountFilled)
            {
                _fillableBar.AmountFilled = value;

                if (value == 1)
                {
                    BarFilled?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public event EventHandler? BarFilled;
}
