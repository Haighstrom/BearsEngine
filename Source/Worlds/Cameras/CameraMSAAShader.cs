using HaighFramework.OpenGL4;
using BearsEngine.Graphics.Shaders;
using BearsEngine.Graphics;

namespace BearsEngine.Worlds.Cameras
{
    /// <summary>
    /// Shader used with the multisample FBO for the MSAA antialiasing pass only
    /// </summary>
    public class CameraMSAAShader : IShader
    {
        #region Static
        private static bool _initialised = false;
        private static uint _ID;
        private static int _locationMVMatrix;
        private static int _locationPMatrix;
        private static int _locationPosition;
        private static int _locationTexture;
        private static int _locationSamplesUniform;

        private static void Initialise()
        {
            _ID = HF.Graphics.CreateShader(Resources.Shaders.vs_camera_msaa, Resources.Shaders.fs_cameraMSAA);
            HF.Graphics.BindShader(_ID);
            _locationMVMatrix = OpenGL.GetUniformLocation(_ID, "MVMatrix");
            _locationPMatrix = OpenGL.GetUniformLocation(_ID, "PMatrix");
            _locationPosition = OpenGL.GetAttribLocation(_ID, "Position");
            _locationTexture = OpenGL.GetAttribLocation(_ID, "TexCoord");
            _locationSamplesUniform = OpenGL.GetUniformLocation(_ID, "MSAASamples");
            _initialised = true;
        }
        #endregion

        #region Constructors
        public CameraMSAAShader()
        {
            if (!_initialised)
                Initialise();
        }
        #endregion

        public MSAA_Samples Samples { get; set; }

        #region IShader
        #region Render
        public void Render(ref Matrix4 projection, ref Matrix4 modelView, int verticesLength, PrimitiveType drawType)
        {
            HF.Graphics.BindShader(_ID);

            OpenGL.UniformMatrix4(_locationMVMatrix, 1, false, modelView.Values);
            OpenGL.UniformMatrix4(_locationPMatrix, 1, false, projection.Values);

            //Bind MSAA sample numbers uniform
            OpenGL.Uniform(_locationSamplesUniform, (int)Samples);

            OpenGL.EnableVertexAttribArray(_locationPosition);
            OpenGL.VertexAttribPointer(_locationPosition, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 0);

            OpenGL.EnableVertexAttribArray(_locationTexture);
            OpenGL.VertexAttribPointer(_locationTexture, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 12);

            OpenGL.DrawArrays(drawType, 0, verticesLength);

            OpenGL.DisableVertexAttribArray(_locationPosition);
            OpenGL.DisableVertexAttribArray(_locationTexture);
        }
        #endregion
        #endregion
    }
}
