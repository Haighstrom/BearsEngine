namespace BearsEngine.UI;

public class CyclingValueButton<T> : Button
    where T : IConvertible, IEquatable<T>
{
    private int _index;
    private readonly List<T> _cycleValues;

    public CyclingValueButton(float layer, Rect position, UITheme theme, List<T> cycleValues)
        : base(layer, position, theme.Button.DefaultColour, theme, "")
    {
        if (cycleValues.Count == 0)
            throw new ArgumentException("CyclingValueButton.ctr: cycleValues contained no values");

        _cycleValues = cycleValues;
        CurrentValue = _cycleValues.First();
    }

    public IReadOnlyList<T> CycleValues => _cycleValues.AsReadOnly();

    public int CurrentIndex
    {
        get => _index;
        set
        {
            if (_index != value)
            {
                if (value >= _cycleValues.Count)
                    throw new Exception($"Tried to set {typeof(CyclingValueButton<T>).Name} ({this}) to an index {value} outside its Cycle Values' ({string.Join(",", _cycleValues)}) size: ({_cycleValues.Count})");

                _index = value;
                CurrentValue = _cycleValues[_index];
            }
        }
    }

    public T CurrentValue
    {
        get => _cycleValues[_index];
        set
        {
            if (!value.Equals(CurrentValue))
            {
                if (!_cycleValues.Contains(value))
                    throw new Exception($"Tried to set {typeof(CyclingValueButton<T>).Name} ({this}) to a value ({value}) not specified in its possible values: ({string.Join(",", _cycleValues)})");

                _index = _cycleValues.IndexOf(value);
                Text = (string)Convert.ChangeType(value, typeof(string));
            }
        }
    }

    protected override void OnLeftClicked()
    {
        base.OnLeftClicked();

        CurrentIndex = (CurrentIndex + 1) % _cycleValues.Count;
    }
}