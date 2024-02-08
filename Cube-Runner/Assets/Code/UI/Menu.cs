using Code.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameStart _gameStart;
        [SerializeField] private GameObject _endMenu;
        
        private GameOver _gameOver;

        public void Construct(GameOver gameOver)
        {
            _gameOver = gameOver;
            _gameOver.EndGame += OnGameEnd;
        }
        
        private void OnEnable() =>
            _gameStart.StartGame += OnGameStart;

        private void OnDestroy()
        {
            _gameStart.StartGame -= OnGameStart;
            _gameOver.EndGame -= OnGameEnd;
        }

        private void Awake()
        {
            _gameStart.gameObject.SetActive(true);
            _endMenu.SetActive(false);
        }

        private void OnGameStart(bool isGameRunning)
        {
            Time.timeScale = 2.0f;
            _gameStart.gameObject.SetActive(false);
        }

        private void OnGameEnd(bool isGameRunning)
        {
            _endMenu.SetActive(true);
            Time.timeScale = 1.0f;
        }
        
        public void ReloadScene() =>
            SceneManager.LoadSceneAsync(0);
    }
}