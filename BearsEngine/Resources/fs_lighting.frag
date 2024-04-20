#version 430

uniform sampler2D Sampler;
uniform vec4 AmbientLightColour;
uniform float Gamma;

uniform struct LightInfo
{
	vec2 Pos;
	vec4 Colour;
	float Radius;
	float CutoffRadius;
} Lights[25];

uniform mat3 InverseSourceMVMatrix;

layout(origin_upper_left) in vec4 gl_FragCoord;	//No idea why, but couldn't get the amtrices to work the right way up. This makes y increase fromt he screen top like it should... fixing that. Redclaring built-in variable with the layout args



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


vec4 GetLightAtPos(vec2 pos, vec2 lightPos, vec4 lightColour, float radius, float cutoffRadius)
{
	float dist = DistanceBetween(pos, lightPos);

	//Return no light if further away than cutoff radius
	if(dist > cutoffRadius)
		return vec4(0.0);

	//Note - radius is a characteristic dropoff radius, gives gradient of curve basically. Will stills tart at intensity 1 at r=0, drop to zero at cutoff radius

	//Precompute a couple of parameters. The latter could actually be made into a precomputed uniform (per light) for performance..
	float invRSq = radius * radius / ((dist + radius) * (dist + radius));
	float invRCutoffSq = radius* radius / ((cutoffRadius + radius) * (cutoffRadius + radius));

	//Calculate relative light strength at this radius
	float intensity = invRSq / (1 - invRCutoffSq) - invRCutoffSq / ( 1 - invRCutoffSq);
	
	return lightColour * min(1.0, intensity);	//min check shouldnt really be required...
}

vec4 SumLights(vec2 pos)
{
	vec4 resultColour = AmbientLightColour;

	for (int i=0; i< Lights.length(); i++)
		if(Lights[i].Colour.w > 0)	//Exclude null or non-instantiated values which will have alpha of zero
			resultColour = resultColour + GetLightAtPos(pos, Lights[i].Pos, Lights[i].Colour, Lights[i].Radius,  Lights[i].CutoffRadius);

	return vec4(min(resultColour.x,1.0), min(resultColour.y,1.0), min(resultColour.z,1.0), min(resultColour.w,1.0));
}

void main()
{
	vec2 fragPos = gl_FragCoord.xy;
	vec2 gameCoordFragPos = (InverseSourceMVMatrix * vec3(fragPos,1)).xy;

	Colour = texture(Sampler, Input_TexCoord.TexCoord) * Input_Colour.Colour * SumLights(gameCoordFragPos);
	
	//Alpha test
	if (Colour.a <= 0)
		discard;

	//Gamma correction
	Colour = vec4(pow(Colour.xyz, vec3(1.0/Gamma)), Colour.w);

}