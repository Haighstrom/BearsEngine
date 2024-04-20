using BearsEngine.Worlds.Graphics.Text;
using HaighFramework.Input;

namespace BearsEngine.SystemTests.Source.InputDemo;

internal class MouseActivityList : Entity
{
    private const int MessageListSize = 10;

    private readonly string[] _activityMessages;
    private readonly TextGraphic _activityText;

    public MouseActivityList()
        : base(10, 90, 300, 220, 160, Colour.White)
    {
        Add(_activityText = new TextGraphic(HFont.Load("Helvetica", 8), Colour.Black, Size));

        _activityMessages = new string[MessageListSize];
        for (var i = 0; i < _activityMessages.Length; i++)
            _activityMessages[i] = "";
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

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        if (Mouse.LeftPressed)
            AddNewMessage("Left Pressed.");
        if (Mouse.RightPressed)
            AddNewMessage("Right Pressed.");
        if (Mouse.Pressed(MouseButton.Middle))
            AddNewMessage("Wheel Pressed.");
        if (Mouse.Pressed(MouseButton.Mouse4))
            AddNewMessage("Mouse 4 Pressed.");
        if (Mouse.Pressed(MouseButton.Mouse5))
            AddNewMessage("Mouse 5 Pressed.");

        if (Mouse.LeftReleased)
            AddNewMessage("Left Released.");
        if (Mouse.RightReleased)
            AddNewMessage("Right Released.");
        if (Mouse.Released(MouseButton.Middle))
            AddNewMessage("Wheel Released.");
        if (Mouse.Released(MouseButton.Mouse4))
            AddNewMessage("Mouse 4 Released.");
        if (Mouse.Released(MouseButton.Mouse5))
            AddNewMessage("Mouse 5 Released.");

        if (Mouse.WheelDelta != 0)
            AddNewMessage("Mouse Wheel Scrolled");
    }
}