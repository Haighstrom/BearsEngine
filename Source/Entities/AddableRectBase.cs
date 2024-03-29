﻿namespace BearsEngine;

public abstract class AddableRectBase : AddableBase, IRectAddable
{
    private float _x, _y, _w, _h;

    public AddableRectBase(Rect r)
        : this(r.X, r.Y, r.W, r.H)
    {
    }

    public AddableRectBase(float x, float y, float w, float h)
    {
        _x = x;
        _y = y;
        _w = w;
        _h = h;
    }

    public virtual float X
    {
        get => _x;
        set
        {
            if (_x != value)
            {
                _x = value;
                OnPositionChanged();
            }
        }
    }

    public virtual float Y
    {
        get => _y;
        set
        {
            if (_y != value)
            {
                _y = value;
                OnPositionChanged();
            }
        }
    }

    public virtual float W
    {
        get => _w;
        set
        {
            if (_w != value)
            {
                var oldSize = Size;
                _w = value;
                OnSizeChanged(new ResizeEventArgs(oldSize, Size));
            }
        }
    }

    public virtual float H
    {
        get => _h;
        set
        {
            if (_h != value)
            {
                var oldSize = Size;
                _h = value;
                OnSizeChanged(new ResizeEventArgs(oldSize, Size));
            }
        }
    }

    public Point P
    {
        get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    public Point Size
    {
        get => new(W, H);
        set
        {
            W = value.X;
            H = value.Y;
        }
    }

    public Rect R
    {
        get => new(X, Y, W, H);
        set
        {
            X = value.X;
            Y = value.Y;
            W = value.W;
            H = value.H;
        }
    }

    public Point Centre => new(X + W / 2, Y + H / 2);

    protected virtual void OnPositionChanged() => PositionChanged?.Invoke(this, new PositionChangedEventArgs(new Rect(X, Y, W, H)));
    protected virtual void OnSizeChanged(ResizeEventArgs args) => SizeChanged?.Invoke(this, args);

    public bool Equals(IPosition? other)
    {
        throw new NotImplementedException();
    }

    public event EventHandler<PositionChangedEventArgs>? PositionChanged;
    public event EventHandler<ResizeEventArgs>? SizeChanged;
}