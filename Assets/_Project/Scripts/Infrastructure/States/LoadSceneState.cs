using UnityEngine;

public class LoadSceneState : IPayloadState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;

    public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
    }

    public void Enter(string sceneName) => 
        _sceneLoader.Load(sceneName, onLoaded: OnLoaded);

    private void OnLoaded() => 
        InitGameObject();

    private void InitGameObject()
    {
        MainCanvas mainCanvas = _gameFactory.InstantiateMainCanvas();
    }

    public void Exit()
    {
        
    }
}