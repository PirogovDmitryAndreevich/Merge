using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPoolService : IItemsPoolService
{
    private Dictionary<Type, Queue<Component>> _pools;
    private Dictionary<Type, Transform> _poolParents;
    private Dictionary<Type, Func<Component>> _factories;

    private List<Item> _itemsOnScene;
    private Transform _itemsParent;

    public List<Item> ItemsOnScene => _itemsOnScene;

    public ItemsPoolService()
    {
        _itemsOnScene = new List<Item>();
        _pools = new();
        _factories = new();
        _poolParents = new();
    }

    public void SetParent(Transform parent) =>
        _itemsParent = parent;

    public void RegisterFactory<TPoolObject>(Func<TPoolObject> factory) where TPoolObject : Component
    {
        var type = typeof(TPoolObject);

        if (_factories.ContainsKey(type))
            return;

        _factories[type] = () => factory();

        CreatePoolParent(type);
    }

    public TPoolObject GetItem<TPoolObject>() where TPoolObject : Component
    {
        Type type = typeof(TPoolObject);

        if (_pools.TryGetValue(type, out var pool) && pool.Count > 0)
        {
            TPoolObject obj = (TPoolObject)pool.Dequeue();
            obj.gameObject.SetActive(true);

            return obj;
        }

        return CreateNew<TPoolObject>(); 
    }

    public void ReturnItem<TPoolObject>(TPoolObject obj) where TPoolObject : Component
    {
        Type type = typeof(TPoolObject);

        if (type == typeof(Item))
        {
            Item item = obj as Item;
            _itemsOnScene.Remove(item);
        }

        if (!_pools.ContainsKey(type))
            _pools[type] = new Queue<Component>();

        obj.gameObject.SetActive(false);

        _pools[type].Enqueue(obj);        
    }

    public void Cleanup()
    {
        _itemsOnScene.Clear();
        _pools.Clear();
        _factories.Clear();
        _poolParents.Clear();
    }

    public void CleanupByID(int id)
    {
        for (int i = _itemsOnScene.Count - 1; i >= 0; i--)
        {
            Item item = _itemsOnScene[i];

            if (item.ID < id)
            {
                ReturnItem(item);
            }
        }
    }

    private TPoolObject CreateNew<TPoolObject>() where TPoolObject : Component
    {
        Type type = typeof(TPoolObject);

        if (!_factories.TryGetValue(type, out var factory))
            throw new Exception($"No factory for {type}");

        TPoolObject obj = (TPoolObject)factory();

        obj.transform.SetParent(_poolParents[type]);

        return obj;
    }

    private void CreatePoolParent(Type type)
    {
        if (_poolParents.ContainsKey(type))
            return;

        GameObject parent = new GameObject($"{type.Name}Pool");
        parent.transform.SetParent(_itemsParent, false);

        _poolParents[type] = parent.transform;
    }
}