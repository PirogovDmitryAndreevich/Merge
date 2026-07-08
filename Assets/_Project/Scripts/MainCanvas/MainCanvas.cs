using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _collectionButton;
    [SerializeField] private SpawnButton _spawnButton;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CoinsCounter _coinsCounter;
    [SerializeField] private RectTransform _spawnFrame;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _itemParent;
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private UIImpactShake _impactShake;

    [Header("Windows")]
    [SerializeField] private NewItemWindow _newItemWindow;
    [SerializeField] private CollectionWindow _collectionWindow;
    [SerializeField] private StartBattleWindow _startBattleWindow;
    [SerializeField] private SelectWindow _selectWindow;
    [SerializeField] private AttackWindow _attackWindow;
    [SerializeField] private BonusView _bonusView;

    private IItemSpawnService _itemSpawn;

    public UIImpactShake ImpactShake => _impactShake;
    public Leaderboard Leaderboard => _leaderboard;
    public SpawnButton SpawnButton => _spawnButton;
    public NewItemWindow NewItemWindow => _newItemWindow;
    public CollectionWindow Collection => _collectionWindow;
    public StartBattleWindow StartBattleWindow => _startBattleWindow;
    public SelectWindow SelectWindow => _selectWindow;
    public AttackWindow AttackWindow => _attackWindow;
    public BonusView BonusView => _bonusView;
    public CoinsCounter CoinsCounter => _coinsCounter;
    public RectTransform SpawnFrame => _spawnFrame;
    public Transform ParentForItems => _itemParent;

    private void Awake()
    {
        _spawnButton.ButtonClicked += OnButtonClicked;
        _collectionButton.onClick.AddListener(ShowCollection);
        _attackButton.onClick.AddListener(StartAttack);
    }

    private void OnDestroy()
    {
        _spawnButton.ButtonClicked -= OnButtonClicked;
        _collectionButton.onClick.RemoveListener(ShowCollection);
    }

    public void SetItemSpawn(IItemSpawnService itemSpawn) => 
        _itemSpawn = itemSpawn;

    private void OnButtonClicked()
    {
        Debug.Log("Spawn Button Clicked");
        _itemSpawn.Spawn();
    }

    private void ShowCollection()
    {
        Debug.Log("ShowCollection");
        _collectionWindow.Show();
    }

    private void StartAttack()
    {
        Debug.Log("Start Attack");
        _startBattleWindow.Show();
    }
}
