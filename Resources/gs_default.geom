// Simple in-out geometry shader that does not affect the vertices at all. Tris or triangle strips

#version 430

layout(triangles) in;
layout(triangle_strip) out;
layout(max_vertices = 4) out;

in ColourData
{
	vec4 Colour;
} Input_Colour[];

in TexCoordData
{
	vec2 TexCoord;
} Input_TexCoord[];

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
	int i;
	for (i = 0; i < gl_in.length(); i++)
	{
		Output_Colour.Colour = Input_Colour[i].Colour;
		Output_TexCoord.TexCoord = Input_TexCoord[i].TexCoord;
		gl_Position = gl_in[i].gl_Position;
		EmitVertex();
	}
	EndPrimitive();
}