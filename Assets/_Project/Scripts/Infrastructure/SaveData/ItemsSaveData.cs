using System;
using System.Collections.Generic;

[Serializable]
public class ItemsSaveData
{
    public List<ItemSaveData> Items;

    public ItemsSaveData()
    {
        Items = new List<ItemSaveData>();
    }
}
