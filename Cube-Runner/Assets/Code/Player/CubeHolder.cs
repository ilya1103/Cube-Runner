using System;
using System.Collections.Generic;
using Code.Infrastructure.Services.GameFactory;
using Code.PossibleCollision;
using Code.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class CubeHolder : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _cubeList = new();
        [SerializeField] private GameOver _gameOver;
        [SerializeField] private Transform _cubeHolderTransform;
        [SerializeField] private ParticleSystem _cubeEffect;
        [SerializeField] private Animation _animation;
        [SerializeField] private Animator _animator;

        private IGameFactory _gameFactory;
        private CubeEvents _cubeEvents;
        private GameStart _gameStart;

        private static readonly int Jump = Animator.StringToHash("Jump");

        private const double DelayBeforeDestroy = 0.5;
        private const double DelayToCheckIfGameStillRunning = 1;
        
        private bool _isGameRunning;

        [Inject]
        public void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public void Construct(GameStart gameStart)
        {
            _gameStart = gameStart;
            _gameStart.StartGame += OnGameStart;
        }

        private void Awake() =>
            _cubeEvents = GetComponentInChildren<CubeEvents>();

        private void OnEnable()
        {
            _cubeEvents.onAddNewCube += OnAddNewCube;
            _cubeEvents.onCollisionWall += RemoveCube;
            _gameOver.EndGame += OnGameEnd;
        }

        private void OnDestroy()
        {
            _gameStart.StartGame -= OnGameStart;
            _cubeEvents.onAddNewCube -= OnAddNewCube;
            _cubeEvents.onCollisionWall -= RemoveCube;
            _gameOver.EndGame -= OnGameEnd;
        }

        private void OnAddNewCube(GameObject cubeGameObject)
        {
            cubeGameObject.SetActive(false);
            
            transform.position += Vector3.up;
            GameObject cubeInstance = _gameFactory.CreateCube(_cubeHolderTransform); 
            cubeInstance.GetComponent<DetectCollision>().Construct(_cubeEvents);
            cubeInstance.transform.position = new Vector3(_cubeList[0].transform.position.x, _cubeList[^1].transform.position.y - 1, _cubeList[0].transform.position.z);
            
            _animation.Play();
            _cubeEffect.Play();
            _animator.SetTrigger(Jump);
            _cubeList.Add(cubeInstance);
        }

        private void RemoveCube(GameObject cube) => 
            RemoveCubeWithDelay(cube).Forget();

        private async UniTaskVoid RemoveCubeWithDelay(GameObject cubeObject)  //Чтобы при конце игры не удалялись кубы под игроком
        {
            cubeObject.transform.SetParent(null);
            await UniTask.Delay(TimeSpan.FromSeconds(DelayToCheckIfGameStillRunning));

            if (!_isGameRunning) return;

            _cubeList.Remove(cubeObject);
            
            await UniTask.Delay(TimeSpan.FromSeconds(DelayBeforeDestroy));
            Destroy(cubeObject);
        }

        private void OnGameStart(bool isGameRunning) =>
            _isGameRunning = isGameRunning;

        private void OnGameEnd(bool isGameRunning) =>
            _isGameRunning = isGameRunning;
    }
}