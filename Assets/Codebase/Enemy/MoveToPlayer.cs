using UnityEngine;
using UnityEngine.AI;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
  public class MoveToPlayer : MonoBehaviour
  {
    private const string Velocity = "Velocity";

    [SerializeField] [Range(0, 100)] private float _minimalDistance;

    private NavMeshAgent _agent;
    private Transform _player;
    private Animator _animator;

    public void Construct(Transform player) =>
      _player = player;

    private void Awake()
    {
      _agent = GetComponent<NavMeshAgent>();
      _animator = GetComponent<Animator>();
    }

    private void Update()
    {
      if (_player && IsNotCloseToPlayer())
        _agent.destination = _player.position;

      _animator.SetFloat(Velocity, _agent.velocity.magnitude);
    }

    private bool IsNotCloseToPlayer()
    {
      float distance = Vector3.Distance(transform.position, _player.position);
      return _minimalDistance <= distance;
    }
  }
}