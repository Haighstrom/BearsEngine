namespace BearsEngine.Input;

public class KeyboardCharEventArgs : EventArgs
{
    public KeyboardCharEventArgs(char key)
    {
        Key = key;
    }

    public char Key { get; set; }
}

public class KeyboardKeyEventArgs : EventArgs
{
    public Key Key { get; internal set; }

    public KeyboardKeyEventArgs(Key key)
    {
        Key = key;
    }        

    public override string ToString()
    {
        return string.Format("KeyboardEventArgs: Key = {0}", Key);
    }
}
