using Codebase.Infrastructure.AssetManagement;
using Codebase.Player;
using Codebase.Services.Input;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assetProvider;
    private readonly IInputService _inputService;

    public GameFactory(IAssetProvider assetProvider, IInputService inputService)
    {
      _assetProvider = assetProvider;
      _inputService = inputService;
    }

    public GameObject CreatePlayer(Vector3 at)
    {
      GameObject player = _assetProvider.Instantiate(AssetPath.Player, at);
      
      player.GetComponent<Movement>().Construct(_inputService);
      
      return player;
    }
  }
}