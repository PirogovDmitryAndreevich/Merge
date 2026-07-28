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
    private readonly IAttackAnimator _attackAnimator;
    private readonly ICoinsCounterService _coinsCounterService;
    private readonly IBonusHandler _bonusHandler;
    private readonly IAwardHandler _awardHandler;
    private readonly ILBRewardHandler _lbRewardHandler;
    private readonly ICursorHandler _cursorHandler;
    private readonly ISpawnButtonService _spawnButtonService;
    private readonly IInterstitialAdvService _interstitialAdvService;
    private readonly IItemSpawnService _itemSpawnService;
    private readonly IReviewHandler _reviewHandler;
    private MainCanvas _mainCanvas;
    private Canvas _canvas;

    public GameFactory(IAssetsProvider assets, IMergeService mergeService,
        IProgressService progress, IPoolService poolService, IItemsPoolService itemsPool,
        IStaticData staticData, IBattleService battleService, IAttackAnimator attackAnimator,
        ICoinsCounterService coinsCounterService, IBonusHandler bonusHandler,
        IAwardHandler awardHandler, ILBRewardHandler lbRewardHandler,
        ICursorHandler cursorHandler, ISpawnButtonService spawnButtonService,
        IInterstitialAdvService interstitialAdvService, IItemSpawnService itemSpawnService, IReviewHandler reviewHandler)
    {
        _assets = assets;
        _mergeService = mergeService;
        _progress = progress;
        _poolService = poolService;
        _itemsPool = itemsPool;
        _staticData = staticData;
        _battleService = battleService;
        _attackAnimator = attackAnimator;
        _coinsCounterService = coinsCounterService;
        _bonusHandler = bonusHandler;
        _awardHandler = awardHandler;
        _lbRewardHandler = lbRewardHandler;
        _cursorHandler = cursorHandler;
        _spawnButtonService = spawnButtonService;
        _interstitialAdvService = interstitialAdvService;
        _itemSpawnService = itemSpawnService;
        _reviewHandler = reviewHandler;
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
        _mainCanvas.ScoreCounter.Construct(_progress);
        _mainCanvas.NewItemWindow.Construct(_mergeService);
        _mainCanvas.StartBattleWindow.Construct(_battleService);
        _mainCanvas.EnemyAttackWindow.Construct(_battleService);
        _mainCanvas.SelectWindow.Construct(_itemsPool, _battleService);
        _mainCanvas.PlayerAttackWindow.Construct(_battleService, _poolService);
        _mainCanvas.PlayerAttackWindow.WinWindow.Construct(_progress, _battleService, _coinsCounterService);
        _mainCanvas.PlayerAttackWindow.FailWindow.Construct(_progress, _battleService, _coinsCounterService);
        _mainCanvas.LBButton.Construct(_lbRewardHandler, _mainCanvas.Leaderboard);
        _mainCanvas.Leaderboard.Construct(_progress, _lbRewardHandler, _mainCanvas.AwardWindow);
        _mainCanvas.BonusView.Construct(_coinsCounterService);
        _mainCanvas.SpawnButton.Construct(_progress, _spawnButtonService);
        _mainCanvas.MultiplayerButton.Construct(_bonusHandler);
        _mainCanvas.AwardWindow.Construct(_bonusHandler);
        _mainCanvas.RewardButton.Construct(_progress, _awardHandler, _mainCanvas.AwardWindow);
        _mainCanvas.CursorPointer.Construct(_cursorHandler, _mainCanvas.SpawnButton.transform);
        _mainCanvas.AttackButton.Construct(_battleService);
        _mainCanvas.InterstitialTimer.Construct(_interstitialAdvService);
        _mainCanvas.ReviewButton.Construct(_reviewHandler);
        _mainCanvas.RewardSpawnButton.Construct(_spawnButtonService, _progress);

        _attackAnimator.SetBullets(_mainCanvas.PlayerAttackWindow.Arrows);

        CreateTest();

        return _mainCanvas;
    }

    public void CreateTest()
    {
        var test = _assets.Instantiate(AssetsPath.TestPath).GetComponent<Test>();

        test.Construct(_coinsCounterService, _progress, _itemSpawnService, _reviewHandler);
    }

    private Item CreateItem()
    {
        Item item = _assets.Instantiate(AssetsPath.ItemPath, _mainCanvas.ParentForItems)
            .GetComponent<Item>();

        item.Construct(_canvas, _mainCanvas.SpawnFrame, _mergeService,
            _coinsCounterService, _mainCanvas.ImpactShake, _cursorHandler);

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

    private ValueEffect CreateProfit()
    {
        ValueEffect profit = _assets.Instantiate(AssetsPath.ProfitPath)
            .GetComponent<ValueEffect>();

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

        RectTransform rect = _mainCanvas.MultiplayerButton.GetComponent<RectTransform>();

        bonusItem.Construct(_itemsPool, _mainCanvas.ImpactShake, _bonusHandler, rect);
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
