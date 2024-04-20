namespace BearsEngine.Source.Tools.IO.TXT;

internal interface ITxtFileIoHelper
{
    string ReadTextFile(string path);

    string[] ReadTextFileAsLines(string path);

    void WriteTextFile(string path, string text);

    void WriteTextFile(string path, IEnumerable<string> lines);

    void AppendTextFile(string path, string text);

    void AppendTextFile(string path, IEnumerable<string> lines);
}