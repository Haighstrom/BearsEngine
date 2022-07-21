using BearsEngine.Win32API;

namespace BearsEngine.Graphics.Shaders;

public class SpritesheetShader : IShader
{
    #region Static
    private static bool _initialised = false;
    private static uint _ID;
    private static int _locationMVMatrix;
    private static int _locationPMatrix;
    private static int _locationPosition;
    private static int _locationColour;
    private static int _locationTexture;
    private static int _locationXIndex;
    private static int _locationYIndex;
    private static int _locationTileW;
    private static int _locationTileH;

    private static void Initialise()
    {
        _ID = HF.Graphics.CreateShader(Resources.Shaders.vs_spritesheet, Resources.Shaders.fs_default);
        HF.Graphics.BindShader(_ID);
        _locationMVMatrix = OpenGL32.GetUniformLocation(_ID, "MVMatrix");
        _locationPMatrix = OpenGL32.GetUniformLocation(_ID, "PMatrix");
        _locationPosition = OpenGL32.GetAttribLocation(_ID, "Position");
        _locationColour = OpenGL32.GetAttribLocation(_ID, "Colour");
        _locationTexture = OpenGL32.GetAttribLocation(_ID, "TexCoord");
        _locationXIndex = OpenGL32.GetUniformLocation(_ID, "XIndex");
        _locationYIndex = OpenGL32.GetUniformLocation(_ID, "YIndex");
        _locationTileW = OpenGL32.GetUniformLocation(_ID, "TileW");
        _locationTileH = OpenGL32.GetUniformLocation(_ID, "TileH");
        _initialised = true;
    }
    #endregion

    #region Constructors
    public SpritesheetShader(float tileW, float tileH)
    {
        if (!_initialised)
            Initialise();

        TileW = tileW;
        TileH = tileH;
    }
    #endregion

    #region IShader
    #region Render
    public void Render(ref Matrix4 projection, ref Matrix4 modelView, int verticesLength, PrimitiveType drawType)
    {
        if (_ID != HV.LastBoundShader)
            HF.Graphics.BindShader(_ID);

        OpenGL32.UniformMatrix4(_locationMVMatrix, 1, false, modelView.Values);
        OpenGL32.UniformMatrix4(_locationPMatrix, 1, false, projection.Values);

        OpenGL32.Uniform(_locationXIndex, IndexX);
        OpenGL32.Uniform(_locationYIndex, IndexY);
        OpenGL32.Uniform(_locationTileW, TileW);
        OpenGL32.Uniform(_locationTileH, TileH);

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
    #endregion
    #endregion

    #region Properties
    public int IndexX { get; set; }

    public int IndexY { get; set; }

    public float TileW { get; set; }

    public float TileH { get; set; }
    #endregion
}
