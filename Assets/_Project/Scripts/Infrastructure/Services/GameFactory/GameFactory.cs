using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IAssetsProvider _assets;
    private readonly IMergeService _mergeService;
    private readonly IProgressService _progress;
    private readonly IPoolService _poolService;
    private MainCanvas _mainCanvas;
    private Canvas _canvas;

    public GameFactory(IAssetsProvider assets, IMergeService mergeService,
        IProgressService progress, IPoolService poolService)
    {
        _assets = assets;
        _mergeService = mergeService;
        _progress = progress;
        _poolService = poolService;
    }

    public MainCanvas CreateMainCanvas()
    {
        _mainCanvas = _assets.Instantiate(AssetsPath.MainCanvasPath)
            .GetComponent<MainCanvas>();

        _poolService.SetRootParent(_mainCanvas.transform);

        _canvas = _mainCanvas.GetComponent<Canvas>();

        _mainCanvas.CoinsCounter.Construct(_progress);  
        _mainCanvas.NewItemWindow.Construct(_mergeService);

        return _mainCanvas;
    }

    public Item CreateItem()
    {
        Item item = _assets.Instantiate(AssetsPath.ItemPath, _mainCanvas.ParentForItems)
            .GetComponent<Item>();

        item.Construct(_canvas, _mergeService, _progress);
        item.Spawner.Construct(_poolService);

        return item;
    }

    public ProfitEffect CreateProfit()
    {
        ProfitEffect profit = _assets.Instantiate(AssetsPath.ProfitPath)
            .GetComponent<ProfitEffect>();

        profit.Construct(_poolService);

        return profit;
    }

    public ClickEffect CreateClickEffect()
    {
        ClickEffect particles = _assets.Instantiate(AssetsPath.ClickEffectPath)
            .GetComponent<ClickEffect>();

        particles.Construct(_poolService);

        return particles;
    }

    public MergeEffect CreateMergeEffect()
    {
        MergeEffect particle = _assets.Instantiate(AssetsPath.MergeEffectPath)
            .GetComponent<MergeEffect>();

        particle.Construct(_poolService);

        return particle;
    }
}
