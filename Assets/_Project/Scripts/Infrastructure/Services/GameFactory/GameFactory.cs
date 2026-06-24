using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IAssetsProvider _assets;
    private readonly IMergeService _mergeService;
    private MainCanvas _mainCanvas;
    private Canvas _canvas;

    public GameFactory(IAssetsProvider assets, IMergeService mergeService)
    {
        _assets = assets;
        _mergeService = mergeService;
    }

    public MainCanvas CreateMainCanvas()
    {
        _mainCanvas = _assets.Instantiate(AssetsPath.MainCanvasPath)
            .GetComponent<MainCanvas>();

        _canvas = _mainCanvas.GetComponent<Canvas>();

        return _mainCanvas;
    }

    public Item CreateItem()
    {
        Item item = _assets.Instantiate(AssetsPath.ItemPath, _mainCanvas.ParentForItems)
            .GetComponent<Item>();

        item.Construct(_canvas, _mergeService);

        return item;
    }
}
