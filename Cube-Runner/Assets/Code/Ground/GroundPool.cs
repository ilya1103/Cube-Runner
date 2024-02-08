using System;
using System.Linq;
using Code.Infrastructure.Services.GameFactory;
using Code.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Ground
{
    public class GroundPool : MonoBehaviour
    {
        private GameObject[] _pooledObjectsVariant1;
        private GameObject[] _pooledObjectsVariant2;
        private GameObject[] _pooledObjectsVariant3;
        private GameObject[] _pooledObjectsVariant4;
        private GameObject[] _pooledObjectsVariant5;

        private IGameFactory _gameFactory;
        private IStaticDataService _staticDataService;

        private const int _numberOfVariants = 5;
        private int _poolSize;

        [Inject]
        public void Construct(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }

        public GameObject GetPooledObject() =>
            Random.Range(0, _numberOfVariants) switch
            {
                0 => _pooledObjectsVariant1.FirstOrDefault(t => !t.activeInHierarchy),
                1 => _pooledObjectsVariant2.FirstOrDefault(t => !t.activeInHierarchy),
                2 => _pooledObjectsVariant3.FirstOrDefault(t => !t.activeInHierarchy),
                3 => _pooledObjectsVariant4.FirstOrDefault(t => !t.activeInHierarchy),
                4 => _pooledObjectsVariant5.FirstOrDefault(t => !t.activeInHierarchy),
                _ => null
            };

        private void Start()
        {
            _poolSize = _staticDataService.LevelStaticData.PoolSize;
            
            _pooledObjectsVariant1 = InitializeObjectPool(_poolSize, () => _gameFactory.CreateGroundVariant1());
            _pooledObjectsVariant2 = InitializeObjectPool(_poolSize, () => _gameFactory.CreateGroundVariant2());
            _pooledObjectsVariant3 = InitializeObjectPool(_poolSize, () => _gameFactory.CreateGroundVariant3());
            _pooledObjectsVariant4 = InitializeObjectPool(_poolSize, () => _gameFactory.CreateGroundVariant4());
            _pooledObjectsVariant5 = InitializeObjectPool(_poolSize, () => _gameFactory.CreateGroundVariant5());
        }

        private GameObject[] InitializeObjectPool(int poolSize, Func<GameObject> createObject)
        {
            GameObject[] pooledObjects = new GameObject[poolSize];

            for (int i = 0; i < pooledObjects.Length; i++)
            {
                GameObject obj = createObject();
                obj.SetActive(false);
                pooledObjects[i] = obj;
            }

            return pooledObjects;
        }
    }
}