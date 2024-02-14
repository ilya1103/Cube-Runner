using System;
using UnityEngine;

namespace Project.Code.PossibleCollision
{
    public class CubeEvents : MonoBehaviour
    {
        public event Action<GameObject> onAddNewCube;
        public event Action<GameObject> onCollisionWall;

        public void RaiseEventAddNewCube(GameObject otherGameObject) =>
            onAddNewCube?.Invoke(otherGameObject);

        public void RaiseEventCollisionWall(GameObject cubeObject) =>
            onCollisionWall?.Invoke(cubeObject);
    }
}