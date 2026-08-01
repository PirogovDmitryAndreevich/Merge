using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private int _coins;

    private ICoinsCounterService _coinsService;
    private IProgressService _progress;
    private IItemSpawnService _itemSpawn;
    private IReviewHandler _reviewHandler;
    private AwardWindow _awardWindow;

    public void Construct(ICoinsCounterService coinsCounterService, IProgressService progressService,
        IItemSpawnService itemSpawnService, IReviewHandler reviewHandler, AwardWindow awardWindow)
    {
        _coinsService = coinsCounterService;
        _progress = progressService;
        _itemSpawn = itemSpawnService;
        _reviewHandler = reviewHandler;
        _awardWindow = awardWindow;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))        
            _coinsService.AddCoins(_coins, Vector2.zero);

        if (Input.GetKeyDown(KeyCode.S))
            _progress.Progress.Player.SpendCoins(_coins);

        if (Input.GetKeyDown(KeyCode.Q))
            _itemSpawn.SpawnBonusItem();

        if(Input.GetKeyDown(KeyCode.O))
            _reviewHandler.UpdateData();

        if (Input.GetKeyDown(KeyCode.W))
            _awardWindow.Show();
    }
}
