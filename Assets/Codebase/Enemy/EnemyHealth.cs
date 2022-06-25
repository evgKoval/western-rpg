using System;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
  [RequireComponent(typeof(Animator))]
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    private const string Hit = "Hit";

    private Animator _animator;
    private ParticleSystem _bloodFX;

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
    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
      if (Current <= 0)
        return;

      Current -= damage;
      Changed?.Invoke();

      _animator.SetTrigger(Hit);

      BleedOut(hitPoint);
    }

    private void BleedOut(Vector3 hitPoint)
    {
      _bloodFX.transform.position = hitPoint;
      _bloodFX.Play();
    }
  }
}