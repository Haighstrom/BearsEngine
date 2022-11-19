namespace BearsEngine;

internal class ConsoleWarningException : Exception
{
    public ConsoleWarningException(string message)
        : base(message)
    {
    }
}
