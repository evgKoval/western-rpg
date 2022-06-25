using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(IHealth), typeof(MoveToPlayer))]
  public class GettingAggroAfterTakingDamage : MonoBehaviour, IDeathable
  {
    private IHealth _health;
    private MoveToPlayer _movement;

    private void Awake()
    {
      _health = GetComponent<IHealth>();
      _movement = GetComponent<MoveToPlayer>();
    }

    private void Start()
    {
      _health.Changed += StartMoving;

      _movement.enabled = false;
    }

    private void OnDisable() =>
      _health.Changed -= StartMoving;

    private void StartMoving() =>
      _movement.enabled = true;
  }
}