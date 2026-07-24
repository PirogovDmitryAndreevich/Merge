using UnityEngine;

public interface IItemSpawnService : IService
{
    void SetSpawnFrameAndButton(RectTransform frame, SpawnButton spawnButton);
    void Spawn(ItemData itemData);
    void SpawnBonusItem();
}