using Code.UI;
using UnityEngine;

namespace Code.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private GameOver _gameOver;

        [SerializeField] private float _sensitivity;
        [SerializeField] private float _speed;

        private GameStart _gameStart;

        private const float _leftGroundBorder = -2.0f;
        private const float _rightGroundBorder = 2.0f;

        private const string AxisName = "Mouse X";

        private bool _isGameRunning;
        
        public void Construct(GameStart gameStart)
        {
            _gameStart = gameStart;
            _gameStart.StartGame += OnGameStart;
        }

        private void OnEnable() => 
            _gameOver.EndGame += OnGameEnd;

        private void OnDestroy()
        {
            _gameStart.StartGame -= OnGameStart;
            _gameOver.EndGame -= OnGameEnd;
        }

        private void Update()
        {
            if (!_isGameRunning) return;

            transform.Translate(transform.forward * (_speed * Time.deltaTime));

#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x + (Input.GetAxis(AxisName) * _sensitivity * Time.deltaTime),
                        _leftGroundBorder, _rightGroundBorder), transform.position.y, transform.position.z);
            }

#elif UNITY_ANDROID 
            if (Input.touchCount <= 0) return;
            
            Touch touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Moved) return;
                
            float touchDeltaX = touch.deltaPosition.x;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x + (touchDeltaX * _sensitivity * Time.deltaTime),
                    _leftGroundBorder, _rightGroundBorder), transform.position.y, transform.position.z);
#endif
        }

        private void OnGameStart(bool isGameRunning) =>
            _isGameRunning = isGameRunning;

        private void OnGameEnd(bool isGameRunning) =>
            _isGameRunning = isGameRunning;
    }
}