using System.Diagnostics;

namespace BearsEngine;

public static class Ensure
{
    /// <summary>
    /// Ensures that the specified argument is not null.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <exception cref="ArgumentNullException">Throws an ArgumentNullException if the argument was null.</exception>
    [DebuggerStepThrough]
    public static void ArgumentNotNull(object argument, string argumentName)
    {
        if (argument == null)
        {
            throw new ArgumentNullException(argumentName);
        }
    }
}