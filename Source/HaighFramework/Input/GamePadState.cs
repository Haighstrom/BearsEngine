namespace BearsEngine.Input;

public struct GamePadState : IEquatable<GamePadState>
{
    public bool IsConnected { get; internal set; }


    public bool Equals(GamePadState other)
    {
        throw new NotImplementedException();
    }
}

