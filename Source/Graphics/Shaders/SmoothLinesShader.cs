using HaighFramework.OpenGL;
using HaighFramework.WinAPI;

namespace BearsEngine.Graphics.Shaders;

public class SmoothLinesShader : IShader
{
    private static bool _initialised = false;
    private static int _ID;
    private static int _locationMVMatrix;
    private static int _locationPMatrix;
    private static int _locationPosition;
    private static int _locationColour;
    private static int _locationThicknessUniform;
    private static int _locationThicknessInPixelsUniform;

    private static void Initialise()
    {
        _ID = HF.Graphics.CreateShader(Resources.Shaders.vs_nomatrixsolidcolour, Resources.Shaders.gs_smoothlines, Resources.Shaders.fs_solidcolour);
        HF.Graphics.BindShader(_ID);
        _locationMVMatrix = OpenGL32.glGetUniformLocation(_ID, "MVMatrix");
        _locationPMatrix = OpenGL32.glGetUniformLocation(_ID, "PMatrix");
        _locationPosition = OpenGL32.glGetAttribLocation(_ID, "Position");
        _locationColour = OpenGL32.glGetAttribLocation(_ID, "Colour");
        _locationThicknessUniform = OpenGL32.glGetUniformLocation(_ID, "Thickness");
        _locationThicknessInPixelsUniform = OpenGL32.glGetUniformLocation(_ID, "ThicknessInPixels");
        _initialised = true;
    }
    

    public SmoothLinesShader(float thickness, bool thicknessInPixels)
    {
        if (!_initialised)
            Initialise();
        Thickness = thickness;
        ThicknessInPixels = thicknessInPixels;
    }
    

    public void Render(ref Matrix3 projection, ref Matrix3 modelView, int verticesLength, PRIMITIVE_TYPE drawType)
    {
        if (drawType != PRIMITIVE_TYPE.GL_LINE_STRIP_ADJACENCY)
            Log.Warning("Smooth lines shader is being used with PrimitiveType " + drawType + "instead of LineStripAdjacency.");

        if (_ID != OpenGL.LastBoundShader)
            HF.Graphics.BindShader(_ID);

        OpenGL.UniformMatrix3(_locationMVMatrix, modelView);
        OpenGL.UniformMatrix3(_locationPMatrix, projection);

        OpenGL32.glUniform1f(_locationThicknessUniform, Thickness);
        OpenGL32.glUniform1i(_locationThicknessInPixelsUniform, ThicknessInPixels.ParseTo<int>());

        OpenGL32.glEnableVertexAttribArray(_locationPosition);
        OpenGL32.glVertexAttribPointer(_locationPosition, 2, VERTEX_DATA_TYPE.GL_FLOAT, false, Vertex.STRIDE, 0);

        OpenGL32.glEnableVertexAttribArray(_locationColour);
        OpenGL32.glVertexAttribPointer(_locationColour, 4, VERTEX_DATA_TYPE.GL_UNSIGNED_BYTE, true, Vertex.STRIDE, 8);

        OpenGL32.glDrawArrays(drawType, 0, verticesLength);

        OpenGL32.glDisableVertexAttribArray(_locationPosition);
        OpenGL32.glDisableVertexAttribArray(_locationColour);
    }
    
    

    public float Thickness { get; set; }

    public bool ThicknessInPixels { get; set; }
}
