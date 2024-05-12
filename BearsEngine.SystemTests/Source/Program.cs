using BearsEngine.SystemTests.Source;

var appSettings = AppSettings.Get();

using (var engine = new GameEngine(appSettings)) //creates window
{
    var screenFactory = new ScreenFactory(engine);

    var firstScene = screenFactory.CreateMainMenuScreen();

    engine.Run(firstScene); //runs game loop inc. 
}
