namespace BearsEngine.Audio.OpenAL.OggVorbis;

    abstract class FuncFloor
{
	internal static FuncFloor[] floor_P={new Floor0(),new Floor1()};

	internal abstract void pack(object i, csBuffer opb);
	internal abstract object unpack(Info vi, csBuffer opb);
	internal abstract object look(DspState vd, InfoMode mi, object i);
	internal abstract void free_info(object i);
	internal abstract void free_look(object i);
	internal abstract void free_state(object vs);
	internal abstract int forward(Block vb, object i, float[] fin, float[] fout, object vs);
	internal abstract object inverse1(Block vb, object i, object memo);
	internal abstract int inverse2(Block vb, object i, object memo, float[] fout);
}
