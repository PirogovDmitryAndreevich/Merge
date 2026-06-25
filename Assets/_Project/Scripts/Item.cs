using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private ContactFilter2D _contactFilter;

    private bool _isMerged;
    private Canvas _canvas;
    private ItemData _data;
    private ItemSaveData _saveData;
    private IMergeService _merge;
    private float _profit;

    public ItemID ID {  get; private set; }
    public ItemID NextID {  get; private set; }
    public Collider2D Collider => _collider;
    public ItemSaveData SaveData => _saveData;
    public ContactFilter2D ContactFilter => _contactFilter;

    private void OnDestroy() => 
        _button.onClick.RemoveListener(OnClicked);

    private void Start() => 
        _button.onClick.AddListener(OnClicked);

    public void Construct(Canvas canvas, IMergeService mergeService)
    {
        _canvas = canvas;
        _merge = mergeService;
    }

    public void Initialize(ItemData itemData, ItemSaveData saveData)
    {
       _data = itemData;
        _saveData = saveData;

        ID = _data.ID;
        _saveData.ID = ID;
        NextID = _data.Next;
        _image.sprite = _data.Sprite;
        _profit = _data.Profit;

        _isMerged = false;
    }

    public void SavePosition() => 
        _saveData.Position = transform.position.AsVectorData();

    public void OnBeginDrag(PointerEventData eventData) => 
        _canvasGroup.alpha = 0.5f;

    public void OnDrag(PointerEventData eventData) =>
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isMerged)
        {
            _isMerged = true;
            _merge.TryMerge(this);
        }

        _canvasGroup.alpha = 1;
        SavePosition();
    }

    private void OnClicked()
    {

    }
}
