using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticData
{
    private const string ItemsDataPath = "StaticData/ItemsStaticData";
    private Dictionary<int, ItemData> _item;

    public void Load()
    {
        _item = Resources.Load<ItemsData>(ItemsDataPath)
            .Items.ToDictionary(x => x.ID, x => x);
    }

    public ItemData ForItem(int id) =>
        _item.TryGetValue(id, out ItemData data)
        ? data
        : null;
}