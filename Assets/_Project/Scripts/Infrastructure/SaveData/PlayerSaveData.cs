using System;

[Serializable]
public class PlayerSaveData
{
    public int Coins;
    public int Score;

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