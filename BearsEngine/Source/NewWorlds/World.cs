using BearsEngine.OpenGL;

namespace BearsEngine.Source.NewWorlds;

public class World : IWorld
{
    private readonly IContainer _container;

    public World()
    {
        _container = new Container();
    }

    public Colour BackgroundColour { get; set; } = Colour.CornflowerBlue;

    public bool Active { get; set; } = true;

    public bool Visible { get; set; } = true;

    public IEnumerable<IUpdatable> UpdatableEntities => _container.UpdatableEntities;

    public IEnumerable<IRenderable> RenderableEntities => _container.RenderableEntities;

    public void Add(object e) => _container.Add(e);

    public void Remove(object e) => _container.Remove(e);

    public void RemoveAll() => _container.RemoveAll();

    public virtual void Render(ref Matrix3 projection, ref Matrix3 modelView)
    {
        //todo: move this up to engine
        OpenGL32.glClearColor(BackgroundColour.R / 255f, BackgroundColour.G / 255f, BackgroundColour.B / 255f, BackgroundColour.A / 255f);
        OpenGL32.glClear(BUFFER_MASK.GL_COLOR_BUFFER_BIT | BUFFER_MASK.GL_DEPTH_BUFFER_BIT);

        OpenGL32.glEnable(GLCAP.GL_BLEND);
        OpenGL32.glBlendFunc(BLEND_SCALE_FACTOR.GL_ONE, BLEND_SCALE_FACTOR.GL_ONE_MINUS_SRC_ALPHA);

        foreach (var entity in RenderableEntities)
        {
            if (entity.Visible)
            {
                entity.Render(ref projection, ref modelView);
            }
        }
    }

    public virtual void Update(float elapsed)
    {
        foreach (var entity in UpdatableEntities)
        {
            if (entity.Active)
            {
                entity.Update(elapsed);
            }
        }
    }

    public void Start()
    {
        Log.Warning("plan to remove this");
    }

    public void End()
    {
        Log.Warning("plan to remove this");
    }

    public void Dispose()
    {
        Log.Warning("not implemented; no crash yet");
    }
}
