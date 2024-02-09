using System;
using System.Collections.Generic;
using Code.Infrastructure.Services.StaticData;
using Code.Player;
using Code.PossibleCollision;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Ground
{
    public class GenerateNewGround : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _activeGrounds = new();
        [SerializeField] private GroundPool groundPool;

        private IStaticDataService _staticDataService;
        private CubeEvents _cubeEvents;
        private GameOver _gameOver;

        private const double RespawnDelay = 0.5;
        
        private Vector3 _targetPosition;
        private float _offsetZ;

        private bool _isReadyToSpawn = true;
        private bool _isGameOver;

        [Inject]
        public void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        public void Construct(CubeEvents cubeEvents, GameOver gameOver)
        {
            _cubeEvents = cubeEvents;
            _cubeEvents.onCollisionWall += RespawnGround;
            _gameOver = gameOver;
            _gameOver.EndGame += OnGameEnd;
        }

        private void OnDestroy()
        {
            _cubeEvents.onCollisionWall -= RespawnGround;
            _gameOver.EndGame -= OnGameEnd;
        }

        private void Start()
        {
            _targetPosition = _activeGrounds[^1].transform.position;
            _offsetZ = _activeGrounds[^1].transform.localScale.z;
            Setup(_staticDataService.LevelStaticData.InitialGroundCount);
        }

        private void Setup(int initialGroundCount)
        {
            for (int i = 0; i < initialGroundCount; i++)
            {
                GetObjectFromPool();
                if (i < initialGroundCount - 1)
                    _targetPosition = new Vector3(_targetPosition.x, _targetPosition.y, _targetPosition.z + _offsetZ);
            }
        }

        private async void RespawnGround(GameObject gameObj) //Добавить анимацию
        {
            if (!_isReadyToSpawn) return;

            await RespawnCooldown();

            if (_isGameOver) return;

            _activeGrounds[0].SetActive(false);
            _activeGrounds.RemoveAt(0);

            _targetPosition = new Vector3(_targetPosition.x, _targetPosition.y, _targetPosition.z + _offsetZ);
            
            GetObjectFromPool();
            //StartCoroutine(MoveToTargetСoroutine());
        }

        private void GetObjectFromPool()
        {
            GameObject pooledProjectile = groundPool.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true);
                pooledProjectile.transform.position = _targetPosition;
            }
            else
            {
                Debug.LogError("No inactive objects in the pool");
            }
            _activeGrounds.Add(pooledProjectile);
        }

        /*private IEnumerator MoveToTargetСoroutine()
        {
            while (track.transform.position != targetPosition)
            {
                track.transform.position = Vector3.MoveTowards(track.transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
        }*/
        
        private async UniTask RespawnCooldown() 
        {
            _isReadyToSpawn = false;
            await UniTask.Delay(TimeSpan.FromSeconds(RespawnDelay));
            _isReadyToSpawn = true;
        }

        private void OnGameEnd(bool isGameOver) =>
            _isGameOver = true;
    }
}