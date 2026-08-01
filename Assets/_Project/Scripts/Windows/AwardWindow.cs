using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AwardWindow : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _buttonCanvasGroup;
    [SerializeField] private RectTransform _targetTransform;
    [SerializeField] private AudioPlayer _audioPlayer;
    [SerializeField] private AudioClip _showSound;
    [SerializeField] private RectTransform _glows;

    private Sequence _appearanceAnimation;
    private Sequence _moveAnimation;
    private IBonusHandler _bonusHandler;
    private Vector2 _targetSize;
    private Vector3 _startPosition;
    private Vector2 _startSize;
    private Vector3 _startScale;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Construct(IBonusHandler bonusHandler)
    {
        _bonusHandler = bonusHandler;

        _targetSize = _targetTransform.rect.size;

        RectTransform rect = _itemIcon.rectTransform;

        _startPosition = rect.position;
        _startSize = rect.sizeDelta;
        _startScale = rect.localScale;

        _button.onClick.AddListener(OnButtonClick);
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        _appearanceAnimation?.Kill();
        _glows.DOKill();

        _appearanceAnimation = DOTween.Sequence();

        _audioPlayer.PlaySound(_showSound);

        _glows.DOLocalRotate(
            new Vector3(0, 0, 360),
            4f,
            RotateMode.FastBeyond360)
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart);

        _appearanceAnimation
            .Append(_itemIcon.transform.DOScale(1, 0.5f).From(0)
            .SetEase(Ease.InOutBounce))
            .Append(_buttonCanvasGroup.DOFade(1, 0.3f).From(0));
    }

    private void OnButtonClick()
    {
        _moveAnimation?.Kill();

        _moveAnimation = DOTween.Sequence();

        _background.SetActive(false);
        _glows.gameObject.SetActive(false);
        _button.gameObject.SetActive(false);

        _moveAnimation
            .Join(_itemIcon.rectTransform.DOMove(_targetTransform.position, 1f)
                .SetEase(Ease.InOutQuad))
            .Join(_itemIcon.rectTransform.DOSizeDelta(_targetSize, 1f)
                .SetEase(Ease.InOutQuad))
            .OnComplete(GiveAward);
    }

    private void GiveAward()
    { 
        gameObject.SetActive(false);
        ResetView();

        _bonusHandler.AddBonus();        
    }

    private void ResetView()
    {
        RectTransform rect = _itemIcon.rectTransform;

        rect.position = _startPosition;
        rect.sizeDelta = _startSize;
        rect.localScale = _startScale;

        _buttonCanvasGroup.alpha = 0;
        _background.SetActive(true);
        _glows.gameObject.SetActive(true);
        _button.gameObject.SetActive(true);
    }
}
