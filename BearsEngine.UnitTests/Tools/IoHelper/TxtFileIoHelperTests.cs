using BearsEngine.Source.Tools;
using BearsEngine.Source.Tools.IO.TXT;
using System.Collections.Generic;
using System.IO;

namespace BearsEngine.UnitTests.Tools.IoHelper;

[TestClass]
public class TxtFileIoHelperTests
{
    private readonly ITxtFileIoHelper _txtFileIoHelper = new TxtFileIoHelper();
    private const string TestFolderPath = "TxtFileIoHelperTestFiles";
    private const string TestFilePath = TestFolderPath + "\\test.txt";

    [TestInitialize]
    public void Initialize()
    {
        if (!Directory.Exists(TestFolderPath))
        {
            Directory.CreateDirectory(TestFolderPath);
        }
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(TestFilePath))
        {
            Repeat.TryMethod(() => File.Delete(TestFilePath), 5, new TimeSpan(1000));
        }

        if (Directory.Exists(TestFolderPath))
        {
            Repeat.TryMethod(() => Directory.Delete(TestFolderPath), 5, new TimeSpan(1000));
        }
    }

    [TestMethod]
    public void ReadTextFile_ReadsFileContent()
    {
        // Arrange
        var text = "Hello, world!";
        File.WriteAllText(TestFilePath, text);

        // Act
        var result = _txtFileIoHelper.ReadTextFile(TestFilePath);

        // Assert
        Assert.AreEqual(text, result);
    }

    [TestMethod]
    public void ReadTextFileAsLines_ReadsFileContentAsLines()
    {
        // Arrange
        string[] lines = { "Line 1", "Line 2", "Line 3" };
        File.WriteAllLines(TestFilePath, lines);

        // Act
        var result = _txtFileIoHelper.ReadTextFileAsLines(TestFilePath);

        // Assert
        CollectionAssert.AreEqual(lines, result);
    }

    [TestMethod]
    public void WriteTextFile_WritesTextToFile()
    {
        // Arrange
        var text = "Hello, world!";

        // Act
        _txtFileIoHelper.WriteTextFile(TestFilePath, text);

        // Assert
        Assert.AreEqual(text, File.ReadAllText(TestFilePath));
    }

    [TestMethod]
    public void WriteTextFile_WritesLinesToFile()
    {
        // Arrange
        var lines = new List<string> { "Line 1", "Line 2", "Line 3" };

        // Act
        _txtFileIoHelper.WriteTextFile(TestFilePath, lines);

        // Assert
        CollectionAssert.AreEqual(lines, File.ReadAllLines(TestFilePath));
    }

    [TestMethod]
    public void AppendTextFile_AppendsTextToFile()
    {
        // Arrange
        var initialText = "Hello,";
        var appendedText = " world!";
        File.WriteAllText(TestFilePath, initialText);

        // Act
        _txtFileIoHelper.AppendTextFile(TestFilePath, appendedText);

        // Assert
        Assert.AreEqual(initialText + appendedText, File.ReadAllText(TestFilePath));
    }

    [TestMethod]
    public void AppendTextFile_AppendsLinesToFile()
    {
        // Arrange
        var initialLines = new List<string> { "Line 1", "Line 2" };
        var appendedLines = new List<string> { "Line 3", "Line 4" };
        File.WriteAllLines(TestFilePath, initialLines);

        // Act
        _txtFileIoHelper.AppendTextFile(TestFilePath, appendedLines);

        // Assert
        var expectedLines = new List<string>(initialLines);
        expectedLines.AddRange(appendedLines);
        CollectionAssert.AreEqual(expectedLines, File.ReadAllLines(TestFilePath));
    }
}
