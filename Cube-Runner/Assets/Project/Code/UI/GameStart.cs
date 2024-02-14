using System;
using UnityEngine;

namespace Project.Code.UI
{
    public class GameStart : MonoBehaviour
    {
        public event Action<bool> StartGame;
        
        private bool _isFirstTouch;

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
    }
}