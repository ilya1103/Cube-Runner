using System;
using Cinemachine;
using Code.PossibleCollision;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Cameras
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float _intensity = 6.0f;
        [SerializeField] private float _time = 0.2f;

        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        private CinemachineBasicMultiChannelPerlin _noise;
        private CubeEvents _cubeEvents;

        public void Construct(CubeEvents cubeEvents)
        {
            _cubeEvents = cubeEvents;
            _cubeEvents.onAddNewCube += OnAddNewCube;
            _cubeEvents.onCollisionWall += OnCollisionWall;
        }

        private void OnDestroy()
        {
            _cubeEvents.onAddNewCube -= OnAddNewCube;
            _cubeEvents.onCollisionWall -= OnCollisionWall;
        }

        private void Awake() => 
            _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        private void OnAddNewCube(GameObject gameObj) =>
            ShakeCamera().Forget();

        private void OnCollisionWall(GameObject gameObj) =>
            ShakeCamera().Forget();

        private async UniTaskVoid ShakeCamera()
        {
            _noise.m_AmplitudeGain = _intensity;
            Handheld.Vibrate();
            await UniTask.Delay(TimeSpan.FromSeconds(_time));
            _noise.m_AmplitudeGain = 0;
        }
    }
}
