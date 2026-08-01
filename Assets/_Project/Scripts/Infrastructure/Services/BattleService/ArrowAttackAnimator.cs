using DG.Tweening;
using UnityEngine;

public class ArrowAttackAnimator : IAttackAnimator
{
    private Arrow[] _arrows;

    public void SetBullets(Arrow[] arrows) => 
        _arrows = arrows;

    public Tween PlayAttack(AttackItem attacker, AttackItem target,
         int attackerIndex)
    {
        Arrow arrow = _arrows[attackerIndex];

        Transform arrowTransform = arrow.transform;

        Vector2 startPosition = attacker.transform.position;
        Vector2 targetPosition = target.transform.position;

        arrowTransform.position = startPosition;
        arrowTransform.right = (targetPosition - startPosition).normalized;

        arrow.SetAlpha(1f);
        arrow.PlayAttackSound();

        return arrowTransform
            .DOMove(targetPosition, 0.5f)
            .OnComplete(() =>
            {
                if (!target.IsDestroy)
                    target.ApplyDamage(attacker.ItemData.Damage);

                arrow.SetAlpha(0f);
            });
    }
}