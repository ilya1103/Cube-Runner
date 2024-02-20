using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Project.Code.PossibleCollision;
using UnityEngine;

namespace Project.Code.Cameras
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float _intensity = 4.0f;
        [SerializeField] private float _shakeTime = 0.2f;

        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        private CinemachineBasicMultiChannelPerlin _noise;
        private CubeEvents _cubeEvents;

        public void Construct(CubeEvents cubeEvents)
        {
            _cubeEvents = cubeEvents;
            _cubeEvents.CollisionWall += CollisionWall;
        }

        private void OnDestroy() =>
            _cubeEvents.CollisionWall -= CollisionWall;

        private void Awake() => 
            _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        private void CollisionWall(GameObject gameObj) =>
            ShakeCamera().Forget();

        private async UniTaskVoid ShakeCamera()
        {
            _noise.m_AmplitudeGain = _intensity;
            Handheld.Vibrate();
            await UniTask.Delay(TimeSpan.FromSeconds(_shakeTime));
            _noise.m_AmplitudeGain = 0;
        }
    }
}
