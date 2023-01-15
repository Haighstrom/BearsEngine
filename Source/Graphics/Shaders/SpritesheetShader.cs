using HaighFramework.OpenGL;

namespace BearsEngine.Graphics.Shaders;

public class SpritesheetShader : IShader
{
    private static bool _initialised = false;
    private static int _ID;
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
        _ID = OpenGL.CreateShader(Resources.Shaders.vs_spritesheet, Resources.Shaders.fs_default);
        OpenGL.BindShader(_ID);
        _locationMVMatrix = OpenGL32.glGetUniformLocation(_ID, "MVMatrix");
        _locationPMatrix = OpenGL32.glGetUniformLocation(_ID, "PMatrix");
        _locationPosition = OpenGL32.glGetAttribLocation(_ID, "Position");
        _locationColour = OpenGL32.glGetAttribLocation(_ID, "Colour");
        _locationTexture = OpenGL32.glGetAttribLocation(_ID, "TexCoord");
        _locationXIndex = OpenGL32.glGetUniformLocation(_ID, "XIndex");
        _locationYIndex = OpenGL32.glGetUniformLocation(_ID, "YIndex");
        _locationTileW = OpenGL32.glGetUniformLocation(_ID, "TileW");
        _locationTileH = OpenGL32.glGetUniformLocation(_ID, "TileH");
        _initialised = true;
    }
    

    public SpritesheetShader(float tileW, float tileH)
    {
        if (!_initialised)
            Initialise();

        TileW = tileW;
        TileH = tileH;
    }
    

    public void Render(ref Matrix3 projection, ref Matrix3 modelView, int verticesLength, PRIMITIVE_TYPE drawType)
    {
        if (_ID != OpenGL.LastBoundShader)
            OpenGL.BindShader(_ID);

        OpenGL.UniformMatrix3(_locationMVMatrix, modelView);
        OpenGL.UniformMatrix3(_locationPMatrix, projection);

        OpenGL32.glUniform1i(_locationXIndex, IndexX);
        OpenGL32.glUniform1i(_locationYIndex, IndexY);
        OpenGL32.glUniform1f(_locationTileW, TileW);
        OpenGL32.glUniform1f(_locationTileH, TileH);

        OpenGL32.glEnableVertexAttribArray(_locationPosition);
        OpenGL32.glVertexAttribPointer(_locationPosition, 2, VERTEX_DATA_TYPE.GL_FLOAT, false, Vertex.STRIDE, 0);

        OpenGL32.glEnableVertexAttribArray(_locationColour);
        OpenGL32.glVertexAttribPointer(_locationColour, 4, VERTEX_DATA_TYPE.GL_UNSIGNED_BYTE, true, Vertex.STRIDE, 8);

        OpenGL32.glEnableVertexAttribArray(_locationTexture);
        OpenGL32.glVertexAttribPointer(_locationTexture, 2, VERTEX_DATA_TYPE.GL_FLOAT, false, Vertex.STRIDE, 12);

        OpenGL32.glDrawArrays(drawType, 0, verticesLength);

        OpenGL32.glDisableVertexAttribArray(_locationPosition);
        OpenGL32.glDisableVertexAttribArray(_locationColour);
        OpenGL32.glDisableVertexAttribArray(_locationTexture);
    }
    
    

    public int IndexX { get; set; }

    public int IndexY { get; set; }

    public float TileW { get; set; }

    public float TileH { get; set; }
    
}
