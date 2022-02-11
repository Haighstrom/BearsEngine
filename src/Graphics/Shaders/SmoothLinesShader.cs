using HaighFramework;
using HaighFramework.OpenGL4;

namespace BearsEngine.Graphics
{
    public class SmoothLinesShader : IShader
    {
        #region Static
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
            _ID = HF.Graphics.CreateShader(Shaders.vs_nomatrixsolidcolour, Shaders.gs_smoothlines, Shaders.fs_solidcolour);
            HF.Graphics.BindShader(_ID);
            _locationMVMatrix = OpenGL.GetUniformLocation(_ID, "MVMatrix");
            _locationPMatrix = OpenGL.GetUniformLocation(_ID, "PMatrix");
            _locationPosition = OpenGL.GetAttribLocation(_ID, "Position");
            _locationColour = OpenGL.GetAttribLocation(_ID, "Colour");
            _locationThicknessUniform = OpenGL.GetUniformLocation(_ID, "Thickness");
            _locationThicknessInPixelsUniform = OpenGL.GetUniformLocation(_ID, "ThicknessInPixels");
            _initialised = true;
        }
        #endregion

        #region Constructors
        public SmoothLinesShader(float thickness, bool thicknessInPixels)
        {
            if (!_initialised)
                Initialise();
            Thickness = thickness;
            ThicknessInPixels = thicknessInPixels;
        }
        #endregion

        #region IShader
        #region Render
        public void Render(ref Matrix4 projection, ref Matrix4 modelView, int verticesLength, PrimitiveType drawType)
        {
            if (drawType != PrimitiveType.LineStripAdjacency)
                HConsole.Warning("Smooth lines shader is being used with PrimitiveType " + drawType + "instead of LineStripAdjacency.");

            if (_ID != HV.LastBoundShader)
                HF.Graphics.BindShader(_ID);

            OpenGL.UniformMatrix4(_locationMVMatrix, 1, false, modelView.Values);
            OpenGL.UniformMatrix4(_locationPMatrix, 1, false, projection.Values);

            OpenGL.Uniform(_locationThicknessUniform, Thickness);
            OpenGL.Uniform(_locationThicknessInPixelsUniform, ThicknessInPixels.ParseTo<int>());

            OpenGL.EnableVertexAttribArray(_locationPosition);
            OpenGL.VertexAttribPointer(_locationPosition, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 0);

            OpenGL.EnableVertexAttribArray(_locationColour);
            OpenGL.VertexAttribPointer(_locationColour, 4, VertexAttribPointerType.UnsignedByte, true, Vertex.STRIDE, 8);

            OpenGL.DrawArrays(drawType, 0, verticesLength);

            OpenGL.DisableVertexAttribArray(_locationPosition);
            OpenGL.DisableVertexAttribArray(_locationColour);
        }
        #endregion
        #endregion

        public float Thickness { get; set; }

        public bool ThicknessInPixels { get; set; }
    }
}