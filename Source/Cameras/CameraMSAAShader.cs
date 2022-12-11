using BearsEngine.Graphics.Shaders;
using HaighFramework.OpenGL;
using HaighFramework.Win32API;

namespace BearsEngine.Worlds.Cameras
{
    /// <summary>
    /// Shader used with the multisample FBO for the MSAA antialiasing pass only
    /// </summary>
    public class CameraMSAAShader : IShader
    {
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
            _locationMVMatrix = OpenGL32.GetUniformLocation(_ID, "MVMatrix");
            _locationPMatrix = OpenGL32.GetUniformLocation(_ID, "PMatrix");
            _locationPosition = OpenGL32.GetAttribLocation(_ID, "Position");
            _locationTexture = OpenGL32.GetAttribLocation(_ID, "TexCoord");
            _locationSamplesUniform = OpenGL32.GetUniformLocation(_ID, "MSAASamples");
            _initialised = true;
        }
        

        public CameraMSAAShader()
        {
            if (!_initialised)
                Initialise();
        }
        

        public MSAA_Samples Samples { get; set; }

        public void Render(ref Matrix4 projection, ref Matrix4 modelView, int verticesLength, PRIMITIVEMODE drawType)
        {
            HF.Graphics.BindShader(_ID);

            OpenGL32.UniformMatrix4(_locationMVMatrix, 1, false, modelView.Values);
            OpenGL32.UniformMatrix4(_locationPMatrix, 1, false, projection.Values);

            //Bind MSAA sample numbers uniform
            OpenGL32.Uniform(_locationSamplesUniform, (int)Samples);

            OpenGL32.EnableVertexAttribArray(_locationPosition);
            OpenGL32.VertexAttribPointer(_locationPosition, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 0);

            OpenGL32.EnableVertexAttribArray(_locationTexture);
            OpenGL32.VertexAttribPointer(_locationTexture, 2, VertexAttribPointerType.Float, false, Vertex.STRIDE, 12);

            OpenGL32.glDrawArrays(drawType, 0, verticesLength);

            OpenGL32.DisableVertexAttribArray(_locationPosition);
            OpenGL32.DisableVertexAttribArray(_locationTexture);
        }
        
        
    }
}
