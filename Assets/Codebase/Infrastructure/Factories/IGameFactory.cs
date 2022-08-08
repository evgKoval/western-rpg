using Codebase.Services;
using Codebase.StaticData;
using System.Threading.Tasks;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public interface IGameFactory : IService
  {
    Task WarmUp();
    void CleanUp();
    Task<GameObject> CreatePlayer(Vector3 at);
    Task<GameObject> CreateHUD();
    Task<GameObject> CreatePlayerCamera();
    Task<GameObject> CreateWeapon(WeaponId weaponId, Transform whom);
    Task CreateSpawner(string id, Vector3 position);
    Task<GameObject> CreateEnemy(Vector3 position);
  }
}