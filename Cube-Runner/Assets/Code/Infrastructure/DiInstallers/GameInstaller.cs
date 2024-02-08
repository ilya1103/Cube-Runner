using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.GameFactory;
using Code.Infrastructure.Services.SceneManagement;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.Services.UIFactory;
using Code.Infrastructure.States;
using Zenject;

namespace Code.Infrastructure.DiInstallers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInfrastructureServices();

            BindStateMachine();
            
            BindGlobalStates();
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<IGameStateFactory>().To<GameStateFactory>().AsSingle();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }
        
        private void BindGlobalStates()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
        }
    }
}