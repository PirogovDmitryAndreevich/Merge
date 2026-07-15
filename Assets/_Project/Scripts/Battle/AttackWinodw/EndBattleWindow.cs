using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class  EndBattleWindow : MonoBehaviour
{
    [SerializeField] private Button _acceptButton;
    [SerializeField] private RewardSlider _rewardSlider;
    [SerializeField] private TMP_Text _scoreText;

    protected IBattleRunner BattleRunner;
    private AttackWindow _attackWindow;
    private Transform _buttonTransform;
    private IProgressService _progress;
    private ICoinsCounterService _coinsService;
    private int _reward;
    private int _score;


    private void OnDestroy()
    {        
        _rewardSlider.RewardButtonClicked -= OnRewardButtonClicked;
        _acceptButton.onClick.RemoveListener(OnAcceptButtonClick);

        Unsubscribes();
    }

    public void Construct(IProgressService progressService, 
        IBattleRunner battleRunner, AttackWindow attackWindow,
        ICoinsCounterService coinsCounterService)
    {
        _progress = progressService;
        BattleRunner = battleRunner;
        _attackWindow = attackWindow;
        _coinsService = coinsCounterService;
        _buttonTransform = _acceptButton.transform;

        _rewardSlider.RewardButtonClicked += OnRewardButtonClicked;
        _acceptButton.onClick.AddListener(OnAcceptButtonClick);

        Subscribes();

        Hide();
    }

    protected abstract void Subscribes();

    protected abstract void Unsubscribes();

    protected void Show()
    {
        gameObject.SetActive(true);

        int maxItemId = _progress.Progress.Items.MaxItemID;

        _reward = Mathf.RoundToInt(20 * Mathf.Pow(1.15f, maxItemId));
        _score = maxItemId * _progress.Progress.Player.RewardLevel;
        _scoreText.text = _score.ToString();

        _rewardSlider.StartSlider(_reward);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnAcceptButtonClick()
    {
        _coinsService.AddCoins(_reward, _buttonTransform.position);
        _coinsService.AddScore(_score, _buttonTransform.position);

        Hide();
        _attackWindow.Hide();
    }

    private void OnRewardButtonClicked(int reward)
    {
        _coinsService.AddCoins(reward, _buttonTransform.position, withMultiplier: false);
        _coinsService.AddScore(_score, _buttonTransform.position);

        Hide();
        _attackWindow.Hide();
    }
}