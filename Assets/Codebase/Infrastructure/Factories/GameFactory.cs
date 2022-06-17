using Cinemachine;
using Codebase.Infrastructure.AssetManagement;
using Codebase.Player;
using Codebase.Services.Input;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    private const string LookAt = "Look_At";

    private readonly IAssetProvider _assetProvider;
    private readonly IInputService _inputService;

    private GameObject _playerGameObject;

    public GameFactory(IAssetProvider assetProvider, IInputService inputService)
    {
      _assetProvider = assetProvider;
      _inputService = inputService;
    }

    public GameObject CreatePlayer(Vector3 at)
    {
      _playerGameObject = _assetProvider.Instantiate(AssetPath.Player, at);

      _playerGameObject.GetComponent<Movement>().Construct(_inputService);

      return _playerGameObject;
    }

    public GameObject CreateHUD() =>
      _assetProvider.Instantiate(AssetPath.HUD);

    public GameObject CreatePlayerCamera()
    {
      GameObject playerCamera = _assetProvider.Instantiate(AssetPath.PlayerCamera);

      CinemachineFreeLook cinemachineComponent = playerCamera.GetComponent<CinemachineFreeLook>();
      cinemachineComponent.Follow = _playerGameObject.transform;
      cinemachineComponent.LookAt = _playerGameObject.transform.Find(LookAt);

      return playerCamera;
    }
  }
}