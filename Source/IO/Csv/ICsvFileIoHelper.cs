namespace BearsEngine.Source.Tools.IO.CSV;

internal interface ICsvFileIoHelper
{
    T[,] ReadCsvFile<T>(string filename) where T : IConvertible;

    void WriteCsvFile<T>(string filename, T[,] data) where T : IConvertible;
}