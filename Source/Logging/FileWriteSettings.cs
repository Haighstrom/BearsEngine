namespace BearsEngine.Logging;

[Serializable]
public record FileWriteSettings(string FilePath, LogLevel LogLevel, bool OverwritePreviousFiles);