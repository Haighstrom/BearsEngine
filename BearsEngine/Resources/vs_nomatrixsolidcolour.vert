#version 430

in vec2 Position;
in vec4 Colour;

out ColourData
{
	vec4 Colour;
} Output_Colour;

void main()
{
	gl_Position = vec4(Position, 0, 1);

    Output_Colour.Colour = vec4(Colour.xyz * Colour.w, Colour.w);
}