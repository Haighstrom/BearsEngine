using BearsEngine.Input;
using BearsEngine.UI;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.Worlds.UI.Controls;

public class TextInputBox : Entity, IActivatable
{
    private enum Mode { Unfocussed, Selecting, Editing }

    #region Consts
    private const float DEFAULT_CURSOR_FLASH_TIME = 0.4f;
    #endregion

    #region Fields
    private readonly HText _textGraphic;
    private readonly Polygon _selection;
    private readonly Line _cursor;
    private Mode _mode = Mode.Unfocussed;
    private string _text = "";
    private string _resetValue;
    private int _firstCharDisplayed = 0;
    private int _selectionStart, _cursorPosition;
    private float _cursorFlashTimer;
    #endregion

    #region Constructors
    public TextInputBox(UITheme theme, Colour bg, int layer, Rect r, string initialValue = "")
        : base(layer, r, bg)
    {
        HV.Window.CharEntered += OnCharPressed;
        HV.Window.KeyDown += OnKeyDown;

        if (initialValue == null)
            initialValue = "";

        Add(_textGraphic = new HText(theme, r.Zeroed, initialValue) { Multiline = false, UseCommandTags = false });

        _text = initialValue;

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
    #endregion

    #region IActivatable
    #region Activate
    public void Activate()
    {
        Active = true;
        _textGraphic.Visible = true;
    }
    #endregion

    #region Deactivate
    public void Deactivate()
    {
        Active = false;
        _textGraphic.Visible = false;
    }
    #endregion
    #endregion

    #region Properties
    public float CursorFlashTime { get; set; } = DEFAULT_CURSOR_FLASH_TIME;

    #region Value
    public string Value
    {
        get => _text;
        set
        {
            _text = value;
            SetTextPositions();
            ValueChanged?.Invoke(this, new ValueEventArgs<string>(_text));
        }
    }
    #endregion

    #region MouseXAsTextIndex
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
    #endregion
    #endregion

    #region Methods
    #region ShowCursor
    private void ShowCursor()
    {
        _cursorFlashTimer = CursorFlashTime;
        _cursor.Visible = true;
    }
    #endregion

    #region SetTextPositions
    private void SetTextPositions()
    {
        _cursorPosition = _selectionStart = _text.Length;
        _selection.Visible = false;

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
    #endregion

    #region SetSelectionGraphic
    private void SetSelectionGraphic()
    {
        var left = _textGraphic.MeasureString(_firstCharDisplayed, HF.Maths.Max(0, _selectionStart - _firstCharDisplayed)).X;
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
    #endregion

    #region ConfirmEdit
    private void ConfirmEdit()
    {
        _mode = Mode.Unfocussed;
        _cursor.Visible = false;
        _selection.Visible = false;

        if (_resetValue != Value)
        {
            if (ValueChanged != null)
                ValueChanged(this, new ValueEventArgs<string>(Value));
            if (ValueChangedByEditing != null)
                ValueChangedByEditing(this, new ValueEventArgs<string>(Value));
        }
    }
    #endregion

    #region CancelEdit
    private void CancelEdit()
    {
        _mode = Mode.Unfocussed;
        _cursor.Visible = false;
        _selection.Visible = false;

        _textGraphic.Text = _text = _resetValue;
    }
    #endregion

    #region Update
    public override void Update(double elapsed)
    {
        base.Update(elapsed);

        switch (_mode)
        {
            #region Mode.Unfocussed
            case Mode.Unfocussed:
                if (HI.MouseLeftPressed && MouseIntersecting)
                {
                    _resetValue = _text;
                    _selection.Visible = true;
                    _cursorPosition = _selectionStart = MouseXAsTextIndex;
                    SetSelectionGraphic();
                    _mode = Mode.Selecting;
                }
                break;
            #endregion

            #region Mode.Selecting
            case Mode.Selecting:
                _cursorPosition = MouseXAsTextIndex;
                SetSelectionGraphic();
                if (HI.MouseLeftReleased)
                {
                    ShowCursor();
                    _cursor.OffsetX = _textGraphic.MeasureString(_firstCharDisplayed, MouseXAsTextIndex - _firstCharDisplayed).X;
                    if (_cursorPosition == _selectionStart)
                        _selection.Visible = false;
                    _mode = Mode.Editing;
                }
                break;
            #endregion

            #region Mode.Editing
            case Mode.Editing:
                if ((_cursorFlashTimer -= (float)elapsed) < 0)
                {
                    _cursor.Visible = !_cursor.Visible;
                    _cursorFlashTimer += CursorFlashTime;
                }

                if (HI.MouseLeftDoubleClicked)
                {
                    _selectionStart = 0;
                    _cursorPosition = _text.Length;
                    SetTextPositions();
                    SetSelectionGraphic();
                    _selection.Visible = true;
                }
                else if (HI.MouseLeftPressed && MouseIntersecting)
                {
                    _cursor.Visible = false;

                    _mode = Mode.Selecting;
                    _cursorPosition = _selectionStart = MouseXAsTextIndex;
                    SetSelectionGraphic();
                    _selection.Visible = true;
                    return;
                }
                else if (HI.MouseLeftPressed && !MouseIntersecting)
                    ConfirmEdit();
                break;
            #endregion

            default:
                throw new System.ComponentModel.InvalidEnumArgumentException();
        }
    }
    #endregion

    #region OnCharPressed
    private void OnCharPressed(object? sender, KeyboardCharEventArgs e)
    {
        if (_mode != Mode.Editing)
            return;

        //delete selected items
        if (_selection.Visible)
        {
            int a = HF.Maths.Min(_selectionStart, _cursorPosition);
            int b = HF.Maths.Max(_selectionStart, _cursorPosition);

            _text = _text.Substring(0, a) + _text.Substring(b);
            _cursorPosition = a;
            _selection.Visible = false;
        }

        _text = _text.Substring(0, _cursorPosition) + e.Key + _text.Substring(_cursorPosition);
        _cursorPosition++;

        SetTextPositions();
    }
    #endregion

    #region OnKeyDown
    private void OnKeyDown(object? sender, KeyboardKeyEventArgs e)
    {
        if (_mode != Mode.Editing)
            return;

        switch (e.Key)
        {
            #region ESC
            case Key.ESC:
                CancelEdit();
                break;
            #endregion

            #region Enter/KeypadEnter
            case Key.Enter:
            case Key.KeypadEnter:
                ConfirmEdit();
                break;
            #endregion

            #region Backspace / Delete
            case Key.Backspace:
            case Key.Delete:
                if (_selection.Visible) //delete selected items
                {
                    if (_selectionStart < _cursorPosition)
                        _text = _text.Substring(0, _selectionStart) + _text.Substring(_cursorPosition);
                    else
                        _text = _text.Substring(0, _cursorPosition) + _text.Substring(_selectionStart);

                    _cursorPosition = HF.Maths.Min(_selectionStart, _cursorPosition);
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
            #endregion

            #region Home / Up
            case Key.Home:
            case Key.Up:
                _selection.Visible = false;
                _cursorPosition = 0;
                SetTextPositions();
                break;
            #endregion

            #region End / Down
            case Key.End:
            case Key.Down:
                _cursorPosition = _text.Length;
                SetTextPositions();
                break;
            #endregion

            #region Left
            case Key.Left:
                if (HI.KeyDown(Key.LeftShift) || HI.KeyDown(Key.RightShift))
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
            #endregion

            #region Right
            case Key.Right:
                if (HI.KeyDown(Key.LeftShift) || HI.KeyDown(Key.RightShift))
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
            #endregion

            default:
                break;
        }
    }
    #endregion
    #endregion

    #region Events
    public event EventHandler<ValueEventArgs<string>> ValueChanged;
    public event EventHandler<ValueEventArgs<string>> ValueChangedByEditing;
    #endregion
}
