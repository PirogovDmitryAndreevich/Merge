using System;
using YG.Utils.LB;

public interface ILBRewardHandler : IService
{
    int AvailableRewards { get; }
    bool HaveAvailableRewards { get; }

    event Action RewardCountChanged;
    event Action RewardAdded;

    void AddReward();
    void OnGetLeaderboard(LBData data);
    void SpendReward();
}