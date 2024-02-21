using DG.Tweening;
using UnityEngine;

namespace Project.Code.Infrastructure.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup Curtain;
        [SerializeField] private float _fadeDuration = 0.5f;

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1f;
        }

        public void Hide() => 
            Curtain.DOFade(0f, _fadeDuration)
                .OnComplete(() => gameObject.SetActive(false)).SetEase(Ease.Linear);
    }
}