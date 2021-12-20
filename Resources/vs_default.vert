#version 430

uniform mat4 MVMatrix;
uniform mat4 PMatrix;

in vec2 Position;
in vec4 Colour;
in vec2 TexCoord;

out ColourData
{
	vec4 Colour;
} Output_Colour;

out TexCoordData
{
    vec2 TexCoord;
} Output_TexCoord;

void main()
{
	gl_Position = PMatrix * MVMatrix * vec4(Position, 0, 1);

    Output_Colour.Colour = vec4(Colour.xyz * Colour.w, Colour.w);
    Output_TexCoord.TexCoord = TexCoord;
}