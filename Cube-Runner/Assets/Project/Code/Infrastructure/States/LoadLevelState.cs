using Cinemachine;
using Project.Code.Cameras;
using Project.Code.Ground;
using Project.Code.Infrastructure.Services.GameFactory;
using Project.Code.Infrastructure.Services.LoadingCurtainService;
using Project.Code.Infrastructure.Services.SceneManagement;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Infrastructure.Services.UIFactory;
using Project.Code.Infrastructure.States.Interfaces;
using Project.Code.Player;
using Project.Code.PossibleCollision;
using Project.Code.StaticData;
using Project.Code.UI;
using UnityEngine;

namespace Project.Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly ILoadingCurtainService _loadingCurtainService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        private CubeEvents _cubeEvents;
        private GameObject _gameUI;
        private GameStart _gameStart;
        private GameOver _gameOver;
        private PlayerMovement _playerMovement;
        private Transform _player;

        public LoadLevelState(ILoadingCurtainService loadingCurtainService, ISceneLoader sceneLoader, IStaticDataService staticData, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _loadingCurtainService = loadingCurtainService;
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
            player.GetComponentInChildren<PlayerBody>().Construct(_gameStart);
            _playerMovement = player.GetComponent<PlayerMovement>();
            _playerMovement.Construct(_gameStart);
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
            _gameFactory.CreateGround().GetComponent<Grounds>().Construct(_cubeEvents, _gameOver, _playerMovement);

        private void HideLoadingCurtain() =>
            _loadingCurtainService.Hide();

        public void Exit() { }
    }
}