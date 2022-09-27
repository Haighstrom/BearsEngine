namespace BearsEngine;

public class BoolEventArgs : EventArgs
{
    public bool Value;
    public BoolEventArgs(bool b)
    {
        Value = b;
    }
}