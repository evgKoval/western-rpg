using Cinemachine;
using Codebase.Enemy;
using Codebase.Infrastructure.AssetManagement;
using Codebase.Logic;
using Codebase.Player;
using Codebase.Services.Input;
using Codebase.Services.Pause;
using Codebase.Services.Progress;
using Codebase.Services.Saving;
using Codebase.Services.StaticData;
using Codebase.Services.Window;
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
    private readonly IWindowService _windowService;
    private readonly IPauseService _pauseService;
    private readonly ISavingService _savingService;

    private GameObject _playerGameObject;

    public GameFactory(
      IAssetProvider assetProvider,
      IInputService inputService,
      IStaticDataService staticDataService,
      IWindowService windowService,
      IPauseService pauseService,
      ISavingService savingService
    )
    {
      _assetProvider = assetProvider;
      _inputService = inputService;
      _staticDataService = staticDataService;
      _windowService = windowService;
      _pauseService = pauseService;
      _savingService = savingService;
    }

    public void WarmUp() =>
      _staticDataService.Load();

    public void CleanUp()
    {
      _savingService.Clear();
      _pauseService.Clear();
    }

    public GameObject CreatePlayer(Vector3 at)
    {
      _playerGameObject = InstantiateRegistered(AssetPath.Player, at);

      _playerGameObject.GetComponent<Movement>().Construct(_inputService);
      _playerGameObject.GetComponent<Aiming>().Construct(_inputService);
      _playerGameObject.GetComponent<Firing>().Construct(_inputService);
      _playerGameObject.GetComponent<CheckingDeath>().Construct(_windowService);
      BuildRig();

      return _playerGameObject;
    }

    public GameObject CreateHUD()
    {
      GameObject hud = InstantiateRegistered(AssetPath.HUD);

      hud.GetComponent<HUDBinding>().Construct(_playerGameObject.GetComponent<IHealth>());
      hud.GetComponentInChildren<PauseWindowOpener>().Construct(_windowService);

      return hud;
    }

    public GameObject CreatePlayerCamera()
    {
      GameObject playerCamera = InstantiateRegistered(AssetPath.PlayerCamera);

      CinemachineFreeLook cinemachineComponent = playerCamera.GetComponent<CinemachineFreeLook>();
      cinemachineComponent.Follow = _playerGameObject.transform;
      cinemachineComponent.LookAt = _playerGameObject.transform.Find(CameraLookAt);
      playerCamera.GetComponent<CameraRotating>().Construct(_playerGameObject.GetComponent<Aiming>(), _inputService);

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

    public void CreateSpawner(string id, Vector3 position)
    {
      EnemySpawner spawner = InstantiateRegistered(AssetPath.Spawner, position).GetComponent<EnemySpawner>();
      spawner.Construct(this, _staticDataService, id);
    }

    public GameObject CreateEnemy(Vector3 position)
    {
      GameObject enemy = InstantiateRegistered(AssetPath.Enemy, position);

      enemy.GetComponent<MoveToPlayer>().Construct(_playerGameObject.transform);
      enemy.GetComponent<MeleeAttack>().Construct(_playerGameObject.transform);
      enemy.GetComponent<HUDBinding>().Construct(enemy.GetComponent<IHealth>());

      return enemy;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assetProvider.Instantiate(prefabPath);
      RegisterProgressWatchers(gameObject);
      RegisterPauseables(gameObject);

      return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assetProvider.Instantiate(prefabPath, at);
      RegisterProgressWatchers(gameObject);
      RegisterPauseables(gameObject);

      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ILoadable progressReader in gameObject.GetComponentsInChildren<ILoadable>())
        _savingService.Register(progressReader);
    }

    private void RegisterPauseables(GameObject gameObject)
    {
      foreach (IPauseable pauseable in gameObject.GetComponentsInChildren<IPauseable>())
        _pauseService.Register(pauseable);
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