using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

public class StartBattleWindow : MonoBehaviour
{
    [SerializeField] private AttackWindowType _type;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _cancelButton;

    private LBData _lbData;
    private IBattleService _battleService;
    private bool _isLeaderboardEnable;

    private void OnDestroy()
    {
        _acceptButton.onClick.RemoveListener(OnAccept);
        _cancelButton.onClick.RemoveListener(OnCancel);
        YG2.onGetLeaderboard -= OnGetLeaderboard;
        _battleService.BattleStarted -= Show;
    }

    public void Construct(IBattleService startBattleService)
    {
        _battleService = startBattleService;

        _battleService.BattleStarted += Show;
        YG2.onGetLeaderboard += OnGetLeaderboard;
        _acceptButton.onClick.AddListener(OnAccept);
        _cancelButton.onClick.AddListener(OnCancel);

        Hide();
    }

    public void Show(AttackWindowType type)
    {
        if (_type != type)
            return;

        if (YG2.player.auth)
        {
            _name.gameObject.SetActive(true);
            UpdateEnemyName();
        }
        else
        {
            _name.gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    private void OnCancel()
    {
        _battleService.OnCancel();
        Hide();
    }

    private void OnAccept()
    {
        _battleService.StartSelecting();
        Hide();
    }

    private void Hide() =>
        gameObject.SetActive(false);

    private void UpdateEnemyName()
    {
        if (!_isLeaderboardEnable || _lbData == null)
        {
            _name.gameObject.SetActive(false);
            return;
        }

        string enemyName = GetEnemyName();

        if (string.IsNullOrEmpty(enemyName))
        {
            _name.gameObject.SetActive(false);
            return;
        }

        _name.text = enemyName;
        _name.gameObject.SetActive(true);
    }

    private string GetEnemyName()
    {
        if (_lbData?.players == null)
            return null;

        LBPlayerData[] enemies = _lbData.players
            .Where(x => x != null && x.rank != _lbData.currentPlayer.rank)
            .ToArray();

        if (enemies.Length == 0)
            return null;

        int randomIndex = Random.Range(0, enemies.Length);

        return enemies[randomIndex].name;
    }

    private void OnGetLeaderboard(LBData data)
    {
        _lbData = data;

        _isLeaderboardEnable =
            _lbData != null &&
            _lbData.players != null &&
            _lbData.players.Length > 0;
    }
}
