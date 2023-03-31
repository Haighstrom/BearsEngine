namespace BearsEngine;

public static class Geometry
{
    public static List<Vertex> QuadToTris(Vertex topLeft, Vertex topRight, Vertex bottomLeft, Vertex bottomRight)
    {
        return new List<Vertex>()
        {
            bottomLeft, topRight, topLeft,
            bottomLeft, topRight, bottomRight
        };
    }

    /// <summary>
    /// Returns a vector of length 1 that points in the angle requested clockwise from up
    /// </summary>
    /// <param name="angleInDegrees">The angle of the vector, in degrees.</param>
    /// <returns>Returns a point of length 1.</returns>
    public static Point GetUnitVectorFromAngle(float angleInDegrees)
    {
        float x = (float)Math.Sin(angleInDegrees * Math.PI / 180);
        float y = (float)Math.Cos(angleInDegrees * Math.PI / 180);
        return new(x, y);
    }
}