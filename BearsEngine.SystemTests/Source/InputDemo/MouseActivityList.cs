using BearsEngine.Worlds.Graphics.Text;
using BearsEngine.Input;

namespace BearsEngine.SystemTests.Source.InputDemo;

internal class MouseActivityList : Entity
{
    private const int MessageListSize = 10;

    private readonly string[] _activityMessages;
    private readonly TextGraphic _activityText;
    private readonly IMouse _mouse;

    public MouseActivityList(IMouse mouse)
        : base(mouse, 10, 90, 300, 220, 160, Colour.White)
    {
        _mouse = mouse;

        Add(_activityText = new TextGraphic(HFont.Load("Helvetica", 8), Colour.Black, Size));

        _activityMessages = new string[MessageListSize];
        for (int i = 0; i < _activityMessages.Length; i++)
            _activityMessages[i] = "";
    }

    private void AddNewMessage(string message)
    {
        for (int i = _activityMessages.Length - 1; i > 0; i--)
            _activityMessages[i] = _activityMessages[i - 1];

        _activityMessages[0] = message;

        string newText = "";
        for (int i = 0; i < _activityMessages.Length; i++)
            newText += _activityMessages[i] + "\n";

        _activityText.Text = newText;
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);

        if (_mouse.LeftPressed)
            AddNewMessage("Left Pressed.");
        if (_mouse.RightPressed)
            AddNewMessage("Right Pressed.");
        if (_mouse.Pressed(MouseButton.Middle))
            AddNewMessage("Wheel Pressed.");
        if (_mouse.Pressed(MouseButton.Mouse4))
            AddNewMessage("Mouse 4 Pressed.");
        if (_mouse.Pressed(MouseButton.Mouse5))
            AddNewMessage("Mouse 5 Pressed.");

        if (_mouse.LeftReleased)
            AddNewMessage("Left Released.");
        if (_mouse.RightReleased)
            AddNewMessage("Right Released.");
        if (_mouse.Released(MouseButton.Middle))
            AddNewMessage("Wheel Released.");
        if (_mouse.Released(MouseButton.Mouse4))
            AddNewMessage("Mouse 4 Released.");
        if (_mouse.Released(MouseButton.Mouse5))
            AddNewMessage("Mouse 5 Released.");

        if (_mouse.WheelDelta != 0)
            AddNewMessage("Mouse Wheel Scrolled");
    }
}
