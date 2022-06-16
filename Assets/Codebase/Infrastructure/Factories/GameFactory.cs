using Codebase.Infrastructure.AssetManagement;

namespace Codebase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assetProvider;

    public GameFactory(IAssetProvider assetProvider) =>
      _assetProvider = assetProvider;
  }
}