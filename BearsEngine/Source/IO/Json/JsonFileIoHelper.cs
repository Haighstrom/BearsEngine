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

    public T DeserialiseFromJSON<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }

    public T ReadJsonFile<T>(string filename)
    {
        Ensure.FileExists(filename);

        var json = System.IO.File.ReadAllText(filename);
        return JsonSerializer.Deserialize<T>(json, _options);
    }

    public void WriteJsonFile<T>(string filename, T data)
    {
        var json = JsonSerializer.Serialize(data, _options);
        System.IO.File.WriteAllText(filename, json);
    }

    public T? TryDeserialiseFromJSON<T>(string json)
    {
        throw new NotImplementedException();
    }

    public T? TryReadJsonFile<T>(string filename)
    {
        throw new NotImplementedException();
    }
}