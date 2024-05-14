using BearsEngine.UI;
using BearsEngine.Worlds.Graphics.Text;
using BearsEngine.Input;
using BearsEngine.Window;

namespace BearsEngine.Worlds.UI.Controls;

public class NumberInputBox<T> : Entity, IActivatable
    where T : struct,
      IComparable,
      IComparable<T>,
      IConvertible,
      IEquatable<T>,
      IFormattable
{
    private enum Mode { Unfocussed, Selecting, Editing }

    private const float DEFAULT_CURSOR_FLASH_TIME = 0.4f;
    

    private readonly TextGraphic _textGraphic;
    private readonly Polygon _selection;
    private readonly Line _cursor;
    private readonly IMouse _mouse;
    private readonly IKeyboard _keyboard;
    private Mode _mode = Mode.Unfocussed;
    private string _text = "";
    private T _resetValue;
    private int _firstCharDisplayed = 0;
    private int _selectionStart, _cursorPosition;
    private float _cursorFlashTimer;
    

    public NumberInputBox(IWindow window, UITheme theme, Colour bg, float layer, Rect r, T initialValue = default)
        : base(layer, r, bg)
    {
        _mouse = Mouse.Instance;
        _keyboard = Keyboard.Instance;
        window.CharEntered += OnCharPressed;
        window.KeyDown += OnKeyDown;

        Add(_textGraphic = new TextGraphic(theme, r.Zeroed, initialValue.ToString()) { Multiline = false, UseCommandTags = false });

        _text = initialValue.ToString();
        float textHeight = _textGraphic.Font.HighestChar;
        Add(_cursor = new Line(Colour.Black, 2, true, new Point(0, r.H / 2 - textHeight / 2), new Point(0, r.H / 2 + textHeight / 2)));
        _cursorFlashTimer = CursorFlashTime;
        _cursor.Visible = false;
        Add(_selection = new Polygon(Colour.LightGray)
        {
            Visible = false,
            Layer = 1
        });
        SetTextPositions();
    }
    

    public void Activate()
    {
        Active = true;
        _textGraphic.Visible = true;
    }
    

    public void Deactivate()
    {
        Active = false;
        _textGraphic.Visible = false;
    }
    
    public float CursorFlashTime { get; set; } = DEFAULT_CURSOR_FLASH_TIME;

    public T? MinValue { get; set; } = null;

    public T? MaxValue { get; set; } = null;

    public T Value
    {
        get => _text.ParseTo<T>();
        set
        {
            T t = value;

            if (MinValue != null && t.CompareTo(MinValue) < 0)
                t = (T)MinValue;
            if (MaxValue != null && t.CompareTo(MaxValue) > 0)
                t = (T)MaxValue;

            _text = t.ToString();
            _cursorPosition = _selectionStart = _text.Length;
            _selection.Visible = false;
            SetTextPositions();

            ValueChanged?.Invoke(this, new InputBoxValueChangedEventArgs<T>(t));
        }
    }
    
    private int MouseXAsTextIndex
    {
        get
        {
            int pos = _firstCharDisplayed;
            float posX = 0;

            while (true)
            {
                if (posX >= W || pos >= _text.Length)
                    break;

                if (posX + _textGraphic.MeasureString(pos).X >= LocalMousePosition.X)
                    break;
                else
                {
                    posX += _textGraphic.MeasureString(pos).X;
                    pos++;
                }
            }

            return pos;
        }
    }
    
    private void ShowCursor()
    {
        _cursorFlashTimer = CursorFlashTime;
        _cursor.Visible = true;
    }
    
    private void SetTextPositions()
    {
        int firstChar = 0, lastChar = 0;

        //work back from selection end to the current first char displayed, then if still space, start working towards end of string
        firstChar = lastChar = _cursorPosition;

        while (true)
        {
            if (firstChar > _firstCharDisplayed && _textGraphic.MeasureString(_text.Substring(firstChar - 1, lastChar - firstChar + 1)).X <= W)
                firstChar--;
            else if (lastChar < _text.Length && _textGraphic.MeasureString(_text.Substring(firstChar, lastChar - firstChar + 1)).X <= W)
                lastChar++;
            else if (firstChar > 0 && _textGraphic.MeasureString(_text.Substring(firstChar - 1, lastChar - firstChar + 1)).X <= W)
                firstChar--;
            else
                break;
        }

        _firstCharDisplayed = firstChar;
        _textGraphic.Text = _text.Substring(firstChar, lastChar - firstChar);
        _cursor.OffsetX = _textGraphic.MeasureString(0, _cursorPosition - _firstCharDisplayed).X;
    }
    
    private void SetSelectionGraphic()
    {
        var left = _textGraphic.MeasureString(_firstCharDisplayed, Maths.Max(0, _selectionStart - _firstCharDisplayed)).X;
        var right = _textGraphic.MeasureString(_firstCharDisplayed, _cursorPosition - _firstCharDisplayed).X;
        if (right < left) //does this actually matter?
        {
            var temp = left;
            left = right;
            right = temp;
        }
        var top = H / 2 - _textGraphic.Font.HighestChar / 2;
        var bottom = H / 2 + _textGraphic.Font.HighestChar / 2;

        _selection.Points = new List<Point>()
        {
            new Point(left, top),
            new Point(right, top),
            new Point(right, bottom),
            new Point(left, bottom)
        };
    }
    
    private void ConfirmEdit()
    {
        _mode = Mode.Unfocussed;
        _cursor.Visible = false;
        _selection.Visible = false;

        try
        {
            Value = Value;

            if (_resetValue.CompareTo(Value) != 0)
                ValueChangedByEditing?.Invoke(this, new InputBoxValueChangedEventArgs<T>(Value));
        }
        catch
        {
            Value = _resetValue;
        }
    }
    
    private void CancelEdit()
    {
        _mode = Mode.Unfocussed;
        _cursor.Visible = false;
        _selection.Visible = false;

        if (Value.CompareTo(_resetValue) != 0)
        {
            _text = _resetValue.ToString();
            _cursorPosition = _selectionStart = _text.Length;
            _selection.Visible = false;
            SetTextPositions();
        }
    }
    
    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        switch (_mode)
        {
            case Mode.Unfocussed:
                if (_mouse.LeftPressed && MouseIntersecting)
                {
                    _resetValue = _text.ParseTo<T>();
                    _selection.Visible = true;
                    _cursorPosition = _selectionStart = MouseXAsTextIndex;
                    SetSelectionGraphic();
                    _mode = Mode.Selecting;
                }
                break;
            
            case Mode.Selecting:
                _cursorPosition = MouseXAsTextIndex;
                SetSelectionGraphic();
                if (_mouse.LeftReleased)
                {
                    ShowCursor();
                    _cursor.OffsetX = _textGraphic.MeasureString(_firstCharDisplayed, MouseXAsTextIndex - _firstCharDisplayed).X;
                    if (_cursorPosition == _selectionStart)
                        _selection.Visible = false;
                    _mode = Mode.Editing;
                }
                break;
            
            case Mode.Editing:
                if ((_cursorFlashTimer -= (float)elapsed) < 0)
                {
                    _cursor.Visible = !_cursor.Visible;
                    _cursorFlashTimer += CursorFlashTime;
                }

                if (_mouse.LeftDoubleClicked)
                {
                    _selectionStart = 0;
                    _cursorPosition = _text.Length;
                    SetTextPositions();
                    SetSelectionGraphic();
                    _selection.Visible = true;
                }
                else if (_mouse.LeftPressed && MouseIntersecting)
                {
                    _cursor.Visible = false;

                    _mode = Mode.Selecting;
                    _cursorPosition = _selectionStart = MouseXAsTextIndex;
                    SetSelectionGraphic();
                    _selection.Visible = true;
                    return;
                }
                else if (_mouse.LeftPressed && !MouseIntersecting)
                    ConfirmEdit();
                break;
            

            default:
                throw new System.ComponentModel.InvalidEnumArgumentException();
        }
    }
    
    private void OnCharPressed(object? sender, BearsEngine.Window.KeyboardCharEventArgs e)
    {
        if (_mode != Mode.Editing)
            return;

        //delete selected items
        if (_selection.Visible)
        {
            int a = Maths.Min(_selectionStart, _cursorPosition);
            int b = Maths.Max(_selectionStart, _cursorPosition);

            _text = _text.Substring(0, a) + _text.Substring(b);
            _cursorPosition = a;
            _selection.Visible = false;
        }

        _text = _text.Substring(0, _cursorPosition) + e.Char + _text.Substring(_cursorPosition);
        _cursorPosition++;

        SetTextPositions();
    }
    
    private void OnKeyDown(object? sender, BearsEngine.Window.KeyboardKeyEventArgs e)
    {
        if (_mode != Mode.Editing)
            return;

        switch (e.Key)
        {
            case Key.ESC:
                CancelEdit();
                break;

            case Key.Enter:
            case Key.KeypadEnter:
                ConfirmEdit();
                break;
            
            case Key.Backspace:
            case Key.Delete:
                if (_selection.Visible) //delete selected items
                {
                    if (_selectionStart < _cursorPosition)
                        _text = _text.Substring(0, _selectionStart) + _text.Substring(_cursorPosition);
                    else
                        _text = _text.Substring(0, _cursorPosition) + _text.Substring(_selectionStart);

                    _cursorPosition = Maths.Min(_selectionStart, _cursorPosition);
                    _selection.Visible = false;
                }
                else if (e.Key == Key.Backspace && _cursorPosition > 0)
                {
                    _text = _text.Remove(_cursorPosition - 1, 1);
                    _cursorPosition--;
                }
                else if (e.Key == Key.Delete && _cursorPosition < _text.Length)
                    _text = _text.Remove(_cursorPosition, 1);

                SetTextPositions();
                break;
            
            case Key.Home:
            case Key.Up:
                _selection.Visible = false;
                _cursorPosition = 0;
                SetTextPositions();
                break;
            
            case Key.End:
            case Key.Down:
                _cursorPosition = _text.Length;
                SetTextPositions();
                break;
            
            case Key.Left:
                if (_keyboard.KeyDown(Key.LeftShift) || _keyboard.KeyDown(Key.RightShift))
                {
                    if (!_selection.Visible && _cursorPosition > 0)
                    {
                        _selection.Visible = true;
                        _selectionStart = _cursorPosition;
                    }
                }
                else if (_selection.Visible)
                {
                    _selection.Visible = false;
                    if (_selectionStart < _cursorPosition)
                        _cursorPosition = _selectionStart;
                }

                if (_cursorPosition > 0)
                {
                    _cursorPosition--;

                    ShowCursor();
                    SetTextPositions();

                    if (_selection.Visible)
                    {
                        if (_cursorPosition == _selectionStart)
                            _selection.Visible = false;
                        else
                            SetSelectionGraphic();
                    }
                }
                break;
            
            case Key.Right:
                if (_keyboard.KeyDown(Key.LeftShift) || _keyboard.KeyDown(Key.RightShift))
                {
                    if (!_selection.Visible && _cursorPosition < _text.Length)
                    {
                        _selection.Visible = true;
                        _selectionStart = _cursorPosition;
                    }
                }
                else if (_selection.Visible)
                {
                    _selection.Visible = false;
                    if (_cursorPosition < _selectionStart)
                        _cursorPosition = _selectionStart;
                }

                if (_cursorPosition < _text.Length)
                {
                    _cursorPosition++;

                    ShowCursor();
                    SetTextPositions();

                    if (_selection.Visible)
                    {
                        if (_cursorPosition == _selectionStart)
                            _selection.Visible = false;
                        else
                            SetSelectionGraphic();
                    }
                }
                break;
            

            default:
                break;
        }
    }
    
    

    public event EventHandler<InputBoxValueChangedEventArgs<T>> ValueChanged;

    public event EventHandler<InputBoxValueChangedEventArgs<T>> ValueChangedByEditing;
}
