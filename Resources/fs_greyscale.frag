#version 430

uniform sampler2D Sampler;

in ColourData
{
	vec4 Colour;
} Input_Colour;

in TexCoordData
{
	vec2 TexCoord;
} Input_TexCoord;

out vec4 Colour;

void main()
{
	Colour = texture(Sampler, Input_TexCoord.TexCoord) * Input_Colour.Colour;
	float lum = dot(Colour.xyz, vec3(0.299, 0.587, 0.114));
	Colour = vec4(lum, lum, lum, Colour.w);

	//Alpha test
	if(Colour.a <= 0) 
		discard;
}