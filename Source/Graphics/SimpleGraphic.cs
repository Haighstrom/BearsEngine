using BearsEngine.Graphics.Shaders;
using BearsEngine.Win32API;

namespace BearsEngine.Graphics
{
    public class SimpleGraphic : IRenderable
    {
        #region Fields
        private readonly object _syncRoot = new();
        private bool _disposed = false;
        #endregion

        #region AutoProperties
        public IShader Shader { get; }
        public uint VertexBuffer { get; }
        public Texture Texture { get; }
        public Vertex[] Vertices { get; }
        public bool Visible { get; set; } = true;
        #endregion

        #region Constructors
        public SimpleGraphic(IShader shader, Texture texture, Vertex[] vertices)
        {
            if (vertices.Length < 3)
                throw new HException("Cannot make a SimpleGraphic with fewer than 3 vertices");

            Shader = shader;
            VertexBuffer = OpenGL32.GenBuffer();
            Texture = texture;
            Vertices = vertices;

            OpenGL32.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
            OpenGL32.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * Vertex.STRIDE, Vertices, BufferUsageHint.StreamDraw);
            OpenGL32.BindBuffer(BufferTarget.ArrayBuffer, 0);
            HV.LastBoundVertexBuffer = 0;
        }
        #endregion

        #region Methods
        public void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            if (HV.LastBoundVertexBuffer != VertexBuffer)
            {
                OpenGL32.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
                HV.LastBoundVertexBuffer = VertexBuffer;
            }

            if (HV.LastBoundTexture != Texture.ID)
            {
                OpenGL32.glBindTexture(TextureTarget.Texture2D, Texture.ID);
                HV.LastBoundTexture = Texture.ID;
            }

            Shader.Render(ref projection, ref modelView, Vertices.Length, PrimitiveType.Triangles);

            OpenGL32.BindBuffer(BufferTarget.ArrayBuffer, 0);
            HV.LastBoundVertexBuffer = 0;
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SimpleGraphic() => Dispose(false);

        private void Dispose(bool manual)
        {
            if (!_disposed)
            {
                if (manual)
                {
                    lock (_syncRoot)
                    {
                        OpenGL32.DeleteBuffer(VertexBuffer);
                    }
                }
                else
                {
                    HConsole.Log("{0} did not dispose correctly, did you forget to call Dispose()?", GetType().FullName);
                }
                _disposed = true;
            }
        }
        #endregion
    }
}