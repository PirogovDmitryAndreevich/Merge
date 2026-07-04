using DG.Tweening;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;

    public void Shoot(AttackItem item)
    {
        _group.alpha = 1;

        transform.DOMove(item.transform.position, 1f)
            .OnComplete(() => TakeDamage(item));
    }

    private void TakeDamage(AttackItem target)
    {
        _group.alpha = 0;
        target.ApplyDamage();
    }
}