using System;

public interface ISpawnButtonService : IService
{
    ItemData CurrentItem { get; }
    ItemData BonusItem { get; }
    bool IsActive { get; }

    event Action ItemChanged;
    event Action<bool> ViewChanged;

    void CheckView();
    void Spawn();
    void BonusSpawn();
    void UpdateItemData();
}