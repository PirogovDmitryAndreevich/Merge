using System;
using System.Collections.Generic;

public interface IBattleService : IService
{
    List<ItemData> SelectedItems { get;}
    List<ItemData> EnemyItems { get;}
    bool IsPlaying { get; }

    event Action ItemSelected;
    event Action<AttackWindowType> BattleStarted;
    event Action StartSelectingItems;
    event Action StartedBattle;
    event Action<BattleResultType> BattleEnded;
    event Action<AttackWindowType> CloseBattle;

    void Start(AttackWindowType type);
    void StartSelecting();
    void StartBattle();
    void BattleRun(List<AttackItem> playerItems, List<AttackItem> enemyItems);
    void ClosingBattel();
    void OnCancel();
    void DeselectItem(ItemData item);
    bool TrySelectItem(ItemData item);
}