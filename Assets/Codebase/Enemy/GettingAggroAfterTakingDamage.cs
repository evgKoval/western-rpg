using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(IHealth), typeof(MoveToPlayer), typeof(AudioSource))]
  public class GettingAggroAfterTakingDamage : MonoBehaviour, IDeathable
  {
    [SerializeField] private AudioClip _aggroSound;

    private IHealth _health;
    private MoveToPlayer _movement;
    private AudioSource _audioSource;

    private void Awake()
    {
      _health = GetComponent<IHealth>();
      _movement = GetComponent<MoveToPlayer>();
      _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
      _health.TakenDamage += StartMoving;

      _movement.enabled = false;
    }

    private void OnDisable() =>
      _health.TakenDamage -= StartMoving;

    private void StartMoving()
    {
      _audioSource.clip = _aggroSound;
      _audioSource.Play();

      _movement.enabled = true;
    }
  }
}