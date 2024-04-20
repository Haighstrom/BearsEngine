namespace BearsEngine.Audio.OpenAL.OggVorbis;

    abstract class FuncMapping
{
	internal static FuncMapping[] mapping_P={new Mapping0()};

	internal abstract void pack(Info info , object imap, csBuffer buffer);
	internal abstract object unpack(Info info , csBuffer buffer);
	internal abstract object look(DspState vd, InfoMode vm, object m);
	internal abstract void free_info(object imap);
	internal abstract void free_look(object imap);
	internal abstract int inverse(Block vd, object lm);
}
