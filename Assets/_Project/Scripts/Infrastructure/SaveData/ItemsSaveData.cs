using System;
using System.Collections.Generic;

[Serializable]
public class ItemsSaveData
{
    public List<ItemSaveData> Items;
    public List<int> OpenedItems;
    public int MaxItemID;
    public int SpawnItemID;

    public event Action<int> NewItemOpened;

    public ItemsSaveData()
    {
        Items = new List<ItemSaveData>();
        MaxItemID = 1;
        SpawnItemID = 1;
        OpenedItems = new List<int>() { MaxItemID };
    }

    public void OpenNewItem(int id)
    {
        OpenedItems.Add(id);
        MaxItemID = id;

        NewItemOpened?.Invoke(id);
    }
}
