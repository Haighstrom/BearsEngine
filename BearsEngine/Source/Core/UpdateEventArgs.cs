namespace BearsEngine;

internal class UpdateEventArgs : EventArgs
{
    public UpdateEventArgs(float elapsedTime)
    {
        ElapsedTime = elapsedTime;
    }

    public float ElapsedTime { get; }
}
