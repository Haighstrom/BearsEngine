namespace BearsEngine.Source.Tools.IO.XML;

internal class XMLSerialisationHelper : IXmlFileIoHelper
{
    public M LoadXML<M>(string fileName) where M : struct
    {
        throw new NotImplementedException();
    }

    public void SaveXML<M>(string fileName, M fileToSave)
    {
        throw new NotImplementedException();
    }
}