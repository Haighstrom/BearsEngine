using BearsEngine.Source.Tools.IO.JSON;
using System.IO;
using System.Text.Json;

namespace BearsEngine.UnitTests.Tools.IoHelper;

[TestClass]
public class JsonFileIoHelperTests
{
    private class TestObject
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [TestMethod]
    public void TestReadJsonFile()
    {
        // Arrange
        var filename = "test.json";
        var json = @"{""Name"":""John Smith"",""Age"":30}";

        using (StreamWriter writer = new(filename))
        {
            writer.Write(json);
        }

        JsonSerializerOptions options = new()
        {
            WriteIndented = false,
        };
        var helper = new JsonFileIoHelper(options);

        // Act
        var result = helper.ReadJsonFile<TestObject>(filename);

        // Assert
        Assert.AreEqual("John Smith", result.Name);
        Assert.AreEqual(30, result.Age);

        // Clean up
        File.Delete(filename);
    }

    [TestMethod]
    public void TestWriteJsonFile()
    {
        // Arrange
        var filename = "test.json";
        var data = new TestObject
        {
            Name = "Jane Doe",
            Age = 25
        };

        JsonSerializerOptions options = new()
        {
            WriteIndented = false,
        };
        var helper = new JsonFileIoHelper(options);

        // Act
        helper.WriteJsonFile(filename, data);

        // Assert
        var json = File.ReadAllText(filename);
        Assert.AreEqual(@"{""Name"":""Jane Doe"",""Age"":25}", json);

        // Clean up
        File.Delete(filename);
    }

    [TestMethod]
    public void TestSerialiseToJSON()
    {
        // Arrange
        var data = new TestObject
        {
            Name = "John Smith",
            Age = 30
        };

        JsonSerializerOptions options = new()
        {
            WriteIndented = false,
        };
        var helper = new JsonFileIoHelper(options);

        // Act
        var json = helper.SerialiseToJSON(data);

        // Assert
        Assert.AreEqual(@"{""Name"":""John Smith"",""Age"":30}", json);
    }

    [TestMethod]
    public void TestDeserialiseFromJSON()
    {
        // Arrange
        var json = @"{""Name"":""Jane Doe"",""Age"":25}";

        JsonSerializerOptions options = new()
        {
            WriteIndented = false,
        };
        var helper = new JsonFileIoHelper(options);

        // Act
        var result = helper.DeserialiseFromJSON<TestObject>(json);

        // Assert
        Assert.AreEqual("Jane Doe", result.Name);
        Assert.AreEqual(25, result.Age);
    }
}