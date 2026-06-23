public class BootstrapState : IState
{
    private const string InitialSceneName = "Initial";
    private const string MainSceneName = "Main";

    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        RegisterService();
    }

    public void Enter() => 
        _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);

    private void EnterLoadLevel()
    {
        _gameStateMachine.Enter<LoadSceneState, string>(MainSceneName);
    }

    public void Exit()
    {
        
    }

    private void RegisterService()
    {
        
    }
}
