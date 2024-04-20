namespace BearsEngine.Graphics;

public class NonSquareAnimation : NonSquareSprite, IUpdatable
{
    private int[] _framesToPlay = new int[] { 0 };
    private int _playIndex = 0;
    private LoopType _loopType;
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
        if (_onComplete != null)
            _onComplete();

        AnimationComplete?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Starts this animation copying another animation, including its current frame and remaining frame time
    /// </summary>
    /// <param name="other"></param>
    public void PlayMatching(NonSquareAnimation other) => PlayFrom(other._loopType, other._playIndex, other.TimeToNextFrame, other._framesToPlay);

    public void PlayAllOnce(Action actionOnComplete)
    {
        Play(LoopType.OneShot, AllFrames);
        _onComplete = actionOnComplete;
    }

    public void PlayAllOnce() => Play(LoopType.OneShot, AllFrames);

    public void PlayAllLooping() => Play(LoopType.Looping, AllFrames);

    /// <summary>
    /// Fixes the Sprite with a single frame
    /// </summary>
    /// <param name="frame"></param>
    public void Play(int frame) => Play(LoopType.OneShot, frame);

    public void Play(LoopType loopType, params int[] frames) => PlayFrom(loopType, 0, AnimStepTime, frames);

    public void PlayOnce(Action actionOnComplete, int startFrame, params int[] frames)
    {
        PlayFrom(LoopType.OneShot, 0, AnimStepTime, frames);
        _onComplete = actionOnComplete;
    }

    public void PlayFrom(LoopType loopType, int fromIndex, float currentFrameRemainingTime, params int[] frames)
    {
        Playing = true;
        _framesToPlay = frames;
        _playIndex = fromIndex;
        TimeToNextFrame = currentFrameRemainingTime;
        Frame = frames[fromIndex];
        _loopType = loopType;
    }

    public virtual void Update(float elapsed)
    {
        if (Playing && _framesToPlay.Length > 1)
        {
            TimeToNextFrame -= (float)elapsed;
            while (TimeToNextFrame <= 0)
            {
                if (_playIndex == _framesToPlay.Length - 1 && _loopType == LoopType.OneShot)
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