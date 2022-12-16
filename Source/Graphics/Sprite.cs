﻿using BearsEngine.Graphics.Shaders;
using HaighFramework.OpenGL;
using HaighFramework.WinAPI;

namespace BearsEngine.Graphics
{
    public class Sprite : RectGraphicBase
    {
        private Texture _texture;
        private Vertex[] _vertices;
        private bool _verticesChanged = true;
        private Point _UV1, _UV2, _UV3, _UV4;
        private readonly float _frameW, _frameH;
        protected int _currentFrame = 0;
        

        public Sprite(string imgPath, Rect r, int spriteSheetColumns, int spriteSheetRows, int initialFrame = 0)
            : this(imgPath, r.X, r.Y, r.W, r.H, spriteSheetColumns, spriteSheetRows, initialFrame)
        {
        }

        public Sprite(string imgPath, Point size, int spriteSheetColumns, int spriteSheetRows, int initialFrame = 0)
            : this(imgPath, 0, 0, size.X, size.Y, spriteSheetColumns, spriteSheetRows, initialFrame)
        {
        }

        public Sprite(string imgPath, float width, float height, int spriteSheetColumns, int spriteSheetRows, int initialFrame = 0)
            : this(imgPath, 0, 0, width, height, spriteSheetColumns, spriteSheetRows, initialFrame)
        {
        }

        public Sprite(string imgPath, float x, float y, float w, float h, int spriteSheetColumns, int spriteSheetRows, int initialFrame = 0)
            : base(new DefaultShader(), x, y, w, h)
        {
            Texture = HF.Graphics.LoadSpriteTexture(imgPath, spriteSheetRows, spriteSheetColumns, TEXPARAMETER_VALUE.GL_NEAREST);

            FramesAcross = spriteSheetColumns;
            FramesDown = spriteSheetRows;

            _frameW = (1f - (1 + spriteSheetColumns) * PaddingWidth) / spriteSheetColumns;
            _frameH = (1f - (1 + spriteSheetRows) * PaddingHeight) / spriteSheetRows;


            _UV1.X = PaddingWidth;
            _UV1.Y = PaddingHeight;

            _UV2 = _UV1 + new Point(_frameW, 0);
            _UV3 = _UV1 + new Point(0, _frameH);
            _UV4 = _UV1 + new Point(_frameW, _frameH);

            Frame = initialFrame;
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
        

        public int FramesAcross { get; }

        public int FramesDown { get; }

        public int Frame
        {
            get => _currentFrame;
            set
            {
                if (value == _currentFrame)
                    return;

                _currentFrame = Maths.Mod(value, TotalFrames);

                int indexX = Maths.Mod(_currentFrame, FramesAcross);
                int indexY = _currentFrame / FramesAcross;

                _UV1.X = PaddingWidth + indexX * (_frameW + PaddingWidth);
                _UV1.Y = PaddingHeight + indexY * (_frameH + PaddingHeight);

                _UV2 = _UV1 + new Point(_frameW, 0);
                _UV3 = _UV1 + new Point(0, _frameH);
                _UV4 = _UV1 + new Point(_frameW, _frameH);

                _verticesChanged = true;
            }
        }
        

        public int TotalFrames => FramesAcross * FramesDown;

        public int LastFrame => TotalFrames - 1;

        protected float PaddingWidth => (float)HF.Graphics.TEXTURE_SPRITE_PADDING / Texture.Width;
        protected float PaddingHeight => (float)HF.Graphics.TEXTURE_SPRITE_PADDING / Texture.Height;

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
                _vertices = new Vertex[4]
                {
                    new Vertex(new Point(0, 0), Colour, _UV1),
                    new Vertex(new Point(W, 0), Colour, _UV2),
                    new Vertex(new Point(0, H), Colour, _UV3),
                    new Vertex(new Point(W, H), Colour, _UV4)
                };

                OpenGL.BufferData(BUFFER_TARGET.ArrayBuffer, _vertices.Length * Vertex.STRIDE, _vertices, USAGE_PATTERN.StreamDraw);

                _verticesChanged = false;
            }

            Shader.Render(ref projection, ref mv, _vertices.Length, PRIMITIVE_TYPE.GL_TRIANGLE_STRIP);

            UnbindVertexBuffer();
        }
        
    }
}