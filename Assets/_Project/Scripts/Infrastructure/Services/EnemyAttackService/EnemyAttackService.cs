using System;

public class EnemyAttackService : IEnemyAttackService
{
    public event Action<AttackWindowType> AttackStarted;

    public void StartAttack() =>
        AttackStarted?.Invoke(AttackWindowType.EnemyAttack);
}