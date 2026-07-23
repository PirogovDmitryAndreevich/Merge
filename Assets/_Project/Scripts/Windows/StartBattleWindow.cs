using System;
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

        if (_lbData == null || _lbData.players == null || _lbData.players.Length == 0)
        {
            if (_lbData == null)
                Debug.LogWarning("Leaderboard data is null");
            else if (_lbData.players == null)
                Debug.LogWarning("Leaderboard players array is null");
            else if (_lbData.players.Length == 0)
                Debug.LogWarning("Leaderboard players array is empty");

            return;
        }

        _name.text = GetEnemyName();

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

    private string GetEnemyName()
    {
        LBPlayerData[] enemies = _lbData.players
        .Where(x => x != null && x.rank != _lbData.currentPlayer.rank)
        .ToArray();

        if (enemies.Length == 0)
            return null;

        int randomIndex = UnityEngine.Random.Range(0, enemies.Length);

        return enemies[randomIndex].name;
    }

    private void OnGetLeaderboard(LBData data) => 
        _lbData = data;
}

[Serializable]
public enum AttackWindowType
{
    PlayerAttack,
    EnemyAttack,
}