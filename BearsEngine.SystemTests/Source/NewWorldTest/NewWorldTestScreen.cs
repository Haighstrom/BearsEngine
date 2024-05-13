using BearsEngine.Source.NewWorlds;

namespace BearsEngine.SystemTests.Source.NewWorldTest;

internal class NewWorldTestScreen : World
{
    public NewWorldTestScreen()
    {
        var entity = new BearsEngine.Source.NewWorlds.Entity(0, new Rect(100, 100, 100, 100));
        entity.Add(new Image(Colour.Yellow, new Point(100, 100)));
        Add(entity);
    }
}
