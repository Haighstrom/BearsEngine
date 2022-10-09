using System.Runtime.InteropServices;

namespace BearsEngine;

[StructLayout(LayoutKind.Sequential)]
public struct Matrix2
{
    //Matrix Layout
    //(0  2)
    //(1  3)

    public static Matrix2 Identity => new(1, 0, 0, 1);
    public static Matrix2 Zero => new();

    public static Matrix2 CreateRotation(float angleInDegrees)
    {
        double radians = Math.PI * angleInDegrees / 180.0;
        return new Matrix2((float)Math.Cos(radians), (float)Math.Sin(radians), -(float)Math.Sin(radians), (float)Math.Cos(radians));
    }

    public static Matrix2 CreateScale(float scaleX, float scaleY)
    {
        return new Matrix2(scaleX, 0, 0, scaleY);
    }
    

    public static Matrix2 Add(ref Matrix2 mat1, ref Matrix2 mat2)
    {
        float[] values = mat1._values.Zip(mat2._values, (a, b) => a + b).ToArray();
        return new Matrix2(values);
    }
    

    public static Matrix2 Subtract(ref Matrix2 mat1, ref Matrix2 mat2) => new(mat1._values.Zip(mat2._values, (a, b) => a - b).ToArray());

    public static Matrix2 Multiply(ref Matrix2 mat1, ref Matrix2 mat2)
    {
        return new Matrix2
            (
                mat1._values[0] * mat2._values[0] + mat1._values[2] * mat2._values[1],
                mat1._values[1] * mat2._values[0] + mat1._values[3] * mat2._values[1], 
                mat1._values[0] * mat2._values[2] + mat1._values[2] * mat2._values[3],
                mat1._values[1] * mat2._values[2] + mat1._values[3] * mat2._values[3]

            );
    }

    /// <summary>
    /// Muplication by scalar - multiplies each matrix element by the scalar value.
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static Matrix2 Multiply(ref Matrix2 mat, float f)
    {
        return new Matrix2
            (
                mat._values[0] * f,
                mat._values[1] * f,
                mat._values[2] * f,
                mat._values[3] * f
            );
    }

    /// <summary>
    /// Apply a Matrix2 mat to a Point p, returning a transformed Point
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static Point Multiply(ref Matrix2 mat, Point p)
    {
        return new Point
            (
                p.X * mat._values[0] + p.Y * mat._values[2],
                p.X * mat._values[1] + p.Y * mat._values[3]
            );
    }
                             

    public static Matrix2 RotateAroundZ(ref Matrix2 mat, float angleInDegrees)
    {
        Matrix2 rotMat = CreateRotation(angleInDegrees);
        return Multiply(ref mat, ref rotMat);
    }        
    

    public static Matrix2 ScaleAroundOrigin(ref Matrix2 mat, float scaleX, float scaleY)
    {
        Matrix2 scaleMat = CreateScale(scaleX, scaleY);
        return Multiply(ref mat, ref scaleMat);
    }
    

    public static Matrix2 Inverse(ref Matrix2 mat)
    {
        float det = mat.Determinant;

        if (det == 0)
            return mat;

        float invDet = 1 / det;

        return new Matrix2
            (
                mat._values[3] * invDet,
                -mat._values[1] * invDet,
                -mat._values[2] * invDet,
                mat._values[0] * invDet
            );
    }
    
    

    private float[] _values;
    

    public Matrix2(float m0, float m1, float m2, float m3)
    {
        _values = new float[4] { m0, m1, m2, m3 };
    }
    public Matrix2(float[] values)
    {
        if (values == null)
            throw new ArgumentNullException(nameof(values));

        if (values.Length != 4)
            throw new ArgumentException($"Did not supply 4 values, but {values.Length}.", nameof(values));

        _values = values;
    }
    

    public float this[int x, int y]
    {
        get
        {
            if (x < 0 || x > 1 || y < 0 || y > 1)
                throw new Exception($"Requested an invalid Matrix2 index:{x},{y}");
            return _values[x * 2 + y];
        }
        set
        {
            if (x < 0 || x > 1 || y < 0 || y > 1)
                throw new Exception($"Requested an invalid Matrix2 index:{x},{y}");
            _values[x * 2 + y] = value;
        }
    }
    

    /// <summary>
    /// Matrix elements, a 4 element array indexed as
    /// (0 2)
    /// (1 3)
    /// </summary>
    public float[] Values
    {
        get => _values;
        set => _values = value;
    }

    /// <summary>
    /// Gets the determinant of this matrix
    /// </summary>
    public float Determinant => _values[0] * _values[3] - _values[1] * _values[2];
    

    /// <summary>
    /// Returns the transpose of this Matrix2 - all elements mirrored in [1 1] diagonal. The values of this instance will not be altered, returns a new matrix.
    /// </summary>
    /// <returns></returns>
    public Matrix2 Transpose()
    {
        return new Matrix2
            (
                _values[0],
                _values[2],
                _values[1],
                _values[3]
            );
    }
    
    /// <summary>
    /// Scalar multiplication.
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix2 which holds the result of the multiplication</returns>
    public static Matrix2 operator *(float left, Matrix2 right) => Multiply(ref right, left);

    /// <summary>
    /// Scalar multiplication.
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix2 which holds the result of the multiplication</returns>
    public static Matrix2 operator *(Matrix2 left, float right) => Multiply(ref left, right);

    public static Point operator *(Matrix2 left, Point right) => Multiply(ref left, right);

    /// <summary>
    /// Matrix multiplication
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix2 which holds the result of the multiplication</returns>
    public static Matrix2 operator *(Matrix2 left, Matrix2 right) => Multiply(ref left, ref right);

    /// <summary>
    /// Matrix addition
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix2 which holds the result of the addition</returns>
    public static Matrix2 operator +(Matrix2 left, Matrix2 right) => Add(ref left, ref right);

    /// <summary>
    /// Matrix subtraction
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix2 which holds the result of the subtraction</returns>
    public static Matrix2 operator -(Matrix2 left, Matrix2 right) => Subtract(ref left, ref right);
    
}
