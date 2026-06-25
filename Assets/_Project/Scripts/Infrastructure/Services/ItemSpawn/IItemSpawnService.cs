using UnityEngine;

public interface IItemSpawnService : IService
{
    void SetSpawnFrame(RectTransform frame);
    void Spawn(Vector3 startPosition);
}