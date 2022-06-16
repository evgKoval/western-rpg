using Codebase.Infrastructure.AssetManagement;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assetProvider;

    public GameFactory(IAssetProvider assetProvider) =>
      _assetProvider = assetProvider;

    public GameObject CreatePlayer(Vector3 at) =>
      _assetProvider.Instantiate(AssetPath.Player, at);
  }
}