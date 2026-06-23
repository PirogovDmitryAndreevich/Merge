public class LoadProgressState : IState
{
    private const string MainSceneName = "Main";

    private readonly GameStateMachine _gameStateMachine;
    private readonly IProgressService _progressService;
    private readonly ISaveLoadService _save;

    public LoadProgressState(GameStateMachine gameStateMachine, IProgressService progressService, ISaveLoadService save)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _save = save;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();
        _gameStateMachine.Enter<LoadSceneState, string>(MainSceneName);
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew()
    {
        _progressService.Progress =
            _save.LoadProgress()
            ?? NewProgress();
    }

    private SaveData NewProgress() =>
        new SaveData();
}
