using System;

public interface IMergeService : IService
{
    event Action<ItemData> NewMaxItemReached;
    void TryMerge(Item item);
}