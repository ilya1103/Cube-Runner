using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Code.UI
{
    public class GameStart : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _holdToMoveText;
        [SerializeField] private Image _handImage;
        [SerializeField] private float _duration = 0.7f;
        
        public event Action<bool> StartGame;

        private Sequence _sequence;
        private bool _isFirstTouch;

        private void Start()
        {
            float targetPosition = _holdToMoveText.rectTransform.rect.width / 2f;

            _sequence = DOTween.Sequence();
            _sequence.Append(_handImage.rectTransform.DOAnchorPosX(targetPosition, _duration).SetEase(Ease.Linear));
            _sequence.Append(_handImage.rectTransform.DOAnchorPosX(-targetPosition, _duration).SetEase(Ease.Linear));
            _sequence.Append(_handImage.rectTransform.DOAnchorPosX(-targetPosition + targetPosition, _duration).SetEase(Ease.Linear));
            _sequence.SetLoops(-1);
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0) && !_isFirstTouch)
#elif UNITY_ANDROID
            if (Input.touchCount > 0 && !_isFirstTouch)
#endif
            {
                StartGame?.Invoke(true);
            }
        }

        private void OnDisable() =>
            _sequence?.Kill();
    }
}