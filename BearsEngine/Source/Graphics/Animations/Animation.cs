namespace BearsEngine.Graphics;

public class Animation : Sprite, IUpdatable, IAnimation
{
    private const bool LoopsByDefault = true;
    private const float DefaultLayer = 999;
    private const float DefaultAnimationStepTime = 0.2f;

    private IList<int> _framesToPlay = new int[] { 0 };
    private int _currentFrameIndex = 0;
    private bool _looping;
    private float _timeToNextFrame;

    public Animation(ISpriteTexture texture, float x, float y, float width, float height, float layer = DefaultLayer, float animationStepTime = DefaultAnimationStepTime)
        : base(texture, x, y, width, height, layer)
    {
        AnimationStepTime = animationStepTime;
    }

    public Animation(ISpriteTexture texture, Rect r, float layer = DefaultLayer, float animationStepTime = DefaultAnimationStepTime)
        : this(texture, r.X, r.Y, r.W, r.H, layer, animationStepTime)
    {
    }

    public Animation(ISpriteTexture texture, Point position, float width, float height, float layer = DefaultLayer, float animationStepTime = DefaultAnimationStepTime)
        : this(texture, position.X, position.Y, width, height, layer, animationStepTime)
    {
    }

    public Animation(ISpriteTexture texture, Point size, float layer = DefaultLayer, float animationStepTime = DefaultAnimationStepTime)
        : this(texture, 0, 0, size.X, size.Y, layer, animationStepTime)
    {
    }

    public Animation(ISpriteTexture texture, float width, float height, float layer = DefaultLayer, float animationStepTime = DefaultAnimationStepTime)
        : this(texture, 0, 0, width, height, layer, animationStepTime)
    {
    }

    public bool Active { get; set; } = false;

    public float AnimationStepTime { get; set; }

    public IList<int> AllFrames => Enumerable.Range(0, Frames).ToArray();

    public event EventHandler? AnimationComplete;

    protected void OnAnimationComplete()
    {
        AnimationComplete?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Start playing the animation going through the specified frames, starting at the index specified and with the remaining time of that frame, then continuing, taking AnimationStepTime per frame after that.
    /// </summary>
    /// <param name="frames">the frames to include (0-indexed)</param>
    /// <param name="currentFrameIndex"></param>
    /// <param name="currentFrameRemainingTime"></param>
    /// <param name="looping"></param>
    public void Play(IList<int> frames, int currentFrameIndex, float currentFrameRemainingTime, bool looping = LoopsByDefault)
    {
        Active = true;
        _framesToPlay = frames;
        _currentFrameIndex = currentFrameIndex;
        _timeToNextFrame = currentFrameRemainingTime;
        Frame = frames[currentFrameIndex];
        _looping = looping;
    }

    /// <summary>
    /// Plays the animation going through the specified frames, taking AnimationStepTime per frame. If no frames are provided, all frames will play.
    /// </summary>
    /// <param name="looping">Whether the loop the animation.</param>
    /// <param name="frames">The 0-indexed frames to include. Omit to loop all frames.</param>
    public void Play(bool looping, params int[] frames)
    {
        if (!frames.Any())
        {
            Play(AllFrames, 0, AnimationStepTime, looping);
        }
        else
        {
            Play(frames, 0, AnimationStepTime, looping);
        }
    }

    /// <summary>
    /// Plays the animation going through the specified frames, taking AnimationStepTime per frame.
    /// </summary>
    /// <param name="frames">The 0-indexed frames to include</param>
    public void Play(params int[] frames) => Play(LoopsByDefault, frames);

    /// <summary>
    /// Starts this animation copying another animation frame indices, current frame and remaining frame time
    /// </summary>
    /// <param name="other">The animation to mimic</param>
    public void Play(Animation other) => Play(other._framesToPlay, other._currentFrameIndex, other._timeToNextFrame, other._looping);

    public virtual void Update(float elapsed)
    {
        if (Active && _framesToPlay.Count > 1)
        {
            _timeToNextFrame -= elapsed;

            while (_timeToNextFrame <= 0)
            {
                if (_currentFrameIndex == _framesToPlay.Count - 1 && !_looping)
                {
                    Active = false;

                    _timeToNextFrame = 0;

                    OnAnimationComplete();

                    break;
                }
                else
                {
                    _currentFrameIndex = Maths.Mod(_currentFrameIndex + 1, _framesToPlay.Count);

                    Frame = _framesToPlay[_currentFrameIndex];

                    _timeToNextFrame += AnimationStepTime;
                }
            }
        }
    }
}
