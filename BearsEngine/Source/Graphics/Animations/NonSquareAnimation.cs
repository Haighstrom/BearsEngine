namespace BearsEngine.Graphics;

public class NonSquareAnimation : NonSquareSprite, IUpdatable
{
    private int[] _framesToPlay = new int[] { 0 };
    private int _playIndex = 0;
    private bool _looping;
    private Action? _onComplete;

    public NonSquareAnimation(float layer, string imgPath, float x, float y, IList<(Point OutputSize, Rect TextureSource)> frames, float animationStepTime = 0.1f)
        : base(layer, imgPath, x, y, frames)
    {
        AnimStepTime = animationStepTime;
    }

    public bool Active { get; set; } = true;

    public float TimeToNextFrame { get; set; }

    public float AnimStepTime { get; set; }

    public bool Playing { get; set; }

    public int[] AllFrames => Enumerable.Range(0, TotalFrames).ToArray();

    public event EventHandler? AnimationComplete;

    protected void OnAnimationComplete()
    {
        _onComplete?.Invoke();

        AnimationComplete?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Starts this animation copying another animation, including its current frame and remaining frame time
    /// </summary>
    /// <param name="other"></param>
    public void PlayMatching(NonSquareAnimation other) => PlayFrom(other._looping, other._playIndex, other.TimeToNextFrame, other._framesToPlay);

    public void PlayAllOnce(Action actionOnComplete)
    {
        Play(false, AllFrames);
        _onComplete = actionOnComplete;
    }

    public void PlayAllOnce() => Play(false, AllFrames);

    public void PlayAllLooping() => Play(true, AllFrames);

    /// <summary>
    /// Fixes the Sprite with a single frame
    /// </summary>
    /// <param name="frame"></param>
    public void Play(int frame) => Play(false, frame);

    public void Play(bool looping, params int[] frames) => PlayFrom(looping, 0, AnimStepTime, frames);

    public void PlayOnce(Action actionOnComplete, int startFrame, params int[] frames)
    {
        PlayFrom(false, 0, AnimStepTime, frames);
        _onComplete = actionOnComplete;
    }

    public void PlayFrom(bool looping, int fromIndex, float currentFrameRemainingTime, params int[] frames)
    {
        Playing = true;
        _framesToPlay = frames;
        _playIndex = fromIndex;
        TimeToNextFrame = currentFrameRemainingTime;
        Frame = frames[fromIndex];
        _looping = looping;
    }

    public virtual void Update(float elapsed)
    {
        if (Playing && _framesToPlay.Length > 1)
        {
            TimeToNextFrame -= (float)elapsed;
            while (TimeToNextFrame <= 0)
            {
                if (_playIndex == _framesToPlay.Length - 1 && !_looping)
                {
                    Playing = false;
                    TimeToNextFrame = 0;
                    OnAnimationComplete();
                    break;
                }
                else
                {
                    _playIndex = Maths.Mod(_playIndex + 1, _framesToPlay.Length);
                    Frame = _framesToPlay[_playIndex];
                    TimeToNextFrame += AnimStepTime;
                }
            }
        }
    }
}
