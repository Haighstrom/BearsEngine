namespace BearsEngine.Audio.OpenAL.OggVorbis;

    abstract class FuncResidue
{
	internal static FuncResidue[] residue_P={new Residue0(),
											  new Residue1(),
											  new Residue2()};

	internal abstract void pack(object vr, csBuffer opb);
	internal abstract object unpack(Info vi, csBuffer opb);
	internal abstract object look(DspState vd, InfoMode vm, object vr);
	internal abstract void free_info(object i);
	internal abstract void free_look(object i);
	internal abstract int forward(Block vb, object vl, float[][] fin, int ch);

	internal abstract int inverse(Block vb, object vl, float[][] fin, int[] nonzero,int ch);
}
