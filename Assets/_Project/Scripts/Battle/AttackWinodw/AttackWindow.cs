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

    private IStartBattleService _battleService;

    private List<AttackItem> _playerItems;
    private List<AttackItem> _enemyItems;

    public Arrow[] Arrows => _arrows;
    public WinWindow WinWindow => _winWindow;
    public FailWindow FailWindow => _failWindow;

    public void Construct(IStartBattleService battleService, IPoolService poolService)
    {
        _battleService = battleService;
        _enemyFrame.Construct(poolService, _impactShake);
        _playerFrames.Construct(poolService, _impactShake);

        Hide();
    }

    public void Show()
    {
        _enemyItems = _enemyFrame.Initialize(_battleService.EnemyItems.ToArray());
        _playerItems = _playerFrames.Initialize(_battleService.SelectedItems.ToArray());

        gameObject.SetActive(true);

        _battleService.StartBattle(_playerItems, _enemyItems);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
