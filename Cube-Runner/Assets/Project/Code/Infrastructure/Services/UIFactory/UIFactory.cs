using Project.Code.Infrastructure.AssetManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Code.Infrastructure.Services.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IAssetProvider _assetProvider;
        
        private Transform _uiRootTransform;

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
            _uiRootTransform = result.transform;
        }
        
        public GameObject CreateGameUI()
        {
            GameObject gameUi = _assetProvider.Instantiate(AssetPath.GameUIPath);
            return _diContainer.InstantiatePrefab(gameUi, _uiRootTransform);  
        }
    }
}