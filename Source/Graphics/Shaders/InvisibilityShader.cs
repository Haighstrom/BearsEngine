using HaighFramework.OpenGL;
using HaighFramework.WinAPI;

namespace BearsEngine.Graphics.Shaders;

public class InvisibilityShader : IShader
{
    /// <summary>
    /// This shader reveals bjects around a single defined centre point, common to all instances. Make sure to set this each render call.
    /// </summary>
    public static Point Centre { get; set; } = new Point();
    public static float Radius = 2f;
    public static float InnerRadius = 1.5f;
    public static Matrix3 mdlMatrix = Matrix3.Identity;

    private static bool _initialised = false;
    private static int _ID;
    private static int _locationMVMatrix;
    private static int _locationPMatrix;
    private static int _locationPosition;
    private static int _locationColour;
    private static int _locationTexture;

    private static int _locationRadiusUniform;
    private static int _locationInnerRadiusUniform;
    private static int _locationSourcePositionUniform;
    private static int _locationSourceInvMVMatrixUniform;

    private static void Initialise()
    {
        _ID = HF.Graphics.CreateShader(Resources.Shaders.vs_default, Resources.Shaders.fs_invisibility);
        HF.Graphics.BindShader(_ID);
        _locationMVMatrix = OpenGL32.glGetUniformLocation(_ID, "MVMatrix");
        _locationPMatrix = OpenGL32.glGetUniformLocation(_ID, "PMatrix");
        _locationPosition = OpenGL32.glGetAttribLocation(_ID, "Position");
        _locationColour = OpenGL32.glGetAttribLocation(_ID, "Colour");
        _locationTexture = OpenGL32.glGetAttribLocation(_ID, "TexCoord");

        _locationRadiusUniform = OpenGL32.glGetUniformLocation(_ID, "radius");
        _locationInnerRadiusUniform = OpenGL32.glGetUniformLocation(_ID, "innerRadius");
        _locationSourcePositionUniform = OpenGL32.glGetUniformLocation(_ID, "source");

        _locationSourceInvMVMatrixUniform = OpenGL32.glGetUniformLocation(_ID, "InverseSourceMVMatrix");

        _initialised = true;
    }
    

    public InvisibilityShader()
    {
        if (!_initialised)
            Initialise();
    }
    

    public void Render(ref Matrix3 projection, ref Matrix3 modelView, int verticesLength, PRIMITIVE_TYPE drawType)
    {
        HF.Graphics.BindShader(_ID);

        OpenGL.UniformMatrix3(_locationMVMatrix, modelView);
        OpenGL.UniformMatrix3(_locationPMatrix, projection);

        OpenGL32.glEnableVertexAttribArray(_locationPosition);
        OpenGL32.glVertexAttribPointer(_locationPosition, 2, VERTEX_DATA_TYPE.GL_FLOAT, false, Vertex.STRIDE, 0);

        OpenGL32.glEnableVertexAttribArray(_locationColour);
        OpenGL32.glVertexAttribPointer(_locationColour, 4, VERTEX_DATA_TYPE.GL_UNSIGNED_BYTE, true, Vertex.STRIDE, 8);

        OpenGL32.glEnableVertexAttribArray(_locationTexture);
        OpenGL32.glVertexAttribPointer(_locationTexture, 2, VERTEX_DATA_TYPE.GL_FLOAT, false, Vertex.STRIDE, 12);


        OpenGL32.glUniform1f(_locationRadiusUniform, Radius);
        OpenGL32.glUniform1f(_locationInnerRadiusUniform, InnerRadius);
        OpenGL32.glUniform2f(_locationSourcePositionUniform, Centre.X, Centre.Y);
        OpenGL.UniformMatrix3(_locationSourceInvMVMatrixUniform, mdlMatrix.Inverse());


        OpenGL32.glDrawArrays(drawType, 0, verticesLength);

        OpenGL32.glDisableVertexAttribArray(_locationPosition);
        OpenGL32.glDisableVertexAttribArray(_locationColour);
        OpenGL32.glDisableVertexAttribArray(_locationTexture);
    }
    
    
}
