using BearsEngine.Input;
using System.Collections;

namespace BearsEngine.Logging;

/// <summary>
/// Default formatter for translating objects into strings for the purposes of logging
/// </summary>
internal class LoggerMessageFormatter : ILoggerMessageFormatter
{
    private const string NullString = "null";

    private string FormatToStringInternal(IDictionary dict)
    {
        string dictString = "[";

        foreach (DictionaryEntry entry in dict)
            dictString += $"({FormatToLoggingString(entry.Key)},{FormatToLoggingString(entry.Value)}),";

        dictString = dictString[0..^1]; //remove last comma

        dictString += "]";

        return dictString;
    }

    private string FormatToStringInternal(IEnumerable collection)
    {
        string collectionString = "[";

        foreach (object item in collection)
            collectionString += $"{FormatToLoggingString(item)},";

        collectionString = collectionString[0..^1]; //remove last comma

        collectionString += "]";

        return collectionString;
    }

    /// <summary>
    /// Translate an object into a logging string
    /// </summary>
    /// <param name="o">the object to translate</param>
    /// <returns>Returns a loggable string</returns>
    public string FormatToLoggingString(object? o) => o switch
    {
        null => NullString,
        string str => str,
        Key key => KeyFormatter.AsString(key),
        IDictionary dict => FormatToStringInternal(dict),
        IEnumerable enumerable => FormatToStringInternal(enumerable),
        _ => o.ToString() ?? o.GetType().ToString(),
    };
}
