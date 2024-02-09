using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path) =>
            Resources.Load<GameObject>(path);
    }
}