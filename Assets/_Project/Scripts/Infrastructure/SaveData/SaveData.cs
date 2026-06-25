using System;

[Serializable]
public class SaveData
{
    public ItemsSaveData Items;

    public SaveData()
    {
        Items = new ItemsSaveData();
    }
}
