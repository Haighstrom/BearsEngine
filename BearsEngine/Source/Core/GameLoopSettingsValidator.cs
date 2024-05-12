namespace BearsEngine;

internal class GameLoopSettingsValidator
{
    private const int MinimumUpdateRate = 5;
    private const int MaximumUpdateRate = 120;
    private const int MinimumRenderRate = 5;
    private const int MaximumRenderRate = 120;

    public GameLoopSettingsValidator()
    {
        
    }

    public void ValidateEngineSettings(GameLoopSettings settings)
    {
        if (settings.TargetUPS < MinimumUpdateRate || settings.TargetUPS > MaximumUpdateRate)
        {
            int newRate = Maths.Clamp(settings.TargetUPS, MinimumUpdateRate, MaximumUpdateRate);

            Log.Warning($"Requested an update rate of {settings.TargetUPS}, which is outside the bounds of the allowed values ({MaximumUpdateRate}-{MinimumUpdateRate}). Adjusting to {newRate}.");

            settings.TargetUPS = newRate;
        }

        if (settings.TargetFramesPerSecond < MinimumRenderRate || settings.TargetFramesPerSecond > MaximumRenderRate)
        {
            int newRate = Maths.Clamp(settings.TargetFramesPerSecond, MinimumRenderRate, MaximumRenderRate);

            Log.Warning($"Requested a render rate of {settings.TargetFramesPerSecond}, which is outside the bounds of the allowed values ({MinimumRenderRate}-{MaximumRenderRate}). Adjusting to {newRate}.");

            settings.TargetFramesPerSecond = newRate;
        }
    }
}
