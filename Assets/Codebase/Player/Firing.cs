using Codebase.Logic;
using Codebase.Services.Input;
using Codebase.Services.Pause;
using UnityEngine;

namespace Codebase.Player
{
  [RequireComponent(typeof(Aiming))]
  public class Firing : MonoBehaviour, IDeathable, IPauseable
  {
    private IInputService _inputService;
    private Weapon _weapon;
    private Aiming _aiming;

    public bool IsPaused { get; private set; }

    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Awake() =>
      _aiming = GetComponent<Aiming>();

    private void Update()
    {
      if (IsPaused)
        return;

      if (_inputService.IsFiringButtonDown() && _aiming.IsReady)
        _weapon.Shoot();
    }

    public void EquipWeapon(Weapon weapon) =>
      _weapon = weapon;

    public void Pause() =>
      IsPaused = true;

    public void Resume() =>
      IsPaused = false;
  }
}