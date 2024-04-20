//Multisampled Antialiased fragment shader which passes through a texture and multiplies it with colour - http://www.opentk.com/node/2251
#version 430

uniform sampler2DMS Sampler;

in TexCoordData
{
	vec2 TexCoord;
} Input_TexCoord;

out vec4 Colour;

uniform int MSAASamples;
float div= 1.0/MSAASamples;

void main()
{
	Colour = vec4(0.0);
    ivec2 texcoord = ivec2(textureSize(Sampler) * Input_TexCoord.TexCoord); // used to fetch msaa texel location
                
    for (int i=0; i<MSAASamples; i++)
    {
        Colour += texelFetch(Sampler, texcoord, i);  // add  color samples together
    }
 
    Colour*= div; //devide by num of samples to get color avg.
}