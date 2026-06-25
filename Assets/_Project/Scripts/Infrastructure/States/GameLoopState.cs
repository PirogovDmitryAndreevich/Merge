using System.Collections;

public class GameLoopState : IState
{
    private const int MinuteCoefficient = 60;
    private const float SaveTimer = 0.5f * MinuteCoefficient;

    private readonly ICoroutineRunner _coroutine;
    private readonly ISaveLoadService _saveLoadService;
    private bool _isGame;

    public GameLoopState(ICoroutineRunner coroutine, ISaveLoadService saveLoadService)
    {
        _coroutine = coroutine;
        _saveLoadService = saveLoadService;
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
        float saveTimer = SaveTimer;

        while (_isGame)
        {
            if (saveTimer <= 0)
            {
                SaveProgress();
                saveTimer = SaveTimer;
            }

            saveTimer -= UnityEngine.Time.deltaTime;

            yield return null;
        }
    }

    private void SaveProgress() => 
        _saveLoadService.SaveProgress();
}