using UnityEngine;

namespace Project.Code.StaticData
{
    [CreateAssetMenu(fileName = "Scenes Data", menuName = "Static Data/Scenes Data")]
    public class ScenesStaticData : ScriptableObject
    {
        public string BootstrapScene;
        public string GameScene;
    }
}