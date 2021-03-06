namespace BearsEngine.Graphics
{
    public enum LoopType
    {
        OneShot,
        Looping
    }

    public class Animation : Sprite, IUpdatable
    {
        #region Fields
        private int[] _framesToPlay = new int[] { 0 };
        private int _playIndex = 0;
        private float _timeToNextFrame;
        private LoopType _looping;
        private Action _onComplete;
        #endregion

        #region Constructors
        public Animation(string imgPath, Point size, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
            : this(imgPath, new Rect(size), spriteSheetColumns, spriteSheetRows, animationStepTime)
        {
        }

        public Animation(string imgPath, float width, float height, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
            : this(imgPath, new Rect(width, height), spriteSheetColumns, spriteSheetRows, animationStepTime)
        {
        }

        public Animation(string imgPath, int x, int y, float width, float height, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
            : this(imgPath, new Rect(x, y, width, height), spriteSheetColumns, spriteSheetRows, animationStepTime)
        {
        }

        public Animation(string imgPath, Rect r, int spriteSheetColumns, int spriteSheetRows, float animationStepTime = 0.1f)
            : base(imgPath, r, spriteSheetColumns, spriteSheetRows)
        {
            AnimStepTime = animationStepTime;
        }
        #endregion

        #region IUpdatable
        public bool Active { get; set; } = true;

        #region Update
        public virtual void Update(double elapsed)
        {
            if (Playing && _framesToPlay.Length > 1)
            {
                _timeToNextFrame -= (float)elapsed;
                while (_timeToNextFrame <= 0)
                {
                    if (_playIndex == _framesToPlay.Length - 1 && _looping == LoopType.OneShot)
                    {
                        Playing = false;
                        _timeToNextFrame = 0;
                        OnAnimationComplete();
                        break;
                    }
                    else
                    {
                        _playIndex = HF.Maths.Mod(_playIndex + 1, _framesToPlay.Length);
                        Frame = _framesToPlay[_playIndex];
                        _timeToNextFrame += AnimStepTime;
                    }
                }
            }
        }
        #endregion
        #endregion

        #region Properties
        public float AnimStepTime { get; set; }

        public bool Playing { get; set; }

        public int[] AllFrames => Enumerable.Range(0, TotalFrames).ToArray();
        #endregion

        #region Methods
        public void PlayAllOnce(Action actionOnComplete)
        {
            Play(LoopType.OneShot, AllFrames);
            _onComplete = actionOnComplete;
        }
        public void PlayAllOnce() => Play(LoopType.OneShot, AllFrames);
        public void PlayAllLooping() => Play(LoopType.Looping, AllFrames);

        #region Play
        /// <summary>
        /// Fixes the Sprite with a single frame
        /// </summary>
        /// <param name="frame"></param>
        public void Play(int frame) => Play(LoopType.OneShot, frame);

        public void Play(LoopType loopType, params int[] frames) => PlayFrom(loopType, 0, AnimStepTime, frames);
        #endregion

        #region PlayOnce
        public void PlayOnce(Action actionOnComplete, int startFrame, params int[] frames)
        {
            PlayFrom(LoopType.OneShot, 0, AnimStepTime, frames);
            _onComplete = actionOnComplete;
        }
        #endregion

        #region PlayFrom
        public void PlayFrom(LoopType loopType, int fromIndex, float currentFrameRemainingTime, params int[] frames)
        {
            Playing = true;
            _framesToPlay = frames;
            _playIndex = fromIndex;
            _timeToNextFrame = currentFrameRemainingTime;
            Frame = frames[fromIndex];
            _looping = loopType;
        }
        #endregion

        #region OnAnimationComplete
        protected void OnAnimationComplete()
        {
            if (_onComplete != null)
                _onComplete();

            AnimationComplete?.Invoke(this, EventArgs.Empty);
        }
        #endregion
        #endregion

        public event EventHandler AnimationComplete;
    }
}