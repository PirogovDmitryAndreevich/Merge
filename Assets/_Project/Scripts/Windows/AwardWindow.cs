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

    private Sequence _appearanceAnimation;
    private Sequence _moveAnimation;
    private IAwardHandler _awardHandler;
    private IBonusHandler _bonusHandler;
    private Vector2 _targetSize;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
        gameObject.SetActive(false);
    }    

    public void Construct(IAwardHandler awardHandler, IBonusHandler bonusHandler)
    {
        _awardHandler = awardHandler;
        _bonusHandler = bonusHandler;

        _targetSize = _targetTransform.rect.size;
    }

    public void Show()
    {
        gameObject.SetActive(true);

        _appearanceAnimation?.Kill();

        _appearanceAnimation = DOTween.Sequence();

        _appearanceAnimation.
            Append(_itemIcon.transform.DOScale(1, 1f).From(0)
            .SetEase(Ease.InOutBounce))
            .Append(_buttonCanvasGroup.DOFade(1, 0.5f).From(0));
    }

    private void OnButtonClick()
    {
        _moveAnimation?.Kill();

        _moveAnimation = DOTween.Sequence();

        _awardHandler.GiveOutReward();
        _background.SetActive(false);

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
        _bonusHandler.AddBonus();        
    }
}
