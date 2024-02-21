using UnityEngine;

namespace Project.Code.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path) =>
            Resources.Load<GameObject>(path);
    }
}