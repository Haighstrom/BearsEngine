﻿using HaighFramework.Window;

namespace BearsEngine;

internal interface IEngine : IDisposable
{
    IConsoleManager ConsoleManager { get; }

    IScene Scene { get; set; }

    IWindow Window { get; }

    void Run();
}