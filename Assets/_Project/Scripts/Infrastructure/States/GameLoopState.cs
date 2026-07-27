using System.Collections;
using System.Diagnostics;
using YG;

public class GameLoopState : IState
{
    private readonly ICoroutineRunner _coroutine;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IItemsAnimation _itemsAnimation;
    private readonly IProgressService _progressService;
    private readonly IStaticData _staticData;
    private readonly IBattleService _battleService;
    private readonly IInterstitialAdvService _interstitialAdvService;
    private readonly IReviewHandler _reviewHandler;
    private bool _isGame;

    public GameLoopState(ICoroutineRunner coroutine, ISaveLoadService saveLoadService,
        IItemsAnimation itemsAnimation, IProgressService progressService,
        IStaticData staticData, IBattleService battleService,
        IInterstitialAdvService interstitialAdvService, IReviewHandler reviewHandler)
    {
        _coroutine = coroutine;
        _saveLoadService = saveLoadService;
        _itemsAnimation = itemsAnimation;
        _progressService = progressService;
        _staticData = staticData;
        _battleService = battleService;
        _interstitialAdvService = interstitialAdvService;
        _reviewHandler = reviewHandler;
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
                yield return null;
                continue;
            }

            if (_progressService.Progress.Items.SpawnItemID > 3)
                _reviewHandler.UpdateData();

            _itemsAnimation.Update();

            if (saveTimer <= 0)
            {
                SaveProgress();
                saveTimer = _staticData.GameData.SaveTimer;
            }

            attackTimer -= UnityEngine.Time.deltaTime;

            if (attackTimer <= 0 && !_battleService.IsPlaying)
            {
                _battleService.Start(AttackWindowType.EnemyAttack);
                attackTimer = _staticData.GameData.AttackTimer;
            }

            if (!_interstitialAdvService.IsShowing && !_battleService.IsPlaying
                && YG2.isTimerAdvCompleted && !YG2.nowAdsShow)
            {
                _interstitialAdvService.ShowAdv();
            }

            saveTimer -= UnityEngine.Time.deltaTime;

            yield return null;
        }
    }

    private void SaveProgress() =>
        _saveLoadService.SaveProgress();
}