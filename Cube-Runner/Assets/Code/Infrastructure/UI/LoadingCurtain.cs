using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup Curtain;
        
        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
        }

        public async UniTask Hide()
        {
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.12f;
                await UniTask.Delay(TimeSpan.Zero);
            }
            gameObject.SetActive(false);
        }
    }
}