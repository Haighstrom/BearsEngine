namespace BearsEngine.Source.Core;

public interface IAppInitialiser
{
    /// <summary>
    /// Gets the ApplicationSettings for this programme.
    /// </summary>
    public ApplicationSettings GetApplicationSettings();

    /// <summary>
    /// Any initialisation code to be done before the first scene is created goes here.
    /// </summary>
    public void Initialise();

    /// <summary>
    /// This creates and returns the first scene/screen
    /// </summary>
    public IScene CreateFirstScene();
}