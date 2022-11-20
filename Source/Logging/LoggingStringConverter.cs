using System.Collections;

namespace BearsEngine.Tools;

internal class LoggingStringConverter : ILoggingStringConverter
{
    private const string NullString = "null";

    private string ConvertIDictionaryToString(IDictionary dict)
    {
        string dictString = "[";

        foreach (DictionaryEntry entry in dict)
            dictString += $"({ConvertToLoggableString(entry.Key)},{ConvertToLoggableString(entry.Value)}),";

        dictString = dictString[0..^1]; //remove last comma

        dictString += "]";

        return dictString;
    }

    private string ConvertIEnumerableToString(IEnumerable collection)
    {
        string collectionString = "[";

        foreach (object item in collection)
            collectionString += $"{ConvertToLoggableString(item)},";

        collectionString = collectionString[0..^1]; //remove last comma

        collectionString += "]";

        return collectionString;
    }

    public string ConvertToLoggableString(object? o) => o switch
    {
        null => NullString,
        string str => str,
        IDictionary dict => ConvertIDictionaryToString(dict),
        IEnumerable enumerable => ConvertIEnumerableToString(enumerable),
        _ => o.ToString() ?? o.GetType().ToString(),
    };
}