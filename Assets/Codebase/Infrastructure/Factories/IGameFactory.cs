using Codebase.Services;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public interface IGameFactory : IService
  {
    void WarmUp();
    GameObject CreatePlayer(Vector3 at);
    GameObject CreateHUD();
    GameObject CreatePlayerCamera();
    GameObject CreateWeapon();
    GameObject CreateEnemy();
  }
}