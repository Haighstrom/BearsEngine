namespace BearsEngine.UnitTests.Tools.IoHelper;

using BearsEngine.Source.Tools;
using BearsEngine.Source.Tools.IO.FileDirectory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

[TestClass]
public class FileDirectoryIoHelperTests
{
    private const string TestFolderPath = "TestFiles";
    private const string TestDirectory = TestFolderPath + "\\FileDirectoryIoHelperTests";
    private const string SourceDirectory = TestFolderPath + "\\FileDirectoryIoHelperTests\\Source";
    private const string DestinationDirectory = TestFolderPath + "\\FileDirectoryIoHelperTests\\Destination";
    private const string TestFileName = "test.txt";
    private const string SourceFile = SourceDirectory + "\\" + TestFileName;
    private const string DestinationFile = DestinationDirectory + "\\" + TestFileName;

    [TestInitialize]
    public void Initialize()
    {
        if (Directory.Exists(TestDirectory))
        {
            Repeat.TryMethod(() => Directory.Delete(TestDirectory, true), 5, new TimeSpan(1000));
        }

        Directory.CreateDirectory(SourceDirectory);
        Directory.CreateDirectory(DestinationDirectory);
        File.Create(SourceFile).Dispose();
    }

    [TestMethod]
    public void CopyFile_CopiesFileToDestination()
    {
        var helper = new FileDirectoryIoHelper();

        helper.CopyFile(SourceFile, DestinationFile);

        Assert.IsTrue(File.Exists(DestinationFile));
    }

    [TestMethod]
    public void CopyFiles_CopiesAllFilesToDestination()
    {
        var helper = new FileDirectoryIoHelper();

        helper.CopyFiles(SourceDirectory, DestinationDirectory, CopyOptions.BringAll);

        Assert.IsTrue(File.Exists(DestinationFile));
    }

    [TestMethod]
    public void CreateDirectory_CreatesDirectory()
    {
        var helper = new FileDirectoryIoHelper();
        var directoryPath = Path.Combine(TestDirectory, "NewDirectory");

        var created = helper.CreateDirectory(directoryPath);

        Assert.IsTrue(created);
        Assert.IsTrue(Directory.Exists(directoryPath));
    }

    [TestMethod]
    public void CreateDirectory_ReturnsFalseIfDirectoryExists()
    {
        var helper = new FileDirectoryIoHelper();
        var directoryPath = Path.Combine(TestDirectory, "Source");

        var created = helper.CreateDirectory(directoryPath);

        Assert.IsFalse(created);
    }

    [TestMethod]
    public void DeleteDirectory_DeletesDirectory()
    {
        var helper = new FileDirectoryIoHelper();

        helper.DeleteDirectory(SourceDirectory);

        Assert.IsFalse(Directory.Exists(SourceDirectory));
    }

    [TestMethod]
    public void DeleteDirectory_DoesNotThrowIfDirectoryDoesNotExist()
    {
        var helper = new FileDirectoryIoHelper();

        helper.DeleteDirectory("C:\\temp\\doesnotexist");

        // No exception should be thrown
    }

    [TestMethod]
    public void DeleteFile_DeletesFile()
    {
        var helper = new FileDirectoryIoHelper();

        helper.DeleteFile(SourceFile);

        Assert.IsFalse(File.Exists(SourceFile));
    }

    [TestMethod]
    public void DeleteFile_DoesNotThrowIfFileDoesNotExist()
    {
        var helper = new FileDirectoryIoHelper();

        helper.DeleteFile("C:\\temp\\doesnotexist.txt");

        // No exception should be thrown
    }

    [TestMethod]
    public void DirectoryExists_ReturnsTrueIfDirectoryExists()
    {
        var helper = new FileDirectoryIoHelper();

        var exists = helper.DirectoryExists(SourceDirectory);

        Assert.IsTrue(exists);
    }

    [TestMethod]
    public void DirectoryExists_ReturnsFalseIfDirectoryDoesNotExist()
    {
        var helper = new FileDirectoryIoHelper();

        var exists = helper.DirectoryExists("C:\\temp\\doesnotexist");

        Assert.IsFalse(exists);
    }

    [TestMethod]
    public void FileExists_ReturnsTrueIfFileExists()
    {
        var helper = new FileDirectoryIoHelper();

        var exists = helper.FileExists(SourceFile);

        Assert.IsTrue(exists);
    }
}
