using System.IO;

namespace BearsEngine.Audio.OpenAL;

internal abstract class AudioFile 
{
    internal Stream? datasource;

    internal AudioFile()
        : base()
    {

    }

    internal abstract void Dispose();
}
