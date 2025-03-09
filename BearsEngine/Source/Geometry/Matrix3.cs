namespace BearsEngine;

public struct Matrix3
{
    //Matrix Layout
    //(0  3  6)
    //(1  4  7)
    //(2  5  8)

    public static Matrix3 Identity = new(1, 0, 0, 0, 1, 0, 0, 0, 1);
    public static Matrix3 Zero = new(0, 0, 0, 0, 0, 0, 0, 0, 0);
    public static Matrix3 FlipXMatrix = new(-1, 0, 0, 0, 1, 0, 0, 0, 1);
    public static Matrix3 FlipYMatrix = new(1, 0, 0, 0, -1, 0, 0, 0, 1);
    

    public static Matrix3 CreateTranslation(float x, float y) => new(1, 0, 0, 0, 1, 0, x, y, 1);
   
    public static Matrix3 CreateRotationAroundZAxis(float angleInDegrees)
    {
        double radians = Math.PI * angleInDegrees / 180.0;
        return new Matrix3((float)Math.Cos(radians), (float)Math.Sin(radians), 0, -(float)Math.Sin(radians), (float)Math.Cos(radians), 0, 0, 0, 1);
    }

    public static Matrix3 CreateScale(float scaleX, float scaleY) => new(scaleX, 0, 0, 0, scaleY, 0, 0, 0, 1);

    public static Matrix3 CreateOrtho(float width, float height)
    {
        Matrix3 mat = Identity;
        mat = ScaleAroundOrigin(ref mat, 2 / width, 2 / height);
        mat = FlipY(ref mat);
        mat = Translate(ref mat, -width / 2, -height / 2);
        return mat;
    }
    

    /// <summary>
    /// Same as CreateOrtho but without the FlipY stage. Use for makign projection matrices when rendering to a framebuffer rather than screen.
    /// </summary>
    public static Matrix3 CreateFBOOrtho(float width, float height)
    {
        Matrix3 mat = CreateScale(2 / width, 2 / height);
        mat = Translate(ref mat, -width / 2, -height / 2);
        return mat;
    }

    public static Matrix3 Add(ref Matrix3 mat1, ref Matrix3 mat2) => new(mat1._values.Zip(mat2._values, (a, b) => a + b).ToArray());
    
    public static Matrix3 Subtract(ref Matrix3 mat1, ref Matrix3 mat2) => new(mat1._values.Zip(mat2._values, (a, b) => a - b).ToArray());
    
    public static Matrix3 Multiply(ref Matrix3 mat1, ref Matrix3 mat2)
    {
        return new Matrix3
            (
                mat1._values[0] * mat2._values[0] + mat1._values[3] * mat2._values[1] + mat1._values[6] * mat2._values[2],
                mat1._values[1] * mat2._values[0] + mat1._values[4] * mat2._values[1] + mat1._values[7] * mat2._values[2],
                mat1._values[2] * mat2._values[0] + mat1._values[5] * mat2._values[1] + mat1._values[8] * mat2._values[2],
                mat1._values[0] * mat2._values[3] + mat1._values[3] * mat2._values[4] + mat1._values[6] * mat2._values[5],
                mat1._values[1] * mat2._values[3] + mat1._values[4] * mat2._values[4] + mat1._values[7] * mat2._values[5],
                mat1._values[2] * mat2._values[3] + mat1._values[5] * mat2._values[4] + mat1._values[8] * mat2._values[5],
                mat1._values[0] * mat2._values[6] + mat1._values[3] * mat2._values[7] + mat1._values[6] * mat2._values[8],
                mat1._values[1] * mat2._values[6] + mat1._values[4] * mat2._values[7] + mat1._values[7] * mat2._values[8],
                mat1._values[2] * mat2._values[6] + mat1._values[5] * mat2._values[7] + mat1._values[8] * mat2._values[8]
            );
    }

    /// <summary>
    /// Applying matrix to a Point to transform it to a new Point - it will be padded to (x y 1) then just the first 2 dimensions returned.
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static Point Multiply(ref Matrix3 mat, Point p)
    {
        return new Point
            (
                mat._values[0] * p.X + mat._values[3] * p.Y + mat._values[6],
                mat._values[1] * p.X + mat._values[4] * p.Y + mat._values[7]
            );
    }

    /// <summary>
    /// Applying matrix to a Point3 to transform it to a new Point3
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static Point3 Multiply(ref Matrix3 mat, Point3 p)
    {
        return new Point3
            (
                mat._values[0] * p.x + mat._values[3] * p.y + mat._values[6] * p.z,
                mat._values[1] * p.x + mat._values[4] * p.y + mat._values[7] * p.z,
                mat._values[2] * p.x + mat._values[5] * p.y + mat._values[8] * p.z
            );
    }

    /// <summary>
    /// Scalar multiplication
    /// </summary>
    public static Matrix3 Multiply(ref Matrix3 mat, float f)
    {
        return new Matrix3
            (
                mat._values[0] * f,
                mat._values[1] * f,
                mat._values[2] * f,
                mat._values[3] * f,
                mat._values[4] * f,
                mat._values[5] * f,
                mat._values[6] * f,
                mat._values[7] * f,
                mat._values[8] * f
            );
    }
    

    public static Matrix3 Translate(ref Matrix3 mat, float x, float y)
    {
        Matrix3 transMat = CreateTranslation(x, y);
        return Multiply(ref mat, ref transMat);
    }
    


    public static Matrix3 RotateAroundZ(ref Matrix3 mat, float angleInDegrees)
    {
        Matrix3 rotMat = CreateRotationAroundZAxis(angleInDegrees);
        return Multiply(ref mat, ref rotMat);
    }

    public static Matrix3 RotateAroundPoint(ref Matrix3 mat, float angleInDegrees, float x, float y)
    {
        Matrix3 translate1 = CreateTranslation(x, y);
        Matrix3 rotate = CreateRotationAroundZAxis(angleInDegrees);
        Matrix3 translate2 = CreateTranslation(-x, -y);

        Matrix3 result = Multiply(ref mat, ref translate1);
        result = Multiply(ref result, ref rotate);
        result = Multiply(ref result, ref translate2);

        return result;
    }

    public static Matrix3 RotateAroundPoint(ref Matrix3 mat, float angleInDegrees, Point p) => RotateAroundPoint(ref mat, angleInDegrees, p.X, p.Y);
  
    

    public static Matrix3 ScaleAroundOrigin(ref Matrix3 mat, float scaleX, float scaleY)
    {
        Matrix3 scaleMat = CreateScale(scaleX, scaleY);
        return Multiply(ref mat, ref scaleMat);
    }
    public static Matrix3 ScaleAroundPoint(ref Matrix3 mat, float scaleX, float scaleY, float x, float y)
    {
        Matrix3 translate1 = CreateTranslation(x, y);
        Matrix3 scale = CreateScale(scaleX, scaleY);
        Matrix3 translate2 = CreateTranslation(-x, -y);

        Matrix3 result = Multiply(ref mat, ref translate1);
        result = Multiply(ref result, ref scale);
        result = Multiply(ref result, ref translate2);

        return result;
    }
    
    public static Matrix3 FlipX(ref Matrix3 mat)
    {
        return Multiply(ref mat, ref FlipXMatrix);
    }
    public static Matrix3 FlipY(ref Matrix3 mat)
    {
        return Multiply(ref mat, ref FlipYMatrix);
    }
    
    /// <summary>
    /// Returns a new Matrix3 which is the inverse of Matrix3 mat
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    public static Matrix3 Invert(Matrix3 mat)
    {
        int[] colIdx = { 0, 0, 0 };
        int[] rowIdx = { 0, 0, 0 };
        int[] pivotIdx = { -1, -1, -1 };

        float[,] inverse = {{mat._values[0], mat._values[3], mat._values[6]},
                            {mat._values[1], mat._values[4], mat._values[7]},
                            {mat._values[2], mat._values[5], mat._values[8]}};

        int icol = 0;
        int irow = 0;
        for (int i = 0; i < 3; i++)
        {
            float maxPivot = 0.0f;
            for (int j = 0; j < 3; j++)
            {
                if (pivotIdx[j] != 0)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        if (pivotIdx[k] == -1)
                        {
                            float absVal = Math.Abs(inverse[j, k]);
                            if (absVal > maxPivot)
                            {
                                maxPivot = absVal;
                                irow = j;
                                icol = k;
                            }
                        }
                        else if (pivotIdx[k] > 0)
                        {
                            return mat;
                        }
                    }
                }
            }

            ++pivotIdx[icol];

            if (irow != icol)
            {
                for (int k = 0; k < 3; ++k)
                {
                    float f = inverse[irow, k];
                    inverse[irow, k] = inverse[icol, k];
                    inverse[icol, k] = f;
                }
            }

            rowIdx[i] = irow;
            colIdx[i] = icol;

            float pivot = inverse[icol, icol];

            if (pivot == 0.0f)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            float oneOverPivot = 1.0f / pivot;
            inverse[icol, icol] = 1.0f;
            for (int k = 0; k < 3; ++k)
                inverse[icol, k] *= oneOverPivot;

            for (int j = 0; j < 3; ++j)
            {
                if (icol != j)
                {
                    float f = inverse[j, icol];
                    inverse[j, icol] = 0.0f;
                    for (int k = 0; k < 3; ++k)
                        inverse[j, k] -= inverse[icol, k] * f;
                }
            }
        }

        for (int j = 2; j >= 0; --j)
        {
            int ir = rowIdx[j];
            int ic = colIdx[j];
            for (int k = 0; k < 3; ++k)
            {
                float f = inverse[k, ir];
                inverse[k, ir] = inverse[k, ic];
                inverse[k, ic] = f;
            }
        }

        return new Matrix3
            (
                inverse[0, 0],
                inverse[1, 0],
                inverse[2, 0],
                inverse[0, 1],
                inverse[1, 1],
                inverse[2, 1],
                inverse[0, 2],
                inverse[1, 2],
                inverse[2, 2]
            );
    }

    /// <summary>
    /// Returns a new Matrix3 which is the inverse of Matrix3 mat
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    public static Matrix3 Invert(ref Matrix3 mat)
    {
        int[] colIdx = { 0, 0, 0 };
        int[] rowIdx = { 0, 0, 0 };
        int[] pivotIdx = { -1, -1, -1 };

        float[,] inverse = {{mat._values[0], mat._values[3], mat._values[6]},
                            {mat._values[1], mat._values[4], mat._values[7]},
                            {mat._values[2], mat._values[5], mat._values[8]}};

        int icol = 0;
        int irow = 0;
        for (int i = 0; i < 3; i++)
        {
            float maxPivot = 0.0f;
            for (int j = 0; j < 3; j++)
            {
                if (pivotIdx[j] != 0)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        if (pivotIdx[k] == -1)
                        {
                            float absVal = Math.Abs(inverse[j, k]);
                            if (absVal > maxPivot)
                            {
                                maxPivot = absVal;
                                irow = j;
                                icol = k;
                            }
                        }
                        else if (pivotIdx[k] > 0)
                        {
                            return mat;
                        }
                    }
                }
            }

            ++pivotIdx[icol];

            if (irow != icol)
            {
                for (int k = 0; k < 3; ++k)
                {
                    float f = inverse[irow, k];
                    inverse[irow, k] = inverse[icol, k];
                    inverse[icol, k] = f;
                }
            }

            rowIdx[i] = irow;
            colIdx[i] = icol;

            float pivot = inverse[icol, icol];

            if (pivot == 0.0f)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            float oneOverPivot = 1.0f / pivot;
            inverse[icol, icol] = 1.0f;
            for (int k = 0; k < 3; ++k)
                inverse[icol, k] *= oneOverPivot;

            for (int j = 0; j < 3; ++j)
            {
                if (icol != j)
                {
                    float f = inverse[j, icol];
                    inverse[j, icol] = 0.0f;
                    for (int k = 0; k < 3; ++k)
                        inverse[j, k] -= inverse[icol, k] * f;
                }
            }
        }

        for (int j = 2; j >= 0; --j)
        {
            int ir = rowIdx[j];
            int ic = colIdx[j];
            for (int k = 0; k < 3; ++k)
            {
                float f = inverse[k, ir];
                inverse[k, ir] = inverse[k, ic];
                inverse[k, ic] = f;
            }
        }

        return new Matrix3
            (
                inverse[0, 0],
                inverse[1, 0],
                inverse[2, 0],
                inverse[0, 1],
                inverse[1, 1],
                inverse[2, 1],
                inverse[0, 2],
                inverse[1, 2],
                inverse[2, 2]
            );
    }
    
    private float[] _values;
    
    public Matrix3(float m0, float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8)
    {
        _values = new float[9] { m0, m1, m2, m3, m4, m5, m6, m7, m8};
    }
    public Matrix3(float[] values)
    {
        _values = values;
    }
    
    public float this[int x, int y]
    {
        get
        {
            if (x < 0 || x > 2 || y < 0 || y > 2)
                throw new ArgumentException($"Requested an invalid Matrix3 index:{x},{y}");

            return _values[x * 3 + y];
        }
        set
        {
            if (x < 0 || x > 2 || y < 0 || y > 2)
                throw new ArgumentException($"Requested an invalid Matrix3 index:{x},{y}");

            _values[x * 3 + y] = value;
        }
    }
    
    /// <summary>
    /// Exposes the 1D array of matrix elements that make up this matrix, indexed as
    /// (0 3 6)
    /// (1 4 7)
    /// (2 5 8)
    /// </summary>
    public float[] Values { get { return _values; } set { _values = value; } }

    /// <summary>
    /// Gets the determinant of this matrix
    /// </summary>
    public float Determinant
    {
        get
        {
            return _values[0] * _values[4] * _values[8] + _values[3] * _values[7] * _values[2] + _values[6] * _values[1] * _values[5]
                 - _values[6] * _values[4] * _values[2] - _values[0] * _values[7] * _values[5] - _values[3] * _values[1] * _values[8];
        }
    }
    
    /// <summary>
    ///  Returns the transpose of this Matrix3 - all elements mirrored in [1 1] diagonal. The values of this instance will not be altered, returns a new matrix.
    /// </summary>
    /// <returns></returns>
    public Matrix3 Transpose()
    {
        return new Matrix3
            (
                _values[0],
                _values[3],
                _values[6],
                _values[1],
                _values[4],
                _values[7],
                _values[2],
                _values[5],
                _values[8]
            );
    }

    /// <summary>
    /// Return a new Matrix that is the inverse of this matrix- singular matrices will throw an exception
    /// </summary>
    /// <returns></returns>
    public Matrix3 Inverse()
    {
        return Invert(this);
    }

    /// <summary>
    /// Scalar multiplication.
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix2 which holds the result of the multiplication</returns>
    public static Matrix3 operator *(float left, Matrix3 right) => Multiply(ref right, left);

    /// <summary>
    /// Scalar multiplication.
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix3 which holds the result of the multiplication</returns>
    public static Matrix3 operator *(Matrix3 left, float right) => Multiply(ref left, right);

    /// <summary>
    /// Matrix multiplication
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix3 which holds the result of the multiplication</returns>
    public static Matrix3 operator *(Matrix3 left, Matrix3 right) => Multiply(ref left, ref right);

    /// <summary>
    /// Multiplying a matrix onto a Point, to return a point
    /// </summary>
    public static Point3 operator *(Matrix3 left, Point3 right) => Multiply(ref left, right);

    /// <summary>
    /// Multiplying a matrix onto a Point, to return a point
    /// </summary>
    public static Point operator *(Matrix3 left, Point right) => Multiply(ref left, right);

    /// <summary>
    /// Matrix addition
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix3 which holds the result of the addition</returns>
    public static Matrix3 operator +(Matrix3 left, Matrix3 right) => Add(ref left, ref right);

    /// <summary>
    /// Matrix subtraction
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix3 which holds the result of the subtraction</returns>
    public static Matrix3 operator -(Matrix3 left, Matrix3 right) => Subtract(ref left, ref right);

    
}
