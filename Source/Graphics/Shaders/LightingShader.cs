using BearsEngine.Win32API;

namespace BearsEngine.Graphics.Shaders;

public class LightInfo
{
    public Point Position;
    public Colour Colour;
    public float Radius;
    public float CutoffRadius;
}


public class LightingShader : IShader
{
    private const int MAX_LIGHTS = 25;

    private static bool _initialised = false;
    private static uint _ID;
    private static int _locationMVMatrix;
    private static int _locationPMatrix;
    private static int _locationPosition;
    private static int _locationColour;
    private static int _locationTexture;

    private static int _locationGammaUniform;

    private static int _locationSourceInvMVMatrixUniform;
    private static int _locationAmbientLightColourUniform;

    private static int[] _locationLights_PosUniformArray;
    private static int[] _locationLights_ColourUniformArray;
    private static int[] _locationLights_RadiusUniformArray;
    private static int[] _locationLights_CutoffRadiusUniformArray;

    private static readonly List<LightInfo> _lights = new();
    
    

    public LightingShader()
    {
        if (!_initialised)
            Initialise();
    }
    

    public static Matrix3 mdlMatrix = Matrix3.Identity;
    public static Colour AmbientLightColour { get; set; } = Colour.Black;
    public static float Gamma { get; set; } = 1f;
    

    private static void Initialise()
    {
        _ID = HF.Graphics.CreateShader(Resources.Shaders.vs_default, Resources.Shaders.fs_lighting);
        HF.Graphics.BindShader(_ID);
        _locationMVMatrix = OpenGL32.GetUniformLocation(_ID, "MVMatrix");
        _locationPMatrix = OpenGL32.GetUniformLocation(_ID, "PMatrix");
        _locationPosition = OpenGL32.GetAttribLocation(_ID, "Position");
        _locationColour = OpenGL32.GetAttribLocation(_ID, "Colour");
        _locationTexture = OpenGL32.GetAttribLocation(_ID, "TexCoord");

        _locationGammaUniform = OpenGL32.GetUniformLocation(_ID, "Gamma");

        _locationSourceInvMVMatrixUniform = OpenGL32.GetUniformLocation(_ID, "InverseSourceMVMatrix");
        _locationAmbientLightColourUniform = OpenGL32.GetUniformLocation(_ID, "AmbientLightColour");

        LoadLightsArrayLocations();

        _initialised = true;
    }
    

    private static void LoadLightsArrayLocations()
    {
        _locationLights_PosUniformArray = new int[MAX_LIGHTS];
        _locationLights_ColourUniformArray = new int[MAX_LIGHTS];
        _locationLights_RadiusUniformArray = new int[MAX_LIGHTS];
        _locationLights_CutoffRadiusUniformArray = new int[MAX_LIGHTS];

        for (int i = 0; i < MAX_LIGHTS; i++)
        {
            _locationLights_PosUniformArray[i] = OpenGL32.GetUniformLocation(_ID, string.Format("Lights[{0}].Pos", i));
            _locationLights_ColourUniformArray[i] = OpenGL32.GetUniformLocation(_ID, string.Format("Lights[{0}].Colour", i));
            _locationLights_RadiusUniformArray[i] = OpenGL32.GetUniformLocation(_ID, string.Format("Lights[{0}].Radius", i));
            _locationLights_CutoffRadiusUniformArray[i] = OpenGL32.GetUniformLocation(_ID, string.Format("Lights[{0}].CutoffRadius", i));
        }
    }
    

    public static void AddLight(LightInfo light)
    {
        if (_lights.Contains(light))
            throw new Exception($"Light {light} was already added to Lighting Shader.");

        _lights.Add(light);
    }
    

    public static void RemoveLight(LightInfo light)
    {
        if (!_lights.Contains(light))
            throw new Exception($"Light {light} was not added to Lighting Shader.");

        _lights.Remove(light);
    }
    

    public static void ClearLights() => _lights.Clear();
    
    

    public void Render(ref Matrix4 projection, ref Matrix4 modelView, int verticesLength, PRIMITIVEMODE drawType)
    {
        HF.Graphics.BindShader(_ID);

        OpenGL32.UniformMatrix4(_locationMVMatrix, 1, false, modelView.Values);
        OpenGL32.UniformMatrix4(_locationPMatrix, 1, false, projection.Values);

        OpenGL32.EnableVertexAttribArray(_locationPosition);
        OpenGL32.VertexAttribPointer(_locationPosition, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 0);

        OpenGL32.EnableVertexAttribArray(_locationColour);
        OpenGL32.VertexAttribPointer(_locationColour, 4, VertexAttribPointerType.UnsignedByte, true, Vertex.STRIDE, 8);

        OpenGL32.EnableVertexAttribArray(_locationTexture);
        OpenGL32.VertexAttribPointer(_locationTexture, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 12);

        OpenGL32.Uniform(_locationGammaUniform, Gamma);

        OpenGL32.Uniform4(_locationAmbientLightColourUniform, AmbientLightColour);
        OpenGL32.UniformMatrix3(_locationSourceInvMVMatrixUniform, 1, false, mdlMatrix.Inverse().Values);

        BindLightsArrayData();

        OpenGL32.glDrawArrays(drawType, 0, verticesLength);

        OpenGL32.DisableVertexAttribArray(_locationPosition);
        OpenGL32.DisableVertexAttribArray(_locationColour);
        OpenGL32.DisableVertexAttribArray(_locationTexture);
    }
    

    private static void BindLightsArrayData()
    {
        for (int i = 0; i < _lights.Count; i++)
        {
            OpenGL32.Uniform2(_locationLights_PosUniformArray[i], _lights[i].Position);
            OpenGL32.Uniform4(_locationLights_ColourUniformArray[i], _lights[i].Colour);
            OpenGL32.Uniform(_locationLights_RadiusUniformArray[i], _lights[i].Radius);
            OpenGL32.Uniform(_locationLights_CutoffRadiusUniformArray[i], _lights[i].CutoffRadius);
        }
    }
    
    
}
