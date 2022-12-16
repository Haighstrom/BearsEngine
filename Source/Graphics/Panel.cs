using BearsEngine.Graphics.Shaders;
using HaighFramework.OpenGL;
using HaighFramework.WinAPI;

namespace BearsEngine.Graphics
{
    public class Panel : RectGraphicBase
    {
        private Texture _texture;
        private Vertex[] _vertices;
        private bool _verticesChanged = true;

        private readonly Rect
            TL = new Rect(0, 0, 0.3f, 0.3f),
            TM = new Rect(0.3f, 0, 0.4f, 0.3f),
            TR = new Rect(0.7f, 0, 0.3f, 0.3f),
            ML = new Rect(0, 0.3f, 0.3f, 0.4f),
            MM = new Rect(0.3f, 0.3f, 0.4f, 0.4f),
            MR = new Rect(0.7f, 0.3f, 0.3f, 0.4f),
            BL = new Rect(0, 0.7f, 0.3f, 0.3f),
            BM = new Rect(0.3f, 0.7f, 0.4f, 0.3f),
            BR = new Rect(0.7f, 0.7f, 0.3f, 0.3f);
        

        public Panel(string imgPath, Rect r)
            : this(imgPath, r.X, r.Y, r.W, r.H)
        {
        }

        public Panel(string imgPath, Point size)
            : this(imgPath, 0, 0, size.X, size.Y)
        {
        }

        public Panel(string imgPath, float x, float y, float w, float h)
            : base(new DefaultShader(), x, y, w, h)
        {
            Texture = HF.Graphics.LoadTexture(imgPath);
        }
        
        public override float W
        {
            set
            {
                base.W = value;
                _verticesChanged = true;
            }
        }
        

        public override float H
        {
            set
            {
                base.H = value;
                _verticesChanged = true;
            }
        }
        

        public override Colour Colour
        {
            set
            {
                base.Colour = value;
                _verticesChanged = true;
            }
        }
        

        public Texture Texture
        {
            get => _texture;
            set
            {
                _texture = value;
                _verticesChanged = true;
            }
        }
        

        public override void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            if (W == 0 || H == 0)
                return;

            var mv = Matrix4.Translate(ref modelView, X, Y, 0);

            if (OpenGL.LastBoundTexture != Texture.ID)
            {
                OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, Texture.ID);
                OpenGL.LastBoundTexture = Texture.ID;
            }

            BindVertexBuffer();

            if (_verticesChanged)
            {
                _vertices = new Vertex[36]
                {
                    new Vertex(new Point(0, 0), Colour, TL.TopLeft),
                    new Vertex(new Point(0.3f * Texture.Width, 0), Colour, TL.TopRight),
                    new Vertex(new Point(0, 0.3f * Texture.Height), Colour, TL.BottomLeft),
                    new Vertex(new Point(0.3f * Texture.Width, 0.3f * Texture.Height), Colour, TL.BottomRight),

                    new Vertex(new Point(0.3f * Texture.Width, 0), Colour, TM.TopLeft),
                    new Vertex(new Point(W - 0.3f * Texture.Width, 0), Colour, TM.TopRight),
                    new Vertex(new Point(0.3f * Texture.Width, 0.3f * Texture.Height), Colour, TM.BottomLeft),
                    new Vertex(new Point(W - 0.3f * Texture.Width, 0.3f * Texture.Height), Colour, TM.BottomRight),

                    new Vertex(new Point(W - 0.3f * Texture.Width, 0), Colour, TR.TopLeft),
                    new Vertex(new Point(W, 0), Colour, TR.TopRight),
                    new Vertex(new Point(W - 0.3f * Texture.Width, 0.3f * Texture.Height), Colour, TR.BottomLeft),
                    new Vertex(new Point(W, 0.3f * Texture.Height), Colour, TR.BottomRight),

                    new Vertex(new Point(0, 0.3f * Texture.Height), Colour, ML.TopLeft),
                    new Vertex(new Point(0.3f * Texture.Width, 0.3f * Texture.Height), Colour, ML.TopRight),
                    new Vertex(new Point(0, H - 0.3f * Texture.Height), Colour, ML.BottomLeft),
                    new Vertex(new Point(0.3f * Texture.Width, H - 0.3f * Texture.Height), Colour, ML.BottomRight),

                    new Vertex(new Point(0.3f * Texture.Width, 0.3f * Texture.Height), Colour, MM.TopLeft),
                    new Vertex(new Point(W - 0.3f * Texture.Width, 0.3f * Texture.Height), Colour, MM.TopRight),
                    new Vertex(new Point(0.3f * Texture.Width, H - 0.3f * Texture.Height), Colour, MM.BottomLeft),
                    new Vertex(new Point(W - 0.3f * Texture.Width, H - 0.3f * Texture.Height), Colour, MM.BottomRight),

                    new Vertex(new Point(W - 0.3f * Texture.Width, 0.3f * Texture.Height), Colour, MR.TopLeft),
                    new Vertex(new Point(W, 0.3f * Texture.Height), Colour, MR.TopRight),
                    new Vertex(new Point(W - 0.3f * Texture.Width, H - 0.3f * Texture.Height), Colour, MR.BottomLeft),
                    new Vertex(new Point(W, H - 0.3f * Texture.Height), Colour, MR.BottomRight),

                    new Vertex(new Point(0, H - 0.3f * Texture.Height), Colour, BL.TopLeft),
                    new Vertex(new Point(0.3f * Texture.Width, H - 0.3f * Texture.Height), Colour, BL.TopRight),
                    new Vertex(new Point(0, H), Colour, BL.BottomLeft),
                    new Vertex(new Point(0.3f * Texture.Width, H), Colour, BL.BottomRight),

                    new Vertex(new Point(0.3f * Texture.Width, H - 0.3f * Texture.Height), Colour, BM.TopLeft),
                    new Vertex(new Point(W - 0.3f * Texture.Width, H - 0.3f * Texture.Height), Colour, BM.TopRight),
                    new Vertex(new Point(0.3f * Texture.Width, H), Colour, BM.BottomLeft),
                    new Vertex(new Point(W - 0.3f * Texture.Width, H), Colour, BM.BottomRight),

                    new Vertex(new Point(W - 0.3f * Texture.Width, H - 0.3f * Texture.Height), Colour, BR.TopLeft),
                    new Vertex(new Point(W, H - 0.3f * Texture.Height), Colour, BR.TopRight),
                    new Vertex(new Point(W - 0.3f * Texture.Width, H), Colour, BR.BottomLeft),
                    new Vertex(new Point(W, H), Colour, BR.BottomRight)
                };

                OpenGL.BufferData(BUFFER_TARGET.ArrayBuffer, _vertices.Length * Vertex.STRIDE, _vertices, USAGE_PATTERN.StreamDraw);

                _verticesChanged = false;
            }

            Shader.Render(ref projection, ref mv, _vertices.Length, PRIMITIVE_TYPE.GL_TRIANGLE_STRIP);

            UnbindVertexBuffer();
        }
        
    }
}