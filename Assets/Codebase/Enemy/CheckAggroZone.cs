using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(MoveToPlayer), typeof(AudioSource))]
  public class CheckAggroZone : MonoBehaviour, IDeathable
  {
    [SerializeField] private TriggerObserver _aggroZone;
    [SerializeField] private AudioClip _aggroSound;

    private MoveToPlayer _movement;
    private AudioSource _audioSource;

    private void Awake()
    {
      _movement = GetComponent<MoveToPlayer>();
      _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
      _aggroZone.Enter += StartMoving;
      _aggroZone.Exit += CancelMoving;

      _movement.enabled = false;
    }

    private void OnDisable()
    {
      _aggroZone.Enter -= StartMoving;
      _aggroZone.Exit -= CancelMoving;
    }

    private void StartMoving(Collider obj)
    {
      _audioSource.clip = _aggroSound;
      _audioSource.Play();

      _movement.enabled = true;
    }

    private void CancelMoving(Collider obj) =>
      _movement.enabled = false;
  }
}