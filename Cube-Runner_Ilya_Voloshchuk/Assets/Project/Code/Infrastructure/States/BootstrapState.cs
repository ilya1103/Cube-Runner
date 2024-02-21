using Project.Code.Infrastructure.Services.LoadingCurtainService;
using Project.Code.Infrastructure.Services.SceneManagement;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Infrastructure.States.Interfaces;

namespace Project.Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly ILoadingCurtainService _loadingCurtainService;
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(ILoadingCurtainService loadingCurtainService, IGameStateMachine stateMachine, ISceneLoader sceneLoader, IStaticDataService staticDataService)
        {
            _loadingCurtainService = loadingCurtainService;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
        }

        public void Enter()   
        {
            _loadingCurtainService.Show();
            
            RegisterInfrastructureServices();

            string bootstrapSceneName = _staticDataService.ScenesStaticData.BootstrapScene;
            _sceneLoader.LoadSceneAsync(bootstrapSceneName, OnInitSceneLoaded);
        }

        private void OnInitSceneLoaded() =>
            _stateMachine.EnterState<LoadLevelState, string>(_staticDataService.ScenesStaticData.GameScene);

        private void RegisterInfrastructureServices() =>
            _staticDataService.LoadAll();

        public void Exit() { }
    }
}