using System.Linq;
using Project.Code.StaticData;
using UnityEditor;

namespace Project.Code.Editor
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