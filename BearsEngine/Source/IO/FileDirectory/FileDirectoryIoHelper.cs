using System.IO;

namespace BearsEngine.Source.Tools.IO.FileDirectory;

internal class FileDirectoryIoHelper : IFileDirectoryIoHelper
{
    public void CopyFile(string sourceFile, string destinationFile)
    {
        File.Copy(sourceFile, destinationFile);
    }

    public void CopyFiles(string sourceDirectory, string destinationDirectory, CopyOptions options)
    {
        if (!Directory.Exists(destinationDirectory))
            Directory.CreateDirectory(destinationDirectory);

        if (sourceDirectory.Last() == '/')
            sourceDirectory = sourceDirectory.Remove(sourceDirectory.Length - 1);

        if (destinationDirectory.Last() != '/')
            destinationDirectory += '/';

        var so = (options & CopyOptions.BringAll) > 0 ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        //copy subfolders first (if applicable)
        if ((options & CopyOptions.BringFolders) > 0)
            foreach (string dir in Directory.GetDirectories(sourceDirectory, "*", so))
                Directory.CreateDirectory(Path.Combine(destinationDirectory, dir[(sourceDirectory.Length + 1)..]));

        //copy files
        foreach (string file in Directory.GetFiles(sourceDirectory, "*", so))
            File.Copy(file, Path.Combine(destinationDirectory, file[(sourceDirectory.Length + 1)..]), (options & CopyOptions.Overwrite) > 0);
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
