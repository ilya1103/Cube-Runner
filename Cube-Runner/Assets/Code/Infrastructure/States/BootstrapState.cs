using Code.Infrastructure.Services.SceneManagement;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.States.Api;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
        }

        public void Enter()   
        {
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