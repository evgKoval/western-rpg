using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(MoveToPlayer))]
  public class CheckAggroZone : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _aggroZone;

    private MoveToPlayer _movement;

    private void Awake() =>
      _movement = GetComponent<MoveToPlayer>();

    private void Start()
    {
      _aggroZone.Enter += StartMoving;
      _aggroZone.Exit += CancelMoving;

      _movement.enabled = false;
    }

    private void OnDestroy()
    {
      _aggroZone.Enter -= StartMoving;
      _aggroZone.Exit -= CancelMoving;
    }

    private void StartMoving(Collider obj) =>
      _movement.enabled = true;

    private void CancelMoving(Collider obj) =>
      _movement.enabled = false;
  }
}