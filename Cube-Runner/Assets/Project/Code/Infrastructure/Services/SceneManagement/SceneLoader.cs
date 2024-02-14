using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Project.Code.Infrastructure.Services.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTaskVoid LoadSceneAsync(string sceneName, Action onLoaded = null)
        {
            bool alreadyOnScene = SceneManager.GetActiveScene().name == sceneName;
            if (alreadyOnScene)
            {
                onLoaded?.Invoke();
                return;
            }

            await SceneManager.LoadSceneAsync(sceneName);
            onLoaded?.Invoke();
        }
    }
}