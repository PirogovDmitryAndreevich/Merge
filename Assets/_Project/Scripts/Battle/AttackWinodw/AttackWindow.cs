using System.Collections.Generic;
using UnityEngine;

public class AttackWindow : MonoBehaviour
{
    [SerializeField] private ItemsFrame _enemyFrame;
    [SerializeField] private ItemsFrame _playerFrames;
    [SerializeField] private Arrow[] _arrows;
    [SerializeField] private WinWindow _winWindow;
    [SerializeField] private FailWindow _failWindow;
    [SerializeField] private UIImpactShake _impactShake;
    [SerializeField] private AudioPlayer _audioPlayer;

    private IBattleService _battleService;

    public Arrow[] Arrows => _arrows;
    public WinWindow WinWindow => _winWindow;
    public FailWindow FailWindow => _failWindow;

    private void OnDestroy()
    {
        _battleService.StartedBattle -= Show;
        _battleService.CloseBattel -= Hide;
    }

    public void Construct(IBattleService battleService, IPoolService poolService, AudioPlayer audioPlayer)
    {
        _battleService = battleService;
        _enemyFrame.Construct(poolService, _impactShake, audioPlayer);
        _playerFrames.Construct(poolService, _impactShake, audioPlayer);

        _battleService.StartedBattle += Show;
        _battleService.CloseBattel += Hide;

        Hide();
    }

    private void Show()
    {
        _audioPlayer.PlayBattleMusic();

        List<AttackItem> enemyItems =
            _enemyFrame.Initialize(_battleService.EnemyItems.ToArray());

        List<AttackItem> playerItems =
            _playerFrames.Initialize(_battleService.SelectedItems.ToArray());

        gameObject.SetActive(true);

        _battleService.BattleRun(playerItems, enemyItems);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _audioPlayer.PlayMainMusic();
    }
}
