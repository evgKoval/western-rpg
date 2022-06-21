using Codebase.Logic;
using Codebase.Services.Input;
using UnityEngine;

namespace Codebase.Player
{
  [RequireComponent(typeof(Aiming))]
  public class Firing : MonoBehaviour, IDeathable
  {
    private IInputService _inputService;
    private Weapon _weapon;
    private Aiming _aiming;

    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Awake() =>
      _aiming = GetComponent<Aiming>();

    private void Update()
    {
      if (_inputService.IsFiringButtonDown() && _aiming.IsReady)
        _weapon.Shoot();
    }

    public void EquipWeapon(Weapon weapon) =>
      _weapon = weapon;
  }
}