using Project.Code.Infrastructure.AssetManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Code.Infrastructure.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;

        private Transform _groundTransform;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
        }

        public GameObject CreateCube(Transform cubeHolderTransform)
        {
            GameObject cube = _assetProvider.Instantiate(AssetPath.CubePath);
            return _diContainer.InstantiatePrefab(cube, cubeHolderTransform);
        }

        public GameObject CreateGround()
        {
            GameObject ground = _assetProvider.Instantiate(AssetPath.GroundPath);
            GameObject result = _diContainer.InstantiatePrefab(ground);
            SceneManager.MoveGameObjectToScene(result, SceneManager.GetActiveScene());
            _groundTransform = result.transform;
            return result;
        }

        public GameObject CreateGroundVariant1()
        {
            GameObject groundVariant = _assetProvider.Instantiate(AssetPath.GroundVariant1Path);
            return _diContainer.InstantiatePrefab(groundVariant, _groundTransform);
        }

        public GameObject CreateGroundVariant2()
        {
            GameObject groundVariant = _assetProvider.Instantiate(AssetPath.GroundVariant2Path);
            return _diContainer.InstantiatePrefab(groundVariant, _groundTransform);
        }

        public GameObject CreateGroundVariant3()
        {
            GameObject groundVariant = _assetProvider.Instantiate(AssetPath.GroundVariant3Path);
            return _diContainer.InstantiatePrefab(groundVariant, _groundTransform);
        }

        public GameObject CreateGroundVariant4()
        {
            GameObject groundVariant = _assetProvider.Instantiate(AssetPath.GroundVariant4Path);
            return _diContainer.InstantiatePrefab(groundVariant, _groundTransform);
        }

        public GameObject CreateGroundVariant5()
        {
            GameObject groundVariant = _assetProvider.Instantiate(AssetPath.GroundVariant5Path);
            return _diContainer.InstantiatePrefab(groundVariant, _groundTransform);
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
            SceneManager.MoveGameObjectToScene(result, SceneManager.GetActiveScene());
            return result;
        }
    }
}