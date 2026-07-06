using System;
using System.Collections.Generic;

public class StartBattleService : IStartBattleService
{
    private const int MaxItemsCount = 5;

    private readonly IBattleRunner _battleRunner;
    private readonly IEnemyItemsSelector _enemyItemsSelector;

    public List<ItemData> SelectedItems { get; private set; }
    public List<ItemData> EnemyItems { get; private set; }

    public event Action ItemSelected;

    public StartBattleService(IBattleRunner battleRunner,
        IEnemyItemsSelector enemyItemsSelector)
    {
        SelectedItems = new List<ItemData>();
        _battleRunner = battleRunner;
        _enemyItemsSelector = enemyItemsSelector;
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

    public void DeselectItem(ItemData item) => 
        SelectedItems.Remove(item);

    public void StartBattle(List<AttackItem> playerItems, List<AttackItem> enemyItems) =>
        _battleRunner.StartBattle(playerItems, enemyItems);

    public void SelectEnemyItems() => 
        EnemyItems = _enemyItemsSelector.SelectEnemyItems();
}
