using System;
using YG.Utils.LB;

public class LBRewardHandler : ILBRewardHandler
{
    private readonly IProgressService _progress;

    public bool HaveAvailableRewards =>
        _progress.Progress.LBRewardData.AvailableReward > 0;

    public int AvailableRewards =>
        _progress.Progress.LBRewardData.AvailableReward;

    public event Action RewardCountChanged;
    public event Action RewardAdded;

    public LBRewardHandler(IProgressService progress)
    {
        _progress = progress;
    }

    public void OnGetLeaderboard(LBData data)
    {
        if (_progress.Progress.LBRewardData.IsFirst)
        {
            _progress.Progress.LBRewardData.CurrentRank = data.currentPlayer.rank;
            _progress.Progress.LBRewardData.IsFirst = false;
            return;
        }

        int oldRank = _progress.Progress.LBRewardData.CurrentRank;
        int newRank = data.currentPlayer.rank;

        if (newRank < oldRank)        
            AddReward();        

        _progress.Progress.LBRewardData.CurrentRank = data.currentPlayer.rank;
    }

    public void AddReward()
    {
        _progress.Progress.LBRewardData.AvailableReward++;
        RewardCountChanged?.Invoke();
        RewardAdded?.Invoke();
    }

    public void SpendReward()
    {
        _progress.Progress.LBRewardData.AvailableReward--;
        RewardCountChanged?.Invoke();
    }
}