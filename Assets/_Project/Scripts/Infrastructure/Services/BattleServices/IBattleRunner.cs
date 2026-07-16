using System;
using System.Collections.Generic;

public interface IBattleRunner : IService
{
    bool IsPlaying { get; }

    event Action<BattleResultType> PlayerWon;
    event Action<BattleResultType> EnemyWon;


    void StartBattle
        (List<AttackItem> playerItems,
        List<AttackItem> enemyItems);
}