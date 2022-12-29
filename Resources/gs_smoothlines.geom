// Used for UI borders - joins up points in neat bevelled lines. Lines adjacency in, triangle strip out. This variant does not calculate texture coords to speed up rendering a solid colour line. 
#version 430

layout(lines_adjacency) in;
layout(triangle_strip, max_vertices = 30) out;

in ColourData
{
	vec4 Colour;
} Input_Colour[];

out ColourData
{
	vec4 Colour;
} Output_Colour;

uniform float MiterLimit = 0.75;	// 1.0: always miter, -1.0: never miter, 0.75: default
uniform float Thickness;
uniform mat3 MVMatrix;
uniform mat3 PMatrix;
uniform bool ThicknessInPixels = true; // if false, based on local co-ords (normally camera coords)
uniform int ShiftMode = 0;	//This is linked to the BorderPosition enum - whether we should shift the vertices in or out or leave them so that the line is centred on the vertices

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
		p0 = (MVMatrix * vec3(gl_in[0].gl_Position.xy, 1)).xy;
		p1 = (MVMatrix * vec3(gl_in[1].gl_Position.xy, 1)).xy;
		p2 = (MVMatrix * vec3(gl_in[2].gl_Position.xy, 1)).xy;	
		p3 = (MVMatrix * vec3(gl_in[3].gl_Position.xy, 1)).xy;	
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
	
	//shift vertices in or out if specified. p0 and p3 not used for any further calcualtions, no need to change them
	if(ShiftMode == 1)	//inside
	{
		p1 = p1 + length_a * miter_a;
		p2 = p2 + length_b * miter_b;
	}
	else if (ShiftMode == 2)	//outside
	{
		p1 = p1 - length_a * miter_a;
		p2 = p2 - length_b * miter_b;
	}	


	//------------- repeat these calculations because we have changed the geometry
	if(ShiftMode != 1 )
	{
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
	}	
	//----------------------------------------------------------------------



	// prevent excessively long miters at sharp corners
	if (dot(v0, v1) < -MiterLimit)
	{
		miter_a = n1;
		length_a = Thickness;

		// close the gap
		if (dot(v0, n1) > 0)
		{
			if (ThicknessInPixels)
			{
				gl_Position = vec4((PMatrix * vec3(p1 + Thickness * n0, 1.0)).xy, 0, 1);
				EmitVertex();
				gl_Position = vec4((PMatrix * vec3(p1 + Thickness * n1, 1.0)).xy, 0, 1);
				EmitVertex();
				gl_Position = vec4((PMatrix * vec3(p1, 1.0)).xy, 0, 1);
				EmitVertex();
				EndPrimitive();
			}
			else
			{
				gl_Position = vec4((PMatrix * MVMatrix * vec3(p1 + Thickness * n0, 1.0)).xy, 0, 1);
				EmitVertex();
				gl_Position = vec4((PMatrix * MVMatrix * vec3(p1 + Thickness * n1, 1.0)).xy, 0, 1);
				EmitVertex();
				gl_Position = vec4((PMatrix * MVMatrix * vec3(p1, 1.0)).xy, 0, 1);
				EmitVertex();
				EndPrimitive();
			}
		}
		else
		{
			if(ThicknessInPixels)
			{
				gl_Position = vec4((PMatrix * vec3(p1 - Thickness * n1, 1.0)).xy, 0, 1);
				EmitVertex();
				gl_Position = vec4((PMatrix * vec3(p1 - Thickness * n0, 1.0)).xy, 0, 1);
				EmitVertex();
				gl_Position = vec4((PMatrix * vec3(p1, 1.0)).xy, 0, 1);
				EmitVertex();
				EndPrimitive();
			}
			else
			{
				gl_Position = vec4((PMatrix * MVMatrix * vec3(p1 - Thickness * n1, 1.0)).xy, 0, 1);
				EmitVertex();
				gl_Position = vec4((PMatrix * MVMatrix * vec3(p1 - Thickness * n0, 1.0)).xy, 0, 1);
				EmitVertex();
				gl_Position = vec4((PMatrix * MVMatrix * vec3(p1, 1.0)).xy, 0, 1);
				EmitVertex();
				EndPrimitive();
			}
		}
	}

	if (dot(v1, v2) < -MiterLimit)
	{
		miter_b = n1;
		length_b = Thickness;
	}

	// generate the triangle strip   
	if(ThicknessInPixels)
	{                
		gl_Position = vec4((PMatrix * vec3(p1 + length_a * miter_a, 1.0)).xy, 0, 1);
		EmitVertex();
		gl_Position = vec4((PMatrix * vec3(p1 - length_a * miter_a, 1.0)).xy, 0, 1);
		EmitVertex();
		gl_Position = vec4((PMatrix * vec3(p2 + length_b * miter_b, 1.0)).xy, 0, 1);
		EmitVertex();
		gl_Position = vec4((PMatrix * vec3(p2 - length_b * miter_b, 1.0)).xy, 0, 1);
		EmitVertex();

		EndPrimitive();
	}
	else
	{	
		gl_Position = vec4((PMatrix * MVMatrix * vec3(p1 + length_a * miter_a, 1.0)).xy, 0, 1);
		EmitVertex();
		gl_Position = vec4((PMatrix * MVMatrix * vec3(p1 - length_a * miter_a, 1.0)).xy, 0, 1);
		EmitVertex();
		gl_Position = vec4((PMatrix * MVMatrix * vec3(p2 + length_b * miter_b, 1.0)).xy, 0, 1);
		EmitVertex();
		gl_Position = vec4((PMatrix * MVMatrix * vec3(p2 - length_b * miter_b, 1.0)).xy, 0, 1);
		EmitVertex();

		EndPrimitive();
	}
}