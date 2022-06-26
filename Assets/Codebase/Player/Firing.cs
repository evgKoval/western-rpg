using System.Collections;
using Codebase.Logic;
using Codebase.Services.Input;
using Codebase.Services.Pause;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Codebase.Player
{
  [RequireComponent(typeof(Aiming), typeof(Animator))]
  public class Firing : MonoBehaviour, IDeathable, IPauseable
  {
    private const string ReloadingState = "Reload";
    private const string RightHand = "Rig_Layer_Hand_IK/Right_Hand_IK";

    [SerializeField] private float _reloadingSpeed;

    private IInputService _inputService;
    private Weapon _weapon;
    private Aiming _aiming;
    private Animator _animator;
    private TwoBoneIKConstraint _rightHand;

    public bool IsPaused { get; private set; }
    public bool IsReloading { get; private set; }

    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Awake()
    {
      _aiming = GetComponent<Aiming>();
      _animator = GetComponent<Animator>();
    }

    private void Start() =>
      _rightHand = transform.Find(RightHand).GetComponent<TwoBoneIKConstraint>();

    private void Update()
    {
      if (IsPaused)
        return;

      if (_inputService.IsFiringButtonDown() && _aiming.IsReady)
      {
        _weapon.Shoot();
        StartCoroutine(Reload());
      }
    }

    private IEnumerator OnReloadEnded()
    {
      yield return RaiseHand();
      IsReloading = false;
    }

    public void EquipWeapon(Weapon weapon) =>
      _weapon = weapon;

    public void Pause() =>
      IsPaused = true;

    public void Resume() =>
      IsPaused = false;

    private IEnumerator Reload()
    {
      IsReloading = true;

      while (!Mathf.Approximately(_aiming.ReadyPercentage, 0))
        yield return null;

      yield return LowerHand();
      _animator.SetTrigger(ReloadingState);
    }

    private IEnumerator LowerHand()
    {
      while (!Mathf.Approximately(_rightHand.weight, 0))
      {
        _rightHand.weight -= Time.deltaTime / _reloadingSpeed;
        yield return null;
      }
    }

    private IEnumerator RaiseHand()
    {
      while (!Mathf.Approximately(_rightHand.weight, 1))
      {
        _rightHand.weight += Time.deltaTime / _reloadingSpeed;
        yield return null;
      }
    }
  }
}