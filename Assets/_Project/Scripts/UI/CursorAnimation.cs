using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CursorAnimation
{
    private Sequence _animation;

    public void ClickAnimation(Transform cursor, CanvasGroup text)
    {
        _animation?.Kill();

        Vector3 startRotation = cursor.localEulerAngles;

        _animation = DOTween.Sequence();

        _animation
        .Append(
            cursor.DOLocalRotate(
                startRotation + new Vector3(0f, 0f, -15f),
                0.5f)
            .SetEase(Ease.InOutSine)
        )
        .Join(
            text.DOFade(1f, 0.5f)
                .From(0f)
                .SetEase(Ease.InOutSine)
        )
        .Join(
            text.transform.DOScale(1f, 0.5f)
                .From(0f)
                .SetEase(Ease.InOutSine)
        )
        .SetLoops(-1, LoopType.Yoyo)
        .Play();
    }

    public IEnumerator MergeAnimation()
    {
        yield return null;
    }
}