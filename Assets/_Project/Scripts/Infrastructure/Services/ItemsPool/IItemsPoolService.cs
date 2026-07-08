using System;
using System.Collections.Generic;
using UnityEngine;

public interface IItemsPoolService : IService
{
    List<Item> ItemsOnScene { get; }

    void SetParent(Transform parent);
    void RegisterFactory<TPoolObject>(Func<TPoolObject> factory) where TPoolObject : Component;
    void Cleanup();
    TPoolObject GetItem<TPoolObject>() where TPoolObject : Component;
    void ReturnItem<TPoolObject>(TPoolObject obj) where TPoolObject : Component;
    void CleanupByID(int id);
}