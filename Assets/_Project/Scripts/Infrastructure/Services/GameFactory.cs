public class GameFactory : IGameFactory
{
    private readonly IAssetsProvider _assets;

    public GameFactory(IAssetsProvider assets)
    {
        _assets = assets;
    }

    public MainCanvas InstantiateMainCanvas()
    {
        MainCanvas mainCanvas = _assets.Instantiate(AssetsPath.MainCanvasPath)
            .GetComponent<MainCanvas>();

        mainCanvas.Construct();

        return mainCanvas;
    }
}
