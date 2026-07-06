using System;
using System.Collections.Generic;

public interface IBattleRunner : IService
{
    bool IsPlaying { get; }

    event Action PlayerWon;
    event Action EnemyWon;


    void StartBattle
        (List<AttackItem> playerItems,
        List<AttackItem> enemyItems);
}