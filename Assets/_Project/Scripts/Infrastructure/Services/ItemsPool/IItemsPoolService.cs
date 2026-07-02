using System;
using System.Collections.Generic;
using UnityEngine;

public interface IItemsPoolService : IService
{
    List<Item> ItemsOnScene { get; }

    void SetItemsParent(Transform parent, Func<Item> itemFunc);
    void Cleanup();
    Item GetItem();
    void ReturnItem(Item item);
    void CleanupByID(int id);
}