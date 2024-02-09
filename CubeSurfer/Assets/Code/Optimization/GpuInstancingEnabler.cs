using UnityEngine;

namespace Code.Optimization
{
    public class GpuInstancingEnabler : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private void Awake()
        {
            MaterialPropertyBlock materialPropertyBlock = new();
            _meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}