using System;
using System.Collections.Generic;

public class BattleService : IBattleService
{
    private const int MaxItemsCount = 5;

    private readonly IBattleRunner _battleRunner;
    private readonly IEnemyItemsSelector _enemyItemsSelector;

    private AttackWindowType _currentAttackType;

    public List<ItemData> SelectedItems { get; private set; }
    public List<ItemData> EnemyItems { get; private set; }
    public bool IsPlaying { get; private set; }

    public event Action ItemSelected;
    public event Action<AttackWindowType> BattleStarted;
    public event Action StartSelectingItems;
    public event Action StartedBattle;
    public event Action<BattleResultType> BattleEnded;
    public event Action<AttackWindowType> CloseBattle;

    public BattleService(IBattleRunner battleRunner,
        IEnemyItemsSelector enemyItemsSelector)
    {
        SelectedItems = new List<ItemData>();
        EnemyItems = new List<ItemData>();
        _battleRunner = battleRunner;
        _enemyItemsSelector = enemyItemsSelector;
    }

    public void Start(AttackWindowType type)
    {
        IsPlaying = true;
        _currentAttackType = type;
        BattleStarted?.Invoke(type);
    }

    public void StartSelecting() => 
        StartSelectingItems?.Invoke();

    public void StartBattle()
    {
        SelectEnemyItems();
        _battleRunner.BattleEnded += EndBattle;
        StartedBattle?.Invoke();
    }

    public void BattleRun(List<AttackItem> playerItems, List<AttackItem> enemyItems) =>
        _battleRunner.StartBattle(playerItems, enemyItems);

    public void EndBattle(BattleResultType type)
    {
        _battleRunner.BattleEnded -= EndBattle;
        BattleEnded?.Invoke(type);        
    }

    public void ClosingBattel()
    {
        CloseBattle?.Invoke(_currentAttackType);
        IsPlaying = false;
        Cleanup();
    }

    public void OnCancel()
    {
        IsPlaying = false;
        Cleanup();
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

    private void SelectEnemyItems() => 
        EnemyItems = _enemyItemsSelector.SelectEnemyItems();

    private void Cleanup()
    {
        SelectedItems.Clear();
        EnemyItems.Clear();
    }
}
