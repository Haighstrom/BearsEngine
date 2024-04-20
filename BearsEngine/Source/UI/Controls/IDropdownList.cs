namespace BearsEngine.UI;

public interface IDropdownList<T> : IAddable
{
    T CurrentValue { get; }
    int OptionsCount { get; }

    event EventHandler<DropdownSelectionChangedEventArgs<T>>? SelectionChanged;

    void AddOption(string text, T value);
    void CloseList();
    void OpenList();
    void SetValue(int value);
}