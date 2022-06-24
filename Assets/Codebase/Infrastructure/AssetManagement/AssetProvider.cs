using UnityEngine;

namespace Codebase.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject Instantiate(string path, Vector3 at)
    {
      GameObject prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }

    public GameObject Instantiate(string path, Transform parent)
    {
      GameObject prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, parent);
    }

    public GameObject Instantiate(string path)
    {
      GameObject prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }
  }
}