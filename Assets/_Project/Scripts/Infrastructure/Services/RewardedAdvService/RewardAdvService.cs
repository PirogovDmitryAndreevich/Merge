using System;
using YG;

public class RewardAdvService : IRewardAdvService
{
    private readonly IProgressService _progressService;
    private readonly IBonusHandler _bonusHandler;
    private readonly ISpawnButtonService _spawnButtonService;

    public RewardAdvService(IProgressService progressService, IBonusHandler bonusHandler, ISpawnButtonService spawnButtonService)
    {
        Subscribe();
        _progressService = progressService;
        _bonusHandler = bonusHandler;
        _spawnButtonService = spawnButtonService;
    }

    public void Subscribe() =>
        YG2.onRewardAdv += GiveReward;

    public void Unsubscribe() =>
        YG2.onRewardAdv -= GiveReward;

    private void GiveReward(string rewardID)
    {
        if (Enum.TryParse(rewardID, out RewardType rewardType))
        {
            switch (rewardType)
            {
                case RewardType.Multiplayer:
                    _bonusHandler.AddBonus();
                    break;
                case RewardType.Item:
                    OnItemsBonus();
                    break;
            }
        }
        else
        {
            return;
        }
    }

    private void OnItemsBonus()
    {
        if (_progressService.Progress.Player.ViewedAdv >= 9)
        {
            for (int i = 0; i < 10; i++)
            {
                _spawnButtonService.BonusSpawn();
            }

            _progressService.Progress.Player.ResetAdvViewed();
        }
        else
        {
            _spawnButtonService.BonusSpawn();
            _progressService.Progress.Player.OnAdvViewed();
        }
    }
}

[Serializable]
public enum RewardType
{
    Multiplayer,
    Item,
}