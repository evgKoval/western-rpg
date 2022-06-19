using UnityEngine;
using UnityEngine.AI;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(NavMeshAgent))]
  public class MoveToPlayer : MonoBehaviour
  {
    [SerializeField] [Range(0, 100)]
    private float _minimalDistance;

    private NavMeshAgent _agent;
    private Transform _player;

    public void Construct(Transform player) =>
      _player = player;

    private void Awake() =>
      _agent = GetComponent<NavMeshAgent>();

    private void Update()
    {
      if (_player && IsNotCloseToPlayer())
        _agent.destination = _player.position;
    }

    private bool IsNotCloseToPlayer()
    {
      float distance = Vector3.Distance(transform.position, _player.position);
      return _minimalDistance <= distance;
    }
  }
}