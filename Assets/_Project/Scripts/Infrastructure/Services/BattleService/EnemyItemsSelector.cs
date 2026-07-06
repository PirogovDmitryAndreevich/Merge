using System.Collections.Generic;
using UnityEngine;

public class EnemyItemsSelector : IEnemyItemsSelector
{
    private const int MaxItemsCount = 5;

    private readonly IProgressService _progressService;
    private readonly IStaticData _staticData;

    private List<ItemData> _enemyItems;

    public EnemyItemsSelector(IProgressService progressService, IStaticData staticData)
    {
        _progressService = progressService;
        _staticData = staticData;

        _enemyItems = new List<ItemData>();
    }

    public List<ItemData> SelectEnemyItems()
    {
        _enemyItems.Clear();

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
            while (index < _enemyItems.Count && _enemyItems[index].ID > item.ID)
            {
                index++;
            }

            _enemyItems.Insert(index, item);
        }

        return _enemyItems;
    }
}