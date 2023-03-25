namespace BearsEngine.Source.Tools.IO.JSON;

internal interface IJsonFileIoHelper
{
    string SerialiseToJSON<M>(M @object, bool indent = true);

    void SaveJSON<M>(string filename, M @object, bool indent = true);

    M? DeserialiseFromJSON<M>(string json);

    M LoadJSON<M>(string filename);

    List<M> LoadJSONFromMultilineTxt<M>(string filename);
}
