using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPoolService : IItemsPoolService
{
    private List<Item> _itemsOnScene;
    private Queue<Item> _pool;

    private Transform _itemsParent;
    private Func<Item> _itemFunc;

    public List<Item> ItemsOnScene => _itemsOnScene;

    public ItemsPoolService()
    {
        _itemsOnScene = new List<Item>();
        _pool = new Queue<Item>();
    }

    public void SetItemsParent(Transform parent, Func<Item> itemFunc)
    {
        _itemFunc = itemFunc;
        _itemsParent = parent;
    }

    public Item GetItem()
    {
        Item item;

        if (_pool.Count > 0)        
            item = _pool.Dequeue();
        else
            item = CreateNew();        

        item.gameObject.SetActive(true);
        _itemsOnScene.Add(item);

        return item;
    }

    public void ReturnItem(Item item)
    {
        _itemsOnScene.Remove(item);
        item.gameObject.SetActive(false);

        _pool.Enqueue(item);
    }

    public void Cleanup() =>
        _itemsOnScene.Clear();

    public void CleanupByID(int id)
    {
        _itemsOnScene.RemoveAll(item =>
        {
            if (item.ID < id)
            {
                item.gameObject.SetActive(false);
                _pool.Enqueue(item);
                return true;
            }
            return false;
        });
    }

    private Item CreateNew()
    {
        Item item = _itemFunc();
        item.transform.SetParent(_itemsParent);

        return item;
    }
}