namespace BearsEngine.Audio.OpenAL.OggVorbis;

    internal class csorbisException : Exception 
{
	internal csorbisException ()
		:base(){}
	internal csorbisException (string s)
		:base("csorbis: "+s){}
}
