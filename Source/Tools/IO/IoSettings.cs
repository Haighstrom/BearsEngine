using HaighFramework.Console;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace BearsEngine.Source.Tools.IO;

/// <summary>
/// A group of settings for instantiating an IoHelper
/// </summary>
public class IoSettings
{
    public static IoSettings Default => new();

    public int RetriesForIoOperations { get; set; } = 5;

    public int MilisecondsBetweenRetriesForIoOperations { get; set; } = 10;
}