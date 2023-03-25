using BearsEngine.Source.Tools.IO.CSV;
using BearsEngine.Source.Tools.IO.FileDirectory;
using BearsEngine.Source.Tools.IO.JSON;
using BearsEngine.Source.Tools.IO.TXT;
using BearsEngine.Source.Tools.IO.XML;
using System.IO;

namespace BearsEngine.Source.Tools.IO;

public class IOHelper : IIoHelper
{
    private readonly IFileDirectoryIoHelper _directoryHelper = new FileDirectoryIoHelper();
    private readonly ITextFileIoHelper _txtSerialiser = new TXTSerialisationHelper();
    private readonly ICsvFileIoHelper _csvSerialiser = new CsvFileIoHelper();
    private readonly IJsonFileIoHelper _jsonSerialiser = new JSONSerialisationHelper();
    private readonly IXmlFileIoHelper _xmlSerialiser = new XMLSerialisationHelper();

    public void AppendText(string filePath, string textToAppend) => _txtSerialiser.AppendText(filePath, textToAppend);

    public void CopyFile(string sourceFile, string destinationFile) => _directoryHelper.CopyFile(sourceFile, destinationFile);

    public void CopyFiles(string sourceDirectory, string destinationDirectory) => _directoryHelper.CopyFiles(sourceDirectory, destinationDirectory);

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

    public M LoadJSON<M>(string filename) => _jsonSerialiser.LoadJSON<M>(filename);

    public List<M> LoadJSONFromMultilineTxt<M>(string filename) => _jsonSerialiser.LoadJSONFromMultilineTxt<M>(filename);

    public string[] LoadTXTAsArray(string filename) => _txtSerialiser.LoadTXTAsArray(filename);

    public string LoadTXTAsString(string filename) => _txtSerialiser.LoadTXTAsString(filename);

    public M LoadXML<M>(string fileName) where M : struct => _xmlSerialiser.LoadXML<M>(fileName);

    public void WriteCsvFile<T>(string filename, T[,] data) where T : IConvertible => _csvSerialiser.WriteCsvFile(filename, data);

    public void SaveJSON<M>(string filename, M @object, bool indent = true) => _jsonSerialiser.SaveJSON(filename, @object, indent);

    public void SaveTXT(string filename, string contents) => _txtSerialiser.SaveTXT(filename, contents);

    public void SaveTXT(string filename, IEnumerable<string> lines) => _txtSerialiser.SaveTXT(filename, lines);

    public void SaveXML<M>(string fileName, M fileToSave) => _xmlSerialiser.SaveXML(fileName, fileToSave);

    public string SerialiseToJSON<M>(M @object, bool indent = true) => _jsonSerialiser.SerialiseToJSON(@object, indent);
}
