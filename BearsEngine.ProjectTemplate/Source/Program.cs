using BearsEngine.ProjectTemplate.Source.Setup;

var appSettings = Initialiser.GetApplicationSettings();

var engine = new GameEngine(appSettings);

var firstScene = Initialiser.CreateFirstScene(engine.Mouse);

engine.Run(firstScene);