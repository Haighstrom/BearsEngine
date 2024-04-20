#version 430

uniform mat3 MVMatrix;
uniform mat3 PMatrix;

in vec2 Position;
in vec4 Colour;

out ColourData
{
	vec4 Colour;
} Output_Colour;

void main()
{
    gl_Position = vec4(PMatrix * MVMatrix * vec3(Position, 1), 1);

    Output_Colour.Colour = vec4(Colour.xyz * Colour.w, Colour.w);
}