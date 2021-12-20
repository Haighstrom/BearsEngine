#version 430

uniform mat4 MVMatrix;
uniform mat4 PMatrix;

in vec2 Position;
in vec2 TexCoord;

out TexCoordData
{
   vec2 TexCoord;
} Output_TexCoord;

void main()
{
	gl_Position = PMatrix * MVMatrix * vec4(Position, 0, 1);
   Output_TexCoord.TexCoord = TexCoord;
}