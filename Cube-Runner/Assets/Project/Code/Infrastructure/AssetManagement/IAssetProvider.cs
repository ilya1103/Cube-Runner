using UnityEngine;

namespace Project.Code.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path);
    }
}