using Code.StaticData.Attribute;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [field: ReadOnly] public Vector3 PlayerInitialPosition;
        [field: ReadOnly] public Quaternion PlayerInitialRotation;
        [field: ReadOnly] public Vector3 CameraInitialPosition;
        [field: ReadOnly] public Quaternion CameraInitialRotation;
        [field: ReadOnly] public int PoolSize;
        [field: ReadOnly] public int InitialGroundCount;
    }
}