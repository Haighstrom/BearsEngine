namespace BearsEngine.Source.NewWorlds;

internal interface IContainer
{
    IEnumerable<IUpdatable> UpdatableEntities { get; }
    IEnumerable<IRenderable> RenderableEntities { get; }

    void Add(object entity);

    void Remove(object entity);

    void RemoveAll();
}
