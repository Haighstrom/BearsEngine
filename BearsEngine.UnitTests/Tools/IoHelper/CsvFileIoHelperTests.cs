using BearsEngine.Source.Tools.IO.CSV;
using System.IO;

namespace BearsEngine.UnitTests.Tools.IoHelper;

[TestClass]
public class CsvFileIoHelperTests
{
    private ICsvFileIoHelper _csvFileIoHelper = null!; //compiler, go fuck yourself

    [TestInitialize]
    public void TestInitialize()
    {
        _csvFileIoHelper = new CsvFileIoHelper();
    }

    [TestMethod]
    public void ReadCsvFile_ReadsIntDataFromFile()
    {
        // Arrange
        var filename = "test.csv";
        var expected = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        File.WriteAllText(filename, $"1,2,3{Environment.NewLine}4,5,6{Environment.NewLine}7,8,9{Environment.NewLine}");

        // Act
        var actual = _csvFileIoHelper.ReadCsvFile<int>(filename);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReadCsvFile_ReadsStringDataFromFile()
    {
        // Arrange
        var filename = "test.csv";
        var expected = new string[,] { { "a", "b", "c" }, { "d", "e", "f" }, { "g", "h", "i" } };
        File.WriteAllText(filename, $"a,b,c{Environment.NewLine}d,e,f{Environment.NewLine}g,h,i{Environment.NewLine}");

        // Act
        var actual = _csvFileIoHelper.ReadCsvFile<string>(filename);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void WriteCsvFile_WritesIntDataToFile()
    {
        // Arrange
        var filename = "test.csv";
        var data = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

        // Act
        _csvFileIoHelper.WriteCsvFile(filename, data);

        // Assert
        var expected = $"1,2,3{Environment.NewLine}4,5,6{Environment.NewLine}7,8,9{Environment.NewLine}";
        var actual = File.ReadAllText(filename);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void WriteCsvFile_WritesStringDataToFile()
    {
        // Arrange
        var filename = "test.csv";
        var data = new string[,] { { "a", "b", "c" }, { "d", "e", "f" }, { "g", "h", "i" } };

        // Act
        _csvFileIoHelper.WriteCsvFile(filename, data);

        // Assert
        var expected = $"a,b,c{Environment.NewLine}d,e,f{Environment.NewLine}g,h,i{Environment.NewLine}";
        var actual = File.ReadAllText(filename);
        Assert.AreEqual(expected, actual);
    }
}
