using Code.StaticData.Attribute;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "Scenes Data", menuName = "Static Data/Scenes Data")]
    public class ScenesStaticData : ScriptableObject
    {
        [field: ReadOnly] public string BootstrapScene;
        [field: ReadOnly] public string GameScene;
    }
}