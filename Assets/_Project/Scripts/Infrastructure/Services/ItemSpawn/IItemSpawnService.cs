using UnityEngine;

public interface IItemSpawnService : IService
{
    void SetSpawnFrame(RectTransform frame, Vector2 startSpawnPoint);
    void Spawn();
}