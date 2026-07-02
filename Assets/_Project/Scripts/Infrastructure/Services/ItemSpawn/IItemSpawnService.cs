using UnityEngine;

public interface IItemSpawnService : IService
{
    void SetSpawnFrameAndButton(RectTransform frame, SpawnButton spawnButton);
    void UpdateItemData();
    void Spawn();
}