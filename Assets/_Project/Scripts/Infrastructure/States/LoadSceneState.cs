using System;
using System.Collections.Generic;

public class LoadSceneState : IPayloadState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IItemSpawnService _itemSpawnService;
    private readonly IStaticData _staticData;
    private readonly IProgressService _progressService;
    private readonly IPoolService _poolService;

    public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
        IGameFactory gameFactory, IItemSpawnService itemSpawnService,
        IStaticData staticData, IProgressService progressService, IPoolService poolService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _itemSpawnService = itemSpawnService;
        _staticData = staticData;
        _progressService = progressService;
        _poolService = poolService;
    }

    public void Enter(string sceneName) => 
        _sceneLoader.Load(sceneName, onLoaded: OnLoaded);

    public void Exit()
    {

    }

    private void OnLoaded()
    {
        InitGameObject();
        _gameStateMachine.Enter<GameLoopState>();
    }

    private void InitGameObject()
    {
        InitMainCanvas();
        RegisterPoolFunc();
        LoadItemsOnScene();
    }

    private void InitMainCanvas()
    {
        MainCanvas mainCanvas = _gameFactory.CreateMainCanvas();
        _itemSpawnService.SetSpawnFrame(mainCanvas.SpawnFrame);
        mainCanvas.SetItemSpawn(_itemSpawnService);
    }

    private void LoadItemsOnScene()
    {
        List<ItemSaveData> items = _progressService.Progress.Items.Items;

        foreach (ItemSaveData item in items)
        {
            Item loadItem = _poolService.Get<Item>();
            ItemData itemData = _staticData.ForItem(item.ID);

            loadItem.Initialize(itemData, item);
            loadItem.transform.position = item.Position.AsUnityVector();
        }
    }

    private void RegisterPoolFunc()
    {
        _poolService.RegisterFactory(() => _gameFactory.CreateItem());
        _poolService.RegisterFactory(() => _gameFactory.CreateProfit());
    }
}
