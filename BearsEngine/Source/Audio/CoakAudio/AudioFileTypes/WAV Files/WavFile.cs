using System.IO;

namespace BearsEngine.Audio.OpenAL.Wav;

internal class WavFile : AudioFile
{
    internal int Channels { get; private set; }
    internal int Bits { get; private set; }
    internal int Bits_per_sample { get; private set; }
    internal int Sample_rate { get; private set; }
    internal int Rate { get; private set; }
    internal byte[] RawData { get; private set; }
    internal ALFormat Format_AL { get; private set; }
    //how many bytes in to the raw data file we are in buffering
    private long LastStreamPosition;

    internal WavFile(string filePath)
        : base()
    {
        Stream stream = File.OpenRead(filePath);
        using (BinaryReader reader = new(stream))
        {
            // RIFF header
            string signature = new(reader.ReadChars(4));
            if (signature != "RIFF")
                throw new NotSupportedException("Specified stream is not a wave file.");

            int riff_chunck_size = reader.ReadInt32();

            string format = new(reader.ReadChars(4));
            if (format != "WAVE")
                throw new NotSupportedException("Specified stream is not a wave file.");

            // WAVE header
            string format_signature = new(reader.ReadChars(4));
            if (format_signature != "fmt ")
                throw new NotSupportedException("Specified wave file is not supported.");

            int format_chunk_size = reader.ReadInt32();
            int audio_format = reader.ReadInt16();
            int num_channels = reader.ReadInt16();
            Sample_rate = reader.ReadInt32();
            int byte_rate = reader.ReadInt32();
            int block_align = reader.ReadInt16();
            Bits_per_sample = reader.ReadInt16();
            Format_AL = GetSoundFormat(num_channels, Bits_per_sample);

            string data_signature = new(reader.ReadChars(4));
            if (data_signature != "data")
                throw new NotSupportedException("Specified wave file is not supported.");

            int data_chunk_size = reader.ReadInt32();

            Channels = num_channels;
            Bits = Bits_per_sample;
            Rate = Sample_rate;

            RawData = reader.ReadBytes((int)reader.BaseStream.Length);
        }
    }

    public static ALFormat GetSoundFormat(int channels, int bits)
    {
        switch (channels)
        {
            case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
            case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
            default: throw new NotSupportedException("The specified sound format is not supported.");
        }
    }

    public void Reset()
    {
        LastStreamPosition = 0;
    }

    /// <summary>
    /// Read length bytes and place them into byte[] buffer (which is of course a reference type)
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    internal int read(byte[] buffer, int length)
    {
        long i;
        int bitsRead = 0;
        for (i = LastStreamPosition; i < LastStreamPosition + length && i < RawData.Length; i++)
        {
            buffer[bitsRead] = RawData[i];
            bitsRead++;
        }
        LastStreamPosition += bitsRead;
        return bitsRead;
    }

    internal override void Dispose()
    {
    }
}
