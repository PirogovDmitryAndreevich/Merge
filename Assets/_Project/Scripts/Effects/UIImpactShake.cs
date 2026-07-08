using DG.Tweening;
using UnityEngine;

public class UIImpactShake : MonoBehaviour
{
    [SerializeField] private RectTransform target;

    [Header("Shake")]
    [SerializeField] private float duration = 0.12f;
    [SerializeField] private float strength = 15f;
    [SerializeField] private int vibrato = 3;
    [SerializeField] private float randomness = 90f;

    private Tween shakeTween;

    public void Play()
    {
        if (target == null)
            return;

        shakeTween?.Complete();

        shakeTween = target
            .DOShakeAnchorPos(
                duration,
                strength,
                vibrato,
                randomness,
                false,
                true)
            .SetEase(Ease.OutQuad);
    }

    private void OnDisable()
    {
        shakeTween?.Kill();
    }
}