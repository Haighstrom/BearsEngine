using BearsEngine.ProjectTemplate.Source.Setup;

var appSettings = Initialiser.GetApplicationSettings();

App app = new(appSettings);

var sceneFactory = Initialiser.CreateFirstScene(app.Mouse);

app.Run(sceneFactory);