using System.Diagnostics.CodeAnalysis;

namespace BearsEngine;

public readonly struct Result<TValue, TError>
{
    private Result(TValue value)
    {
        Value = value;
        Error = default;
        Success = true;
    }

    private Result(TError error)
    {
        Value = default;
        Error = error;
        Success = false;
    }

    public TValue? Value { get; }

    public TError? Error { get; }

    [MemberNotNullWhen(true, nameof(Value))]
    public bool Success { get; }

    [MemberNotNullWhen(false, nameof(Value))]
    public readonly bool Failed => !Success;

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);

    public static implicit operator Result<TValue, TError>(TError error) => new(error);
}
