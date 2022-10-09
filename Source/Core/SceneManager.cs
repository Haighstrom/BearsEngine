namespace BearsEngine;

internal class SceneManager : ISceneManager
{
    private bool _sceneStarted = false;
    private IScene? _nextScene;

    public SceneManager(IScene startingScene)
    {
        CurrentScene = startingScene;
    }

    public IScene CurrentScene { get; private set; }

    public void ChangeScene(IScene nextScene)
    {
        if (_nextScene != null)
            Console.WriteLine("Tried to set a new scene before a previous one had started.");

        _nextScene = nextScene;
    }

    public void UpdateScene(float elapsedTime)
    {
        if (_nextScene != null)
        {
            if (CurrentScene.Active)
                CurrentScene.End();

            CurrentScene.Dispose(); //add dispose:bool somewhere to clarify if this should happen? What if scene will be reused?

            CurrentScene = _nextScene;

            _nextScene = null;
            _sceneStarted = false;
        }

        if (CurrentScene.Active)
        {
            if (!_sceneStarted)
            {
                CurrentScene.Start();
                _sceneStarted = true;
            }

            CurrentScene.Update(elapsedTime);
        }
    }
}
