using HaighFramework;
using HaighFramework.OpenGL4;

namespace BearsEngine.Graphics
{
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
            _ID = HF.Graphics.CreateShader(Shaders.vs_spritesheet, Shaders.fs_default);
            HF.Graphics.BindShader(_ID);
            _locationMVMatrix = OpenGL.GetUniformLocation(_ID, "MVMatrix");
            _locationPMatrix = OpenGL.GetUniformLocation(_ID, "PMatrix");
            _locationPosition = OpenGL.GetAttribLocation(_ID, "Position");
            _locationColour = OpenGL.GetAttribLocation(_ID, "Colour");
            _locationTexture = OpenGL.GetAttribLocation(_ID, "TexCoord");
            _locationXIndex = OpenGL.GetUniformLocation(_ID, "XIndex");
            _locationYIndex = OpenGL.GetUniformLocation(_ID, "YIndex");
            _locationTileW = OpenGL.GetUniformLocation(_ID, "TileW");
            _locationTileH = OpenGL.GetUniformLocation(_ID, "TileH");
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

            OpenGL.UniformMatrix4(_locationMVMatrix, 1, false, modelView.Values);
            OpenGL.UniformMatrix4(_locationPMatrix, 1, false, projection.Values);

            OpenGL.Uniform(_locationXIndex, IndexX);
            OpenGL.Uniform(_locationYIndex, IndexY);
            OpenGL.Uniform(_locationTileW, TileW);
            OpenGL.Uniform(_locationTileH, TileH);

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

        #region Properties
        public int IndexX { get; set; }

        public int IndexY { get; set; }

        public float TileW { get; set; }

        public float TileH { get; set; }
        #endregion
    }
}