using System;
using System.Collections.Generic;

public interface IBattleRunner : IService
{
    event Action<BattleResultType> BattleEnded;

    void StartBattle
        (List<AttackItem> playerItems,
        List<AttackItem> enemyItems);
}