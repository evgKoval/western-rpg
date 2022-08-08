using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Codebase.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

    public void Initialize() =>
      Addressables.InitializeAsync();

    public void CleanUp()
    {
      foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
        foreach (AsyncOperationHandle handle in resourceHandles)
          Addressables.Release(handle);

      _handles.Clear();
    }

    public async Task<T> Load<T>(AssetReference assetReference) where T : class
    {
      if (_handles.TryGetValue(assetReference.AssetGUID, out List<AsyncOperationHandle> handles))
      {
        AsyncOperationHandle completedHandle = handles.FirstOrDefault(handle => handle.IsDone);

        if (IsCompletedHandleNotDefault(completedHandle))
          return completedHandle.Result as T;
      }

      return await RunWithCacheOnCompleted(
        Addressables.LoadAssetAsync<T>(assetReference),
        cacheKey: assetReference.AssetGUID);
    }

    public async Task<T> Load<T>(string address) where T : class
    {
      if (_handles.TryGetValue(address, out List<AsyncOperationHandle> handles))
      {
        AsyncOperationHandle completedHandle = handles.FirstOrDefault(handle => handle.IsDone);

        if (IsCompletedHandleNotDefault(completedHandle))
          return completedHandle.Result as T;
      }

      return await RunWithCacheOnCompleted(
        Addressables.LoadAssetAsync<T>(address),
        cacheKey: address);
    }

    public async Task<GameObject> Instantiate(string address, Vector3 at)
    {
      GameObject prefab = await Load<GameObject>(address);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }

    public async Task<GameObject> Instantiate(string address, Transform parent)
    {
      GameObject prefab = await Load<GameObject>(address);
      return Object.Instantiate(prefab, parent);
    }

    public async Task<GameObject> Instantiate(string address)
    {
      GameObject prefab = await Load<GameObject>(address);
      return Object.Instantiate(prefab);
    }

    private async Task<T> RunWithCacheOnCompleted<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
    {
      AddHandle(cacheKey, handle);

      return await handle.Task;
    }

    private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
    {
      if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
      {
        resourceHandles = new List<AsyncOperationHandle>();
        _handles[key] = resourceHandles;
      }

      resourceHandles.Add(handle);
    }

    private static bool IsCompletedHandleNotDefault(AsyncOperationHandle completedHandle) =>
      !completedHandle.Equals(default);

  }
}