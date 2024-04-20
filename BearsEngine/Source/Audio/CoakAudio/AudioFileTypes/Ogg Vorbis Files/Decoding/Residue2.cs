namespace BearsEngine.Audio.OpenAL.OggVorbis;

    class Residue2 : Residue0
{
	override internal int forward(Block vb, object vl, float[][] fin, int ch)
	{
		return 0;
	}

	override internal int inverse(Block vb, object vl, float[][] fin, int[] nonzero, int ch)
	{
		int i=0;
		for(i=0;i<ch;i++)if(nonzero[i]!=0)break;
		if(i==ch)return 0; /* no nonzero vectors */

		return _2inverse(vb, vl, fin, ch);
	}
}
