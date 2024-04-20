using BearsEngine.Source.Tools.IO.JSON;
using System.Text.Json;

namespace BearsEngine.Source.Tools.IO;

/// <summary>
/// A group of settings for instantiating an IoHelper
/// </summary>
public class IoSettings
{
    public static IoSettings Default => new();

    private static JsonSerializerOptions GetDefaultJsonSerializerOptions()
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            IncludeFields = true,
        };
        options.Converters.Add(new TwoDimensionalArrayConverter());
        return options;
    }

    public int RetriesForIoOperations { get; set; } = 5;

    public int MilisecondsBetweenRetriesForIoOperations { get; set; } = 10;

    public JsonSerializerOptions JsonOptions { get; set; } = GetDefaultJsonSerializerOptions();
}