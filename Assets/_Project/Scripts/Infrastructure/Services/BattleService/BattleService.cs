using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleService : IBattleService
{
    private const int MaxItemsCount = 5;

    private readonly IProgressService _progressService;
    private readonly IStaticData _staticData;

    public List<ItemData> SelectedItems { get; private set; }
    public List<ItemData> EnemyItems { get; private set; }

    public event Action ItemSelected;

    public BattleService(IProgressService progressService, IStaticData staticData)
    {
        SelectedItems = new List<ItemData>();
        EnemyItems = new List<ItemData>();

        _progressService = progressService;
        _staticData = staticData;
    }

    public void Cleanup()
    {
        SelectedItems.Clear();
        EnemyItems.Clear();
    }

    public bool TrySelectItem(ItemData item)
    {
        if (SelectedItems.Count >= MaxItemsCount)
            return false;

        int index = 0;
        while (index < SelectedItems.Count && SelectedItems[index].ID > item.ID)
        {
            index++;
        }

        SelectedItems.Insert(index, item);
        ItemSelected?.Invoke();
        return true;
    }

    public void DeselectItem(ItemData item)
    {
        SelectedItems.Remove(item);
    }

    public void SelectEnemyItems()
    {
        int itemsCount = _staticData.AllItems().Count - 1;
        int maxID = _progressService.Progress.Items.MaxItemID;
        System.Random random = new System.Random();

        for (int i = 0; i < MaxItemsCount; i++)
        {
            int id;

            if (i == 0) // Первый предмет
            {
                if (random.NextDouble() < 0.25) // 25%
                    id = maxID + 1;
                else
                    id = maxID;
            }
            else if (i == 1) // Второй предмет
            {
                if (random.NextDouble() < 0.40) // 40%
                    id = maxID - 1;
                else
                    id = maxID - 2;
            }
            else if (i == 2) // Третий предмет
            {
                if (random.NextDouble() < 0.20) // 20%
                    id = maxID - 1;
                else
                    id = maxID - 2;
            }
            else // Остальные предметы
            {
                double chance = random.NextDouble();
                if (chance < 0.30) // 30%
                    id = maxID - 4;
                else if (chance < 0.60) // 30%
                    id = maxID - 3;
                else // 40% (оставшиеся)
                    id = maxID - 2;
            }

            id = Mathf.Clamp(id, 1, itemsCount);

            ItemData item = _staticData.ForItem(id);

            // Вставка с сортировкой по убыванию ID
            int index = 0;
            while (index < EnemyItems.Count && EnemyItems[index].ID > item.ID)
            {
                index++;
            }

            EnemyItems.Insert(index, item);
        }
    }
}