namespace BearsEngine.Audio.OpenAL.OggVorbis;

    class Residue1 : Residue0
{
	new int forward(Block vb, object vl, float[][] fin, int ch)
	{
		return 0;
	}

	override internal int inverse(Block vb, object vl, float[][] fin, int[] nonzero, int ch)
	{
		int used=0;
		for(int i=0; i<ch; i++)
		{
			if(nonzero[i]!=0)
			{
				fin[used++]=fin[i];
			}
		}
		if(used!=0)
		{
			return(_01inverse(vb, vl, fin, used, 1));
		}
		else
		{
			return 0;
		}
	}
}
