namespace BearsEngine;

public struct Matrix4
{
    //Matrix Layout
    //(0  4  8  12)
    //(1  5  9  13)
    //(2  6  10 14)
    //(3  7  11 15)

    public static Matrix4 Identity => new(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
    public static Matrix4 Zero => new(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    public static Matrix4 FlipXMatrix = new(-1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
    public static Matrix4 FlipYMatrix = new(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
    public static Matrix4 FlipZMatrix = new(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Matrix4 CreateTranslation(float x, float y, float z)
    {
        return new Matrix4( 1, 0, 0, 0, 
                            0, 1, 0, 0, 
                            0, 0, 1, 0, 
                            x, y, z, 1);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Matrix4 CreateTranslation(float x, float y)
    {
        return new Matrix4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, x, y, 0, 1);
    }

    public static Matrix4 CreateRotationAroundZAxis(float angleInDegrees)
    {
        double radians = Math.PI * angleInDegrees / 180.0;
        return new Matrix4((float)Math.Cos(radians), (float)Math.Sin(radians), 0, 0, -(float)Math.Sin(radians), (float)Math.Cos(radians), 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
    }

    public static Matrix4 CreateScale(float scaleX, float scaleY, float scaleZ = 1) => new(scaleX, 0, 0, 0, 0, scaleY, 0, 0, 0, 0, scaleZ, 0, 0, 0, 0, 1);

    public static Matrix4 CreateOrtho(float width, float height)
    {
        Matrix4 mat = Identity;
        mat = ScaleAroundOrigin(ref mat, 2 / width, 2 / height, 1);
        mat = FlipY(ref mat);
        mat = Translate(ref mat, -width / 2, -height / 2, 0);
        return mat;
    }
    
    public static Matrix4 CreateFBOOrtho(float width, float height)
    {
        Matrix4 mat = Identity;
        mat = ScaleAroundOrigin(ref mat, 2 / width, 2 / height, 1);
        mat = Translate(ref mat, -width / 2, -height / 2, 0);
        return mat;
    }
            
    public static Matrix4 Add(ref Matrix4 mat1, ref Matrix4 mat2)
    {
        float[] values = mat1._values.Zip(mat2._values, (a, b) => a + b).ToArray();
        return new Matrix4(values);
    }
    
    public static Matrix4 Subtract(ref Matrix4 mat1, ref Matrix4 mat2)
    {
        float[] values = mat1._values.Zip(mat2._values, (a, b) => a - b).ToArray();
        return new Matrix4(values);
    }

    public static Matrix4 Multiply(ref Matrix4 mat1, ref Matrix4 mat2)
    {
        float[] values = new float[16] {
     /*0*/  mat1._values[0]*mat2._values[0]+mat1._values[4]*mat2._values[1]+mat1._values[8]*mat2._values[2]+mat1._values[12]*mat2._values[3],
     /*1*/  mat1._values[1]*mat2._values[0]+mat1._values[5]*mat2._values[1]+mat1._values[9]*mat2._values[2]+mat1._values[13]*mat2._values[3],
     /*2*/  mat1._values[2]*mat2._values[0]+mat1._values[6]*mat2._values[1]+mat1._values[10]*mat2._values[2]+mat1._values[14]*mat2._values[3],
     /*3*/  mat1._values[3]*mat2._values[0]+mat1._values[7]*mat2._values[1]+mat1._values[11]*mat2._values[2]+mat1._values[15]*mat2._values[3],
     /*4*/  mat1._values[0]*mat2._values[4]+mat1._values[4]*mat2._values[5]+mat1._values[8]*mat2._values[6]+mat1._values[12]*mat2._values[7],
     /*5*/  mat1._values[1]*mat2._values[4]+mat1._values[5]*mat2._values[5]+mat1._values[9]*mat2._values[6]+mat1._values[13]*mat2._values[7],
     /*6*/  mat1._values[2]*mat2._values[4]+mat1._values[6]*mat2._values[5]+mat1._values[10]*mat2._values[6]+mat1._values[14]*mat2._values[7],
     /*7*/  mat1._values[3]*mat2._values[4]+mat1._values[7]*mat2._values[5]+mat1._values[11]*mat2._values[6]+mat1._values[15]*mat2._values[7],
     /*8*/  mat1._values[0]*mat2._values[8]+mat1._values[4]*mat2._values[9]+mat1._values[8]*mat2._values[10]+mat1._values[12]*mat2._values[11],
     /*9*/  mat1._values[1]*mat2._values[8]+mat1._values[5]*mat2._values[9]+mat1._values[9]*mat2._values[10]+mat1._values[13]*mat2._values[11],
    /*10*/  mat1._values[2]*mat2._values[8]+mat1._values[6]*mat2._values[9]+mat1._values[10]*mat2._values[10]+mat1._values[14]*mat2._values[11],
    /*11*/  mat1._values[3]*mat2._values[8]+mat1._values[7]*mat2._values[9]+mat1._values[11]*mat2._values[10]+mat1._values[15]*mat2._values[11],
    /*12*/  mat1._values[0]*mat2._values[12]+mat1._values[4]*mat2._values[13]+mat1._values[8]*mat2._values[14]+mat1._values[12]*mat2._values[15],
    /*13*/  mat1._values[1]*mat2._values[12]+mat1._values[5]*mat2._values[13]+mat1._values[9]*mat2._values[14]+mat1._values[13]*mat2._values[15],
    /*14*/  mat1._values[2]*mat2._values[12]+mat1._values[6]*mat2._values[13]+mat1._values[10]*mat2._values[14]+mat1._values[14]*mat2._values[15],
    /*15*/  mat1._values[3]*mat2._values[12]+mat1._values[7]*mat2._values[13]+mat1._values[11]*mat2._values[14]+mat1._values[15]*mat2._values[15]
        };
        return new Matrix4(values);
    }

    /// <summary>
    /// Scalar multiplication
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static Matrix4 Multiply(ref Matrix4 mat, float f)
    {
        return new Matrix4
            (
                mat._values[0] * f,
                mat._values[1] * f,
                mat._values[2] * f,
                mat._values[3] * f,
                mat._values[4] * f,
                mat._values[5] * f,
                mat._values[6] * f,
                mat._values[7] * f,
                mat._values[8] * f,
                mat._values[9] * f,
                mat._values[10] * f,
                mat._values[11] * f,
                mat._values[12] * f,
                mat._values[13] * f,
                mat._values[14] * f,
                mat._values[15] * f
            );
    }

    /// <summary>
    /// Apply matrix mat to Point4 p, which returns a transformed Point4 p
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static Point4 Multiply(ref Matrix4 mat, Point4 p)
    {
        return new Point4
            (
                mat._values[0] * p.x + mat._values[4] * p.y + mat._values[8] * p.z + mat._values[12] * p.w,
                mat._values[1] * p.x + mat._values[5] * p.y + mat._values[9] * p.z + mat._values[13] * p.w,
                mat._values[2] * p.x + mat._values[6] * p.y + mat._values[10] * p.z + mat._values[14] * p.w,
                mat._values[3] * p.x + mat._values[7] * p.y + mat._values[11] * p.z + mat._values[15] * p.w
            );
    }

    /// <summary>
    /// Applies Matrix4 mat to a Point4 (p.x, p.y, 0, 1) to result in a transformed Point4, then returns the x,y of that Point4 as a Point - ie transforms the Point using the Matrix4
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static Point Multiply(ref Matrix4 mat, Point p)
    {
        return new Point
            (
                mat._values[0] * p.X + mat._values[4] * p.Y + mat._values[12],
                mat._values[1] * p.X + mat._values[5] * p.Y + mat._values[13]
            );
    }
                 
    public static Matrix4 Translate(ref Matrix4 mat, float x, float y, float z)
    {
        Matrix4 transMat = CreateTranslation(x, y, z);
        return Multiply(ref mat, ref transMat);
    }
    
    public static Matrix4 RotateAroundZ(ref Matrix4 mat, float angleInDegrees)
    {
        Matrix4 rotMat = CreateRotationAroundZAxis(angleInDegrees);
        return Multiply(ref mat, ref rotMat);
    }
    public static Matrix4 RotateAroundPoint(ref Matrix4 mat, float angleInDegrees, Point p) => RotateAroundPoint(ref mat, angleInDegrees, p.X, p.Y);
    public static Matrix4 RotateAroundPoint(ref Matrix4 mat, float angleInDegrees, float x, float y)
    {
        Matrix4 translate1 = CreateTranslation(x, y, 0);
        Matrix4 rotate = CreateRotationAroundZAxis(angleInDegrees);
        Matrix4 translate2 = CreateTranslation(-x, -y, 0);

        Matrix4 result = Multiply(ref mat, ref translate1);
        result = Multiply(ref result, ref rotate);
        result = Multiply(ref result, ref translate2);

        return result;
    }
    
    public static Matrix4 ScaleAroundOrigin(ref Matrix4 mat, float scaleX, float scaleY, float scaleZ)
    {
        Matrix4 scaleMat = CreateScale(scaleX, scaleY, scaleZ);
        return Multiply(ref mat, ref scaleMat);
    }
    public static Matrix4 ScaleAroundPoint(ref Matrix4 mat, float scaleX, float scaleY, float x, float y)
    {
        Matrix4 translate1 = CreateTranslation(x, y, 0);
        Matrix4 scale = CreateScale(scaleX, scaleY, 0);
        Matrix4 translate2 = CreateTranslation(-x, -y, 0);

        Matrix4 result = Multiply(ref mat, ref translate1);
        result = Multiply(ref result, ref scale);
        result = Multiply(ref result, ref translate2);

        return result;
    }
    
    public static Matrix4 FlipX(ref Matrix4 mat)
    {
        return Multiply(ref mat, ref FlipXMatrix);
    }
    public static Matrix4 FlipY(ref Matrix4 mat)
    {
        return Multiply(ref mat, ref FlipYMatrix);
    }
    public static Matrix4 FlipZ(ref Matrix4 mat)
    {
        return Multiply(ref mat, ref FlipZMatrix);
    }
    
    public static Matrix4 Invert(Matrix4 mat)
    {
        int[] colIdx = { 0, 0, 0, 0 };
        int[] rowIdx = { 0, 0, 0, 0 };
        int[] pivotIdx = { -1, -1, -1, -1 };

        // convert the matrix to an array for easy looping
        float[,] inverse = {{mat._values[0], mat._values[4], mat._values[8], mat._values[12]},
                            {mat._values[1], mat._values[5], mat._values[9], mat._values[13]},
                            {mat._values[2], mat._values[6], mat._values[10], mat._values[14]},
                            {mat._values[3], mat._values[7], mat._values[11], mat._values[15]} };
        int icol = 0;
        int irow = 0;
        for (int i = 0; i < 4; i++)
        {
            // Find the largest pivot value
            float maxPivot = 0.0f;
            for (int j = 0; j < 4; j++)
            {
                if (pivotIdx[j] != 0)
                {
                    for (int k = 0; k < 4; ++k)
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

            // Swap rows over so pivot is on diagonal
            if (irow != icol)
            {
                for (int k = 0; k < 4; ++k)
                {
                    float f = inverse[irow, k];
                    inverse[irow, k] = inverse[icol, k];
                    inverse[icol, k] = f;
                }
            }

            rowIdx[i] = irow;
            colIdx[i] = icol;

            float pivot = inverse[icol, icol];
            // check for singular matrix
            if (pivot == 0.0f)
            {
                throw new Exception("Matrix is singular and cannot be inverted.");
            }

            // Scale row so it has a unit diagonal
            float oneOverPivot = 1.0f / pivot;
            inverse[icol, icol] = 1.0f;
            for (int k = 0; k < 4; ++k)
                inverse[icol, k] *= oneOverPivot;

            // Do elimination of non-diagonal elements
            for (int j = 0; j < 4; ++j)
            {
                // check this isn't on the diagonal
                if (icol != j)
                {
                    float f = inverse[j, icol];
                    inverse[j, icol] = 0.0f;
                    for (int k = 0; k < 4; ++k)
                        inverse[j, k] -= inverse[icol, k] * f;
                }
            }
        }

        for (int j = 3; j >= 0; --j)
        {
            int ir = rowIdx[j];
            int ic = colIdx[j];
            for (int k = 0; k < 4; ++k)
            {
                float f = inverse[k, ir];
                inverse[k, ir] = inverse[k, ic];
                inverse[k, ic] = f;
            }
        }

        return new Matrix4(
                            inverse[0, 0],
                            inverse[1, 0],
                            inverse[2, 0],
                            inverse[3, 0],
                            inverse[0, 1],
                            inverse[1, 1],
                            inverse[2, 1],
                            inverse[3, 1],
                            inverse[0, 2],
                            inverse[1, 2],
                            inverse[2, 2],
                            inverse[3, 2],
                            inverse[0, 3],
                            inverse[1, 3],
                            inverse[2, 3],
                            inverse[3, 3]
                           );
    }
    

    private float[] _values;
    
    public Matrix4(float m0, float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9, float m10, float m11, float m12, float m13, float m14, float m15)
    {
        _values = new float[16] { m0, m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15 };
    }
    public Matrix4(float[] values)
    {
        _values = values;
    }
    public Matrix4(Matrix4 matrix)
        :this()
    {
        _values = matrix.Values;
    }
    
    public float this[int x, int y]
    {
        get
        {
            if (x < 0 || x > 3 || y < 0 || y > 3)
                throw new Exception($"Requested an invalid Matrix4 index:{x},{y}");
            return _values[x * 4 + y];
        }
        set
        {
            if (x < 0 || x > 3 || y < 0 || y > 3)
                throw new Exception($"Requested an invalid Matrix4 index:{x},{y}");
            _values[x * 4 + y] = value;
        }
    }
    
    /// <summary>
    /// Underlying array of matrix elements held by this matrix. Indexed as
    /// (0 4 8 12)
    /// (1 5 9 13)
    /// (2 6 10 14)
    /// (3 7 11 15)
    /// </summary>
    public float[] Values { get { return _values; } set { _values = value; } }

    /// <summary>
    /// Gets the determinant of this matrix
    /// </summary>
    public float Determinant
    {
        get
        {
            return
                _values[0] * _values[5] * _values[10] * _values[15] - _values[0] * _values[5] * _values[14] * _values[11] + _values[0] * _values[9] * _values[14] * _values[7] - _values[0] * _values[9] * _values[6] * _values[15]
              + _values[0] * _values[13] * _values[6] * _values[11] - _values[0] * _values[13] * _values[10] * _values[7] - _values[4] * _values[9] * _values[14] * _values[3] + _values[4] * _values[9] * _values[2] * _values[15]
              - _values[4] * _values[13] * _values[2] * _values[11] + _values[4] * _values[13] * _values[10] * _values[3] - _values[4] * _values[1] * _values[10] * _values[15] + _values[4] * _values[1] * _values[14] * _values[11]
              + _values[8] * _values[13] * _values[2] * _values[7] - _values[8] * _values[13] * _values[6] * _values[3] + _values[8] * _values[1] * _values[6] * _values[15] - _values[8] * _values[1] * _values[14] * _values[7]
              + _values[8] * _values[5] * _values[14] * _values[3] - _values[8] * _values[5] * _values[2] * _values[15] - _values[12] * _values[1] * _values[6] * _values[11] + _values[12] * _values[1] * _values[10] * _values[7]
              - _values[12] * _values[5] * _values[10] * _values[3] + _values[12] * _values[5] * _values[2] * _values[11] - _values[12] * _values[9] * _values[2] * _values[7] + _values[12] * _values[9] * _values[6] * _values[3];
        }
    }
    
    /// <summary>
    /// Returns the transpose of this Matrix4 - all elements mirrored in [1 1] diagonal. The values of this instance will not be altered, returns a new matrix.
    /// </summary>
    /// <returns></returns>
    public Matrix4 Transpose()
    {
        return new Matrix4
            (
                _values[0],
                _values[4],
                _values[8],
                _values[12],
                _values[1],
                _values[5],
                _values[9],
                _values[13],
                _values[2],
                _values[6],
                _values[10],
                _values[14],
                _values[3],
                _values[7],
                _values[11],
                _values[15]
            );
    }

    /// <summary>
    /// Return a new Matrix that is the inverse of this matrix- singular matrices will throw an exception
    /// </summary>
    /// <returns></returns>
    public Matrix4 Inverse()
    {
        return Invert(this);
    }
    
    /// <summary>
    /// Scalar multiplication.
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix2 which holds the result of the multiplication</returns>
    public static Matrix4 operator *(float left, Matrix4 right)
    {
        return Multiply(ref right, left);
    }

    /// <summary>
    /// Scalar multiplication.
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix4 which holds the result of the multiplication</returns>
    public static Matrix4 operator *(Matrix4 left, float right) => Multiply(ref left, right);

    /// <summary>
    /// Matrix multiplication
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix4 which holds the result of the multiplication</returns>
    public static Matrix4 operator *(Matrix4 left, Matrix4 right)
    {
        return Multiply(ref left, ref right);
        //return Multiply(ref right, ref left);   //this needs to be changed back
    }

    /// <summary>
    /// Multiplying a matrix onto a Point, to return a point
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Point4 operator *(Matrix4 left, Point4 right) => Multiply(ref left, right);

    /// <summary>
    ///  Multiplying a matrix onto a Point - it will be padded to (x, y, 0, 1) to allow multiplying with a Point4 then the xy taken, to return a point
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Point operator *(Matrix4 left, Point right) => Multiply(ref left, right);

    /// <summary>
    /// Matrix addition
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix4 which holds the result of the addition</returns>
    public static Matrix4 operator +(Matrix4 left, Matrix4 right) => Add(ref left, ref right);

    /// <summary>
    /// Matrix subtraction
    /// </summary>
    /// <param name="left">left-hand operand</param>
    /// <param name="right">right-hand operand</param>
    /// <returns>A new Matrix4 which holds the result of the subtraction</returns>
    public static Matrix4 operator -(Matrix4 left, Matrix4 right) => Subtract(ref left, ref right);
    
    public override string ToString()
    {
        return string.Format("({0} {4} {8} {12})\n({1} {5} {9} {13})\n({2} {6} {10} {14})\n({3} {7} {11} {15})\n", _values[0], _values[1], _values[2], _values[3], _values[4], _values[5], _values[6], _values[7], _values[8], _values[9], _values[10], _values[11], _values[12], _values[13], _values[14], _values[15]);
    }
    
}
