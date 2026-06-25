using System.Collections.Generic;

public class LoadSceneState : IPayloadState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IItemSpawnService _itemSpawnService;
    private readonly IStaticData _staticData;
    private readonly IProgressService _progressService;

    public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
        IGameFactory gameFactory, IItemSpawnService itemSpawnService,
        IStaticData staticData, IProgressService progressService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _itemSpawnService = itemSpawnService;
        _staticData = staticData;
        _progressService = progressService;
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
            Item loadItem = _gameFactory.CreateItem();
            ItemData itemData = _staticData.ForItem(item.ID);

            loadItem.Initialize(itemData, item);
            loadItem.transform.position = item.Position.AsUnityVector();
        }
    }
}
