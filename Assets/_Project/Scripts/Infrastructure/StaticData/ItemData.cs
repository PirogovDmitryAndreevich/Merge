using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public int ID;
    public Sprite Sprite;
    public int Profit;
    public int Price;

    public ItemData(int id, Sprite sprite, int profit, int price)
    {
        ID = id;
        Sprite = sprite;
        Profit = profit;
        Price = price;
    }
}

[Serializable]
public class EconomySettings
{
    [Header("Income")]
    public int StartProfit = 1;
    public float ProfitGrowth = 1.18f;

    [Header("Time")]
    public float StartUnlockTime = 6f;
    public float TimeGrowth = 1.07f;

    [Header("Expected Player")]
    public float AverageComboMultiplier = 2.8f;

    [Header("Curves")]
    public AnimationCurve ProfitCurve =
        AnimationCurve.Linear(0, 1, 1, 1);

    public AnimationCurve TimeCurve =
        AnimationCurve.Linear(0, 1, 1, 1);
}