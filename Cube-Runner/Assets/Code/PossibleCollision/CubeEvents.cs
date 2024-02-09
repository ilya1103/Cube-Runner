using System;
using UnityEngine;

namespace Code.PossibleCollision
{
    public class CubeEvents : MonoBehaviour
    {
        public event Action<GameObject> onAddNewCube;
        public event Action<GameObject> onCollisionWall;

        public void AddNewCube(GameObject otherGameObject) =>
            onAddNewCube?.Invoke(otherGameObject);

        public void CollisionWall(GameObject cubeObject) =>
            onCollisionWall?.Invoke(cubeObject);
    }
}