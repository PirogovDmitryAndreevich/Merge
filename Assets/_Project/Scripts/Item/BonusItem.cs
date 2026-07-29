using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BonusItem : MonoBehaviour, IPointerDownHandler, IMoveableItem
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private ParticleSpawner _particle;
    [SerializeField] private AudioClip _clickSound;

    private UIImpactShake _impactShake;
    private Sequence _moveAnimation;
    private IItemsPoolService _pool;
    private IBonusHandler _bonusHandler;
    private Vector2 _targetPosition;
    private Vector2 _targetSize;
    private AudioPlayer _audioPlayer;
    private bool _isClicked;
    private Vector2 _startSize;
    private Vector3 _startScale;

    public RectTransform RectTransform => _rectTransform;
    public ParticleSpawner Spawner => _particle;

    public void Construct(IItemsPoolService poolService, UIImpactShake impactShake,
        IBonusHandler bonusHandler,
        RectTransform multiplayerButton, AudioPlayer audioPlayer)
    {
        _impactShake = impactShake;
        _pool = poolService;
        _bonusHandler = bonusHandler;
        _targetPosition = multiplayerButton.TransformPoint(multiplayerButton.rect.center);
        _targetSize = multiplayerButton.rect.size;
        _audioPlayer = audioPlayer;

        _startSize = _rectTransform.sizeDelta;
        _startScale = _rectTransform.localScale;
    }

    public void Init()
    {
        _isClicked = false;
        ResetView();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isClicked)
            return;

        _isClicked = true;

        _audioPlayer.PlaySound(_clickSound);
        _impactShake.Play();
        _particle.PlayClick(eventData.position);    
        OnClicked();
    }

    public void SavePosition(){}

    private void OnClicked()
    {
        _moveAnimation?.Kill();

        _moveAnimation = DOTween.Sequence();

        _moveAnimation
            .Join(_rectTransform.DOMove(_targetPosition, 1f)
                .SetEase(Ease.InOutQuad))
            .Join(_rectTransform.DOSizeDelta(_targetSize, 1f)
                .SetEase(Ease.InOutQuad))
            .OnComplete(GiveAward);
    }

    private void GiveAward()
    {
        _bonusHandler.AddBonus();
        _pool.ReturnItem(this);
        ResetView();
    }

    private void ResetView()
    {
        _rectTransform.sizeDelta = _startSize;
        _rectTransform.localScale = _startScale;
    }
}
