using System.IO;

namespace BearsEngine.Source.Tools.IO.XML;

internal interface IXmlFileIoHelper
{
    void SaveXML<M>(string fileName, M fileToSave);

    M LoadXML<M>(string fileName) where M : struct;
}