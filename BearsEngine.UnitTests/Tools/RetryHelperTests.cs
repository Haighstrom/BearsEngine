namespace BearsEngine.UnitTests.Tools;

using System;
using BearsEngine.Source.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RetryHelperTests
{
    [TestMethod]
    public void Retry_Action_Successful()
    {
        int numTries = 0;
        Repeat.TryMethod(() =>
        {
            if (numTries < 2)
            {
                numTries++;
                throw new Exception("Fail");
            }
        }, maxTries: 3, waitTime: TimeSpan.FromSeconds(1));

        Assert.AreEqual(2, numTries);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Retry_Action_MaxTriesExceeded()
    {
        Repeat.TryMethod(() =>
        {
            throw new ArgumentException("Invalid argument");
        }, maxTries: 2, waitTime: TimeSpan.FromSeconds(1));
    }

    [TestMethod]
    public void Retry_Func_Successful()
    {
        int numTries = 0;
        int result = Repeat.TryMethod(() =>
        {
            if (numTries < 2)
            {
                numTries++;
                throw new Exception("Fail");
            }
            return 42;
        }, maxTries: 3, waitTime: TimeSpan.FromSeconds(1));

        Assert.AreEqual(42, result);
        Assert.AreEqual(2, numTries);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Retry_Func_MaxTriesExceeded()
    {
        Repeat.TryMethod(() =>
        {
            throw new InvalidOperationException("Invalid operation");
        }, maxTries: 2, waitTime: TimeSpan.FromSeconds(1));
    }
}

