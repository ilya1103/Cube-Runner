using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Code.Infrastructure.Services.GameFactory;
using Project.Code.PossibleCollision;
using Project.Code.UI;
using UnityEngine;
using Zenject;

namespace Project.Code.Player
{
    public class PlayerBody : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _cubeList = new();
        [SerializeField] private GameOver _gameOver;
        [SerializeField] private Transform _cubeHolderTransform;
        [SerializeField] private ParticleSystem _cubeEffect;
        [SerializeField] private Animator _animator;
        [SerializeField] private Animation _textAnimation;

        private IGameFactory _gameFactory;
        private CubeEvents _cubeEvents;
        private GameStart _gameStart;

        private readonly int Jump = Animator.StringToHash("Jump");

        private const double DelayBeforeDestroy = 3;
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
            _cubeEvents.AddNewCube += AddNewCube;
            _cubeEvents.CollisionWall += RemoveCube;
            _gameOver.EndGame += OnGameEnd;
        }

        private void OnDestroy()
        {
            _gameStart.StartGame -= OnGameStart;
            _cubeEvents.AddNewCube -= AddNewCube;
            _cubeEvents.CollisionWall -= RemoveCube;
            _gameOver.EndGame -= OnGameEnd;
        }

        private void AddNewCube(GameObject cubeGameObject)
        {
            cubeGameObject.SetActive(false);
            
            transform.position += Vector3.up;
            GameObject cubeInstance = _gameFactory.CreateCube(_cubeHolderTransform); 
            cubeInstance.GetComponent<DetectCollision>().Construct(_cubeEvents);
            cubeInstance.transform.position = new Vector3(_cubeList[0].transform.position.x, _cubeList[^1].transform.position.y - 1, _cubeList[0].transform.position.z);

            _textAnimation.Play();
            _cubeEffect.Play();
            _animator.SetTrigger(Jump);
            _cubeList.Add(cubeInstance);
        }

        private void RemoveCube(GameObject cube) => 
            RemoveCubeWithDelay(cube).Forget();

        private async UniTaskVoid RemoveCubeWithDelay(GameObject cubeObject)  
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