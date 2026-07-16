using UnityEngine;

public class AwardHandler : IAwardHandler
{
    private readonly IProgressService _progress;  

    public bool IsReached =>
        _progress.Progress.Player.UnspentScores >=
        _progress.Progress.Player.TargetScoreToReward;

    public bool HaveAvailable =>
        _progress.Progress.Player.AvailableAwards > 0;

    public AwardHandler(IProgressService progress)
    {
        _progress = progress;
    }

    public void CheckReached()
    {
         PlayerSaveData data = _progress.Progress.Player;

        if (IsReached)
        {
            int rewardLevel = data.RewardLevel;
            int targetScore = Mathf.CeilToInt(10 * Mathf.Pow(1.25f, rewardLevel - 1));

            data.SpendScore(data.TargetScoreToReward);
            data.SetNextTargetScore(targetScore);
            data.AddAward();
        }
    }

    public void GiveOutReward() =>
        _progress.Progress.Player.SpendAward();
}