using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StaticData/Items", fileName = "ItemsStaticData")]
public class ItemsData : ScriptableObject
{
    public int ItemsCount;
    public List<ItemData> Items;

    public int StartProfit = 1;
    public float ProfitGrowth = 1.18f;

    public float StartUnlockTime = 6f;
    public float TimeGrowth = 1.07f;

    public float AverageComboMultiplier = 2.8f;
    public AnimationCurve DifficultyCurve;
}
