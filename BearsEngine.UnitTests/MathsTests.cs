namespace BearsEngine.UnitTests;

[TestClass]
public class MathsTests
{
    [TestMethod]
    public void ClampIntsPositiveTest()
    {
        Assert.AreEqual(Maths.Clamp(7, 5, 10), 7);
        Assert.AreEqual(Maths.Clamp(1, 5, 10), 5);
        Assert.AreEqual(Maths.Clamp(12, 5, 10), 10);
        Assert.AreEqual(Maths.Clamp(10, 5, 5), 5);
    }

    [TestMethod]
    public void ClampIntsNegativeTest()
    {
        Assert.AreEqual(Maths.Clamp(-3, -5, -2), -3);
        Assert.AreEqual(Maths.Clamp(0, -5, -2), -2);
        Assert.AreEqual(Maths.Clamp(-10, -5, -2), -5);
        Assert.AreEqual(Maths.Clamp(-10, -5, -5), -5);
    }

    [TestMethod]
    public void ClampIntsPositiveAndNegativeTest()
    {
        Assert.AreEqual(Maths.Clamp(1, -5, 5), 1);
        Assert.AreEqual(Maths.Clamp(-10, -5, 5), -5);
        Assert.AreEqual(Maths.Clamp(10, -5, 5), 5);
        Assert.AreEqual(Maths.Clamp(5, -5, -5), -5);
    }

    [TestMethod]
    public void ClampIntsMaxAndMinValueTest()
    {
        Assert.AreEqual(Maths.Clamp(0, int.MinValue, int.MaxValue), 0);
        Assert.AreEqual(Maths.Clamp(int.MinValue, 0, int.MaxValue), 0);
        Assert.AreEqual(Maths.Clamp(int.MaxValue, int.MinValue, 0), 0);
    }

    [TestMethod]
    public void ClampIntsExceptionsTest()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Maths.Clamp(0, 4, 2));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Maths.Clamp(0, -2, -4));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Maths.Clamp(0, 2, -2));
    }

    [TestMethod]
    public void ClampFloatsTest()
    {
        //float
        Assert.AreEqual(Maths.Clamp(7.5f, 5.5f, 10.5f), 7.5f);
        Assert.AreEqual(Maths.Clamp(1.5f, 5.5f, 10.5f), 5.5f);
        Assert.AreEqual(Maths.Clamp(12.5f, 5.5f, 10.5f), 10.5f);
        Assert.AreEqual(Maths.Clamp(-3.5f, -5.5f, -2.5f), -3.5f);
        Assert.AreEqual(Maths.Clamp(0, -5.5f, -2.5f), -2.5f);
        Assert.AreEqual(Maths.Clamp(-10.5f, -5.5f, -2.5f), -5.5f);
        Assert.AreEqual(Maths.Clamp(1.5f, -5.5f, 5.5f), 1.5f);
        Assert.AreEqual(Maths.Clamp(-10.5f, -5.5f, 5.5f), -5.5f);
        Assert.AreEqual(Maths.Clamp(10.5f, -5.5f, 5.5f), 5.5f);
        Assert.AreEqual(Maths.Clamp(10.5f, 5.5f, 5.5f), 5.5f);
        Assert.AreEqual(Maths.Clamp(0, float.MinValue, float.MaxValue), 0);
        Assert.AreEqual(Maths.Clamp(float.MinValue, 0, float.MaxValue), 0);
        Assert.AreEqual(Maths.Clamp(float.MaxValue, float.MinValue, 0), 0);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Maths.Clamp(0, 4.5f, 2.5f));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Maths.Clamp(0, -2.5f, -4.5f));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Maths.Clamp(0, 2.5f, -2.5f));
    }

    [TestMethod]
    public void MinTest()
    {
        //int
        Assert.AreEqual(Maths.Min(5, 10), 5);
        Assert.AreEqual(Maths.Min(10, 5), 5);
        Assert.AreEqual(Maths.Min(1, 1), 1);
        Assert.AreEqual(Maths.Min(-10, 0), -10);
        Assert.AreEqual(Maths.Min(-10, -5), -10);
        Assert.AreEqual(Maths.Min(-5, 10), -5);
        Assert.AreEqual(Maths.Min(int.MaxValue, int.MinValue), int.MinValue);

        //float
        Assert.AreEqual(Maths.Min(5.5f, 10.5f), 5.5f);
        Assert.AreEqual(Maths.Min(10.5f, 5.5f), 5.5f);
        Assert.AreEqual(Maths.Min(1.5f, 1.5f), 1.5f);
        Assert.AreEqual(Maths.Min(-10.5f, 0f), -10.5f);
        Assert.AreEqual(Maths.Min(-10.5f, -5.5f), -10.5);
        Assert.AreEqual(Maths.Min(-5.5f, 10.5f), -5.5f);
        Assert.AreEqual(Maths.Min(float.MaxValue, float.MinValue), float.MinValue);
    }

    [TestMethod]
    public void MaxTest()
    {
        //int
        Assert.AreEqual(Maths.Max(5, 10), 10);
        Assert.AreEqual(Maths.Max(10, 5), 10);
        Assert.AreEqual(Maths.Max(1, 1), 1);
        Assert.AreEqual(Maths.Max(-10, 0), 0);
        Assert.AreEqual(Maths.Max(-10, -5), -5);
        Assert.AreEqual(Maths.Max(-5, 10), 10);
        Assert.AreEqual(Maths.Max(int.MaxValue, int.MinValue), int.MaxValue);

        //float
        Assert.AreEqual(Maths.Max(5.5f, 10.5f), 10.5f);
        Assert.AreEqual(Maths.Max(10.5f, 5.5f), 10.5f);
        Assert.AreEqual(Maths.Max(1.5f, 1.5f), 1.5f);
        Assert.AreEqual(Maths.Max(-10.5f, 0), 0);
        Assert.AreEqual(Maths.Max(-10.5f, -5.5f), -5.5f);
        Assert.AreEqual(Maths.Max(-5.5f, 10.5f), 10.5f);
        Assert.AreEqual(Maths.Max(float.MaxValue, float.MinValue), float.MaxValue);
    }
}