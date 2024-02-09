using System;
using Code.PossibleCollision.Walls;
using UnityEngine;

namespace Code.Player
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