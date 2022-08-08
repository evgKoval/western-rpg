using Codebase.Services;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Infrastructure.AssetManagement
{
  public interface IAssetProvider : IService
  {
    void Initialize();
    void CleanUp();
    Task<T> Load<T>(AssetReference assetReference) where T : class;
    Task<T> Load<T>(string address) where T : class;
    Task<GameObject> Instantiate(string address, Vector3 at);
    Task<GameObject> Instantiate(string address, Transform parent);
    Task<GameObject> Instantiate(string address);
  }
}