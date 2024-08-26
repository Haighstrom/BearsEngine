namespace BearsEngine;

internal class SceneManager : ISceneManager
{
    private bool _sceneStarted = false;
    private IScene? _currentScene, _nextScene;

    public SceneManager()
    {
    }

    public IScene CurrentScene => _currentScene ?? throw new InvalidOperationException($"Tried to access {nameof(SceneManager)}.{nameof(CurrentScene)} before it was set.");

    public void ChangeScene(IScene scene)
    {
        if (_currentScene is null) //handle case this is the first scene being set
        {
            _currentScene = scene;
        }
        else //changing scene
        {
            if (_nextScene is not null)
            {
                Log.Warning("Set a new scene before a previous one had started.");
            }

            _nextScene = scene;
        }
    }

    public void UpdateScene(float elapsedTime)
    {
        if (_nextScene != null)
        {
            if (CurrentScene.Active)
            {
                CurrentScene.End();
            }

            Log.Warning("SceneManager not currently disposing prior scene");
            //CurrentScene.Dispose(); //add dispose:bool somewhere to clarify if this should happen? What if scene will be reused?

            _currentScene = _nextScene;

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
