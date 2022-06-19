using Cinemachine;
using Codebase.Enemy;
using Codebase.Infrastructure.AssetManagement;
using Codebase.Logic;
using Codebase.Player;
using Codebase.Services.Input;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Codebase.UI;
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

    public GameObject CreateHUD()
    {
      GameObject hud = _assetProvider.Instantiate(AssetPath.HUD);

      hud.GetComponent<HUDBinding>().Construct(_playerGameObject.GetComponent<IHealth>());

      return hud;
    }

    public GameObject CreatePlayerCamera()
    {
      GameObject playerCamera = _assetProvider.Instantiate(AssetPath.PlayerCamera);

      CinemachineFreeLook cinemachineComponent = playerCamera.GetComponent<CinemachineFreeLook>();
      cinemachineComponent.Follow = _playerGameObject.transform;
      cinemachineComponent.LookAt = _playerGameObject.transform.Find(CameraLookAt);

      return playerCamera;
    }

    public GameObject CreateWeapon(WeaponId weaponId, Transform whom)
    {
      WeaponStaticData weaponData = _staticDataService.GetWeapon(weaponId);
      GameObject weapon = Object.Instantiate(weaponData.Prefab, whom.Find(WeaponPivot));

      if (whom.TryGetComponent(out Firing firing))
        firing.EquipWeapon(weapon.GetComponent<Weapon>());

      AttachWeapon(weapon.transform, whom);

      return weapon;
    }

    public GameObject CreateEnemy()
    {
      GameObject enemy = _assetProvider.Instantiate(AssetPath.Enemy, new Vector3(0, 2.2f, 11f));

      enemy.GetComponent<MoveToPlayer>().Construct(_playerGameObject.transform);

      return enemy;
    }

    private void BuildRig()
    {
      Transform aimLookAt = Camera.main.transform.Find(AimLookAt);

      foreach (MultiAimConstraint multiAimConstraint in _playerGameObject.GetComponentsInChildren<MultiAimConstraint>())
        SetSourceObject(multiAimConstraint, aimLookAt);

      _playerGameObject.GetComponentInChildren<RigBuilder>().Build();
    }

    private static void AttachWeapon(Transform weapon, Transform whom)
    {
      TwoBoneIKConstraint[] twoBoneIKConstraints = whom.GetComponentsInChildren<TwoBoneIKConstraint>();

      twoBoneIKConstraints[0].data.target = weapon.Find(RightHandGrip);
      twoBoneIKConstraints[1].data.target = weapon.Find(LeftHandGrip);

      whom.GetComponentInChildren<RigBuilder>().Build();
    }

    private static void SetSourceObject(MultiAimConstraint component, Transform aimLookAt)
    {
      WeightedTransformArray data = component.data.sourceObjects;
      data.SetTransform(0, aimLookAt);
      component.data.sourceObjects = data;
    }
  }
}