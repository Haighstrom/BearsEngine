namespace BearsEngine.Source.Tools.IO.FileDirectory;

[Flags]
public enum CopyOptions : uint
{
    None = 0,

    /// <summary>
    /// Overwrite files in the target directory
    /// </summary>
    Overwrite = 1,

    /// <summary>
    /// Bring top level folders
    /// </summary>
    BringFolders = 2,

    /// <summary>
    /// Bring all subdirectories and folders within them
    /// </summary>
    BringAll = 6,
}