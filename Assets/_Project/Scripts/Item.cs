using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private ParticleSpawner _particle;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private ContactFilter2D _contactFilter;

    private Canvas _canvas;
    private ItemData _data;
    private ItemSaveData _saveData;
    private IMergeService _merge;
    private IProgressService _progress;
    private RectTransform _frame;
    private int _profit;
    private Rect _frameBounds;

    public int ID { get; private set; }
    public RectTransform RectTransform => _rectTransform;
    public ParticleSpawner Spawner => _particle;
    public Collider2D Collider => _collider;
    public ItemSaveData SaveData => _saveData;
    public ContactFilter2D ContactFilter => _contactFilter;

    public void Construct(Canvas canvas, RectTransform frame, IMergeService mergeService, IProgressService progress)
    {
        _canvas = canvas;
        _merge = mergeService;
        _progress = progress;
        _frame = frame;

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
    }

    public void SavePosition() =>
        _saveData.Position = _rectTransform.position.AsVectorData();

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
        _merge.TryMerge(this);

        _canvasGroup.alpha = 1;
        SavePosition();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _progress.Progress.Player.AddCoins(_profit);
        _particle.Play(_profit, eventData.position);
    }

    private Vector2 GetNewPosition(Vector2 newPosition)
    {
        newPosition.x = Mathf.Clamp(newPosition.x, _frameBounds.xMin, _frameBounds.xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, _frameBounds.yMin, _frameBounds.yMax);
        return newPosition;
    }

    private void CacheFrameBounds()
    {
        Vector2 itemSize = _rectTransform.rect.size * _rectTransform.lossyScale;

        Vector2 frameSize = _frame.rect.size * _frame.lossyScale;
        Vector2 framePosition = _frame.anchoredPosition;

        float minX = framePosition.x - frameSize.x / 2 + itemSize.x / 2;
        float maxX = framePosition.x + frameSize.x / 2 - itemSize.x / 2;
        float minY = framePosition.y - frameSize.y / 2 + itemSize.y / 2;
        float maxY = framePosition.y + frameSize.y / 2 - itemSize.y / 2;

        _frameBounds = new Rect(minX, minY, maxX - minX, maxY - minY);
    }
}
