using UnityEngine;

namespace Project.Code.Ground
{
    public class DestroyGroundAfterDisable : MonoBehaviour
    {
        private void OnDisable() =>
            Destroy(gameObject);
    }
}
