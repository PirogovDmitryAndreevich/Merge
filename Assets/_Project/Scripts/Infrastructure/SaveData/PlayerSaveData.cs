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
    public int ViewedAdv;

    public PlayerSaveData()
    {
        Coins = 250;
        Score = 0;
        UnspentScores = 0;
        AvailableAwards = 0;
        RewardLevel = 1;
        TargetScoreToReward = 10;
        ViewedAdv = 0;
    }

    public event Action CoinsChanged;
    public event Action UnspentScoreChanged;
    public event Action ScoreChanged;
    public event Action TargetScoreChanged;
    public event Action ViewedAdvChanged;

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
        UnspentScoreChanged?.Invoke();
    }

    public void SpendScore(int value)
    {
        UnspentScores -= value;
        UnspentScoreChanged?.Invoke();
    }

    public void SetNextTargetScore(int score)
    {
        RewardLevel++;
        TargetScoreToReward = score;
        TargetScoreChanged?.Invoke();
    }

    public void AddAward()
    {
        AvailableAwards++;
        UnspentScoreChanged?.Invoke();
    }

    public void SpendAward()
    {
        AvailableAwards--;
        UnspentScoreChanged?.Invoke();
    }

    public void OnAdvViewed()
    {
        ViewedAdv++;
        ViewedAdvChanged?.Invoke();
    }

    public void ResetAdvViewed()
    {
        ViewedAdv = 0;
        ViewedAdvChanged?.Invoke();
    }
}