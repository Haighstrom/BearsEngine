using BearsEngine.Source.Tools.IO.CSV;
using BearsEngine.Source.Tools.IO.FileDirectory;
using BearsEngine.Source.Tools.IO.JSON;
using BearsEngine.Source.Tools.IO.TXT;
using BearsEngine.Source.Tools.IO.XML;

namespace BearsEngine.Source.Tools.IO;

public class IOHelper : IIoHelper
{
    private readonly IFileDirectoryIoHelper _directoryHelper;
    private readonly ITxtFileIoHelper _txtSerialiser;
    private readonly ICsvFileIoHelper _csvSerialiser;
    private readonly IJsonFileIoHelper _jsonSerialiser;
    private readonly IXmlFileIoHelper _xmlSerialiser;

    public IOHelper(IoSettings settings)
    {
        //todo: cascade more settings
        _directoryHelper = new FileDirectoryIoHelper();
        _txtSerialiser = new TxtFileIoHelper();
        _csvSerialiser = new CsvFileIoHelper();
        _jsonSerialiser = new JsonFileIoHelper(settings.JsonOptions);
        _xmlSerialiser = new XMLSerialisationHelper();
    }

    public void CopyFile(string sourceFile, string destinationFile) => _directoryHelper.CopyFile(sourceFile, destinationFile);

    public void CopyFiles(string sourceDirectory, string destinationDirectory, CopyOptions options) => _directoryHelper.CopyFiles(sourceDirectory, destinationDirectory, options);

    public bool CreateDirectory(string directoryPath) => _directoryHelper.CreateDirectory(directoryPath);

    public void DeleteDirectory(string directoryPath) => _directoryHelper.DeleteDirectory(directoryPath);

    public void DeleteFile(string filePath) => _directoryHelper.DeleteFile(filePath);

    public M? DeserialiseFromJSON<M>(string json) => _jsonSerialiser.DeserialiseFromJSON<M>(json);

    public bool DirectoryExists(string directoryPath) => _directoryHelper.DirectoryExists(directoryPath);

    public bool FileExists(string path) => _directoryHelper.FileExists(path);

    public List<string> GetDirectories(string path, bool includeSubDirectories) => _directoryHelper.GetDirectories(path, includeSubDirectories);

    public List<string> GetDirectories(string path, bool includeSubDirectories, string searchPattern) => _directoryHelper.GetDirectories(path, includeSubDirectories, searchPattern);

    public string? GetDirectoryFromFilePath(string filePath) => _directoryHelper.GetDirectoryFromFilePath(filePath);

    public T[,] ReadCsvFile<T>(string filename) where T : IConvertible => _csvSerialiser.ReadCsvFile<T>(filename);

    public M? ReadJsonFile<M>(string filename) => _jsonSerialiser.ReadJsonFile<M>(filename);

    public M LoadXML<M>(string fileName) where M : struct => _xmlSerialiser.LoadXML<M>(fileName);

    public void WriteCsvFile<T>(string filename, T[,] data) where T : IConvertible => _csvSerialiser.WriteCsvFile(filename, data);

    public void WriteJsonFile<M>(string filename, M @object) => _jsonSerialiser.WriteJsonFile(filename, @object);

    public void SaveXML<M>(string fileName, M fileToSave) => _xmlSerialiser.SaveXML(fileName, fileToSave);

    public string SerialiseToJSON<M>(M data) => _jsonSerialiser.SerialiseToJSON(data);

    public string ReadTextFile(string path) => _txtSerialiser.ReadTextFile(path);

    public string[] ReadTextFileAsLines(string path) => _txtSerialiser.ReadTextFileAsLines(path);

    public void WriteTextFile(string path, string text) => _txtSerialiser.WriteTextFile(path, text);

    public void WriteTextFile(string path, IEnumerable<string> lines) => _txtSerialiser.WriteTextFile(path, lines);

    public void AppendTextFile(string path, string text) => _txtSerialiser.AppendTextFile(path, text);

    public void AppendTextFile(string path, IEnumerable<string> lines) => _txtSerialiser.AppendTextFile(path, lines);

    public T? TryDeserialiseFromJSON<T>(string json) => _jsonSerialiser.TryDeserialiseFromJSON<T>(json);

    public T? TryReadJsonFile<T>(string filename) => _jsonSerialiser.TryReadJsonFile<T>(filename);
}
