namespace BearsEngine;

public class InputBoxValueChangedEventArgs<T> : EventArgs
{
    public InputBoxValueChangedEventArgs(T newValue)
    {
        NewValue = newValue;
    }

    public T NewValue { get; }
}