using System;
using System.Collections;
using System.Collections.Generic;

public class BattleRunner : IBattleRunner
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IBattleTurnExecutor _battleTurnExecutor;
    private readonly ITargetSelector _targetSelector;

    public bool IsPlaying { get; private set; }

    public event Action<BattleResultType> PlayerWon;
    public event Action<BattleResultType> EnemyWon;

    public BattleRunner(ICoroutineRunner coroutineRunner, IBattleTurnExecutor battleTurnExecutor,
        ITargetSelector targetSelector)
    {
        _coroutineRunner = coroutineRunner;
        _targetSelector = targetSelector;
        _battleTurnExecutor = battleTurnExecutor;
    }

    public void StartBattle(List<AttackItem> playerItems, List<AttackItem> enemyItems) =>
        _coroutineRunner.StartCoroutine(StartBattleCoroutine(playerItems, enemyItems));


    private IEnumerator StartBattleCoroutine(List<AttackItem> playerItems, List<AttackItem> enemyItems)
    {
        IsPlaying = true;

        yield return null;

        while (IsPlaying)
        {
            yield return _battleTurnExecutor.ExecuteTurn(playerItems, enemyItems);

            if (!_targetSelector.HasAliveItems(enemyItems))
            {
                PlayerWon?.Invoke(BattleResultType.Win);
                IsPlaying = false;
                yield break;
            }

            yield return _battleTurnExecutor.ExecuteTurn(enemyItems, playerItems);

            if (!_targetSelector.HasAliveItems(playerItems))
            {
                EnemyWon?.Invoke(BattleResultType.Fail);
                IsPlaying = false;
                yield break;
            }
        }
    }
}
