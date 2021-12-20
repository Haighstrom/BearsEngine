// Used for UI borders - joins up points in neat bevelled lines. Lines adjacency in, triangle strip out. This variant does not calculate texture coords to speed up rednering a solid colour line. 
#version 430

layout(lines_adjacency) in;
layout(triangle_strip, max_vertices = 7) out;

in ColourData
{
	vec4 Colour;
} Input_Colour[];

out ColourData
{
	vec4 Colour;
} Output_Colour;

uniform float MITER_LIMIT = 1.0;	// 1.0: always miter, -1.0: never miter, 0.75: default
uniform mat4 MVMatrix;
uniform mat4 PMatrix;
uniform float Thickness;
uniform bool ThicknessInPixels = true; // if false, based on local co-ords (normally camera coords)

void main()
{
	Output_Colour.Colour = Input_Colour[1].Colour;

	// get the four vertices passed to the shader:
	vec2 p0; // start of previous segment
	vec2 p1; // end of previous segment, start of current segment
	vec2 p2; // end of current segment, start of next segment
	vec2 p3; // end of next segment

	if (ThicknessInPixels)
	{
		p0 = (MVMatrix * gl_in[0].gl_Position).xy;
		p1 = (MVMatrix * gl_in[1].gl_Position).xy;	
		p2 = (MVMatrix * gl_in[2].gl_Position).xy;	
		p3 = (MVMatrix * gl_in[3].gl_Position).xy;	
	}
	else
	{
		p0 = gl_in[0].gl_Position.xy;
		p1 = gl_in[1].gl_Position.xy;	
		p2 = gl_in[2].gl_Position.xy;	
		p3 = gl_in[3].gl_Position.xy;
	}

	// determine the direction of each of the 3 segments (previous, current, next)
	vec2 v0 = normalize(p1 - p0);
	vec2 v1 = normalize(p2 - p1);
	vec2 v2 = normalize(p3 - p2);

	// determine the normal of each of the 3 segments (previous, current, next)
	vec2 n0 = vec2(-v0.y, v0.x);
	vec2 n1 = vec2(-v1.y, v1.x);
	vec2 n2 = vec2(-v2.y, v2.x);

	// determine miter lines by averaging the normals of the 2 segments
	vec2 miter_a = normalize(n0 + n1);	// miter at start of current segment
	vec2 miter_b = normalize(n1 + n2);	// miter at end of current segment

	// determine the length of the miter by projecting it onto normal and then inverse it
	float length_a = Thickness / dot(miter_a, n1);
	float length_b = Thickness / dot(miter_b, n1);

	// prevent excessively long miters at sharp corners
	if (dot(v0, v1) < -MITER_LIMIT)
	{
		miter_a = n1;
		length_a = Thickness;

		// close the gap
		if (dot(v0, n1) > 0)
		{
			if (ThicknessInPixels)
				gl_Position = PMatrix * vec4(p1 + Thickness * n0, 0.0, 1.0);
			else
				gl_Position = PMatrix * MVMatrix * vec4(p1 + Thickness * n0, 0.0, 1.0);
			EmitVertex();
			
			if (ThicknessInPixels)
				gl_Position = PMatrix * vec4(p1 + Thickness * n1, 0.0, 1.0);
			else
				gl_Position = PMatrix * MVMatrix * vec4(p1 + Thickness * n1, 0.0, 1.0);
			EmitVertex();
		}
		else
		{
			if (ThicknessInPixels)
				gl_Position = PMatrix * vec4(p1 - Thickness * n1, 0.0, 1.0);
			else
				gl_Position = PMatrix * MVMatrix * vec4(p1 - Thickness * n1, 0.0, 1.0);
			EmitVertex();

			if (ThicknessInPixels)
				gl_Position = PMatrix * vec4(p1 - Thickness * n0, 0.0, 1.0);
			else
				gl_Position =  PMatrix * MVMatrix *vec4(p1 - Thickness * n0, 0.0, 1.0);
			EmitVertex();
		}
		if (ThicknessInPixels)
			gl_Position = PMatrix * vec4(p1, 0.0, 1.0);
		else
			gl_Position = PMatrix * MVMatrix * vec4(p1, 0.0, 1.0);
		EmitVertex();
		EndPrimitive();
	}

	if (dot(v1, v2) < -MITER_LIMIT)
	{
		miter_b = n1;
		length_b = Thickness;
	}

	// generate the triangle strip                   
	if (ThicknessInPixels)
		gl_Position = PMatrix * vec4(p1 + length_a * miter_a, 0.0, 1.0);
	else
		gl_Position = PMatrix * MVMatrix * vec4(p1 + length_a * miter_a, 0.0, 1.0);
	EmitVertex();

	if (ThicknessInPixels)
		gl_Position = PMatrix * vec4(p1 - length_a * miter_a, 0.0, 1.0);
	else
		gl_Position = PMatrix * MVMatrix * vec4(p1 - length_a * miter_a, 0.0, 1.0);
	EmitVertex();

	if (ThicknessInPixels)
		gl_Position = PMatrix * vec4(p2 + length_b * miter_b, 0.0, 1.0);
	else
		gl_Position = PMatrix * MVMatrix * vec4(p2 + length_b * miter_b, 0.0, 1.0);
	EmitVertex();

	if (ThicknessInPixels)
		gl_Position = PMatrix * vec4(p2 - length_b * miter_b, 0.0, 1.0);
	else
		gl_Position = PMatrix * MVMatrix * vec4(p2 - length_b * miter_b, 0.0, 1.0);
	EmitVertex();
	EndPrimitive();
}