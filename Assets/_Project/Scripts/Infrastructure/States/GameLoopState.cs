using System.Collections;

public class GameLoopState : IState
{
    private readonly ICoroutineRunner _coroutine;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IItemsAnimation _itemsAnimation;
    private readonly IStaticData _staticData;
    private bool _isGame;

    public GameLoopState(ICoroutineRunner coroutine, ISaveLoadService saveLoadService, IItemsAnimation itemsAnimation,
        IStaticData staticData)
    {
        _coroutine = coroutine;
        _saveLoadService = saveLoadService;
        _itemsAnimation = itemsAnimation;
        _staticData = staticData;
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

        while (_isGame)
        {
            _itemsAnimation.Update();

            if (saveTimer <= 0)
            {
                SaveProgress();
                saveTimer = _staticData.GameData.SaveTimer;
            }

            saveTimer -= UnityEngine.Time.deltaTime;

            yield return null;
        }
    }

    private void SaveProgress() => 
        _saveLoadService.SaveProgress();
}