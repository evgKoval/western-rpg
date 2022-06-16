using Codebase.Services;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public interface IGameFactory : IService
  {
    GameObject CreatePlayer(Vector3 at);
    GameObject CreateHUD();
  }
}