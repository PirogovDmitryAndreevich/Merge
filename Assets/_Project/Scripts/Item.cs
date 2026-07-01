using DG.Tweening;
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
    private int _profit;

    public int ID { get; private set; }
    public ParticleSpawner Spawner => _particle;
    public Collider2D Collider => _collider;
    public ItemSaveData SaveData => _saveData;
    public ContactFilter2D ContactFilter => _contactFilter;

    private Tween _idleAnimation;

    private void OnDestroy() => 
        _idleAnimation?.Kill();

    public void Construct(Canvas canvas, IMergeService mergeService, IProgressService progress)
    {
        _canvas = canvas;
        _merge = mergeService;
        _progress = progress;

        _idleAnimation = transform.DORotate(new Vector3(0, 0, 10), 1f)
            .SetRelative()
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
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
        _saveData.Position = transform.position.AsVectorData();

    public void OnBeginDrag(PointerEventData eventData) =>
        _canvasGroup.alpha = 0.5f;

    public void OnDrag(PointerEventData eventData) =>
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

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
}
