using BearsEngine.Source.Tools.IO.FileDirectory;
using System.IO;

namespace BearsEngine;

internal interface IBMPHelper
{
    bool FileExists();

    DirectoryInfo CreateDirectory(string directoryPath);

    bool DirectoryExists(string directoryPath);

    void DeleteFile(string filePath);

    void DeleteDirectory(string directoryPath);

    string GetDirectoryFromFilePath(string filePath);

    List<string> GetDirectories(string path, bool includeSubDirectories);

    List<string> GetDirectories(string path, bool includeSubDirectories, string searchPattern);

    void CopyFiles(string fromDir, string toDir, CopyOptions options);

    void AppendText(string filePath, string textToAppend);

    void SaveTXT(string filename, string contents);

    void SaveTXT(string filename, IEnumerable<string> lines);

    string[] LoadTXTAsArray(string filename);

    string LoadTXTAsString(string filename);

    void SaveCSV<T>(string filename, T[,] data);

    T[,] LoadCSV<T>(string filename) where T : IConvertible;

    System.Drawing.Bitmap LoadBMP(string filePath);

    void SaveXML<M>(string fileName, M fileToSave);

    M LoadXML<M>(string fileName) where M : struct;

    string SerialiseToJSON<M>(M @object, bool indent = true);

    void SaveJSON<M>(string filename, M @object, bool indent = true);

    M? DeserialiseFromJSON<M>(string json);

    M LoadJSON<M>(string filename);

    List<M> LoadJSONFromMultilineTxt<M>(string filename);
}