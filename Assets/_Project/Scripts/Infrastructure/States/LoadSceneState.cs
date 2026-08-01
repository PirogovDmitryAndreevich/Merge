using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using YG;

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
    private readonly ICoroutineRunner _coroutineRunner;

    public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
        IGameFactory gameFactory, IItemSpawnService itemSpawnService,
        IStaticData staticData, IProgressService progressService, IItemsPoolService itemsPoolService,
        ISpawnButtonService spawnButtonService, ICoroutineRunner coroutineRunner)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _itemSpawnService = itemSpawnService;
        _staticData = staticData;
        _progressService = progressService;
        _itemsPoolService = itemsPoolService;
        _spawnButtonService = spawnButtonService;
        _coroutineRunner = coroutineRunner;
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
        SetLanguage();
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

    private void SetLanguage()
    {
        try
        {
            _coroutineRunner.StartCoroutine(LoadLocale(YG2.lang));
        }
        catch (System.Exception) { }
    }

    private IEnumerator LoadLocale(string languageIdentifier)
    {
        yield return LocalizationSettings.InitializationOperation;

        LocaleIdentifier localeCode = new LocaleIdentifier(languageIdentifier);

        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            Locale locale = LocalizationSettings.AvailableLocales.Locales[i];

            if (locale.Identifier == localeCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                yield break;
            }
        }
    }
}
