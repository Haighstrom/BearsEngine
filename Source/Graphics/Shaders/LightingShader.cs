using HaighFramework.OpenGL4;

namespace BearsEngine.Graphics.Shaders;

#region struct LightInfo
public class LightInfo
{
    public Point Position;
    public Colour Colour;
    public float Radius;
    public float CutoffRadius;
}
#endregion

public class LightingShader : IShader
{
    private const int MAX_LIGHTS = 25;

    #region Static
    #region Fields
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
    #endregion
    #endregion

    #region Constructors
    public LightingShader()
    {
        if (!_initialised)
            Initialise();
    }
    #endregion

    #region Properties
    public static Matrix3 mdlMatrix = Matrix3.Identity;
    public static Colour AmbientLightColour { get; set; } = Colour.Black;
    public static float Gamma { get; set; } = 1f;
    #endregion

    #region Methods
    #region Initialise
    private static void Initialise()
    {
        _ID = HF.Graphics.CreateShader(Resources.Shaders.vs_default, Resources.Shaders.fs_lighting);
        HF.Graphics.BindShader(_ID);
        _locationMVMatrix = OpenGL.GetUniformLocation(_ID, "MVMatrix");
        _locationPMatrix = OpenGL.GetUniformLocation(_ID, "PMatrix");
        _locationPosition = OpenGL.GetAttribLocation(_ID, "Position");
        _locationColour = OpenGL.GetAttribLocation(_ID, "Colour");
        _locationTexture = OpenGL.GetAttribLocation(_ID, "TexCoord");

        _locationGammaUniform = OpenGL.GetUniformLocation(_ID, "Gamma");

        _locationSourceInvMVMatrixUniform = OpenGL.GetUniformLocation(_ID, "InverseSourceMVMatrix");
        _locationAmbientLightColourUniform = OpenGL.GetUniformLocation(_ID, "AmbientLightColour");

        LoadLightsArrayLocations();

        _initialised = true;
    }
    #endregion

    #region LoadLightsArrayLocations
    private static void LoadLightsArrayLocations()
    {
        _locationLights_PosUniformArray = new int[MAX_LIGHTS];
        _locationLights_ColourUniformArray = new int[MAX_LIGHTS];
        _locationLights_RadiusUniformArray = new int[MAX_LIGHTS];
        _locationLights_CutoffRadiusUniformArray = new int[MAX_LIGHTS];

        for (int i = 0; i < MAX_LIGHTS; i++)
        {
            _locationLights_PosUniformArray[i] = OpenGL.GetUniformLocation(_ID, string.Format("Lights[{0}].Pos", i));
            _locationLights_ColourUniformArray[i] = OpenGL.GetUniformLocation(_ID, string.Format("Lights[{0}].Colour", i));
            _locationLights_RadiusUniformArray[i] = OpenGL.GetUniformLocation(_ID, string.Format("Lights[{0}].Radius", i));
            _locationLights_CutoffRadiusUniformArray[i] = OpenGL.GetUniformLocation(_ID, string.Format("Lights[{0}].CutoffRadius", i));
        }
    }
    #endregion

    #region AddLight
    public static void AddLight(LightInfo light)
    {
        if (_lights.Contains(light))
            throw new HException("Light {0} was already added to Lighting Shader.", light);

        _lights.Add(light);
    }
    #endregion

    #region RemoveLight
    public static void RemoveLight(LightInfo light)
    {
        if (!_lights.Contains(light))
            throw new HException("Light {0} was not added to Lighting Shader.", light);

        _lights.Remove(light);
    }
    #endregion

    #region ClearLights
    public static void ClearLights() => _lights.Clear();
    #endregion
    #endregion

    #region IShader
    #region Render
    public void Render(ref Matrix4 projection, ref Matrix4 modelView, int verticesLength, PrimitiveType drawType)
    {
        HF.Graphics.BindShader(_ID);

        OpenGL.UniformMatrix4(_locationMVMatrix, 1, false, modelView.Values);
        OpenGL.UniformMatrix4(_locationPMatrix, 1, false, projection.Values);

        OpenGL.EnableVertexAttribArray(_locationPosition);
        OpenGL.VertexAttribPointer(_locationPosition, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 0);

        OpenGL.EnableVertexAttribArray(_locationColour);
        OpenGL.VertexAttribPointer(_locationColour, 4, VertexAttribPointerType.UnsignedByte, true, Vertex.STRIDE, 8);

        OpenGL.EnableVertexAttribArray(_locationTexture);
        OpenGL.VertexAttribPointer(_locationTexture, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 12);

        OpenGL.Uniform(_locationGammaUniform, Gamma);

        OpenGL.Uniform4(_locationAmbientLightColourUniform, AmbientLightColour);
        OpenGL.UniformMatrix3(_locationSourceInvMVMatrixUniform, 1, false, mdlMatrix.Inverse().Values);

        BindLightsArrayData();

        OpenGL.DrawArrays(drawType, 0, verticesLength);

        OpenGL.DisableVertexAttribArray(_locationPosition);
        OpenGL.DisableVertexAttribArray(_locationColour);
        OpenGL.DisableVertexAttribArray(_locationTexture);
    }
    #endregion

    #region BindLightsArrayData
    private static void BindLightsArrayData()
    {
        for (int i = 0; i < _lights.Count; i++)
        {
            OpenGL.Uniform2(_locationLights_PosUniformArray[i], _lights[i].Position);
            OpenGL.Uniform4(_locationLights_ColourUniformArray[i], _lights[i].Colour);
            OpenGL.Uniform(_locationLights_RadiusUniformArray[i], _lights[i].Radius);
            OpenGL.Uniform(_locationLights_CutoffRadiusUniformArray[i], _lights[i].CutoffRadius);
        }
    }
    #endregion
    #endregion
}
