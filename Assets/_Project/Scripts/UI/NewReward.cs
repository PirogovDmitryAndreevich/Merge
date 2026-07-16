using DG.Tweening;
using UnityEngine;

public class NewReward : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _transform;

    [SerializeField] private float _showDuration = 0.35f;
    [SerializeField] private float _visibleTime = 2f;
    [SerializeField] private float _hideDuration = 0.3f;

    private Sequence _sequence;

    private void Start() => 
        gameObject.SetActive(false);

    public void Show()
    {
        _sequence?.Kill();

        gameObject.SetActive(true);

        _canvasGroup.alpha = 0f;

        _transform.localScale = Vector3.one * 0.8f;
        _transform.anchoredPosition = Vector2.down * 20f;

        _sequence = DOTween.Sequence();

        // Появление
        _sequence.Join(_canvasGroup.DOFade(1f, _showDuration).From(0f));
        _sequence.Join(_transform.DOScale(1.05f, _showDuration).SetEase(Ease.OutBack));
        _sequence.Join(_transform.DOAnchorPosY(0f, _showDuration).SetEase(Ease.OutCubic));

        // Небольшой отскок
        _sequence.Append(_transform.DOScale(1f, 0.15f));

        // Пауза
        _sequence.AppendInterval(_visibleTime);

        // Исчезновение
        _sequence.Append(_canvasGroup.DOFade(0f, _hideDuration));
        _sequence.Join(_transform.DOAnchorPosY(20f, _hideDuration));
        _sequence.Join(_transform.DOScale(1.1f, _hideDuration));

        _sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}