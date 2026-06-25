using System;

[Serializable]
public class PlayerSaveData
{
    public int Coins;

    public event Action CoinsChanged;

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
}