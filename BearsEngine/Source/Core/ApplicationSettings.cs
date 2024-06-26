﻿using BearsEngine.IO;

namespace BearsEngine;

public class ApplicationSettings
{
    public static ApplicationSettings Default => new();

    public ConsoleSettings ConsoleSettings { get; init; } = ConsoleSettings.Default;

    public GameLoopSettings GameLoopSettings { get; init; } = GameLoopSettings.Default;

    public IoSettings IoSettings { get; init; } = IoSettings.Default;

    public LogSettings LogSettings { get; init; } = LogSettings.Default;

    public WindowSettings WindowSettings { get; init; } = WindowSettings.Default;
}
