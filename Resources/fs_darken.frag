#version 430

uniform sampler2D Sampler;
uniform float DarkenValue;

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
	Colour = vec4(Colour.x * DarkenValue, Colour.y * DarkenValue, Colour.z * DarkenValue, Colour.w);

	//Alpha test
	if(Colour.a <= 0) 
		discard;
}