using System;
using UnityEngine;

public class MergeService : IMergeService
{
    private readonly Collider2D[] _overlapResults = new Collider2D[8];
    private readonly IStaticData _staticData;
    private readonly IProgressService _progressService;
    private readonly IPoolService _pool;

    private int _maxItem;

    public event Action<ItemData> NewMaxItemReached;

    public MergeService(IStaticData staticData, IProgressService progressService, IPoolService pool)
    {
        _staticData = staticData;
        _progressService = progressService;
        _pool = pool;
    }

    public void TryMerge(Item item)
    {
        int count = item.Collider.OverlapCollider(item.ContactFilter, _overlapResults);

        for (int i = 0; i < count; i++)
        {
            if (_overlapResults[i].TryGetComponent(out Item other) == false)
                continue;

            if (other == item || other.ID != item.ID)
                continue;

            Merge(item, other);
            return;
        }

        return;
    }

    private void Merge(Item item, Item other)
    {  
        ItemData nextItemData = _staticData.ForItem(item.ID + 1);

        if(nextItemData == null)
            return;

        _progressService.Progress.Items.Items.Remove(item.SaveData);
        MergeEffect particle = _pool.Get<MergeEffect>();

        particle.Play(other.transform.position);
        _pool.Return(item);


        if (nextItemData.ID > _maxItem)
        {
            _maxItem = nextItemData.ID;
            _progressService.Progress.Items.MaxItemID = _maxItem;
            NewMaxItemReached?.Invoke(nextItemData);
        }

        other.Initialize(nextItemData, other.SaveData);
        other.SavePosition();
    }
}