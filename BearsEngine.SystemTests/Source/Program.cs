using BearsEngine.SystemTests.Source;
using BearsEngine.SystemTests.Source.NewWorldTest;

var appSettings = AppSettings.Get();

using (var engine = new GameEngine(appSettings)) //creates window
{
    var screenFactory = new ScreenFactory(engine);

    //var firstScene = screenFactory.CreateMainMenuScreen();
    var firstScene = new NewWorldTestScreen();

    engine.Run(firstScene); //runs game loop inc. 
}
