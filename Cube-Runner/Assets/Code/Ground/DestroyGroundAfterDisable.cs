using UnityEngine;

namespace Code.Ground
{
    public class DestroyGroundAfterDisable : MonoBehaviour
    {
        private void OnDisable() =>
            Destroy(gameObject);
    }
}
