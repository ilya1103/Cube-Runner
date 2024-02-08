using Cinemachine;
using Code.Cameras;
using Code.Ground;
using Code.Infrastructure.Services.GameFactory;
using Code.Infrastructure.Services.SceneManagement;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.Services.UIFactory;
using Code.Infrastructure.States.Api;
using Code.Player;
using Code.PossibleCollision;
using Code.StaticData;
using Code.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const float StartGameplayDelay = 0.3f;

        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        private CubeEvents _cubeEvents;
        private GameObject _gameUI;
        private GameStart _gameStart;
        private GameOver _gameOver;
        private Transform _player;

        public LoadLevelState(ISceneLoader sceneLoader, IStaticDataService staticData, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter(string payload) =>
            _sceneLoader.LoadSceneAsync(payload, OnLevelLoaded);

        private void OnLevelLoaded()
        {
            LevelStaticData levelData = _staticData.LevelStaticData;
            
            InitUI();
            InitPlayer(levelData);
            InitCamera(levelData);
            InitGround();

            StartGameplay().Forget();
        }

        private void InitUI()
        {
            _uiFactory.CreateUIRoot();
            _gameUI = _uiFactory.CreateGameUI();
            _gameStart = _gameUI.GetComponentInChildren<GameStart>();
        }

        private void InitPlayer(LevelStaticData levelData)
        {
            GameObject player = _gameFactory.CreatePlayer(levelData.PlayerInitialPosition, levelData.PlayerInitialRotation);
            player.GetComponent<PlayerMovement>().Construct(_gameStart);
            player.GetComponentInChildren<CubeHolder>().Construct(_gameStart);
            _cubeEvents = player.GetComponentInChildren<CubeEvents>();
            _gameOver = player.GetComponentInChildren<GameOver>();
            _player = player.transform;
            
            _gameUI.GetComponent<Menu>().Construct(_gameOver);
        }

        private void InitCamera(LevelStaticData levelData)
        {
            GameObject cinemachine = _gameFactory.CreateCinemachine(levelData.CameraInitialPosition, levelData.CameraInitialRotation);
            cinemachine.GetComponent<CameraShake>().Construct(_cubeEvents);
            cinemachine.GetComponent<CinemachineVirtualCamera>().m_Follow = _player;
        }

        private void InitGround()
        {
            GroundPooler groundPooler = _gameFactory.CreateGroundPooler().GetComponent<GroundPooler>();
            GenerateNewGround groundGenerator = _gameFactory.CreateGroundGenerator().GetComponent<GenerateNewGround>();
            groundGenerator.Construct(groundPooler, _cubeEvents, _gameOver);
        }

        private async UniTaskVoid StartGameplay()
        {
            //_loadingCurtainService.Hide();
            await UniTask.WaitForSeconds(StartGameplayDelay);
        }

        public void Exit() { }
    }
}