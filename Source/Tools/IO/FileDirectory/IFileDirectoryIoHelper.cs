namespace BearsEngine.Source.Tools.IO.FileDirectory;

internal interface IFileDirectoryIoHelper
{
    void CopyFile(string sourceFile, string destinationFile);

    void CopyFiles(string sourceDirectory, string destinationDirectory);

    bool CreateDirectory(string path);

    void DeleteDirectory(string path);

    void DeleteFile(string path);

    bool DirectoryExists(string path);

    bool FileExists(string path);

    List<string> GetDirectories(string path, bool includeSubDirectories);

    List<string> GetDirectories(string path, bool includeSubDirectories, string searchPattern);

    string? GetDirectoryFromFilePath(string path);
}