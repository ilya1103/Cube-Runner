using System;
using Cysharp.Threading.Tasks;
using Project.Code.PossibleCollision.Pickups;
using Project.Code.PossibleCollision.Walls;
using UnityEngine;

namespace Project.Code.PossibleCollision
{
    public class DetectCollision : MonoBehaviour
    {
        [SerializeField] private CubeEvents _cubeEvents;
        
        private const double CollisionResetDelay = 0.5;

        private bool collided;

        public void Construct(CubeEvents cubeEvents) =>
            _cubeEvents = cubeEvents;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Pickup _))
            {
                _cubeEvents.RaiseEventAddNewCube(other.gameObject);
            }
            else if (other.gameObject.TryGetComponent(out Wall _) && !collided)  
            {
                collided = true;
                _cubeEvents.RaiseEventCollisionWall(gameObject);
                ResetCollided().Forget();
            }
        }
                    
        private async UniTaskVoid ResetCollided()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(CollisionResetDelay));
            collided = false;
        }
    }
}
