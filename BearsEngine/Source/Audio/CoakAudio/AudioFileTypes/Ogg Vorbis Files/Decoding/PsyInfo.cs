namespace BearsEngine.Audio.OpenAL.OggVorbis;

    // psychoacoustic setup
    class PsyInfo
{ 
	float[] toneatt_125Hz=new float[5];
	float[] toneatt_250Hz=new float[5];
	float[] toneatt_500Hz=new float[5];
	float[] toneatt_1000Hz=new float[5];
	float[] toneatt_2000Hz=new float[5];
	float[] toneatt_4000Hz=new float[5];
	float[] toneatt_8000Hz=new float[5];

	float[] peakatt_125Hz=new float[5];
	float[] peakatt_250Hz=new float[5];
	float[] peakatt_500Hz=new float[5];
	float[] peakatt_1000Hz=new float[5];
	float[] peakatt_2000Hz=new float[5];
	float[] peakatt_4000Hz=new float[5];
	float[] peakatt_8000Hz=new float[5];
	
	float[] noiseatt_125Hz=new float[5];
	float[] noiseatt_250Hz=new float[5];
	float[] noiseatt_500Hz=new float[5];
	float[] noiseatt_1000Hz=new float[5];
	float[] noiseatt_2000Hz=new float[5];
	float[] noiseatt_4000Hz=new float[5];
	float[] noiseatt_8000Hz=new float[5];

	internal void free(){}
}
