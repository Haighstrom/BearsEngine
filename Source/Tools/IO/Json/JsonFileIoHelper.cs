using System.Text.Json;

namespace BearsEngine.Source.Tools.IO.JSON;

internal class JsonFileIoHelper : IJsonFileIoHelper
{
    private readonly JsonSerializerOptions _options;

    public JsonFileIoHelper(JsonSerializerOptions options)
    {
        _options = options;
    }

    public string SerialiseToJSON<T>(T @object)
    {
        return JsonSerializer.Serialize(@object, _options);
    }

    public T? DeserialiseFromJSON<T>(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (JsonException)
        {
            return default;
        }
    }

    public T? ReadJsonFile<T>(string filename)
    {
        try
        {
            var json = System.IO.File.ReadAllText(filename);
            return JsonSerializer.Deserialize<T>(json, _options);
        }
        catch (Exception)
        {
            return default;
        }
    }

    public void WriteJsonFile<T>(string filename, T data)
    {
        var json = JsonSerializer.Serialize(data, _options);
        System.IO.File.WriteAllText(filename, json);
    }
}