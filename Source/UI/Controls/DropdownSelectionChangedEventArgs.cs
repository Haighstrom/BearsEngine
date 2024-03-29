﻿namespace BearsEngine.UI;

public class DropdownSelectionChangedEventArgs<T> : EventArgs
{
    public DropdownSelectionChangedEventArgs(T newSelection)
    {
        NewSelection = newSelection;
    }

    public T NewSelection { get; }
}