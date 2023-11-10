using BearsEngine.Source.UI.Controls;

namespace BearsEngine.UI
{
    public interface IDropdownList<T>
    {
        T CurrentValue { get; }
        int OptionsCount { get; }

        event EventHandler<DropdownSelectionChangedEventArgs<T>>? DropdownSelectionChanged;

        void AddOption(string text, T value);
        void CloseList();
        void OpenList();
        void SetValue(int value);
    }
}