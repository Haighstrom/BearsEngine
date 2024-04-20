namespace BearsEngine.Graphics;

public enum LoopType
{
    OneShot,
    Looping
}

public class Animation : Sprite, IUpdatable
{
    private const float DefaultLayer = 999;

    private int[] _framesToPlay = new int[] { 0 };
    private int _playIndex = 0;
    private LoopType _loopType;
    private Action? _onComplete;

    public Animation(float layer, string imgPath, float x, float y, float width, float height, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
        : base(layer, imgPath, x, y, width, height, spriteSheetColumns, spriteSheetRows)
    {
        AnimStepTime = animationStepTime;
    }

    public Animation(string imgPath, Point size, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
        : this(DefaultLayer, imgPath, 0, 0, size.X, size.Y, spriteSheetColumns, spriteSheetRows, animationStepTime)
    {
    }

    public Animation(string imgPath, float width, float height, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
        : this(DefaultLayer, imgPath, 0,0,width, height, spriteSheetColumns, spriteSheetRows, animationStepTime)
    {
    }

    public Animation(string imgPath, float x, float y, float width, float height, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
        : this(DefaultLayer, imgPath, x, y, width, height, spriteSheetColumns, spriteSheetRows, animationStepTime)
    {
    }

    public Animation(string imgPath, Rect r, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
        : this(DefaultLayer, imgPath, r.X, r.Y, r.W, r.H, spriteSheetColumns, spriteSheetRows, animationStepTime)
    {
    }

    public Animation(float layer, string imgPath, Point size, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
        : this(layer, imgPath, 0, 0, size.X, size.Y, spriteSheetColumns, spriteSheetRows, animationStepTime)
    {
    }

    public Animation(float layer, string imgPath, float width, float height, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
        : this(layer, imgPath, 0, 0, width, height, spriteSheetColumns, spriteSheetRows, animationStepTime)
    {
    }

    public Animation(float layer, string imgPath, Rect r, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
        : this(layer, imgPath, r.X, r.Y, r.W, r.H, spriteSheetColumns, spriteSheetRows, animationStepTime)
    {
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
    public void PlayMatching(Animation other) => PlayFrom(other._loopType, other._playIndex, other.TimeToNextFrame, other._framesToPlay);

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