using Cinemachine;
using Code.Cameras;
using Code.Ground;
using Code.Infrastructure.Services.GameFactory;
using Code.Infrastructure.Services.SceneManagement;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.Services.UIFactory;
using Code.Infrastructure.States.Api;
using Code.Infrastructure.UI;
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
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly LoadingCurtain _loadingCurtain;

        private CubeEvents _cubeEvents;
        private GameObject _gameUI;
        private GameStart _gameStart;
        private GameOver _gameOver;
        private Transform _player;

        public LoadLevelState(LoadingCurtain loadingCurtain, ISceneLoader sceneLoader, IStaticDataService staticData, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _loadingCurtain = loadingCurtain;
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

            HideLoadingCurtain();
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

        private void InitGround() =>
            _gameFactory.CreateGround().GetComponent<GenerateNewGround>().Construct(_cubeEvents, _gameOver);

        private void HideLoadingCurtain() =>
            _loadingCurtain.Hide().Forget();

        public void Exit() { }
    }
}