using System.Linq;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(Animator))]
  public class MeleeAttack : MonoBehaviour, IDeathable
  {
    private const string Attack = "Attack";
    private const string Player = "Player";

    [SerializeField] [Range(0, 10)] private float _attackCooldown;
    [SerializeField] [Range(0, 10)] private float _attackPointOffset;
    [SerializeField] [Range(0, 10)] private float _attackRadius;
    [SerializeField] [Range(1, 100)] private int _damage;
    [SerializeField] private float _positionWeaponByY;

    private readonly Collider[] _hits = new Collider[1];

    private Animator _animator;
    private Transform _player;
    private int _playerLayerMask;
    private float _cooldownTimeLeft;
    private bool _isAttacking;

    public void Construct(Transform player) =>
      _player = player;

    private void Awake() =>
      _animator = GetComponent<Animator>();

    private void Start() =>
      _playerLayerMask = 1 << LayerMask.NameToLayer(Player);

    private void Update()
    {
      UpdateCooldown();

      if (CanAttack())
        StartAttack();
    }

    private void OnAttackStarted()
    {
      if (Hit(out Collider hit))
      {
        Debug.DrawRay(AttackPoint(), _attackRadius * Vector3.forward, Color.red, 1);
        hit.GetComponent<IHealth>().TakeDamage(_damage);
      }
    }

    private void OnAttackEnded()
    {
      _cooldownTimeLeft = _attackCooldown;
      _isAttacking = false;
    }

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

    private bool Hit(out Collider hit)
    {
      int hitAmount = Physics.OverlapSphereNonAlloc(AttackPoint(), _attackRadius, _hits, _playerLayerMask);

      hit = _hits.FirstOrDefault();

      return hitAmount > 0;
    }

    private Vector3 AttackPoint() =>
      new Vector3(transform.position.x, transform.position.y + _positionWeaponByY, transform.position.z) +
      transform.forward * _attackPointOffset;

    private bool CooldownIsUp() =>
      _cooldownTimeLeft <= 0f;
  }
}