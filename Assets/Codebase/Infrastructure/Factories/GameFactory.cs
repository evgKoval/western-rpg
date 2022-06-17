using Cinemachine;
using Codebase.Infrastructure.AssetManagement;
using Codebase.Player;
using Codebase.Services.Input;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Codebase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    private const string CameraLookAt = "Look_At";
    private const string AimLookAt = "Aim Look At";

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
      _playerGameObject.GetComponent<Aiming>().Construct(_inputService);
      BuildRig();

      return _playerGameObject;
    }

    public GameObject CreateHUD() =>
      _assetProvider.Instantiate(AssetPath.HUD);

    public GameObject CreatePlayerCamera()
    {
      GameObject playerCamera = _assetProvider.Instantiate(AssetPath.PlayerCamera);

      CinemachineFreeLook cinemachineComponent = playerCamera.GetComponent<CinemachineFreeLook>();
      cinemachineComponent.Follow = _playerGameObject.transform;
      cinemachineComponent.LookAt = _playerGameObject.transform.Find(CameraLookAt);

      return playerCamera;
    }

    private void BuildRig()
    {
      Transform aimLookAt = Camera.main.transform.Find(AimLookAt);

      foreach (MultiAimConstraint multiAimConstraint in _playerGameObject.GetComponentsInChildren<MultiAimConstraint>())
        SetSourceObject(multiAimConstraint, aimLookAt);

      _playerGameObject.GetComponentInChildren<RigBuilder>().Build();
    }

    private static void SetSourceObject(MultiAimConstraint component, Transform aimLookAt)
    {
      WeightedTransformArray data = component.data.sourceObjects;
      data.SetTransform(0, aimLookAt);
      component.data.sourceObjects = data;
    }
  }
}