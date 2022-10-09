using BearsEngine.Win32API;

namespace BearsEngine.Graphics.Shaders;

public class DarkenShader : IShader
{
    private static bool _initialised = false;
    private static uint _ID;
    private static int _locationMVMatrix;
    private static int _locationPMatrix;
    private static int _locationPosition;
    private static int _locationColour;
    private static int _locationTexture;
    private static int _locationDarkenValue;

    private static void Initialise()
    {
        _ID = HF.Graphics.CreateShader(Resources.Shaders.vs_default, Resources.Shaders.fs_darken);
        HF.Graphics.BindShader(_ID);
        _locationMVMatrix = OpenGL32.GetUniformLocation(_ID, "MVMatrix");
        _locationPMatrix = OpenGL32.GetUniformLocation(_ID, "PMatrix");
        _locationPosition = OpenGL32.GetAttribLocation(_ID, "Position");
        _locationColour = OpenGL32.GetAttribLocation(_ID, "Colour");
        _locationTexture = OpenGL32.GetAttribLocation(_ID, "TexCoord");
        _locationDarkenValue = OpenGL32.GetUniformLocation(_ID, "DarkenValue");
        _initialised = true;
    }
    

    public DarkenShader()
    {
        if (!_initialised)
            Initialise();
    }
    

    /// <summary>
    /// 0-1 (0 = black, 1 = normal colour)
    /// </summary>
    public static float DarkenValue { get; set; } = 1f;

    public void Render(ref Matrix4 projection, ref Matrix4 modelView, int verticesLength, PrimitiveType drawType)
    {
        if (_ID != BE.LastBoundShader)
            HF.Graphics.BindShader(_ID);

        OpenGL32.Uniform(_locationDarkenValue, DarkenValue);

        OpenGL32.UniformMatrix4(_locationMVMatrix, 1, false, modelView.Values);
        OpenGL32.UniformMatrix4(_locationPMatrix, 1, false, projection.Values);

        OpenGL32.EnableVertexAttribArray(_locationPosition);
        OpenGL32.VertexAttribPointer(_locationPosition, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 0);

        OpenGL32.EnableVertexAttribArray(_locationColour);
        OpenGL32.VertexAttribPointer(_locationColour, 4, VertexAttribPointerType.UnsignedByte, true, Vertex.STRIDE, 8);

        OpenGL32.EnableVertexAttribArray(_locationTexture);
        OpenGL32.VertexAttribPointer(_locationTexture, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 12);

        OpenGL32.DrawArrays(drawType, 0, verticesLength);

        OpenGL32.DisableVertexAttribArray(_locationPosition);
        OpenGL32.DisableVertexAttribArray(_locationColour);
        OpenGL32.DisableVertexAttribArray(_locationTexture);
    }
    
    
}
