using System.Threading;

namespace BearsEngine.Source.Tools;

public static class Retry
{
    public static void TryMethod(Action action, int maxTries, TimeSpan waitTime)
    {
        int numTries = 0;
        while (numTries < maxTries)
        {
            try
            {
                action();
                return;
            }
            catch
            {
                numTries++;
                if (numTries == maxTries)
                {
                    throw;
                }
                Thread.Sleep(waitTime);
            }
        }

        //todo: throw new System.Diagnostics.UnreachableException(); Needs .NET 7
        throw new Exception("Unreachable code");
    }

    public static TResult TryMethod<TResult>(Func<TResult> func, int maxTries, TimeSpan waitTime)
    {
        int numTries = 0;
        while (numTries < maxTries)
        {
            try
            {
                TResult result = func();
                return result;
            }
            catch
            {
                numTries++;
                if (numTries == maxTries)
                {
                    throw;
                }
                Thread.Sleep(waitTime);
            }
        }

        throw new Exception("Unreachable code");
    }
}