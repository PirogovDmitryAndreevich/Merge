using UnityEngine;
using UnityEngine.UI;

public abstract class  EndBattleWindow : MonoBehaviour
{
    [SerializeField] private Button _acceptButton;
    [SerializeField] private RewardSlider _rewardSlider;

    protected IBattleRunner BattleRunner;
    private IProgressService _progress;
    private AttackWindow _attackWindow;
    private ICoinsCounterService _coinsService;
    private int _reward;


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

        _rewardSlider.StartSlider(_reward);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnAcceptButtonClick()
    {
        _coinsService.AddCoins(_reward, _acceptButton.transform.position);

        Hide();
        _attackWindow.Hide();
    }

    private void OnRewardButtonClicked(int reward)
    {
        _coinsService.AddCoins(reward, _acceptButton.transform.position, withMultiplier: false);

        Hide();
        _attackWindow.Hide();
    }
}