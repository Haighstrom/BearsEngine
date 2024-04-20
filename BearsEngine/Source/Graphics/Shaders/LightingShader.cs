using BearsEngine.OpenGL;

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
    private static int _ID;
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
        _ID = OpenGLHelper.CreateShader(Resources.Shaders.vs_default, Resources.Shaders.fs_lighting);
        OpenGLHelper.BindShader(_ID);
        _locationMVMatrix = OpenGL32.glGetUniformLocation(_ID, "MVMatrix");
        _locationPMatrix = OpenGL32.glGetUniformLocation(_ID, "PMatrix");
        _locationPosition = OpenGL32.glGetAttribLocation(_ID, "Position");
        _locationColour = OpenGL32.glGetAttribLocation(_ID, "Colour");
        _locationTexture = OpenGL32.glGetAttribLocation(_ID, "TexCoord");

        _locationGammaUniform = OpenGL32.glGetUniformLocation(_ID, "Gamma");

        _locationSourceInvMVMatrixUniform = OpenGL32.glGetUniformLocation(_ID, "InverseSourceMVMatrix");
        _locationAmbientLightColourUniform = OpenGL32.glGetUniformLocation(_ID, "AmbientLightColour");

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
            _locationLights_PosUniformArray[i] = OpenGL32.glGetUniformLocation(_ID, string.Format("Lights[{0}].Pos", i));
            _locationLights_ColourUniformArray[i] = OpenGL32.glGetUniformLocation(_ID, string.Format("Lights[{0}].Colour", i));
            _locationLights_RadiusUniformArray[i] = OpenGL32.glGetUniformLocation(_ID, string.Format("Lights[{0}].Radius", i));
            _locationLights_CutoffRadiusUniformArray[i] = OpenGL32.glGetUniformLocation(_ID, string.Format("Lights[{0}].CutoffRadius", i));
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
    
    

    public void Render(ref Matrix3 projection, ref Matrix3 modelView, int verticesLength, PRIMITIVE_TYPE drawType)
    {
        OpenGLHelper.BindShader(_ID);

        OpenGLHelper.UniformMatrix3(_locationMVMatrix, modelView);
        OpenGLHelper.UniformMatrix3(_locationPMatrix, projection);

        OpenGL32.glEnableVertexAttribArray(_locationPosition);
        OpenGL32.glVertexAttribPointer(_locationPosition, 2, VERTEX_DATA_TYPE.GL_FLOAT, false, Vertex.STRIDE, 0);

        OpenGL32.glEnableVertexAttribArray(_locationColour);
        OpenGL32.glVertexAttribPointer(_locationColour, 4, VERTEX_DATA_TYPE.GL_UNSIGNED_BYTE, true, Vertex.STRIDE, 8);

        OpenGL32.glEnableVertexAttribArray(_locationTexture);
        OpenGL32.glVertexAttribPointer(_locationTexture, 2, VERTEX_DATA_TYPE.GL_FLOAT, false, Vertex.STRIDE, 12);

        OpenGL32.glUniform1f(_locationGammaUniform, Gamma);

        OpenGL32.glUniform4f(_locationAmbientLightColourUniform, AmbientLightColour.R / 255f, AmbientLightColour.G / 255f, AmbientLightColour.B / 255f, AmbientLightColour.A / 255f);
        OpenGLHelper.UniformMatrix3(_locationSourceInvMVMatrixUniform, mdlMatrix.Inverse());

        BindLightsArrayData();

        OpenGL32.glDrawArrays(drawType, 0, verticesLength);

        OpenGL32.glDisableVertexAttribArray(_locationPosition);
        OpenGL32.glDisableVertexAttribArray(_locationColour);
        OpenGL32.glDisableVertexAttribArray(_locationTexture);
    }
    

    private static void BindLightsArrayData()
    {
        for (int i = 0; i < _lights.Count; i++)
        {
            OpenGL32.glUniform2f(_locationLights_PosUniformArray[i], _lights[i].Position.X, _lights[i].Position.Y);

            OpenGL32.glUniform4f(_locationLights_ColourUniformArray[i], _lights[i].Colour.R / 255f, _lights[i].Colour.G / 255f, _lights[i].Colour.B / 255f, _lights[i].Colour.A / 255f);
            OpenGL32.glUniform1f(_locationLights_RadiusUniformArray[i], _lights[i].Radius);
            OpenGL32.glUniform1f(_locationLights_CutoffRadiusUniformArray[i], _lights[i].CutoffRadius);
        }
    }
    
    
}
