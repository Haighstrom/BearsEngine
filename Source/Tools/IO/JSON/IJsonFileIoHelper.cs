namespace BearsEngine.Source.Tools.IO.JSON;

internal interface IJsonFileIoHelper
{
    string SerialiseToJSON<T>(T @object);

    T? DeserialiseFromJSON<T>(string json);

    T? ReadJsonFile<T>(string filename);

    void WriteJsonFile<T>(string filename, T data);
}