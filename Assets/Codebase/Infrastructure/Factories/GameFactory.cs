using Cinemachine;
using Codebase.Infrastructure.AssetManagement;
using Codebase.Player;
using Codebase.Services.Input;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Codebase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    private const string CameraLookAt = "Look_At";
    private const string AimLookAt = "Aim Look At";
    private const string WeaponPivot = "Pivot_Wrapper/Weapon_Pivot";
    private const string RightHandGrip = "Ref_Right_Hand_Grip";
    private const string LeftHandGrip = "Ref_Left_Hand_Grip";

    private readonly IAssetProvider _assetProvider;
    private readonly IInputService _inputService;
    private readonly IStaticDataService _staticDataService;

    private GameObject _playerGameObject;

    public GameFactory(IAssetProvider assetProvider, IInputService inputService, IStaticDataService staticDataService)
    {
      _assetProvider = assetProvider;
      _inputService = inputService;
      _staticDataService = staticDataService;
    }

    public void WarmUp() =>
      _staticDataService.Load();

    public GameObject CreatePlayer(Vector3 at)
    {
      _playerGameObject = _assetProvider.Instantiate(AssetPath.Player, at);

      _playerGameObject.GetComponent<Movement>().Construct(_inputService);
      _playerGameObject.GetComponent<Aiming>().Construct(_inputService);
      _playerGameObject.GetComponent<Firing>().Construct(_inputService);
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

    public GameObject CreateWeapon()
    {
      WeaponStaticData weaponData = _staticDataService.GetWeapon(WeaponId.Shotgun);
      Weapon weapon = Object.Instantiate(weaponData.Prefab, _playerGameObject.transform.Find(WeaponPivot));

      _playerGameObject.GetComponent<Firing>().EquipWeapon(weapon);
      AttachWeaponToPlayer(weapon.transform);

      return weapon.gameObject;
    }

    private void BuildRig()
    {
      Transform aimLookAt = Camera.main.transform.Find(AimLookAt);

      foreach (MultiAimConstraint multiAimConstraint in _playerGameObject.GetComponentsInChildren<MultiAimConstraint>())
        SetSourceObject(multiAimConstraint, aimLookAt);

      _playerGameObject.GetComponentInChildren<RigBuilder>().Build();
    }

    private void AttachWeaponToPlayer(Transform weapon)
    {
      TwoBoneIKConstraint[] twoBoneIKConstraints = _playerGameObject.GetComponentsInChildren<TwoBoneIKConstraint>();

      twoBoneIKConstraints[0].data.target = weapon.Find(RightHandGrip);
      twoBoneIKConstraints[1].data.target = weapon.Find(LeftHandGrip);

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