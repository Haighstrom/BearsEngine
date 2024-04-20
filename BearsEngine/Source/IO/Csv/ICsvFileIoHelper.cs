namespace BearsEngine.Source.Tools.IO.CSV;

internal interface ICsvFileIoHelper
{
    T[,] ReadCsvFile<T>(string filename, char separator = ',') where T : IConvertible;

    void WriteCsvFile<T>(string filename, T[,] data, char separator = ',') where T : IConvertible;
}