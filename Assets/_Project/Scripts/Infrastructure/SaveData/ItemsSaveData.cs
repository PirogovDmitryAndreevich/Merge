using System;
using System.Collections.Generic;

[Serializable]
public class ItemsSaveData
{
    public List<ItemSaveData> Items;
    public int MaxItemID;
    public int SpawnItemID;

    public ItemsSaveData()
    {
        Items = new List<ItemSaveData>();
        MaxItemID = 1;
        SpawnItemID = 1;
    }

    public void UpdateMaxItem(int NewMaxItemID, int spawnItemId)
    {
        MaxItemID = NewMaxItemID;
        SpawnItemID = spawnItemId;
    }
}
