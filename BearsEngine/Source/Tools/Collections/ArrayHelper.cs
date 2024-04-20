namespace BearsEngine;

public static class ArrayHelper
{
    public static T[] CreateFilledArray<T>(int size, T value)
    {
        var ret = new T[size];
        Array.Fill(ret, value);
        return ret;
    }

    public static T[,] CreateFilledArray<T>(int arrayWidth, int arrayHeight, T value)
    {
        var ret = new T[arrayWidth, arrayHeight];

        for (int i = 0; i < arrayWidth; i++)
            for (int j = 0; j < arrayHeight; j++)
                ret[i, j] = value;

        return ret;
    }
}