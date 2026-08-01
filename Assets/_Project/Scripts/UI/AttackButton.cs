using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AttackTimer _attackTimer;

    private IBattleService _battleService;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);

        if (_battleService != null)
            _battleService.CloseBattle -= StartTimer;

        if (_attackTimer != null)
            _attackTimer.TimerFinished -= OnTimerFinished;
    }

    public void Construct(IBattleService battleService)
    {
        _battleService = battleService;

        _battleService.CloseBattle += StartTimer;
        _attackTimer.TimerFinished += OnTimerFinished;
        _button.onClick.AddListener(OnClick);

        _attackTimer.StopTimer();
    }

    private void OnClick() => 
        _battleService.Start(AttackWindowType.PlayerAttack);

    private void StartTimer(AttackWindowType type)
    {
        if (type == AttackWindowType.EnemyAttack)
            return;

        _button.interactable = false;
        _attackTimer.StartTimer();
    }

    private void OnTimerFinished()
    {
        _button.interactable = true;
    }
}
