using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(MeleeAttack))]
  public class CheckAttackZone : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _attackZone;

    private MeleeAttack _attack;

    private void Awake() =>
      _attack = GetComponent<MeleeAttack>();

    private void Start()
    {
      _attackZone.Enter += StartAttack;
      _attackZone.Exit += CancelAttack;

      _attack.enabled = false;
    }

    private void OnDestroy()
    {
      _attackZone.Enter -= StartAttack;
      _attackZone.Exit -= CancelAttack;
    }

    private void StartAttack(Collider obj) =>
      _attack.enabled = true;

    private void CancelAttack(Collider obj) =>
      _attack.enabled = false;
  }
}