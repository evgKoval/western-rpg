using System.Linq;
using UnityEngine;

namespace Codebase.Logic
{
  [RequireComponent(typeof(AudioSource))]
  public class Steelarm : Weapon
  {
    private const string Player = "Player";

    [SerializeField] [Range(0, 10)] private float _attackCooldown;
    [SerializeField] [Range(0, 10)] private float _attackPointOffset;
    [SerializeField] [Range(0, 10)] private float _attackRadius;
    [SerializeField] [Range(1, 100)] private int _damage;
    [SerializeField] private float _positionWeaponByY;

    private readonly Collider[] _hits = new Collider[1];
    private AudioSource _swingAudio;
    private int _playerLayerMask;
    private Transform _owner;

    public float AttackCooldown => _attackCooldown;

    public void Construct(Transform owner) =>
      _owner = owner;

    private void Awake() =>
      _swingAudio = GetComponent<AudioSource>();

    private void Start() =>
      _playerLayerMask = 1 << LayerMask.NameToLayer(Player);

    public void Strike()
    {
      _swingAudio.Play();

      if (TryHit(out Collider hit))
        hit.GetComponent<IHealth>().TakeDamage(_damage, AttackPoint());
    }

    private bool TryHit(out Collider hit)
    {
      int hitAmount = Physics.OverlapSphereNonAlloc(AttackPoint(), _attackRadius, _hits, _playerLayerMask);

      hit = _hits.FirstOrDefault();

      return hitAmount > 0;
    }

    private Vector3 AttackPoint() =>
      new Vector3(_owner.position.x, _owner.position.y + _positionWeaponByY, _owner.position.z) +
      _owner.forward * _attackPointOffset;
  }
}