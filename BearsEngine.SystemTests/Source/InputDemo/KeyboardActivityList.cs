using BearsEngine.Worlds.Graphics.Text;
using BearsEngine.Input;

namespace BearsEngine.SystemTests.Source.InputDemo;

internal class KeyboardActivityList : Entity
{
    private const int MessageListSize = 20;

    private readonly string[] _activityMessages;
    private readonly TextGraphic _activityText;

    public KeyboardActivityList()
        : base(10, 400, 100, 150, 320, Colour.White)
    {
        Add(_activityText = new TextGraphic(HFont.Load("Helvetica", 8), Colour.Black, Size));

        _activityMessages = new string[MessageListSize];
        for (var i = 0; i < _activityMessages.Length; i++)
            _activityMessages[i] = "";

        AppWindow.KeyDown += Window_KeyDown;
        AppWindow.KeyUp += Window_KeyUp;
    }

    private void Window_KeyUp(object? sender, BearsEngine.Window.KeyboardKeyEventArgs e)
    {
        AddNewMessage(e.Key.AsString() + " Released.");
    }

    private void Window_KeyDown(object? sender, BearsEngine.Window.KeyboardKeyEventArgs e)
    {
        AddNewMessage(e.Key.AsString() + " Pressed.");
    }

    private void AddNewMessage(string message)
    {
        for (var i = _activityMessages.Length - 1; i > 0; i--)
            _activityMessages[i] = _activityMessages[i - 1];

        _activityMessages[0] = message;

        var newText = "";
        for (var i = 0; i < _activityMessages.Length; i++)
            newText += _activityMessages[i] + "\n";

        _activityText.Text = newText;
    }
}
