using System;

[Serializable]
public class LBRewardData
{
    public int CurrentRank;
    public int AvailableReward;
    public bool IsFirst;

    public LBRewardData()
    {
        CurrentRank = 0;
        AvailableReward = 0;
        IsFirst = true;
    }
}