using System;

[Serializable]
public class PlayerSaveData
{
    public int Coins;
    public int Score;
    public int UnspentScores;
    public int TargetScoreToReward;
    public int AvailableAwards;
    public int RewardLevel;

    public PlayerSaveData()
    {
        Coins = 250;
        Score = 0;
        UnspentScores = 0;
        AvailableAwards = 0;
        RewardLevel = 1;
        TargetScoreToReward = 10;
    }

    public event Action CoinsChanged;
    public event Action ScoreChanged;
    public event Action TargetScoreChanged;

    public void AddCoins(int value)
    {
        Coins += value;
        CoinsChanged?.Invoke();
    }

    public void SpendCoins(int value)
    {
        Coins -= value;
        CoinsChanged?.Invoke();
    }

    public void AddScore(int value)
    {
        Score += value;
        UnspentScores += value;
        ScoreChanged?.Invoke();
    }

    public void SpendScore(int value)
    {
        UnspentScores -= value;
        ScoreChanged?.Invoke();
    }

    public void SetNextTargetScore()
    {
        RewardLevel++;
        TargetScoreToReward = TargetScoreToReward * RewardLevel;
        TargetScoreChanged?.Invoke();
    }

    public void AddAward()
    {
        AvailableAwards++;
        ScoreChanged?.Invoke();
    }

    public void SpendAward()
    {
        AvailableAwards--;
        ScoreChanged?.Invoke();
    }
}