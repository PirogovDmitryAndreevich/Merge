using UnityEngine;
using UnityEngine.UI;

public abstract class  EndBattleWindow : MonoBehaviour
{
    [SerializeField] private Button _acceptButton;
    [SerializeField] private RewardSlider _rewardSlider;

    protected IBattleRunner BattleRunner;
    private IProgressService _progress;
    private AttackWindow _attackWindow;

    private int _reward;

    private void OnDestroy()
    {        
        _rewardSlider.RewardButtonClicked -= Hide;
        _acceptButton.onClick.RemoveListener(OnAcceptButtonClick);

        Unsubscribes();
    }

    private void Start() => 
        Hide();

    public void Construct(IProgressService progressService, IBattleRunner battleRunner, AttackWindow attackWindow)
    {
        _progress = progressService;
        BattleRunner = battleRunner;
        _attackWindow = attackWindow;

        _rewardSlider.RewardButtonClicked += Hide;
        _acceptButton.onClick.AddListener(OnAcceptButtonClick);

        Subscribes();
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
        Debug.Log($"Reward: {_reward}");

        Hide();
        _attackWindow.Hide();
    }
}