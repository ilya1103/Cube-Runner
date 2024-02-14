using Project.Code.Infrastructure.AssetManagement;
using Project.Code.Infrastructure.Services.GameFactory;
using Project.Code.Infrastructure.Services.LoadingCurtainService;
using Project.Code.Infrastructure.Services.SceneManagement;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Infrastructure.Services.UIFactory;
using Project.Code.Infrastructure.States;
using Project.Code.Infrastructure.UI;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.DiInstallers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        
        public override void InstallBindings()
        {
            BindInfrastructureServices();

            BindStateMachine();
            
            BindGlobalStates();
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtain).AsSingle();
            Container.Bind<ILoadingCurtainService>().To<LoadingCurtainService>().AsSingle();
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