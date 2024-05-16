using BearsEngine.OpenGL;

namespace BearsEngine.Graphics;

/// <summary>
/// A graphic made up of multiple parts that need to sync their animations
/// </summary>
public class MultiLayerAnimation : RectGraphicBase, IMultiLayerAnimation
{
    private const bool LoopsByDefault = true;
    private const float DefaultLayer = 999;
    private const float DefaultAnimationStepTime = 0.2f;
    private const int Vertices = 4;

    private IList<int> _framesToPlay = new int[] { 0 };
    private int _currentFrameIndex = 0;
    private bool _looping;
    private float _timeToNextFrame;
    private bool _verticesNeedBuffering = true;
    private int _frame = 0;
    private readonly Dictionary<float, List<ISpriteTexture>> _textures = new();

    public MultiLayerAnimation(float x, float y, float width, float height, float layer = DefaultLayer, float animationStepTime = DefaultAnimationStepTime)
        : base(layer, x, y, width, height)
    {
        AnimationStepTime = animationStepTime;
    }

    public MultiLayerAnimation(Rect r, float layer = DefaultLayer, float animationStepTime = DefaultAnimationStepTime)
        : this(r.X, r.Y, r.W, r.H, layer, animationStepTime)
    {
    }

    public MultiLayerAnimation(Point position, float width, float height, float layer = DefaultLayer, float animationStepTime = DefaultAnimationStepTime)
        : this(position.X, position.Y, width, height, layer, animationStepTime)
    {
    }

    public MultiLayerAnimation(Point size, float layer = DefaultLayer, float animationStepTime = DefaultAnimationStepTime)
        : this(0, 0, size.X, size.Y, layer, animationStepTime)
    {
    }

    public MultiLayerAnimation(float width, float height, float layer = DefaultLayer, float animationStepTime = DefaultAnimationStepTime)
        : this(0, 0, width, height, layer, animationStepTime)
    {
    }

    private ISpriteTexture ReferenceTexture
    {
        get
        {
            if (_textures.IsEmpty()) 
            {
                throw new InvalidOperationException("Cannot Add or Play MultiLayerAnimation until at least one Texture has been added using AddTexture.");
            }

            return _textures.First().Value.First();
        }
    }

    public override float W
    {
        set
        {
            base.W = value;
            _verticesNeedBuffering = true;
        }
    }

    public override float H
    {
        set
        {
            base.H = value;
            _verticesNeedBuffering = true;
        }
    }

    public override Colour Colour
    {
        set
        {
            base.Colour = value;
            _verticesNeedBuffering = true;
        }
    }

    public bool Active { get; set; } = false;

    public float AnimationStepTime { get; set; }

    public IList<int> AllFrames => Enumerable.Range(0, Frames).ToArray();

    public int Frame
    {
        get => _frame;
        set
        {
            if (_frame != value)
            {
                _frame = Maths.Mod(value, Frames);

                _verticesNeedBuffering = true;
            }
        }
    }

    public int Frames => ReferenceTexture.Frames;

    public event EventHandler? AnimationComplete;

    private (Point UV1, Point UV2, Point UV3, Point UV4) GetUVCoordinates()
    {
        return ReferenceTexture.GetUVCoordinates(Frame);
    }

    private void BufferVertices()
    {
        var (uv1, uv2, uv3, uv4) = GetUVCoordinates();

        var vertices = new Vertex[Vertices]
        {
            new(new Point(0, 0), Colour, uv1),
            new(new Point(W, 0), Colour, uv2),
            new(new Point(0, H), Colour, uv3),
            new(new Point(W, H), Colour, uv4)
        };

        OpenGLHelper.BufferData(BUFFER_TARGET.GL_ARRAY_BUFFER, vertices.Length * Vertex.STRIDE, vertices, USAGE_PATTERN.GL_STREAM_DRAW);
    }

    protected void OnAnimationComplete()
    {
        AnimationComplete?.Invoke(this, EventArgs.Empty);
    }

    public void AddTexture(ISpriteTexture texture, float layer)
    {
        //ensure new texture is compatible
        if (_textures.Any())
        {
            Ensure.That(texture.Frames == ReferenceTexture.Frames);
            Ensure.That(texture.FrameWidth == ReferenceTexture.FrameWidth);
            Ensure.That(texture.FrameHeight == ReferenceTexture.FrameHeight);
        }

        if (!_textures.ContainsKey(layer))
        {
            _textures.Add(layer, new List<ISpriteTexture>());
        }

        _textures[layer].Add(texture);
    }

    public void RemoveTexture(ISpriteTexture texture)
    {
        var layer = _textures.First(x => x.Value.Contains(texture)).Key;

        _textures[layer].Remove(texture);

        if (!_textures[layer].Any())
        {
            _textures.Remove(layer);
        }
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

    public override void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        if (W == 0 || H == 0)
        {
            return;
        }

        var mv = Matrix3.Translate(ref modelView, X, Y);

        OpenGLHelper.BindVertexBuffer(VertexBuffer);

        if (_verticesNeedBuffering)
        {
            BufferVertices();
            _verticesNeedBuffering = false;
        }

        var keys = _textures.Keys.OrderDescending();

        foreach (var key in keys)
        {
            foreach (var texture in _textures[key])
            {
                OpenGLHelper.BindTexture(texture);

                Shader.Render(ref projection, ref mv, Vertices, PRIMITIVE_TYPE.GL_TRIANGLE_STRIP);
            }
        }

        OpenGLHelper.UnbindVertexBuffer();
    }
}
