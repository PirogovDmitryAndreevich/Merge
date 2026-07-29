using System;

public interface IMergeService : IService
{
    event Action<ItemData> NewMaxItemReached;
    bool TryMerge(Item item);
}