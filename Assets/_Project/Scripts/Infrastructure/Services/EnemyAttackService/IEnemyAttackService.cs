using System;

public interface IEnemyAttackService : IService
{
    event Action<AttackWindowType> AttackStarted;

    void StartAttack();
}