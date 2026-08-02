using System.Collections.Generic;
using UnityEngine;

public class EnemyItemsSelector : IEnemyItemsSelector
{
    private const int MaxItemsCount = 5;

    private readonly IProgressService _progressService;
    private readonly IStaticData _staticData;

    private readonly List<ItemData> _enemyItems = new();

    public EnemyItemsSelector(IProgressService progressService, IStaticData staticData)
    {
        _progressService = progressService;
        _staticData = staticData;
    }

    private enum EnemyPower
    {
        Weak,
        Normal,
        Strong,
        Elite
    }

    public List<ItemData> SelectEnemyItems()
    {
        _enemyItems.Clear();

        int maxId = _progressService.Progress.Items.MaxItemID;
        int maxItemId = _staticData.AllItems().Count - 1;

        EnemyPower power = RollEnemyPower();

        for (int slot = 0; slot < MaxItemsCount; slot++)
        {
            int offset = GetOffset(power, slot);

            int id = Mathf.Clamp(maxId + offset, 1, maxItemId);

            ItemData item = _staticData.ForItem(id);

            InsertSorted(item);
        }

        return _enemyItems;
    }

    private EnemyPower RollEnemyPower()
    {
        int value = Random.Range(0, 100);

        if (value < 30)
            return EnemyPower.Weak;

        if (value < 75)
            return EnemyPower.Normal;

        if (value < 95)
            return EnemyPower.Strong;

        return EnemyPower.Elite;
    }

    private int GetOffset(EnemyPower power, int slot)
    {
        switch (power)
        {
            case EnemyPower.Weak:
                return slot switch
                {
                    0 => Roll((-2, 60), (-1, 40)),
                    1 => Roll((-3, 60), (-2, 40)),
                    2 => Roll((-3, 70), (-2, 30)),
                    3 => Roll((-4, 60), (-3, 40)),
                    _ => Roll((-5, 50), (-4, 50))
                };

            case EnemyPower.Normal:
                return slot switch
                {
                    0 => Roll((0, 60), (-1, 40)),
                    1 => Roll((-1, 60), (-2, 40)),
                    2 => Roll((-2, 70), (-1, 30)),
                    3 => Roll((-3, 60), (-2, 40)),
                    _ => Roll((-4, 50), (-3, 50))
                };

            case EnemyPower.Strong:
                return slot switch
                {
                    0 => Roll((1, 20), (0, 60), (-1, 20)),
                    1 => Roll((0, 60), (-1, 40)),
                    2 => Roll((-1, 60), (-2, 40)),
                    3 => Roll((-2, 70), (-1, 30)),
                    _ => Roll((-3, 70), (-2, 30))
                };

            case EnemyPower.Elite:
                return slot switch
                {
                    0 => Roll((1, 50), (0, 50)),
                    1 => Roll((0, 80), (-1, 20)),
                    2 => Roll((0, 50), (-1, 50)),
                    3 => Roll((-1, 70), (-2, 30)),
                    _ => Roll((-1, 60), (-2, 40))
                };

            default:
                return 0;
        }
    }

    private int Roll(params (int offset, int weight)[] values)
    {
        int total = 0;

        foreach (var value in values)
            total += value.weight;

        int random = Random.Range(0, total);

        foreach (var value in values)
        {
            if (random < value.weight)
                return value.offset;

            random -= value.weight;
        }

        return values[0].offset;
    }

    private void InsertSorted(ItemData item)
    {
        int index = 0;

        while (index < _enemyItems.Count && _enemyItems[index].ID > item.ID)
            index++;

        _enemyItems.Insert(index, item);
    }
}