using UnityEngine;

namespace Project.Code.Infrastructure.Services.GameFactory
{
    public interface IGameFactory
    {
        GameObject CreateGround();
        GameObject CreateGroundVariant1();
        GameObject CreateGroundVariant2();
        GameObject CreateGroundVariant3();
        GameObject CreateGroundVariant4();
        GameObject CreateGroundVariant5();
        GameObject CreateCinemachine(Vector3 at, Quaternion rotation);
        GameObject CreatePlayer(Vector3 at, Quaternion rotation);
        GameObject CreateCube(Transform cubeHolderTransform);
    }
}