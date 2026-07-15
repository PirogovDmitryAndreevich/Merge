using System;

public class MultiplierBonusHandler : IBonusHandler
{
    private readonly IProgressService _progressService;
    private readonly ICoinsCounterService _coinsCounterService;

    public event Action BonusCounterChanger;

    public bool IsBonusAvailable => _progressService.Progress.
            BonusItemsData.MultiplierCount > 0;

    public int MultiplierCount => _progressService.Progress.
            BonusItemsData.MultiplierCount;

    public MultiplierBonusHandler(IProgressService progressService, ICoinsCounterService coinsCounterService)
    {
        _progressService = progressService;
        _coinsCounterService = coinsCounterService;
    }

    public void AddBonus()
    {
        _progressService.Progress.BonusItemsData.MultiplierCount++;
        BonusCounterChanger?.Invoke();
    }

    public void OnClickedBonus()
    {
        if (IsBonusAvailable)
            PlayMultiplayer();
        else
            ShowRewardAdv();        
    }

    private void PlayMultiplayer()
    {
        _progressService.Progress.BonusItemsData.MultiplierCount--;
        _coinsCounterService.AddMultiplier();
        BonusCounterChanger?.Invoke();
    }

    private void ShowRewardAdv()
    {

    }
}