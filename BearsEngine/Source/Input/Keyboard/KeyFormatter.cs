using System.Runtime.InteropServices;

namespace BearsEngine.Input;

/// <summary>
/// Class for supporting formatting the <see cref="Key"/> enum.
/// </summary>
public static class KeyFormatter
{
    private static readonly Func<Key, string> _api;

    static KeyFormatter()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            _api = Windows.KeyTranslator.ToWindowsString;
        else
            throw new NotImplementedException();
    }

    /// <summary>
    /// Extension method for Key to get the official Windows readable string for the key. May not handle Left/Right shift properly because eat a dick Bill Gates
    /// </summary>
    public static string AsString(this Key k) => _api(k);
}
