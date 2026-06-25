using UnityEngine;

public class MergeService : IMergeService
{
    private readonly Collider2D[] _overlapResults = new Collider2D[8];
    private readonly IStaticData _staticData;
    private readonly IProgressService _progressService;

    public MergeService(IStaticData staticData, IProgressService progressService)
    {
        _staticData = staticData;
        _progressService = progressService;
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
    }

    private void Merge(Item item, Item other)
    {
        ItemID nextItemID = item.NextID;

        if (nextItemID == ItemID.Non)
        {
            Debug.Log("Last Item Reached");
            return;
        }

        _progressService.Progress.Items.Items.Remove(item.SaveData);
        Object.Destroy(item.gameObject); // return to pool 

        ItemData nextItemData = _staticData.ForItem(nextItemID);

        other.Initialize(nextItemData, other.SaveData);
        other.SavePosition();
    }
}