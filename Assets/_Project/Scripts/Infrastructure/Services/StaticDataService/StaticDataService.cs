using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticData
{
    private const string ItemsDataPath = "StaticData/ItemsStaticData";
    private const string GameDataPath = "StaticData/GameStaticData";
    private Dictionary<int, ItemData> _item;

    public GameData GameData { get; private set; }

    public void Load()
    {
        _item = Resources.Load<ItemsData>(ItemsDataPath)
            .Items.ToDictionary(x => x.ID, x => x);

        GameData = Resources.Load<GameData>(GameDataPath);
    }

    public ItemData ForItem(int id) =>
        _item.TryGetValue(id, out ItemData data)
        ? data
        : null;

    public Dictionary<int, ItemData> AllItems() =>
        _item;
}