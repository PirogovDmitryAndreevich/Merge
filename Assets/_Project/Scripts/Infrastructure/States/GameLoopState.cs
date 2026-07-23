using System.Collections;
using YG;

public class GameLoopState : IState
{
    private readonly ICoroutineRunner _coroutine;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IItemsAnimation _itemsAnimation;
    private readonly IStaticData _staticData;
    private readonly IBattleService _battleService;
    private readonly IInterstitialAdvService _interstitialAdvService;
    private bool _isGame;

    public GameLoopState(ICoroutineRunner coroutine, ISaveLoadService saveLoadService,
        IItemsAnimation itemsAnimation,
        IStaticData staticData, IBattleService battleService,
        IInterstitialAdvService interstitialAdvService)
    {
        _coroutine = coroutine;
        _saveLoadService = saveLoadService;
        _itemsAnimation = itemsAnimation;
        _staticData = staticData;
        _battleService = battleService;
        _interstitialAdvService = interstitialAdvService;
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
            if (_battleService.IsPlaying)
            {
                attackTimer = _staticData.GameData.AttackTimer;
                continue;
            }

            _itemsAnimation.Update();

            if (saveTimer <= 0)
            {
                SaveProgress();
                saveTimer = _staticData.GameData.SaveTimer;
            }

            if (attackTimer <= 0)
            {
                _battleService.Start(AttackWindowType.EnemyAttack);
                attackTimer = _staticData.GameData.AttackTimer;
            }

            if (YG2.isTimerAdvCompleted && !YG2.nowAdsShow)
            {
                _interstitialAdvService.ShowAdv();
            }

            attackTimer -= UnityEngine.Time.deltaTime;
            saveTimer -= UnityEngine.Time.deltaTime;

            yield return null;
        }
    }

    private void SaveProgress() =>
        _saveLoadService.SaveProgress();
}