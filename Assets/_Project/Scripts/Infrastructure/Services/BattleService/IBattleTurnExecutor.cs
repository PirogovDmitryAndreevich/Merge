using System.Collections;
using System.Collections.Generic;

public interface IBattleTurnExecutor : IService
{
    IEnumerator ExecuteTurn
        (List<AttackItem> attackers, List<AttackItem> defenders);
}