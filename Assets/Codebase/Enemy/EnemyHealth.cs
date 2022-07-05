using System;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(Animator), typeof(AudioSource))]
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    private const string Hit = "Hit";

    [SerializeField] private AudioClip _bleedOutSound;

    private Animator _animator;
    private ParticleSystem _bloodFX;
    private AudioSource _audioSource;

    public event Action Changed;

    public int Current { get; private set; }
    public int Max { get; private set; }

    public void Construct(int currentHealth, int maxHealth)
    {
      Current = currentHealth;
      Max = maxHealth;
      Changed?.Invoke();
    }

    private void Awake()
    {
      _animator = GetComponent<Animator>();
      _bloodFX = GetComponentInChildren<ParticleSystem>();
      _audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
      if (Current <= 0)
        return;

      _animator.SetTrigger(Hit);
      BleedOut(hitPoint);

      Current -= damage;
      Changed?.Invoke();
    }

    private void BleedOut(Vector3 hitPoint)
    {
      _bloodFX.transform.position = hitPoint;
      _bloodFX.Play();

      _audioSource.clip = _bleedOutSound;
      _audioSource.Play();
    }
  }
}