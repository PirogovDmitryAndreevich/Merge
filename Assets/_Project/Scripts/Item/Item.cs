using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler,
    IEndDragHandler, IPointerDownHandler, IMoveableItem
{
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private ParticleSpawner _particle;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _mergeSound;

    private Canvas _canvas;
    private ItemData _data;
    private ItemSaveData _saveData;
    private ICursorHandler _cursorHandler;
    private AudioPlayer _audioPlayer;
    private IMergeService _merge;
    private RectTransform _frame;
    private ICoinsCounterService _coinsService;
    private UIImpactShake _impactShake;
    private int _profit;
    private Rect _frameBounds;
    private bool _isCursorView;

    public int ID { get; private set; }
    public RectTransform RectTransform => _rectTransform;
    public ParticleSpawner Spawner => _particle;
    public Collider2D Collider => _collider;
    public ItemData ItemData => _data;
    public ItemSaveData SaveData => _saveData;
    public ContactFilter2D ContactFilter => _contactFilter;

    public void Construct(Canvas canvas, RectTransform frame, IMergeService mergeService,
        ICoinsCounterService coinsCounterService, UIImpactShake impactShake,
        ICursorHandler cursorHandler, AudioPlayer audio)
    {
        _canvas = canvas;
        _merge = mergeService;
        _frame = frame;
        _coinsService = coinsCounterService;
        _impactShake = impactShake;
        _cursorHandler = cursorHandler;
        _audioPlayer = audio;

        CacheFrameBounds();
    }

    public void Initialize(ItemData itemData, ItemSaveData saveData)
    {
        _data = itemData;
        _saveData = saveData;        

        ID = _data.ID;
        _saveData.ID = ID;
        _image.sprite = _data.Sprite;
        _profit = _data.Profit;
        _canvasGroup.alpha = 1;

        _rectTransform.SetAsLastSibling();
    }

    public void SavePosition() =>
        _saveData.Position = _rectTransform.localPosition.AsVectorData();

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 0.5f;
        _rectTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = _rectTransform.anchoredPosition + eventData.delta / _canvas.scaleFactor;
        newPosition = GetNewPosition(newPosition);

        _rectTransform.anchoredPosition = newPosition;
    }    

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_merge.TryMerge(this))
        {
            _audioPlayer.PlaySound(_mergeSound);
        }
        else
        {
            _canvasGroup.alpha = 1;
            SavePosition();
        } 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_cursorHandler.IsEnableCursor)        
            _cursorHandler.HideCursor();        

        _audioPlayer.PlaySound(_clickSound);
        _coinsService.AddCoins(_profit, eventData.position, withMultiplier: true);
        _impactShake.Play();
        _particle.PlayClick( eventData.position);
    }

    private Vector2 GetNewPosition(Vector2 newPosition)
    {
        newPosition.x = Mathf.Clamp(newPosition.x, _frameBounds.xMin, _frameBounds.xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, _frameBounds.yMin, _frameBounds.yMax);
        return newPosition;
    }

    private void CacheFrameBounds()
    {
        Vector3[] frameCorners = new Vector3[4];
        _frame.GetWorldCorners(frameCorners);

        Vector3[] itemCorners = new Vector3[4];
        _rectTransform.GetWorldCorners(itemCorners);

        Vector3 itemSize = itemCorners[2] - itemCorners[0];
        Vector3 halfSize = itemSize * 0.5f;

        Vector3 frameMin = frameCorners[0];
        Vector3 frameMax = frameCorners[2];

        RectTransform frameRect = _frame;
        Vector2 localMin = frameRect.InverseTransformPoint(frameMin + halfSize);
        Vector2 localMax = frameRect.InverseTransformPoint(frameMax - halfSize);

        _frameBounds = new Rect(localMin.x, localMin.y, localMax.x - localMin.x, localMax.y - localMin.y);
    }
}
