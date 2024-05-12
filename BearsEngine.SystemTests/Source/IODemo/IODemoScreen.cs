using BearsEngine.Input;
using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source.IODemo;

internal class IODemoScreen : Screen
{
    private CyclingNumberButton[,] _CNBGrid;
    private readonly IScreenFactory _screenFactory;

    public IODemoScreen(IGameEngine app, IScreenFactory screenFactory)
        : base(app.Mouse)
    {
        _screenFactory = screenFactory;

        Add(new Button(app.Mouse, 1, new Rect(10, 10, 60, 40), Colour.LightGray, GV.Theme, "Return", () => app.ChangeScene(_screenFactory.CreateMainMenuScreen())));

        _CNBGrid = new CyclingNumberButton[3, 3];

        Add(_CNBGrid[0, 0] = new CyclingNumberButton(app.Mouse, 100, new Rect(100, 10, 50, 50), Colour.AliceBlue, 1));
        Add(_CNBGrid[1, 0] = new CyclingNumberButton(app.Mouse, 100, new Rect(155, 10, 50, 50), Colour.AliceBlue, 2));
        Add(_CNBGrid[2, 0] = new CyclingNumberButton(app.Mouse, 100, new Rect(210, 10, 50, 50), Colour.AliceBlue, 3));
        Add(_CNBGrid[0, 1] = new CyclingNumberButton(app.Mouse, 100, new Rect(100, 65, 50, 50), Colour.AliceBlue, 4));
        Add(_CNBGrid[1, 1] = new CyclingNumberButton(app.Mouse, 100, new Rect(155, 65, 50, 50), Colour.AliceBlue, 5));
        Add(_CNBGrid[2, 1] = new CyclingNumberButton(app.Mouse, 100, new Rect(210, 65, 50, 50), Colour.AliceBlue, 6));
        Add(_CNBGrid[0, 2] = new CyclingNumberButton(app.Mouse, 100, new Rect(100, 120, 50, 50), Colour.AliceBlue, 7));
        Add(_CNBGrid[1, 2] = new CyclingNumberButton(app.Mouse, 100, new Rect(155, 120, 50, 50), Colour.AliceBlue, 8));
        Add(_CNBGrid[2, 2] = new CyclingNumberButton(app.Mouse, 100, new Rect(210, 120, 50, 50), Colour.AliceBlue, 9));

        Add(new Button(app.Mouse, 100, new Rect(100, 180, 160, 25), Colour.LightGray, GV.Theme, "Save to JSON", SaveCNBGridAs2DArrayJson));
        Add(new Button(app.Mouse, 100, new Rect(100, 210, 160, 25), Colour.LightGray, GV.Theme, "Load from JSON", LoadCNBGridAs2DArrayJson));
    }

    public void SaveCNBGridAs2DArrayJson()
    {
        var data = new int[3, 3];

        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                data[j, i] = _CNBGrid[i, j].ButtonValue;

        var json = Files.SerialiseToJSON(data);

        Files.WriteTextFile("CNBGrid.json", json);
    }
    public void LoadCNBGridAs2DArrayJson()
    {
        var data = Files.ReadJsonFile<int[,]>("CNBGrid.json");

        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                _CNBGrid[i, j].ButtonValue = data[j, i];
    }
}
