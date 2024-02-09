using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        private void Awake()
        {
            GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper == null)
                SceneManager.LoadScene(0);
        }
    }
}