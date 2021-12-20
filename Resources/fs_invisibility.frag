// Fragment shader for invisible objects in Haigh's mushroom game. only draws pixels within a range of a point.
#version 430

uniform sampler2D Sampler;
uniform float radius;    //Radius (in pixels) within which things will be drawn
uniform float innerRadius;    //Inner cutoff Radius (in pixels) within which things will be drawn - there will be a gradual cutoff between this and radius.
uniform vec2 source;	//Point to reveal around

uniform mat3 InverseSourceMVMatrix;

in ColourData
{
	vec4 Colour;
} Input_Colour;

in TexCoordData
{
	vec2 TexCoord;
} Input_TexCoord;

out vec4 Colour;

float DistanceBetween(vec2 pos1, vec2 pos2)
{
	vec2 d = (pos1 - pos2);
	return sqrt(d.x * d.x + d.y * d.y);
}

void main()
{
	vec2 fragPos = gl_FragCoord.xy;
	vec2 gameCoordFragPos = (InverseSourceMVMatrix * vec3(fragPos,1)).xy;
	float dist = DistanceBetween(gameCoordFragPos, source);

	if(dist > radius)
		discard;

	vec2 texSize = textureSize(Sampler, 0);
	vec2 pixelOffset = vec2(0.5/texSize.x, 0.5/texSize.y);

	Colour = texture(Sampler, Input_TexCoord.TexCoord + pixelOffset) * Input_Colour.Colour;

	vec4 transpColour = vec4(Colour.xyz, 0);									//Transparent version of the colour
	Colour = mix(Colour, transpColour, smoothstep(innerRadius, radius, dist));	//Ramp alpha down smoothly between innerradius and radius
	Colour = vec4(Colour.xyz * Colour.w, Colour.w);								//premultiply alpha
	
	//Alpha test
	if (Colour.a <= 0)
		discard;
}