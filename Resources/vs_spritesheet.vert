// Vertex shader that cuts a rectangle out of a sprite sheet
#version 430

uniform mat3 MVMatrix;
uniform mat3 PMatrix;
uniform int XIndex;
uniform int YIndex;
uniform float TileW;
uniform float TileH;

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
    gl_Position = vec4(PMatrix * MVMatrix * vec3(Position, 1), 1);

	Output_Colour.Colour = vec4(Colour.xyz * Colour.w, Colour.w);
	Output_TexCoord.TexCoord = vec2(TexCoord.x + XIndex * TileW, TexCoord.y + YIndex * TileH);
}