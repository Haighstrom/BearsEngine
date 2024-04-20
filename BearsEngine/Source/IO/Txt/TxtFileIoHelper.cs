using System.IO;

namespace BearsEngine.Source.Tools.IO.TXT;

internal class TxtFileIoHelper : ITxtFileIoHelper
{
    public string ReadTextFile(string path)
    {
        return File.ReadAllText(path);
    }

    public string[] ReadTextFileAsLines(string path)
    {
        return File.ReadAllLines(path);
    }

    public void WriteTextFile(string path, string text)
    {
        File.WriteAllText(path, text);
    }

    public void WriteTextFile(string path, IEnumerable<string> lines)
    {
        File.WriteAllLines(path, lines);
    }

    public void AppendTextFile(string path, string text)
    {
        File.AppendAllText(path, text);
    }

    public void AppendTextFile(string path, IEnumerable<string> lines)
    {
        using StreamWriter sw = File.AppendText(path);

        foreach (string line in lines)
        {
            sw.WriteLine(line);
        }
    }
}
