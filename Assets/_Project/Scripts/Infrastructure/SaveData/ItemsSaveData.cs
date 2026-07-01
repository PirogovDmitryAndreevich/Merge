using System;
using System.Collections.Generic;

[Serializable]
public class ItemsSaveData
{
    public List<ItemSaveData> Items;
    public int MaxItemID;

    public ItemsSaveData()
    {
        Items = new List<ItemSaveData>();
    }
}
