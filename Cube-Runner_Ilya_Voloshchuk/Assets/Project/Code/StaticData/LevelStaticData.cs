using UnityEngine;

namespace Project.Code.StaticData
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public Vector3 PlayerInitialPosition;
        public Quaternion PlayerInitialRotation;
        public Vector3 CameraInitialPosition;
        public Quaternion CameraInitialRotation;
        public int PoolSize;
        public int InitialGroundCount;
    }
}