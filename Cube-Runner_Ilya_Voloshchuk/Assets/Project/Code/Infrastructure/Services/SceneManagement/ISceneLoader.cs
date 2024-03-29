using System;
using Cysharp.Threading.Tasks;

namespace Project.Code.Infrastructure.Services.SceneManagement
{
    public interface ISceneLoader
    {
        UniTaskVoid LoadSceneAsync(string sceneName, Action onLoaded = null);
    }
}