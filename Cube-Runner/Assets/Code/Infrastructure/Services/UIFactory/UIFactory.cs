using Code.Infrastructure.AssetManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.Services.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IAssetProvider _assetProvider;
        
        private Transform _uiRoot;

        public UIFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
        }
        
        public void CreateUIRoot()
        {
            GameObject uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath);
            GameObject result = _diContainer.InstantiatePrefab(uiRoot);
            SceneManager.MoveGameObjectToScene(result, SceneManager.GetActiveScene());
            _uiRoot = result.transform;
        }
        
        public GameObject CreateGameUI()
        {
            GameObject gameUi = _assetProvider.Instantiate(AssetPath.GameUIPath);
            return _diContainer.InstantiatePrefab(gameUi, _uiRoot);   //Когда спавнишь в какой-то parent не надо делать SceneManager.MoveGameObjectToScene
        }
    }
}