using System.Collections;

public class GameLoopState : IState
{
    private readonly ICoroutineRunner _coroutine;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IItemsAnimation _itemsAnimation;
    private readonly IStaticData _staticData;
    private readonly IBattleRunner _battleRunner;
    private readonly IEnemyAttackService _enemyAttackService;
    private bool _isGame;

    public GameLoopState(ICoroutineRunner coroutine, ISaveLoadService saveLoadService, IItemsAnimation itemsAnimation,
        IStaticData staticData, IBattleRunner battleRunner, IEnemyAttackService enemyAttackService)
    {
        _coroutine = coroutine;
        _saveLoadService = saveLoadService;
        _itemsAnimation = itemsAnimation;
        _staticData = staticData;
        _battleRunner = battleRunner;
        _enemyAttackService = enemyAttackService;
    }

    public void Enter()
    {
        _isGame = true;
        _coroutine.StartCoroutine(Gameplay());
    }

    public void Exit()
    {

    }

    private IEnumerator Gameplay()
    {
        float saveTimer = _staticData.GameData.SaveTimer;
        float attackTimer = _staticData.GameData.AttackTimer;

        while (_isGame)
        {
            if (_battleRunner.IsPlaying)
            {
                attackTimer = _staticData.GameData.AttackTimer;
                yield return null;
            }

            _itemsAnimation.Update();

            if (saveTimer <= 0)
            {
                SaveProgress();
                saveTimer = _staticData.GameData.SaveTimer;
            }

            if (attackTimer <= 0)
            {
                _enemyAttackService.StartAttack();
                attackTimer = _staticData.GameData.AttackTimer;
            }

            attackTimer -= UnityEngine.Time.deltaTime;
            saveTimer -= UnityEngine.Time.deltaTime;

            yield return null;
        }
    }

    private void SaveProgress() => 
        _saveLoadService.SaveProgress();
}