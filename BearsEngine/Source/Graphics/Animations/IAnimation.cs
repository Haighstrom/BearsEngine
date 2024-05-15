
namespace BearsEngine.Graphics;

public interface IAnimation : ISprite, IUpdatable
{
    float AnimationStepTime { get; set; }

    event EventHandler? AnimationComplete;

    void Play(bool looping, params int[] frames);
    void Play(IList<int> frames, int currentFrameIndex, float currentFrameRemainingTime, bool looping = true);
    void Play(params int[] frames);
}
