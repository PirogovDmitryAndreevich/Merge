using DG.Tweening;
using TMPro;
using UnityEngine;

public class ProfitEffect : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;

    private IPoolService _pool;
    private Sequence _sequence;

    public void Construct(IPoolService poolService) => 
        _pool = poolService;

    public void Play(int value, Vector2 startPoint)
    {
        _sequence?.Kill();

        _text.text = $"+{value}";

        _rectTransform.position = startPoint;
        _rectTransform.localScale = Vector3.zero;
        _canvasGroup.alpha = 1f;

        // Небольшой случайный разброс
        Vector2 offset = new Vector2(
            Random.Range(-80f, 80f),
            Random.Range(120f, 180f));

        Vector2 targetPos = startPoint + offset;

        _sequence = DOTween.Sequence();

        // Быстрое появление
        _sequence.Append(
            _rectTransform.DOScale(1.2f, 0.15f)
                .SetEase(Ease.OutBack));

        // Полет
        _sequence.Join(
            _rectTransform.DOMove(targetPos, 0.8f)
                .SetEase(Ease.OutQuad));

        // Небольшое уменьшение в конце
        _sequence.Append(
            _rectTransform.DOScale(0.8f, 0.2f));

        // Исчезновение начинается чуть позже
        _canvasGroup.DOFade(0f, 0.4f)
            .SetDelay(0.4f);

        _sequence.OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        _pool.Return(this);
    }

    private void OnDisable()
    {
        _sequence?.Kill();
    }
}