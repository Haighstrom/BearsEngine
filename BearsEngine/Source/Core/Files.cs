using BearsEngine.IO;
using BearsEngine.Source.Tools.IO.FileDirectory;

namespace BearsEngine;

public static class Files
{
    internal static IIoHelper Instance { get; set; } = new IOHelper(IoSettings.Default);

    public static void CopyFile(string sourceFile, string destinationFile) => Instance.CopyFile(sourceFile, destinationFile);

    public static void CopyFiles(string sourceDirectory, string destinationDirectory, CopyOptions options) => Instance.CopyFiles(sourceDirectory, destinationDirectory, options);

    public static bool CreateDirectory(string directoryPath) => Instance.CreateDirectory(directoryPath);

    public static void DeleteDirectory(string directoryPath) => Instance.DeleteDirectory(directoryPath);

    public static void DeleteFile(string filePath) => Instance.DeleteFile(filePath);

    public static M? DeserialiseFromJson<M>(string json) => Instance.DeserialiseFromJSON<M>(json);

    public static bool DirectoryExists(string directoryPath) => Instance.DirectoryExists(directoryPath);

    public static bool FileExists(string path) => Instance.FileExists(path);

    public static List<string> GetDirectories(string path, bool includeSubDirectories) => Instance.GetDirectories(path, includeSubDirectories);

    public static List<string> GetDirectories(string path, bool includeSubDirectories, string searchPattern) => Instance.GetDirectories(path, includeSubDirectories, searchPattern);

    public static string? GetDirectoryFromFilePath(string filePath) => Instance.GetDirectoryFromFilePath(filePath);

    public static T[,] ReadCsvFile<T>(string filename, char separator = ',') where T : IConvertible => Instance.ReadCsvFile<T>(filename, separator);

    public static M ReadJsonFile<M>(string filename) => Instance.ReadJsonFile<M>(filename);

    public static M LoadXML<M>(string fileName) where M : struct => Instance.LoadXML<M>(fileName);

    public static void WriteCsvFile<T>(string filename, T[,] data, char separator = ',') where T : IConvertible => Instance.WriteCsvFile(filename, data, separator);

    public static void WriteJsonFile<M>(string filename, M @object) => Instance.WriteJsonFile(filename, @object);

    public static void SaveXML<M>(string fileName, M fileToSave) => Instance.SaveXML(fileName, fileToSave);

    public static string SerialiseToJSON<M>(M data) => Instance.SerialiseToJSON(data);

    public static string ReadTextFile(string path) => Instance.ReadTextFile(path);

    public static string[] ReadTextFileAsLines(string path) => Instance.ReadTextFileAsLines(path);

    public static void WriteTextFile(string path, string text) => Instance.WriteTextFile(path, text);

    public static void WriteTextFile(string path, IEnumerable<string> lines) => Instance.WriteTextFile(path, lines);

    public static void AppendTextFile(string path, string text) => Instance.AppendTextFile(path, text);

    public static void AppendTextFile(string path, IEnumerable<string> lines) => Instance.AppendTextFile(path, lines);
}
