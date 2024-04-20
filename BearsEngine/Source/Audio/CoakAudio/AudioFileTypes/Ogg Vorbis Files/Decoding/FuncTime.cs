namespace BearsEngine.Audio.OpenAL.OggVorbis;

    abstract class FuncTime
{
	internal static FuncTime[] time_P={new Time0()};

	internal abstract void pack(object i, csBuffer opb);
	internal abstract object unpack(Info vi , csBuffer opb);
	internal abstract object look(DspState vd, InfoMode vm, object i);
	internal abstract void free_info(object i);
	internal abstract void free_look(object i);
	internal abstract int forward(Block vb, object i);
	internal abstract int inverse(Block vb, object i, float[] fin, float[] fout);
}
