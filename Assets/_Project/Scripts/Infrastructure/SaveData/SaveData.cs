using System;

[Serializable]
public class SaveData
{
    public ItemsSaveData Items;
    public PlayerSaveData Player;

    public SaveData()
    {
        Items = new ItemsSaveData();
        Player = new PlayerSaveData();
    }
}
