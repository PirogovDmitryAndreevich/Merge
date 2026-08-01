using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardYG _leaderboard;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private CanvasGroup _canvasGroup;

    private IProgressService _progressService;
    private ILBRewardHandler _rewardHandler;
    private AwardWindow _awardWindow;
    private bool _isInitialize;

    public bool IsActive { get; private set; }

    private void OnEnable()
    {
        _leaderboard.UpdateLB();

        if (_isInitialize)
            _rewardButton.gameObject.SetActive(_rewardHandler.HaveAvailableRewards);
    }

    private void OnDestroy()
    {
        _progressService.Progress.Player.UnspentScoreChanged -= SetNewValueLB;
        _rewardHandler.RewardCountChanged -= UpdateRewardButton;
        _closeButton.onClick.AddListener(Hide);
        _rewardButton.onClick.RemoveListener(OnRewardButtonClicked);

    }

    public void Construct(IProgressService progressService, ILBRewardHandler rewardHandler, AwardWindow awardWindow)
    {
        _progressService = progressService;
        _rewardHandler = rewardHandler;
        _awardWindow = awardWindow;

        _progressService.Progress.Player.UnspentScoreChanged += SetNewValueLB;
        _rewardHandler.RewardCountChanged += UpdateRewardButton;
        _closeButton.onClick.AddListener(Hide);
        _rewardButton.onClick.AddListener(OnRewardButtonClicked);

        SetNewValueLB();
        UpdateRewardButton();
        _isInitialize = true;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        IsActive = true;

        gameObject.SetActive(true);

        _canvasGroup.DOFade(1f, 0.5f).From(0f);
    }

    public void Hide()
    {
        IsActive = false;

        _canvasGroup.DOFade(0f, 0.5f).From(1f)
            .OnComplete(() => gameObject.SetActive(false));
    }

    private void SetNewValueLB()
    {
        _leaderboard.SetLeaderboard(_progressService.Progress.Player.Score);
        _leaderboard.UpdateLB();
    }

    private void UpdateRewardButton()
    {
        _rewardButton.gameObject.SetActive(_rewardHandler.HaveAvailableRewards);
    }

    private void OnRewardButtonClicked()
    {
        _awardWindow.Show();
        _rewardHandler.SpendReward();       
    }
}