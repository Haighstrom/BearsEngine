﻿using BearsEngine.Graphics.Shaders;
using BearsEngine.OpenGL;

namespace BearsEngine.Graphics;

public class FillableBar : RectGraphicBase
{
    private Texture _texture;
    private Vertex[] _vertices;
    private bool _verticesChanged = true;
    private readonly Point _UV1, _UV2, _UV3, _UV4;
    private Point _UV5, _UV6, _UV7, _UV8;
    private float _amount;

    public FillableBar(string graphicPath, Point position, Point size, float initialPercentage = 0)
        : this(graphicPath, position.X, position.Y, size.X, size.Y, initialPercentage)
    {
    }

    public FillableBar(string graphicPath, Point size, float initialPercentage = 0)
        : this(graphicPath, 0, 0, size.X, size.Y, initialPercentage)
    {
    }

    public FillableBar(string graphicPath, float width, float height, float initialAmountFilled = 0)
        : this(graphicPath, 0, 0, width, height, initialAmountFilled)
    {
    }

    public FillableBar(string graphicPath, Rect r, float initialAmountFilled = 0)
        : this(graphicPath, r.X, r.Y, r.W, r.H, initialAmountFilled)
    {
    }

    public FillableBar(string graphicPath, float x, float y, float w, float h, float initialAmountFilled = 0)
        : base(x, y, w, h)
    {
        _texture = OpenGLHelper.LoadTexture(graphicPath);
        AmountFilled = initialAmountFilled;

        _UV1 = new Point(0, 0);
        _UV2 = new Point(1, 0);
        _UV3 = new Point(0, 0.5f);
        _UV4 = new Point(1, 0.5f);
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
    

    /// <summary>
    /// [0,1]
    /// </summary>
    public virtual float AmountFilled
    {
        get => _amount;
        set
        {
            Ensure.IsInRange(value, 0, 1);

            if (value != _amount)
            {
                _amount = value;
                _verticesChanged = true;
            }
        }
    }
    
    public override void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        if (W == 0 || H == 0)
            return;

        var mv = Matrix3.Translate(ref modelView, X, Y);

        OpenGLHelper.BindTexture(_texture);

        OpenGLHelper.BindVertexBuffer(VertexBuffer);

        if (_verticesChanged)
        {
            _UV5 = new Point(0, 0.5f);
            _UV6 = new Point(AmountFilled, 0.5f);
            _UV7 = new Point(0, 1);
            _UV8 = new Point(AmountFilled, 1);

            _vertices = new Vertex[12]
            {
                new Vertex(new Point(0, 0), Colour, _UV1),
                new Vertex(new Point(W, 0), Colour, _UV2),
                new Vertex(new Point(0, H), Colour, _UV3),

                new Vertex(new Point(0, H), Colour, _UV3),
                new Vertex(new Point(W, 0), Colour, _UV2),
                new Vertex(new Point(W, H), Colour, _UV4),

                new Vertex(new Point(0, 0), Colour, _UV5),
                new Vertex(new Point(W * AmountFilled, 0), Colour, _UV6),
                new Vertex(new Point(0, H), Colour, _UV7),

                new Vertex(new Point(0, H), Colour, _UV7),
                new Vertex(new Point(W * AmountFilled, 0), Colour, _UV6),
                new Vertex(new Point(W * AmountFilled, H), Colour, _UV8)
            };

            OpenGLHelper.BufferData(BUFFER_TARGET.GL_ARRAY_BUFFER, _vertices.Length * Vertex.STRIDE, _vertices, USAGE_PATTERN.GL_STREAM_DRAW);

            _verticesChanged = false;
        }

        Shader.Render(ref projection, ref mv, _vertices.Length, PRIMITIVE_TYPE.GL_TRIANGLES);

        OpenGLHelper.UnbindVertexBuffer();
    }
}
