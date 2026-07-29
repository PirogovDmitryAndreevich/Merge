using System;
using UnityEngine;

public class MergeService : IMergeService
{
    private readonly Collider2D[] _overlapResults = new Collider2D[10];
    private readonly IStaticData _staticData;
    private readonly IProgressService _progressService;
    private readonly IPoolService _pool;
    private readonly ISpawnButtonService _spawnButtonService;
    private readonly IItemsPoolService _itemsPool;

    private int _maxItem => _progressService.Progress.Items.MaxItemID;

    public event Action<ItemData> NewMaxItemReached;

    public MergeService(IStaticData staticData, IProgressService progressService, IPoolService pool,
        ISpawnButtonService spawnButtonService, IItemsPoolService itemsPool)
    {
        _staticData = staticData;
        _progressService = progressService;
        _pool = pool;
        _spawnButtonService = spawnButtonService;
        _itemsPool = itemsPool;
    }

    public bool TryMerge(Item item)
    {
        int count = item.Collider.OverlapCollider(item.ContactFilter, _overlapResults);

        for (int i = 0; i < count; i++)
        {
            var other = _overlapResults[i].GetComponent<Item>();
            if (other == null) continue;

            if (other == item || other.ID != item.ID) continue;

            Merge(item, other);
            return true;
        }

        return false;
    }

    private void Merge(Item item, Item other)
    {  
        ItemData nextItemData = _staticData.ForItem(item.ID + 1);

        if(nextItemData == null)
            return;

        _progressService.Progress.Items.Items.Remove(item.SaveData);
        MergeEffect particle = _pool.Get<MergeEffect>();

        particle.Play(other.transform.position);
        _itemsPool.ReturnItem(item);

        if (nextItemData.ID > _maxItem)        
            OpenNewItem(nextItemData);        

        other.Initialize(nextItemData, other.SaveData);
        other.SavePosition();
    }

    private void OpenNewItem(ItemData nextItemData)
    {
        _progressService.Progress.Items.OpenNewItem(nextItemData.ID);

        int value = _maxItem - _staticData.GameData.SpawnIDCoefficient;
        int spawnItemID = Mathf.Max(1, value);

        _progressService.Progress.Items.SpawnItemID = spawnItemID;

        _spawnButtonService.UpdateItemData();
        _itemsPool.CleanupByID(spawnItemID);
        NewMaxItemReached?.Invoke(nextItemData);      
    }
}