using System.Collections;

namespace BearsEngine.Tools;

internal class MessageFormatter : IMessageFormatter
{
    private const string NullString = "null";

    private string FormatToStringInternal(IDictionary dict)
    {
        string dictString = "[";

        foreach (DictionaryEntry entry in dict)
            dictString += $"({FormatToString(entry.Key)},{FormatToString(entry.Value)}),";

        dictString = dictString[0..^1]; //remove last comma

        dictString += "]";

        return dictString;
    }

    private string FormatToStringInternal(IEnumerable collection)
    {
        string collectionString = "[";

        foreach (object item in collection)
            collectionString += $"{FormatToString(item)},";

        collectionString = collectionString[0..^1]; //remove last comma

        collectionString += "]";

        return collectionString;
    }

    public string FormatToString(object? o) => o switch
    {
        null => NullString,
        string str => str,
        IDictionary dict => FormatToStringInternal(dict),
        IEnumerable enumerable => FormatToStringInternal(enumerable),
        _ => o.ToString() ?? o.GetType().ToString(),
    };
}