using System;
using System.Collections.Generic;

public interface IStartBattleService : IService
{
    List<ItemData> SelectedItems { get;}
    List<ItemData> EnemyItems { get;}

    event Action ItemSelected;

    void Cleanup();
    void DeselectItem(ItemData item);
    void SelectEnemyItems();
    bool TrySelectItem(ItemData item);
    void StartBattle(List<AttackItem> playerItems, List<AttackItem> enemyItems);
}