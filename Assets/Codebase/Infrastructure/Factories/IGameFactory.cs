using System.Collections.Generic;
using Codebase.Services;
using Codebase.Services.Progress;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public interface IGameFactory : IService
  {
    List<ISaveable> ProgressSaveables { get; }
    List<ILoadable> ProgressLoadables { get; }
    void WarmUp();
    void CleanUp();
    GameObject CreatePlayer(Vector3 at);
    GameObject CreateHUD();
    GameObject CreatePlayerCamera();
    GameObject CreateWeapon(WeaponId weaponId, Transform whom);
    GameObject CreateEnemy();
  }
}