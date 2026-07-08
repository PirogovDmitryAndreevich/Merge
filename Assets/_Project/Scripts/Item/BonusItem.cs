using UnityEngine;
using UnityEngine.EventSystems;

public class BonusItem : MonoBehaviour, IPointerDownHandler, IMoveableItem
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private ParticleSpawner _particle;

    private ICoinsCounterService _coinsService;
    private UIImpactShake _impactShake;

    public RectTransform RectTransform => _rectTransform;
    public ParticleSpawner Spawner => _particle;

    public void Construct(ICoinsCounterService coinsCounterService, UIImpactShake impactShake)
    {
        _coinsService = coinsCounterService;
        _impactShake = impactShake;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _impactShake.Play();
        _particle.PlayClick(eventData.position);
        _coinsService.AddMultiplier(this);
    }

    public void SavePosition()
    {
        
    }
}
