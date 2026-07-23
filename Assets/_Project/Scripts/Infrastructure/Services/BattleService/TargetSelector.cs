using System.Collections.Generic;

public class TargetSelector : ITargetSelector
{
    public bool HasAliveItems(List<AttackItem> items)
    {
        foreach (AttackItem item in items)
        {
            if (!item.IsDestroy)
                return true;
        }
        return false;
    }

    public AttackItem GetTarget(List<AttackItem> items, int preferredIndex)
    {
        if (items == null || items.Count == 0)
            return null;

        if (preferredIndex < items.Count
            && !items[preferredIndex].IsDestroy)
        {
            return items[preferredIndex];
        }

        for (int i = preferredIndex + 1; i < items.Count; i++)
        {
            if (!items[i].IsDestroy)
                return items[i];
        }

        for (int i = 0; i < preferredIndex && i < items.Count; i++)
        {
            if (!items[i].IsDestroy)
                return items[i];
        }

        return null;
    }
}