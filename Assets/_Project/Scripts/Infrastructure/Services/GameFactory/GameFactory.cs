using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IAssetsProvider _assets;
    private readonly IMergeService _mergeService;
    private readonly IProgressService _progress;
    private readonly IPoolService _poolService;
    private readonly IItemsPoolService _itemsPool;
    private readonly IStaticData _staticData;
    private readonly IStartBattleService _battleService;
    private readonly IAttackAnimator _attackAnimator;
    private readonly IBattleRunner _battleRunner;
    private readonly ICoinsCounterService _coinsCounterService;
    private MainCanvas _mainCanvas;
    private Canvas _canvas;

    public GameFactory(IAssetsProvider assets, IMergeService mergeService,
        IProgressService progress, IPoolService poolService, IItemsPoolService itemsPool,
        IStaticData staticData, IStartBattleService battleService, IAttackAnimator attackAnimator,
        IBattleRunner battleRunner, ICoinsCounterService coinsCounterService)
    {
        _assets = assets;
        _mergeService = mergeService;
        _progress = progress;
        _poolService = poolService;
        _itemsPool = itemsPool;
        _staticData = staticData;
        _battleService = battleService;
        _attackAnimator = attackAnimator;
        _battleRunner = battleRunner;
        _coinsCounterService = coinsCounterService;
    }

    public MainCanvas CreateMainCanvas()
    {
        _mainCanvas = _assets.Instantiate(AssetsPath.MainCanvasPath)
            .GetComponent<MainCanvas>();

        _poolService.SetRootParent(_mainCanvas.transform);

        RegisterPoolFunc();
        RegisterItemPool();

        _mainCanvas.Collection.Construct();
        CreateCollection(_mainCanvas.Collection.Content);

        _canvas = _mainCanvas.GetComponent<Canvas>();

        _mainCanvas.CoinsCounter.Construct(_progress);
        _mainCanvas.NewItemWindow.Construct(_mergeService);
        _mainCanvas.StartBattleWindow.Construct(_mainCanvas.SelectWindow);
        _mainCanvas.SelectWindow.Construct(_itemsPool, _battleService,
            _mainCanvas.AttackWindow);
        _mainCanvas.AttackWindow.Construct(_battleService, _poolService);
        _mainCanvas.AttackWindow.WinWindow.Construct(_progress, _battleRunner,
            _mainCanvas.AttackWindow, _coinsCounterService);
        _mainCanvas.AttackWindow.FailWindow.Construct(_progress, _battleRunner,
            _mainCanvas.AttackWindow, _coinsCounterService);
        _mainCanvas.Leaderboard.Construct(_progress);
        _mainCanvas.BonusView.Construct(_coinsCounterService);
        _mainCanvas.SpawnButton.Construct(_progress);

        _attackAnimator.SetBullets(_mainCanvas.AttackWindow.Arrows);

        return _mainCanvas;
    }

    private Item CreateItem()
    {
        Item item = _assets.Instantiate(AssetsPath.ItemPath, _mainCanvas.ParentForItems)
            .GetComponent<Item>();

        item.Construct(_canvas, _mainCanvas.SpawnFrame, _mergeService,
            _coinsCounterService, _mainCanvas.ImpactShake);

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

    private DamageEffect CreateDamageEffect()
    {
        DamageEffect damage = _assets.Instantiate(AssetsPath.DamageEffectPath)
            .GetComponent<DamageEffect>();

        damage.Construct(_poolService);

        return damage;
    }

    private DeathEffect CreateDeathEffect()
    {
        DeathEffect death = _assets.Instantiate(AssetsPath.DeathEffectPath)
            .GetComponent<DeathEffect>();

        death.Construct(_poolService);

        return death;
    }

    private BonusItem CreateBonusItem()
    {
        BonusItem bonusItem = _assets.Instantiate(AssetsPath.BonusItemPath, _mainCanvas.ParentForItems)
            .GetComponent<BonusItem>();

        bonusItem.Construct(_coinsCounterService, _mainCanvas.ImpactShake);
        bonusItem.Spawner.Construct(_poolService);

        return bonusItem;
    }

    private void RegisterItemPool()
    {
        _itemsPool.SetParent(_mainCanvas.ParentForItems);

        _itemsPool.RegisterFactory(() => CreateItem());
        _itemsPool.RegisterFactory(() => CreateBonusItem());
    }

    private void RegisterPoolFunc()
    {        
        _poolService.RegisterFactory(() => CreateDeathEffect());
        _poolService.RegisterFactory(() => CreateDamageEffect());
        _poolService.RegisterFactory(() => CreateProfit());
        _poolService.RegisterFactory(() => CreateClickEffect());
        _poolService.RegisterFactory(() => CreateMergeEffect());
    }
}
