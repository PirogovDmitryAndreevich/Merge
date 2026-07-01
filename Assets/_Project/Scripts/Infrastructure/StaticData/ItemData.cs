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