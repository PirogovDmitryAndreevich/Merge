using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public int ID;
    public Sprite Sprite;
    public int Profit;
    public int Price;
    public int Damage;
    public int Health;

    public ItemData(int id, Sprite sprite, int profit, int price, int health, int damage)
    {
        ID = id;
        Sprite = sprite;
        Profit = profit;
        Price = price;
        Health = health;
        Damage = damage;
    }
}