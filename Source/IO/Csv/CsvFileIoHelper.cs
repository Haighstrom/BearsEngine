using System.IO;
using System.Text;

namespace BearsEngine.Source.Tools.IO.CSV;

internal class CsvFileIoHelper : ICsvFileIoHelper
{
    public void WriteCsvFile<T>(string filename, T[,] data) where T : IConvertible
    {
        var rowCount = data.GetLength(0);
        var colCount = data.GetLength(1);

        var csv = new StringBuilder();

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                csv.Append($"{data[i, j]}{(j == colCount - 1 ? "" : ",")}");
            }
            csv.AppendLine();
        }

        File.WriteAllText(filename, csv.ToString());
    }

    public T[,] ReadCsvFile<T>(string filename) where T : IConvertible
    {
        var lines = File.ReadAllLines(filename);
        var rowCount = lines.Length;
        var colCount = lines[0].Split(',').Length;
        var data = new T[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            var values = lines[i].Split(',');

            for (int j = 0; j < colCount; j++)
            {
                data[i, j] = (T)Convert.ChangeType(values[j], typeof(T));
            }
        }

        return data;
    }
}