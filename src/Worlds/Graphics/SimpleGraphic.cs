using BearsEngine.Graphics;
using HaighFramework;
using HaighFramework.OpenGL4;

namespace BearsEngine.Worlds
{
    public class SimpleGraphic : IRenderable
    {
        #region Fields
        private object _syncRoot = new object();
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
            VertexBuffer = OpenGL.GenBuffer();
            Texture = texture;
            Vertices = vertices;

            OpenGL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
            OpenGL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * Vertex.STRIDE, Vertices, BufferUsageHint.StreamDraw); 
            OpenGL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            HV.LastBoundVertexBuffer = 0;
        }
        #endregion

        #region Methods
        public void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            if (HV.LastBoundVertexBuffer != VertexBuffer)
            {
                OpenGL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
                HV.LastBoundVertexBuffer = VertexBuffer;
            }

            if (HV.LastBoundTexture != Texture.ID)
            {
                OpenGL.BindTexture(TextureTarget.Texture2D, Texture.ID);
                HV.LastBoundTexture = Texture.ID;
            }

            Shader.Render(ref projection, ref modelView, Vertices.Length, PrimitiveType.Triangles);

            OpenGL.BindBuffer(BufferTarget.ArrayBuffer, 0);
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
                        OpenGL.DeleteBuffer(VertexBuffer);
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