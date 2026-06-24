using UnityEngine;

public class LoadSceneState : IPayloadState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IItemSpawnService _itemSpawnService;

    public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, IItemSpawnService itemSpawnService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _itemSpawnService = itemSpawnService;
    }

    public void Enter(string sceneName) => 
        _sceneLoader.Load(sceneName, onLoaded: OnLoaded);

    public void Exit()
    {

    }

    private void OnLoaded() => 
        InitGameObject();

    private void InitGameObject() => 
        InitMainCanvas();

    private void InitMainCanvas()
    {
        MainCanvas mainCanvas = _gameFactory.CreateMainCanvas();
        _itemSpawnService.SetSpawnFrame(mainCanvas.SpawnFrame, mainCanvas.StartItemSpawnPoint);
        mainCanvas.SetItemSpawn(_itemSpawnService);
    }
}