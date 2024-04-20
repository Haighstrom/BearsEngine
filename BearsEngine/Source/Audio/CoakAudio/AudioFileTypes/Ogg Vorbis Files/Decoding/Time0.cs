namespace BearsEngine.Audio.OpenAL.OggVorbis;

    class Time0 : FuncTime
{
	override internal void pack(object i, csBuffer opb){}
	override internal object unpack(Info vi , csBuffer opb){return "";}
	override internal object look(DspState vd, InfoMode mi, object i){return "";}
	override internal void free_info(object i){}
	override internal void free_look(object i){}
	override internal int forward(Block vb, object i){return 0;}
	override internal int inverse(Block vb, object i, float[] fin, float[] fout){return 0;}
}
