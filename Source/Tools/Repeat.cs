using System.Threading;

namespace BearsEngine.Source.Tools;

public static class Repeat
{
    public static void TryMethod(Action method, int maxTries, TimeSpan waitTime)
    {
        int numTries = 0;
        while (numTries < maxTries)
        {
            try
            {
                method();
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

    public static TResult TryMethod<TResult>(Func<TResult> method, int maxTries, TimeSpan waitTime)
    {
        int numTries = 0;
        while (numTries < maxTries)
        {
            try
            {
                TResult result = method();
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

    public static void CallMethod(Action method, int times)
    {
        Ensure.ArgumentNotNegative(times, nameof(times));

        for (int i = 0; i < times; ++i)
            method();
    }
}