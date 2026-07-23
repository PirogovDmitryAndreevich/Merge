using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StaticData/Items", fileName = "ItemsStaticData")]
public class ItemsData : ScriptableObject
{
    public int ItemsCount;
    public List<ItemData> Items;

    public int StartHealth = 100;
    public float MaxLuck = 10f;
    public float MinLuck = 3f;

    [Header("Profit")]
    public float ProfitGrowth = 1.18f;

    [Tooltip("Ручные значения прибыли первой пачки")]
    public List<int> FirstPackProfits = new()
{
    1,
    2,
    3,
    5,
    8,
    12,
    18
};

    [Header("Price")]
    [Tooltip("Во сколько раз примерно увеличивается размер пачки")]
    public float PackSizeGrowth = 1.35f;

    [Tooltip("Множитель цены первой пачки")]
    public float StartPriceMultiplier = 8f;

    [Tooltip("Во сколько раз растёт цена каждой следующей пачки")]
    public float PackPriceGrowth = 1.5f;

    [Tooltip("Форма роста цены внутри каждой пачки")]
    public AnimationCurve PriceMultiplierCurve =
        AnimationCurve.EaseInOut(
            0f,
            0f,
            1f,
            1f);


    [Header("Combo")]
    public float AverageComboMultiplier = 2.8f;
}
