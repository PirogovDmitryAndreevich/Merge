using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _icon; 
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Icons")]
    [SerializeField] private Sprite _coinsSprite;
    [SerializeField] private Sprite _damageSprite;
    [SerializeField] private Sprite _scoreSprite;

    [Header("Colors")]
    [SerializeField] private Color _coinsColor;
    [SerializeField] private Color _damageColor;
    [SerializeField] private Color _scoreColor;

    private Color _color;
    private IPoolService _pool;
    private Sequence _sequence;

    public void Construct(IPoolService poolService) => 
        _pool = poolService;

    public void Play(int value, Vector2 startPoint, ValueType type)
    {
        _sequence?.Kill();

        string symbol = SetType(type);
        string numberFormat = NumberFormatter.NumberAsString(value);

        _text.color = _color;
        _text.text = $"{symbol}{numberFormat}";

        _rectTransform.position = startPoint;
        _rectTransform.localScale = Vector3.zero;
        _canvasGroup.alpha = 1f;

        Vector2 offset = new Vector2(
            Random.Range(-80f, 80f),
            Random.Range(120f, 180f));

        Vector2 targetPos = startPoint + offset;

        _sequence = DOTween.Sequence();

        _sequence.Append(
            _rectTransform.DOScale(1.2f, 0.15f)
                .SetEase(Ease.OutBack));

        _sequence.Join(
            _rectTransform.DOMove(targetPos, 0.8f)
                .SetEase(Ease.OutQuad));

        _sequence.Append(
            _rectTransform.DOScale(0.8f, 0.2f));

        _canvasGroup.DOFade(0f, 0.4f)
            .SetDelay(0.4f);

        _sequence.OnComplete(OnComplete);
    }

    private string SetType(ValueType type)
    {
        switch (type)
        {
            case ValueType.Coins:
                _icon.sprite = _coinsSprite;
                _color = _coinsColor;
                return "+";
            case ValueType.Damage:
                _icon.sprite = _damageSprite;
                _color = _damageColor;
                return "-";
            case ValueType.Score:
                _icon.sprite = _scoreSprite;
                _color = _scoreColor;
                return "+";
            default:
                return "+";
        }
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

public enum ValueType
{
    Coins,
    Damage,
    Score
}