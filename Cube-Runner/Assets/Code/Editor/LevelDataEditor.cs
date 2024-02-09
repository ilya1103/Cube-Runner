using Code.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelDataEditor : UnityEditor.Editor
    {
        private const string PlayerInitialPoint = "PlayerInitialPoint";
        private const string CameraInitialPoint = "CameraInitialPoint";

        private void OnEnable()
        {
            LevelStaticData levelStaticData = (LevelStaticData)target;

            GameObject playerInitialPoint = GameObject.FindWithTag(PlayerInitialPoint);

            levelStaticData.PlayerInitialPosition = playerInitialPoint.transform.position;
            levelStaticData.PlayerInitialRotation = playerInitialPoint.transform.rotation;
            
            GameObject cameraInitialPoint = GameObject.FindWithTag(CameraInitialPoint);

            levelStaticData.CameraInitialPosition = cameraInitialPoint.transform.position;
            levelStaticData.CameraInitialRotation = cameraInitialPoint.transform.rotation;
            
            levelStaticData.PoolSize = 6;
            levelStaticData.InitialGroundCount = 3;
            
            EditorUtility.SetDirty(target);
        }
    }
}