using UnityEngine;

namespace Project.Code.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private GameObject _playerRagdoll;
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private GameOver _gameOver;
        
        private void OnEnable() =>
            _gameOver.EndGame += OnGameEnd;

        private void OnDestroy() =>
            _gameOver.EndGame -= OnGameEnd;

        private void OnGameEnd(bool isGameRunning)
        {
            _trailRenderer.time = Mathf.Infinity;
            RadgollAnimation();
        }
        
        private void RadgollAnimation()
        {
            _gameOver.GetComponent<Rigidbody>().mass = 1.0f;
            _gameOver.GetComponent<Animator>().enabled = false;
            _playerRagdoll.SetActive(true);
        }
    }
}