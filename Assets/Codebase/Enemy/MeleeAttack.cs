using Codebase.Logic;
using Codebase.Services.Pause;
using UnityEngine;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(Animator))]
  public class MeleeAttack : MonoBehaviour, IDeathable, IPauseable
  {
    private const string Attack = "Attack";

    private Animator _animator;
    private Transform _player;
    private float _cooldownTimeLeft;
    private bool _isAttacking;
    private Steelarm _steelarm;

    public bool IsPaused { get; private set; }

    public void Construct(Transform player) =>
      _player = player;

    private void Awake() =>
      _animator = GetComponent<Animator>();

    private void Update()
    {
      if (IsPaused)
        return;

      UpdateCooldown();

      if (CanAttack())
        StartAttack();
    }

    private void OnAttackStarted() =>
      _steelarm.Strike();

    private void OnAttackEnded()
    {
      _cooldownTimeLeft = _steelarm.AttackCooldown;
      _isAttacking = false;
    }

    public void Pause()
    {
      IsPaused = true;

      _animator.enabled = false;
    }

    public void Resume()
    {
      IsPaused = false;

      _animator.enabled = true;
    }

    public void EquipWeapon(Steelarm steelarm) =>
      _steelarm = steelarm;

    private void UpdateCooldown()
    {
      if (!CooldownIsUp())
        _cooldownTimeLeft -= Time.deltaTime;
    }

    private bool CanAttack() =>
      !_isAttacking && CooldownIsUp();

    private void StartAttack()
    {
      transform.LookAt(_player);
      _animator.SetTrigger(Attack);
      _isAttacking = true;
    }

    private bool CooldownIsUp() =>
      _cooldownTimeLeft <= 0f;
  }
}