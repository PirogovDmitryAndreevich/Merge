using DG.Tweening;

public interface IAttackAnimator : IService
{
    void SetBullets(Arrow[] arrows);
    Tween PlayAttack(
        AttackItem attacker,
        AttackItem target,
        int attackerIndex);
}