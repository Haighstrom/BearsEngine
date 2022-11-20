namespace BearsEngine;

internal interface ISceneManager
{
    IScene CurrentScene { get; }

    public void ChangeScene(IScene nextScene);

    public void UpdateScene(float elapsedTime);
}
