using System;

[Serializable]
public class PlayerSaveData
{
    public int Coins;
    public int Score;

    public PlayerSaveData()
    {
        Coins = 250;
        Score = 0;
    }

    public event Action CoinsChanged;
    public event Action ScoreChanged;

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
        ScoreChanged?.Invoke();
    }
}