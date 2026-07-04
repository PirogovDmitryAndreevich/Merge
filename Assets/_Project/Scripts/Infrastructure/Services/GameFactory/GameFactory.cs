using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IAssetsProvider _assets;
    private readonly IMergeService _mergeService;
    private readonly IProgressService _progress;
    private readonly IPoolService _poolService;
    private readonly IItemsPoolService _itemsPool;
    private readonly IStaticData _staticData;
    private readonly IBattleService _battleService;
    private MainCanvas _mainCanvas;
    private Canvas _canvas;

    public GameFactory(IAssetsProvider assets, IMergeService mergeService,
        IProgressService progress, IPoolService poolService, IItemsPoolService itemsPool,
        IStaticData staticData, IBattleService battleService)
    {
        _assets = assets;
        _mergeService = mergeService;
        _progress = progress;
        _poolService = poolService;
        _itemsPool = itemsPool;
        _staticData = staticData;
        _battleService = battleService;
    }

    public MainCanvas CreateMainCanvas()
    {
        _mainCanvas = _assets.Instantiate(AssetsPath.MainCanvasPath)
            .GetComponent<MainCanvas>();

        _poolService.SetRootParent(_mainCanvas.transform);
        RegisterPoolFunc();

        _itemsPool.SetItemsParent(_mainCanvas.SpawnFrame, () => CreateItem());

        CreateCollection(_mainCanvas.Collection.Content);

        _canvas = _mainCanvas.GetComponent<Canvas>();
        
        _mainCanvas.CoinsCounter.Construct(_progress);  
        _mainCanvas.NewItemWindow.Construct(_mergeService);
        _mainCanvas.StartBattleWindow.Construct(_mainCanvas.SelectWindow);
        _mainCanvas.SelectWindow.Construct(_itemsPool, _battleService,
            _mainCanvas.AttackWindow);
        _mainCanvas.AttackWindow.Construct(_battleService, _poolService);
        _mainCanvas.Leaderboard.Construct(_progress);

        return _mainCanvas;
    }

    private Item CreateItem()
    {
        Item item = _assets.Instantiate(AssetsPath.ItemPath, _mainCanvas.ParentForItems)
            .GetComponent<Item>();

        item.Construct(_canvas, _mainCanvas.SpawnFrame, _mergeService, _progress);
        item.Spawner.Construct(_poolService);

        return item;
    }

    private void CreateCollection(Transform parent)
    {
        var items = _staticData.AllItems();

        foreach (ItemData item in items.Values)
            CreateItemIcon(item, parent);
    }

    private ItemIcon CreateItemIcon(ItemData item, Transform parent)
    {
        ItemIcon icon = _assets.Instantiate(AssetsPath.ItemIconPath, parent)
            .GetComponent<ItemIcon>();

        icon.Construct(item.Sprite, _progress, item.ID);

        return icon; 
    }

    private ProfitEffect CreateProfit()
    {
        ProfitEffect profit = _assets.Instantiate(AssetsPath.ProfitPath)
            .GetComponent<ProfitEffect>();

        profit.Construct(_poolService);

        return profit;
    }

    private ClickEffect CreateClickEffect()
    {
        ClickEffect particles = _assets.Instantiate(AssetsPath.ClickEffectPath)
            .GetComponent<ClickEffect>();

        particles.Construct(_poolService);

        return particles;
    }

    private MergeEffect CreateMergeEffect()
    {
        MergeEffect particle = _assets.Instantiate(AssetsPath.MergeEffectPath)
            .GetComponent<MergeEffect>();

        particle.Construct(_poolService);

        return particle;
    }

    private void RegisterPoolFunc()
    {
        _poolService.RegisterFactory(() => CreateProfit());
        _poolService.RegisterFactory(() => CreateClickEffect());
        _poolService.RegisterFactory(() => CreateMergeEffect());
    }
}
