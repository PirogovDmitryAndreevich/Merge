using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTurnExecutor : IBattleTurnExecutor
{
    private readonly ITargetSelector _targetSelector;
    private readonly IAttackAnimator _attackAnimator;

    public BattleTurnExecutor(ITargetSelector targetSelector, IAttackAnimator attackAnimator)
    {
        _targetSelector = targetSelector;
        _attackAnimator = attackAnimator;
    }

    public IEnumerator ExecuteTurn(List<AttackItem> attackers, List<AttackItem> defenders)
    {
        Sequence attackSequence = DOTween.Sequence();

        for (int i = 0; i < attackers.Count; i++)
        {
            AttackItem attacker = attackers[i];

            if (attacker.IsDestroy)
                continue;

            AttackItem target = _targetSelector.GetTarget(defenders, i);

            if (target == null)
                continue;

            attackSequence.Join(
                _attackAnimator.PlayAttack(
                    attacker,
                    target,
                    i));
        }

        yield return attackSequence.WaitForCompletion();

        yield return new WaitForSeconds(1f);
    }
}
