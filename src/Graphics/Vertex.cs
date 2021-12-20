using HaighFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace BearsEngine.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public const int STRIDE = 4 * sizeof(float) + 4 * sizeof(byte);

        public float X, Y;
        public Colour Colour;
        public float U, V;

        public Vertex(IPoint<float> position, Colour colour, IPoint<float> textureCoords)
            : this(position.X, position.Y, colour, textureCoords.X, textureCoords.Y)
        {
        }

        public Vertex(float x, float y, Colour colour, float u, float v)
        {
            X = x;
            Y = y;
            Colour = colour;
            U = u;
            V = v;
        }
    }
}