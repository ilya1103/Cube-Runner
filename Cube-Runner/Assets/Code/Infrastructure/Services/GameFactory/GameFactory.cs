using Code.Infrastructure.AssetManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;

        private Transform _groundPooler;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
        }

        public GameObject CreateCube(Transform cubeHolderTransform)
        {
            GameObject gameEventManager = _assetProvider.Instantiate(AssetPath.CubePath);
            return _diContainer.InstantiatePrefab(gameEventManager, cubeHolderTransform);
        }

        public GameObject CreateGroundPooler()
        {
            GameObject groundPooler = _assetProvider.Instantiate(AssetPath.GroundPoolerPath);
            GameObject result = _diContainer.InstantiatePrefab(groundPooler);
            SceneManager.MoveGameObjectToScene(result, SceneManager.GetActiveScene());
            _groundPooler = result.transform;
            return result;
        }

        public GameObject CreateGroundGenerator()
        {
            GameObject groundGenerator = _assetProvider.Instantiate(AssetPath.GroundGeneratorPath);
            GameObject result = _diContainer.InstantiatePrefab(groundGenerator);
            SceneManager.MoveGameObjectToScene(result, SceneManager.GetActiveScene());
            return result;
        }

        public GameObject CreateGroundVariant1()
        {
            GameObject ground = _assetProvider.Instantiate(AssetPath.GroundVariant1Path);
            return _diContainer.InstantiatePrefab(ground, _groundPooler);
        }

        public GameObject CreateGroundVariant2()
        {
            GameObject ground = _assetProvider.Instantiate(AssetPath.GroundVariant2Path);
            return _diContainer.InstantiatePrefab(ground, _groundPooler);
        }

        public GameObject CreateGroundVariant3()
        {
            GameObject ground = _assetProvider.Instantiate(AssetPath.GroundVariant3Path);
            return _diContainer.InstantiatePrefab(ground, _groundPooler);
        }

        public GameObject CreateGroundVariant4()
        {
            GameObject ground = _assetProvider.Instantiate(AssetPath.GroundVariant4Path);
            return _diContainer.InstantiatePrefab(ground, _groundPooler);
        }

        public GameObject CreateGroundVariant5()
        {
            GameObject ground = _assetProvider.Instantiate(AssetPath.GroundVariant5Path);
            return _diContainer.InstantiatePrefab(ground, _groundPooler);
        }

        public GameObject CreatePlayer(Vector3 at, Quaternion rotation)
        {
            GameObject player = _assetProvider.Instantiate(AssetPath.PlayerPath);
            GameObject result = _diContainer.InstantiatePrefab(player, at, rotation, null);
            SceneManager.MoveGameObjectToScene(result, SceneManager.GetActiveScene());
            return result;
        }

        public GameObject CreateCinemachine(Vector3 at, Quaternion rotation)
        {
            GameObject cinemachine = _assetProvider.Instantiate(AssetPath.CinemachinePath);
            GameObject result = _diContainer.InstantiatePrefab(cinemachine, at, rotation, null);
            SceneManager.MoveGameObjectToScene(result, SceneManager.GetActiveScene());  //Без этого объекты создаются в DontDestroyOnLoad
            return result;
        }
    }
}