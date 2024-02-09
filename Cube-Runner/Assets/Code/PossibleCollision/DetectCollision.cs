using System;
using Code.PossibleCollision.Pickups;
using Code.PossibleCollision.Walls;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.PossibleCollision
{
    public class DetectCollision : MonoBehaviour
    {
        [SerializeField] private CubeEvents _cubeEvents;
        
        private const double DelayDuration = 0.5;

        private bool collided;

        public void Construct(CubeEvents cubeEvents) =>
            _cubeEvents = cubeEvents;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Pickup _))
            {
                _cubeEvents.AddNewCube(other.gameObject);
            }
            else if (other.gameObject.TryGetComponent(out Wall _) && !collided)  
            {
                collided = true;
                _cubeEvents.CollisionWall(gameObject);
                ResetCollided().Forget();
            }
        }
                    
        private async UniTaskVoid ResetCollided()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(DelayDuration));
            collided = false;
        }
    }
}
