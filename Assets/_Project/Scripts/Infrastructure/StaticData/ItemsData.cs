using UnityEngine;

[CreateAssetMenu(menuName = "StaticData/Items", fileName = "ItemsStaticData")]
public class ItemsData : ScriptableObject
{
    public ItemData[] Items;
}
