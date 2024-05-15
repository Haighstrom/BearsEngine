using BearsEngine.Graphics.Shaders;
using BearsEngine.OpenGL;

namespace BearsEngine.Graphics;

public class SimpleGraphic : IRenderable
{
    private readonly object _syncRoot = new();
    private bool _disposed = false;
    

    public IShader Shader { get; }
    public int VertexBuffer { get; }
    public Texture Texture { get; }
    public Vertex[] Vertices { get; }
    public bool Visible { get; set; } = true;
    

    public SimpleGraphic(IShader shader, Texture texture, Vertex[] vertices)
    {
        if (vertices.Length < 3)
            throw new ArgumentException("Cannot make a SimpleGraphic with fewer than 3 vertices", nameof(vertices));

        Shader = shader;
        VertexBuffer = OpenGLHelper.GenBuffer();
        Texture = texture;
        Vertices = vertices;

        OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, VertexBuffer);
        OpenGLHelper.BufferData(BUFFER_TARGET.GL_ARRAY_BUFFER, Vertices.Length * Vertex.STRIDE, Vertices, USAGE_PATTERN.GL_STREAM_DRAW);
        OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, 0);
        OpenGLHelper.LastBoundVertexBuffer = 0;
    }
    

    public void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        if (OpenGLHelper.LastBoundVertexBuffer != VertexBuffer)
        {
            OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, VertexBuffer);
            OpenGLHelper.LastBoundVertexBuffer = VertexBuffer;
        }

        OpenGLHelper.BindTexture(Texture);

        Shader.Render(ref projection, ref modelView, Vertices.Length, PRIMITIVE_TYPE.GL_TRIANGLES);

        OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, 0);
        OpenGLHelper.LastBoundVertexBuffer = 0;
    }
    

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
                    OpenGLHelper.DeleteBuffer(VertexBuffer);
                }
            }
            else
            {
                Log.Warning($"{nameof(SimpleGraphic)} did not dispose correctly, did you forget to call Dispose()?");
            }
            _disposed = true;
        }
    }
    
}
