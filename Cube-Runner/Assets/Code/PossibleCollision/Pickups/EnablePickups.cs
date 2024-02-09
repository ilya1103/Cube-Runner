using UnityEngine;

namespace Code.PossibleCollision.Pickups
{
    public class EnablePickups : MonoBehaviour
    {
        [SerializeField] private GameObject[] _pickups;

        private void OnEnable()
        {
            foreach (GameObject pickup in _pickups)
            {
                pickup.SetActive(true);
            }
        }
    }
}
