using System;
using UnityEngine;

namespace Project.Code.PossibleCollision
{
    public class CubeEvents : MonoBehaviour
    {
        public event Action<GameObject> AddNewCube;
        public event Action<GameObject> CollisionWall;

        public void RaiseEventAddNewCube(GameObject cubeObject) =>
            AddNewCube?.Invoke(cubeObject);

        public void RaiseEventCollisionWall(GameObject cubeObject) =>
            CollisionWall?.Invoke(cubeObject);
    }
}