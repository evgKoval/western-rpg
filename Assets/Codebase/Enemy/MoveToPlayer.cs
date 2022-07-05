using Codebase.Logic;
using Codebase.Services.Pause;
using UnityEngine;
using UnityEngine.AI;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
  public class MoveToPlayer : MonoBehaviour, IDeathable, IPauseable
  {
    private const string Velocity = "Velocity";

    [SerializeField] [Range(0, 100)] private float _minimalDistance;

    private NavMeshAgent _agent;
    private Transform _player;
    private Animator _animator;

    public bool IsPaused { get; private set; }

    public void Construct(Transform player) =>
      _player = player;

    private void Awake()
    {
      _agent = GetComponent<NavMeshAgent>();
      _animator = GetComponent<Animator>();
    }

    private void Update()
    {
      if (IsPaused)
        return;

      if (_player && IsNotCloseToPlayer())
        _agent.destination = _player.position;

      _animator.SetFloat(Velocity, _agent.velocity.magnitude);
    }

    private bool IsNotCloseToPlayer()
    {
      float distance = Vector3.Distance(transform.position, _player.position);
      return _minimalDistance <= distance;
    }

    public void Pause()
    {
      IsPaused = true;

      _animator.enabled = false;

      _agent.ResetPath();
    }

    public void Resume()
    {
      IsPaused = false;

      _animator.enabled = true;
    }
  }
}