using UnityEngine;
using UnityEngine.EventSystems;

public class BonusItem : MonoBehaviour, IPointerDownHandler, IMoveableItem
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private ParticleSpawner _particle;

    private UIImpactShake _impactShake;
    private IItemsPoolService _pool;
    private IBonusHandler _bonusHandler;

    public RectTransform RectTransform => _rectTransform;
    public ParticleSpawner Spawner => _particle;

    public void Construct(IItemsPoolService poolService, UIImpactShake impactShake, IBonusHandler bonusHandler)
    {
        _impactShake = impactShake;
        _pool = poolService;
        _bonusHandler = bonusHandler;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _bonusHandler.AddBonus();
        _impactShake.Play();
        _particle.PlayClick(eventData.position);
        _pool.ReturnItem(this);
    }

    public void SavePosition()
    {
        
    }
}
