using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.UI;

public class DropdownList<T> : Entity, IDropdownList<T>
{
    private class DropdownOption : Button
    {
        private readonly IDropdownList<T> _parent;
        private readonly int _listIndex;

        public DropdownOption(IDropdownList<T> parent, Rect position, Colour bgColour, HFont font, Colour textColour, string name, int listIndex)
            : base(0, position, bgColour)
        {
            _parent = parent;
            _listIndex = listIndex;

            Add(new TextGraphic(font, textColour, position.Zeroed, name)
            {
                HAlignment = HAlignment.Left,
                VAlignment = VAlignment.Centred,
            });
        }

        protected override void OnLeftReleased()
        {
            base.OnLeftReleased();

            _parent.SetValue(_listIndex);
        }
    }

    private readonly List<DropdownOption> _dropdownOptions = new();
    private readonly List<(string Name, T Value)> _listValues = new();
    private int _currentSelection = -1;
    private readonly TextGraphic _currentSelectionText;
    private bool _isOpen = false;
    private float _optionHeight;

    public DropdownList(float layer, Rect boxPosition, float optionHeight, Colour bgColour, HFont font, Colour textColour)
        : base(layer, boxPosition, bgColour)
    {
        _optionHeight = optionHeight;
        Add(_currentSelectionText = new TextGraphic(font, textColour, boxPosition.Zeroed, "")
        {
            HAlignment = HAlignment.Left,
            VAlignment = VAlignment.Centred,
        });
    }

    public int OptionsCount => _listValues.Count;

    public T CurrentValue => _listValues[_currentSelection].Value;

    public event EventHandler<DropdownSelectionChangedEventArgs<T>>? SelectionChanged;

    public void AddOption(string text, T value)
    {
        _listValues.Add((text, value));
    }

    public void SetValue(int value)
    {
        if (_currentSelection != value)
        {
            _currentSelection = value;
            _currentSelectionText.Text = _listValues[_currentSelection].Name;

            SelectionChanged?.Invoke(this, new DropdownSelectionChangedEventArgs<T>(_listValues[_currentSelection].Value));
        }

        if (_isOpen)
        {
            CloseList();
        }
    }

    public void OpenList()
    {
        for (int i = 0; i < _listValues.Count; i++)
        {
            var d = new DropdownOption(this, new Rect(0, H + i * _optionHeight, W, _optionHeight), Colour.AntiqueWhite, _currentSelectionText.Font, _currentSelectionText.Colour, _listValues[i].Name, i);
            Add(d);
            _dropdownOptions.Add(d);
        }
        _isOpen = true;
    }

    public void CloseList()
    {
        foreach (var d in _dropdownOptions)
            d.Remove();
        _dropdownOptions.Clear();
        _isOpen = false;
    }

    protected override void OnLeftPressed()
    {
        base.OnLeftPressed();

        if (!_isOpen)
        {
            OpenList();
        }
    }
}