using BearsEngine.Input;
using BearsEngine.Window;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.InputDemo;

internal class CharActivityList : Entity
{
    private const int CharListSize = 20;

    private readonly List<char> _activityChars = new();
    private readonly TextGraphic _activityText;

    public CharActivityList(IWindow window, IMouse mouse)
        : base(mouse, 10, 400, 50, 150, 35, Colour.White)
    {
        Add(_activityText = new TextGraphic(HFont.Load("Helvetica", 8), Colour.Black, Size));

        window.CharEntered += Window_CharEntered;
    }

    private void Window_CharEntered(object? sender, Window.KeyboardCharEventArgs e)
    {
        AddNewChar(e.Char);
    }

    private void AddNewChar(char newChar)
    {
        if (_activityChars.Count < CharListSize)
            _activityChars.Add(newChar);
        else
        {
            for (var i = 0; i < CharListSize - 1; i++)
                _activityChars[i] = _activityChars[i + 1];

            _activityChars[CharListSize - 1] = newChar;
        }

        _activityText.Text = new string(_activityChars.ToArray());
    }
}
