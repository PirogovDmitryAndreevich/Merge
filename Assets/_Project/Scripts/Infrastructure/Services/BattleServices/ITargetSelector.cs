using System.Collections.Generic;

public interface ITargetSelector : IService
{
    AttackItem GetTarget(List<AttackItem> items, int preferredIndex);
    bool HasAliveItems(List<AttackItem> items);
}