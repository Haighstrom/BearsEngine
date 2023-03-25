namespace BearsEngine.Source.Tools.IO.JSON;

internal class JSONSerialisationHelper : IJsonFileIoHelper
{
    public M? DeserialiseFromJSON<M>(string json)
    {
        throw new NotImplementedException();
    }

    public M LoadJSON<M>(string filename)
    {
        throw new NotImplementedException();
    }

    public List<M> LoadJSONFromMultilineTxt<M>(string filename)
    {
        throw new NotImplementedException();
    }

    public void SaveJSON<M>(string filename, M @object, bool indent = true)
    {
        throw new NotImplementedException();
    }

    public string SerialiseToJSON<M>(M @object, bool indent = true)
    {
        throw new NotImplementedException();
    }
}
