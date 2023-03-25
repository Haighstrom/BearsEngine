using System.IO;

namespace BearsEngine.Source.Tools.IO.TXT;

internal interface ITextFileIoHelper
{
    void AppendText(string filePath, string textToAppend);

    void SaveTXT(string filename, string contents);

    void SaveTXT(string filename, IEnumerable<string> lines);

    string[] LoadTXTAsArray(string filename);

    string LoadTXTAsString(string filename);
}
