using System;
using Project.Code.PossibleCollision.Walls;
using UnityEngine;

namespace Project.Code.Player
{
    public class GameOver : MonoBehaviour
    {
        public event Action<bool> EndGame;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Wall _))
            {
                EndGame?.Invoke(false);
            }
        }
    }
}