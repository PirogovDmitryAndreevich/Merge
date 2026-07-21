using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CursorAnimation
{
    public void ClickAnimation(Transform cursor)
    {
        cursor.DOKill();

        Vector3 startRotation = cursor.localEulerAngles;

        cursor.DOLocalRotate(startRotation + new Vector3(0f, 0f, -15f), 0.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public IEnumerator MergeAnimation()
    {
        yield return null;
    }
}