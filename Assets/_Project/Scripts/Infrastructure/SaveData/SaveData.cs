using System;

[Serializable]
public class SaveData
{
    public ItemsSaveData Items;
    public PlayerSaveData Player;
    public BonusItemsData BonusItemsData;

    public SaveData()
    {
        Items = new ItemsSaveData();
        Player = new PlayerSaveData();
        BonusItemsData = new BonusItemsData();
    }
}
