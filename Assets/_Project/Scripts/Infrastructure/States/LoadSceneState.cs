using System.Collections.Generic;

public class LoadSceneState : IPayloadState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IItemSpawnService _itemSpawnService;
    private readonly IStaticData _staticData;
    private readonly IProgressService _progressService;
    private readonly IItemsPoolService _itemsPoolService;
    private readonly ISpawnButtonService _spawnButtonService;

    public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
        IGameFactory gameFactory, IItemSpawnService itemSpawnService,
        IStaticData staticData, IProgressService progressService, IItemsPoolService itemsPoolService,
        ISpawnButtonService spawnButtonService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _itemSpawnService = itemSpawnService;
        _staticData = staticData;
        _progressService = progressService;
        _itemsPoolService = itemsPoolService;
        _spawnButtonService = spawnButtonService;
    }

    public void Enter(string sceneName) => 
        _sceneLoader.Load(sceneName, onLoaded: OnLoaded);

    public void Exit()
    {

    }

    private void OnLoaded()
    {
        InitGameObject();
        _spawnButtonService.UpdateItemData();
        _gameStateMachine.Enter<GameLoopState>();
    }

    private void InitGameObject()
    {
        InitMainCanvas();        
    }

    private void InitMainCanvas()
    {
        MainCanvas mainCanvas = _gameFactory.CreateMainCanvas();
        _itemSpawnService.SetSpawnFrameAndButton(mainCanvas.SpawnFrame, mainCanvas.SpawnButton);

        LoadItemsOnScene();
    }

    private void LoadItemsOnScene()
    {
        List<ItemSaveData> items = _progressService.Progress.Items.Items;

        foreach (ItemSaveData item in items)
        {
            Item loadItem = _itemsPoolService.GetItem<Item>();
            ItemData itemData = _staticData.ForItem(item.ID);

            loadItem.Initialize(itemData, item);
            loadItem.transform.position = item.Position.AsUnityVector();
        }
    }
}
