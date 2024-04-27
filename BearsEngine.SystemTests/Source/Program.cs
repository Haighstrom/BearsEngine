using BearsEngine.SystemTests.Source;

var appSettings = AppSettings.Get();

using (var app = new App(appSettings)) //creates window
{
    var screenFactory = new ScreenFactory(app);

    var firstScene = screenFactory.CreateMainMenuScreen();

    app.Run(firstScene); //runs game loop inc. 
}
