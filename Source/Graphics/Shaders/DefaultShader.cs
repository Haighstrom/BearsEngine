using HaighFramework.OpenGL4;

namespace BearsEngine.Graphics.Shaders;

public class DefaultShader : IShader
{
    #region Static
    private static bool _initialised = false;
    private static uint _ID;
    private static int _locationMVMatrix;
    private static int _locationPMatrix;
    private static int _locationPosition;
    private static int _locationColour;
    private static int _locationTexture;

    private static void Initialise()
    {
        _ID = HF.Graphics.CreateShader(Resources.Shaders.vs_default, Resources.Shaders.fs_default);
        HF.Graphics.BindShader(_ID);
        _locationMVMatrix = OpenGL.GetUniformLocation(_ID, "MVMatrix");
        _locationPMatrix = OpenGL.GetUniformLocation(_ID, "PMatrix");
        _locationPosition = OpenGL.GetAttribLocation(_ID, "Position");
        _locationColour = OpenGL.GetAttribLocation(_ID, "Colour");
        _locationTexture = OpenGL.GetAttribLocation(_ID, "TexCoord");
        _initialised = true;
    }
    #endregion

    #region Constructors
    public DefaultShader()
    {
        if (!_initialised)
            Initialise();
    }
    #endregion

    #region IShader
    #region Render
    public void Render(ref Matrix4 projection, ref Matrix4 modelView, int verticesLength, PrimitiveType drawType)
    {
        if (_ID != HV.LastBoundShader)
            HF.Graphics.BindShader(_ID);

        OpenGL.UniformMatrix4(_locationMVMatrix, 1, false, modelView.Values);
        OpenGL.UniformMatrix4(_locationPMatrix, 1, false, projection.Values);

        OpenGL.EnableVertexAttribArray(_locationPosition);
        OpenGL.VertexAttribPointer(_locationPosition, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 0);

        OpenGL.EnableVertexAttribArray(_locationColour);
        OpenGL.VertexAttribPointer(_locationColour, 4, VertexAttribPointerType.UnsignedByte, true, Vertex.STRIDE, 8);

        OpenGL.EnableVertexAttribArray(_locationTexture);
        OpenGL.VertexAttribPointer(_locationTexture, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 12);

        OpenGL.DrawArrays(drawType, 0, verticesLength);

        OpenGL.DisableVertexAttribArray(_locationPosition);
        OpenGL.DisableVertexAttribArray(_locationColour);
        OpenGL.DisableVertexAttribArray(_locationTexture);
    }
    #endregion
    #endregion
}
