using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private int _coins;

    private ICoinsCounterService _coinsService;
    private IProgressService _progress;

    public void Construct(ICoinsCounterService coinsCounterService, IProgressService progressService)
    {
        _coinsService = coinsCounterService;
        _progress = progressService;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))        
            _coinsService.AddCoins(_coins, Vector2.zero);

        if (Input.GetKeyDown(KeyCode.S))
            _progress.Progress.Player.SpendCoins(_coins);
    }
}
