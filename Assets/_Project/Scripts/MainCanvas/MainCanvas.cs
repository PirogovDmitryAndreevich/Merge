using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private SpawnButton _spawnButton;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CoinsCounter _coinsCounter;
    [SerializeField] private RectTransform _spawnFrame;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _itemParent;

    private IItemSpawnService _itemSpawn;

    public CoinsCounter CoinsCounter => _coinsCounter;
    public RectTransform SpawnFrame => _spawnFrame;
    public Transform ParentForItems => _itemParent;
    private Vector2 StartItemSpawnPoint => _spawnButton.transform.position;

    private void Awake() => 
        _spawnButton.ButtonClicked += OnButtonClicked;

    private void OnDestroy() => 
        _spawnButton.ButtonClicked -= OnButtonClicked;

    public void SetItemSpawn(IItemSpawnService itemSpawn) => 
        _itemSpawn = itemSpawn;

    private void OnButtonClicked() => 
        _itemSpawn.Spawn(StartItemSpawnPoint);
}
