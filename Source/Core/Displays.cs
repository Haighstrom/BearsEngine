using HaighFramework.Displays;
using HaighFramework.Input;
using HaighFramework.Window;

namespace BearsEngine;

/// <summary>
/// Provides access to Monitor information and features
/// </summary>
public static class Displays
{
    private static IDisplayManager? _instance;

    internal static IDisplayManager Instance
    {
        get
        {
            if (_instance is null)
                throw new InvalidOperationException($"You must call {nameof(GameEngine)}.{nameof(GameEngine.Run)} before accessing functions within {nameof(Displays)}.");

            return _instance;
        }
        set => _instance = value;
    }
}