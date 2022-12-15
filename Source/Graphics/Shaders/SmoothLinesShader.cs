using HaighFramework.OpenGL;
using HaighFramework.WinAPI;

namespace BearsEngine.Graphics.Shaders;

public class SmoothLinesShader : IShader
{
    private static bool _initialised = false;
    private static uint _ID;
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
        _locationMVMatrix = OpenGL32.GetUniformLocation(_ID, "MVMatrix");
        _locationPMatrix = OpenGL32.GetUniformLocation(_ID, "PMatrix");
        _locationPosition = OpenGL32.GetAttribLocation(_ID, "Position");
        _locationColour = OpenGL32.GetAttribLocation(_ID, "Colour");
        _locationThicknessUniform = OpenGL32.GetUniformLocation(_ID, "Thickness");
        _locationThicknessInPixelsUniform = OpenGL32.GetUniformLocation(_ID, "ThicknessInPixels");
        _initialised = true;
    }
    

    public SmoothLinesShader(float thickness, bool thicknessInPixels)
    {
        if (!_initialised)
            Initialise();
        Thickness = thickness;
        ThicknessInPixels = thicknessInPixels;
    }
    

    public void Render(ref Matrix4 projection, ref Matrix4 modelView, int verticesLength, PRIMITIVE_TYPE drawType)
    {
        if (drawType != PRIMITIVE_TYPE.GL_LINE_STRIP_ADJACENCY)
            Log.Warning("Smooth lines shader is being used with PrimitiveType " + drawType + "instead of LineStripAdjacency.");

        if (_ID != OpenGL.LastBoundShader)
            HF.Graphics.BindShader(_ID);

        OpenGL32.UniformMatrix4(_locationMVMatrix, 1, false, modelView.Values);
        OpenGL32.UniformMatrix4(_locationPMatrix, 1, false, projection.Values);

        OpenGL32.Uniform(_locationThicknessUniform, Thickness);
        OpenGL32.Uniform(_locationThicknessInPixelsUniform, ThicknessInPixels.ParseTo<int>());

        OpenGL32.EnableVertexAttribArray(_locationPosition);
        OpenGL32.VertexAttribPointer(_locationPosition, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 0);

        OpenGL32.EnableVertexAttribArray(_locationColour);
        OpenGL32.VertexAttribPointer(_locationColour, 4, VertexAttribPointerType.UnsignedByte, true, Vertex.STRIDE, 8);

        OpenGL32.glDrawArrays(drawType, 0, verticesLength);

        OpenGL32.DisableVertexAttribArray(_locationPosition);
        OpenGL32.DisableVertexAttribArray(_locationColour);
    }
    
    

    public float Thickness { get; set; }

    public bool ThicknessInPixels { get; set; }
}
