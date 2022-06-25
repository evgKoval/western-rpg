using Codebase.Services;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public interface IGameFactory : IService
  {
    void WarmUp();
    void CleanUp();
    GameObject CreatePlayer(Vector3 at);
    GameObject CreateHUD();
    GameObject CreatePlayerCamera();
    GameObject CreateWeapon(WeaponId weaponId, Transform whom);
    void CreateSpawner(string id, Vector3 position);
    GameObject CreateEnemy(Vector3 position);
  }
}