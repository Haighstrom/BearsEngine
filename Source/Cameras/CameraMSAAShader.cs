using BearsEngine.Graphics.Shaders;
using HaighFramework.OpenGL;

namespace BearsEngine.Worlds.Cameras
{
    /// <summary>
    /// Shader used with the multisample FBO for the MSAA antialiasing pass only
    /// </summary>
    public class CameraMSAAShader : IShader
    {
        private static bool _initialised = false;
        private static int _ID;
        private static int _locationMVMatrix;
        private static int _locationPMatrix;
        private static int _locationPosition;
        private static int _locationTexture;
        private static int _locationSamplesUniform;

        private static void Initialise()
        {
            _ID = HF.Graphics.CreateShader(Resources.Shaders.vs_camera_msaa, Resources.Shaders.fs_cameraMSAA);
            HF.Graphics.BindShader(_ID);
            _locationMVMatrix = OpenGL32.glGetUniformLocation(_ID, "MVMatrix");
            _locationPMatrix = OpenGL32.glGetUniformLocation(_ID, "PMatrix");
            _locationPosition = OpenGL32.glGetAttribLocation(_ID, "Position");
            _locationTexture = OpenGL32.glGetAttribLocation(_ID, "TexCoord");
            _locationSamplesUniform = OpenGL32.glGetUniformLocation(_ID, "MSAASamples");
            _initialised = true;
        }
        

        public CameraMSAAShader()
        {
            if (!_initialised)
                Initialise();
        }
        

        public MSAA_SAMPLES Samples { get; set; }

        public void Render(ref Matrix3 projection, ref Matrix3 modelView, int verticesLength, PRIMITIVE_TYPE drawType)
        {
            HF.Graphics.BindShader(_ID);

            OpenGL.UniformMatrix3(_locationMVMatrix, modelView);
            OpenGL.UniformMatrix3(_locationPMatrix, projection);

            //Bind MSAA sample numbers uniform
            OpenGL32.glUniform1i(_locationSamplesUniform, (int)Samples);

            OpenGL32.glEnableVertexAttribArray(_locationPosition);
            OpenGL32.glVertexAttribPointer(_locationPosition, 2, VERTEX_DATA_TYPE.GL_FLOAT, false, Vertex.STRIDE, 0);

            OpenGL32.glEnableVertexAttribArray(_locationTexture);
            OpenGL32.glVertexAttribPointer(_locationTexture, 2, VERTEX_DATA_TYPE.GL_FLOAT, false, Vertex.STRIDE, 12);

            OpenGL32.glDrawArrays(drawType, 0, verticesLength);

            OpenGL32.glDisableVertexAttribArray(_locationPosition);
            OpenGL32.glDisableVertexAttribArray(_locationTexture);
        }
    }
}
