using System.IO;

namespace BearsEngine.Source.Tools.IO.FileDirectory;

internal class FileDirectoryIoHelper : IFileDirectoryIoHelper
{
    public void CopyFile(string sourceFile, string destinationFile)
    {
        File.Copy(sourceFile, destinationFile);
    }

    public void CopyFiles(string sourceDirectory, string destinationDirectory)
    {
        if (!Directory.Exists(sourceDirectory))
        {
            throw new DirectoryNotFoundException($"Source directory not found: {sourceDirectory}");
        }

        var sourceDirectoryInfo = new DirectoryInfo(sourceDirectory);
        var files = sourceDirectoryInfo.GetFiles("*", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var relativePath = file.FullName[(sourceDirectory.Length + 1)..];
            var destinationPath = Path.Combine(destinationDirectory, relativePath);

            var destinationDirectoryPath = Path.GetDirectoryName(destinationPath);

            if (destinationDirectoryPath is null)
                throw new NullReferenceException();

            if (!Directory.Exists(destinationDirectoryPath))
            {
                Directory.CreateDirectory(destinationDirectoryPath);
            }

            File.Copy(file.FullName, destinationPath);
        }
    }

    public bool CreateDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            return false;
        }

        Directory.CreateDirectory(path);
        return true;
    }

    public void DeleteDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            return;
        }

        Directory.Delete(path, true);
    }

    public void DeleteFile(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        File.Delete(path);
    }

    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }

    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

    public List<string> GetDirectories(string path, bool includeSubDirectories)
    {
        return GetDirectories(path, includeSubDirectories, "*");
    }

    public List<string> GetDirectories(string path, bool includeSubDirectories, string searchPattern)
    {
        var searchOption = includeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        var directories = Directory.GetDirectories(path, searchPattern, searchOption)
            .Select(d => Path.GetFullPath(d))
            .ToList();

        return directories;
    }

    public string? GetDirectoryFromFilePath(string path)
    {
        Ensure.ArgumentNotNullOrEmpty(path, "path");

        try
        {
            return Path.GetDirectoryName(path);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is PathTooLongException || ex is NotSupportedException)
        {
            throw new ArgumentException($"Invalid path: {path}", nameof(path), ex);
        }
    }
}
