using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CoinsCounter _coinsCounter;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private RectTransform _spawnFrame;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _itemParent;
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private UIImpactShake _impactShake;
    [SerializeField] private CursorPointer _cursorPointer;
    [SerializeField] private InterstitialTimer _interstitialTimer;
    [SerializeField] private AudioPlayer _audioPlayer;

    [Header("Buttons")]
    [SerializeField] private SpawnButton _spawnButton;    
    [SerializeField] private CollectionButton _collectionButton;
    [SerializeField] private AttackButton _attackButton;
    [SerializeField] private MultiplayerButton _multiplayerButton;
    [SerializeField] private RewardButton _rewardButton;
    [SerializeField] private LBButton _leaderboardButton;
    [SerializeField] private ReviewButton _reviewButton;
    [SerializeField] private RewardSpawnButton _rewardSpawnButton;
    [SerializeField] private AudioButtons _audioButtons;

    [Header("Windows")]
    [SerializeField] private NewItemWindow _newItemWindow;
    [SerializeField] private CollectionWindow _collectionWindow;
    [SerializeField] private StartBattleWindow _startBattleWindow;
    [SerializeField] private StartBattleWindow _enemyAttackWindow;
    [SerializeField] private SelectWindow _selectWindow;
    [SerializeField] private AttackWindow _attackWindow;
    [SerializeField] private BonusView _bonusView;
    [SerializeField] private AwardWindow _awardWindow;

    public AudioPlayer AudioPlayer => _audioPlayer;
    public InterstitialTimer InterstitialTimer => _interstitialTimer;
    public MultiplayerButton MultiplayerButton => _multiplayerButton;
    public UIImpactShake ImpactShake => _impactShake;
    public Leaderboard Leaderboard => _leaderboard;
    public CollectionButton CollectionButton => _collectionButton;
    public RewardSpawnButton RewardSpawnButton => _rewardSpawnButton;
    public ReviewButton ReviewButton => _reviewButton;
    public AttackButton AttackButton => _attackButton;
    public SpawnButton SpawnButton => _spawnButton;
    public RewardButton RewardButton => _rewardButton;
    public LBButton LBButton => _leaderboardButton;
    public AudioButtons AudioButtons => _audioButtons;
    public NewItemWindow NewItemWindow => _newItemWindow;
    public CollectionWindow Collection => _collectionWindow;
    public StartBattleWindow StartBattleWindow => _startBattleWindow;
    public StartBattleWindow EnemyAttackWindow => _enemyAttackWindow;
    public AwardWindow AwardWindow => _awardWindow;
    public SelectWindow SelectWindow => _selectWindow;
    public AttackWindow PlayerAttackWindow => _attackWindow;
    public BonusView BonusView => _bonusView;
    public CoinsCounter CoinsCounter => _coinsCounter;
    public ScoreCounter ScoreCounter => _scoreCounter;
    public RectTransform SpawnFrame => _spawnFrame;
    public Transform ParentForItems => _itemParent;
    public CursorPointer CursorPointer => _cursorPointer;
}
