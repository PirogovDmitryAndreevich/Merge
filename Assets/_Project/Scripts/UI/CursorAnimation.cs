using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CursorAnimation
{
    private Sequence _animation;

    private Transform _currentCursor;
    private Quaternion _startRotation;

    public void ClickAnimation(Transform cursor, CanvasGroup text)
    {
        if (_currentCursor == cursor)
        {
            _animation?.Kill();
            cursor.localRotation = _startRotation;
        }
        else
        {
            _animation?.Kill();

            _currentCursor = cursor;
            _startRotation = cursor.localRotation;
        }

        Vector3 startRotation = cursor.localEulerAngles;

        _animation = DOTween.Sequence();

        _animation
        .Append(
            cursor.DOLocalRotateQuaternion(
                 _startRotation * Quaternion.Euler(0f, 0f, -15f),
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

    public void StopAnimation()
    {
        _animation?.Kill();

        if (_currentCursor != null)
            _currentCursor.localRotation = _startRotation;
    }

    public void MergeAnimation(Transform cursor, Transform first, Transform second)
    {
        _animation?.Kill();

        cursor.position = first.position;

        _animation = DOTween.Sequence();

        _animation
            .Append(cursor.DOMove(second.position, 0.8f).SetEase(Ease.InOutSine))
            .Join(cursor.DOScale(0.9f, 0.2f).SetLoops(4, LoopType.Yoyo))
            .AppendInterval(0.2f)
            .Append(cursor.DOMove(first.position, 0.8f).SetEase(Ease.InOutSine))
            .SetLoops(-1)
            .Play();
    }
}