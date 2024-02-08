using System.Linq;
using Code.StaticData;
using UnityEditor;

namespace Code.Editor
{
    [CustomEditor(typeof(ScenesStaticData))]
    public class ScenesDataEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            ScenesStaticData scenesStaticData = (ScenesStaticData)target;

            string[] scenes = EditorBuildSettings.scenes
                .Select(scene => System.IO.Path.GetFileNameWithoutExtension(scene.path))
                .ToArray();

            scenesStaticData.BootstrapScene = scenes[0];
            scenesStaticData.GameScene = scenes[1];
            
            EditorUtility.SetDirty(target);
        }
    }
}