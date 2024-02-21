using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Player;
using Project.Code.PossibleCollision;
using UnityEngine;
using Zenject;

namespace Project.Code.Ground
{
    public class Grounds : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _activeGrounds = new();
        [SerializeField] private GroundPool groundPool;
        [SerializeField] private float _timeToTargetPosition = 1;

        private IStaticDataService _staticDataService;
        private CubeEvents _cubeEvents;
        private GameOver _gameOver;
        private PlayerMovement _playerMovement;

        private const double EnableGroundDelay = 0.5;

        private Vector3 _groundTargetPosition;

        private float _enableGroundOffsetX;
        private float _enableGroundOffsetY;
        private float _enableGroundOffsetZ;
        
        private float _activeGroundIntervalZ;

        private bool _isReadyToEnable;
        private bool _isGameOver;

        [Inject]
        public void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        public void Construct(CubeEvents cubeEvents, GameOver gameOver, PlayerMovement playerMovement)
        {
            _cubeEvents = cubeEvents;
            _cubeEvents.CollisionWall += EnableGround;
            _gameOver = gameOver;
            _gameOver.EndGame += OnGameEnd;
            
            _playerMovement = playerMovement;
            _enableGroundOffsetX = playerMovement.transform.position.y + 15;
            _enableGroundOffsetY = playerMovement.transform.position.y - 20;
            _enableGroundOffsetZ = playerMovement.transform.position.z - 8;
        }

        private void OnDestroy()
        {
            _cubeEvents.CollisionWall -= EnableGround;
            _gameOver.EndGame -= OnGameEnd;
        }

        private void Start()
        {
            _isReadyToEnable = true;
            _activeGroundIntervalZ = _activeGrounds[^1].transform.localScale.z;
            _groundTargetPosition = _activeGrounds[^1].transform.position;
            PlaceInitialGround(_staticDataService.LevelStaticData.InitialGroundCount);
        }

        private void PlaceInitialGround(int initialGroundCount)
        {
            for (int i = 0; i < initialGroundCount; i++)
            {
                GetObjectFromPoolAndSetToTargetPosition(false);
                _groundTargetPosition += new Vector3(0, 0, _activeGroundIntervalZ); 
            }
        }

        private async void EnableGround(GameObject gameObj) 
        {
            if (!_isReadyToEnable) return;

            await EnableGroundCooldown();

            if (_isGameOver) return;

            _activeGrounds[0].SetActive(false);
            _activeGrounds.RemoveAt(0);
            
            GetObjectFromPoolAndSetToTargetPosition(true);
            
            _groundTargetPosition += new Vector3(0, 0, _activeGroundIntervalZ);
        }

        private void GetObjectFromPoolAndSetToTargetPosition(bool shouldMoveGround)
        {
            GameObject pooledProjectile = groundPool.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true);
                _activeGrounds.Add(pooledProjectile);
                if (shouldMoveGround)
                {
                    pooledProjectile.transform.position = _playerMovement.transform.position + new Vector3(_enableGroundOffsetX, _enableGroundOffsetY, _enableGroundOffsetZ);
                    pooledProjectile.transform.DOMove(_groundTargetPosition, _timeToTargetPosition).SetEase(Ease.Linear);
                }
                else
                {
                    pooledProjectile.transform.position = _groundTargetPosition;
                }
            }
            else
            {
                Debug.LogWarning("No inactive objects in the pool");
            }
        }

        private async UniTask EnableGroundCooldown()
        {
            _isReadyToEnable = false;
            await UniTask.Delay(TimeSpan.FromSeconds(EnableGroundDelay));
            _isReadyToEnable = true;
        }

        private void OnGameEnd(bool isGameOver) =>
            _isGameOver = true;
    }
}