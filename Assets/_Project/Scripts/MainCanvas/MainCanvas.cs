using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private Button _collectionButton;
    [SerializeField] private SpawnButton _spawnButton;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CoinsCounter _coinsCounter;
    [SerializeField] private RectTransform _spawnFrame;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _itemParent;

    [Header("Windows")]
    [SerializeField] private NewItemWindow _newItemWindow;
    [SerializeField] private CollectionWindow _collectionWindow;

    private IItemSpawnService _itemSpawn;

    public SpawnButton SpawnButton => _spawnButton;
    public NewItemWindow NewItemWindow => _newItemWindow;
    public CollectionWindow Collection => _collectionWindow;
    public CoinsCounter CoinsCounter => _coinsCounter;
    public RectTransform SpawnFrame => _spawnFrame;
    public Transform ParentForItems => _itemParent;

    private void Awake()
    {
        _spawnButton.ButtonClicked += OnButtonClicked;
        _collectionButton.onClick.AddListener(ShowCollection);
    }

    private void OnDestroy()
    {
        _spawnButton.ButtonClicked -= OnButtonClicked;
        _collectionButton.onClick.RemoveListener(ShowCollection);
    }

    public void SetItemSpawn(IItemSpawnService itemSpawn) => 
        _itemSpawn = itemSpawn;

    private void OnButtonClicked() => 
        _itemSpawn.Spawn();

    private void ShowCollection() =>
        _collectionWindow.Show();
}
