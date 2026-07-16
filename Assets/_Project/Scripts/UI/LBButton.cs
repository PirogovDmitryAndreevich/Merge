using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

public class LBButton : MonoBehaviour
{
    [SerializeField] private GameObject _alert;
    [SerializeField] private TMP_Text _alertText;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _rank;
    [SerializeField] private NewReward _newReward;

    private ILBRewardHandler _rewardHandler;
    private Leaderboard _leaderboard;
    private bool _isActive;

    private void OnDestroy()
    {
        _rewardHandler.RewardCountChanged -= UpdateAlert;
        YG2.onGetLeaderboard -= OnGetLeaderboard;
        _rewardHandler.RewardAdded -= ShowNewReward;
        _button.onClick.RemoveListener(OnClickButton);
    }

    public void Construct(ILBRewardHandler rewardHandler, Leaderboard leaderboard)
    {
        _rewardHandler = rewardHandler;
        _leaderboard = leaderboard;

        YG2.onGetLeaderboard += OnGetLeaderboard;
        _rewardHandler.RewardCountChanged += UpdateAlert;
        _rewardHandler.RewardAdded += ShowNewReward;
        _button.onClick.AddListener(OnClickButton);

        _isActive = !_rewardHandler.HaveAvailableRewards;
        UpdateAlert();
    }

    private void OnGetLeaderboard(LBData data)
    {
        _rank.text = data.currentPlayer.rank.ToString();
        _rewardHandler.OnGetLeaderboard(data);
    }

    private void OnClickButton()
    {
        if (!_leaderboard.IsActive)
            _leaderboard.Show();
        else
            _leaderboard.Hide();
    }

    private void UpdateAlert()
    {
        if (_isActive != _rewardHandler.HaveAvailableRewards)
        {
            _isActive = _rewardHandler.HaveAvailableRewards;
            _alert.SetActive(_isActive);
        }

        if (_isActive)
            _alertText.text = _rewardHandler.AvailableRewards.ToString();
    }

    private void ShowNewReward() =>
        _newReward.Show();
}
