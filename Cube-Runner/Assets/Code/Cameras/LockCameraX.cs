using Cinemachine;
using UnityEngine;

namespace Code.Cameras
{
    public class LockCameraX : CinemachineExtension
    {
        [Tooltip("Lock the camera's X position to this value")]
        [SerializeField] private float _xPosition;

        protected override void Awake()
        {
            base.Awake();
            _xPosition = transform.position.x;
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != CinemachineCore.Stage.Body) return;
            
            Vector3 pos = state.RawPosition;
            pos.x = _xPosition;
            state.RawPosition = pos;
        }
    }
}