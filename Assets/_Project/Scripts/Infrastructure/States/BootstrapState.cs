public class BootstrapState : IState
{
    private const string InitialSceneName = "Initial";

    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _allServices;
    private readonly ICoroutineRunner _coroutineRunner;

    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices allServices,
        ICoroutineRunner coroutineRunner)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _allServices = allServices;
        _coroutineRunner = coroutineRunner;
        RegisterService();
    }

    public void Enter() => 
        _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);

    private void EnterLoadLevel()
    {
        _gameStateMachine.Enter<LoadProgressState>();
    }

    public void Exit()
    {
        
    }

    private void RegisterService()
    {
        RegisterStaticData();
        _allServices.RegisterSingle<IProgressService>(new ProgressService());
        _allServices.RegisterSingle<ISaveLoadService>(new SaveLoadService(_allServices.Single<IProgressService>()));
        _allServices.RegisterSingle<IAssetsProvider>(new AssetsProvider());
        _allServices.RegisterSingle<IPoolService>(new PoolService());
        _allServices.RegisterSingle<IItemsPoolService>(new ItemsPoolService());

        RegisterBattleServices();

        _allServices.RegisterSingle<IItemsAnimation>(new ItemsAnimation(_allServices.Single<IItemsPoolService>(),
           _allServices.Single<IStaticData>()));

        _allServices.RegisterSingle<IItemSpawnService>(new ItemSpawnService(_allServices.Single<IStaticData>(),
            _allServices.Single<IProgressService>(), _allServices.Single<IItemsPoolService>()));

        _allServices.RegisterSingle<IMergeService>(new MergeService(_allServices.Single<IStaticData>(),
            _allServices.Single<IProgressService>(), _allServices.Single<IPoolService>(),
            _allServices.Single<IItemSpawnService>(), _allServices.Single<IItemsPoolService>()));

        _allServices.RegisterSingle<IGameFactory>(new GameFactory(
            _allServices.Single<IAssetsProvider>(),
            _allServices.Single <IMergeService>(),
            _allServices.Single<IProgressService>(),
            _allServices.Single<IPoolService>(),
            _allServices.Single<IItemsPoolService>(),
            _allServices.Single<IStaticData>(),
            _allServices.Single<IStartBattleService>(),
            _allServices.Single<IAttackAnimator>(),
            _allServices.Single<IBattleRunner>()));
    }

    private void RegisterStaticData()
    {
        IStaticData staticData = new StaticDataService();
        staticData.Load();
        _allServices.RegisterSingle<IStaticData>(staticData);
    }

    private void RegisterBattleServices()
    {
        _allServices.RegisterSingle<IEnemyItemsSelector>(new EnemyItemsSelector(_allServices.Single<IProgressService>(),
            _allServices.Single<IStaticData>()));

        _allServices.RegisterSingle<IAttackAnimator>(new ArrowAttackAnimator());
        _allServices.RegisterSingle<ITargetSelector>(new TargetSelector());

        _allServices.RegisterSingle<IBattleTurnExecutor>(new BattleTurnExecutor(
            _allServices.Single<ITargetSelector>(),
            _allServices.Single<IAttackAnimator>()));

        _allServices.RegisterSingle<IBattleRunner>(new BattleRunner(
            _coroutineRunner,
            _allServices.Single<IBattleTurnExecutor>(),
            _allServices.Single<ITargetSelector>()));

        _allServices.RegisterSingle<IStartBattleService>(new StartBattleService(
            _allServices.Single<IBattleRunner>(),
            _allServices.Single<IEnemyItemsSelector>()));
    }
}
