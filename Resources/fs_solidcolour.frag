#version 430

in ColourData
{
	vec4 Colour;
} Input_Colour;

out vec4 Colour;

void main()
{
	Colour = Input_Colour.Colour;	

    //Alpha test
	if(Colour.a <= 0) 
		discard;
}