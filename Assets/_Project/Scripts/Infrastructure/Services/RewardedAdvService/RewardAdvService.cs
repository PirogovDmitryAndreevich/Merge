using System;
using YG;

public class RewardAdvService : IRewardAdvService
{
    private readonly IBonusHandler _bonusHandler;

    public RewardAdvService(IBonusHandler bonusHandler)
    {
        Subscribe();
        _bonusHandler = bonusHandler;
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
            }
        }
        else
        {
            return;
        }
    }
}

[Serializable]
public enum RewardType
{
    Multiplayer
}