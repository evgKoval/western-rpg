using System;
using Codebase.Data;
using Codebase.Logic;
using Codebase.Services.Progress;
using UnityEngine;

namespace Codebase.Player
{
  public class PlayerHealth : MonoBehaviour, IHealth, ISaveable
  {
    private const string Hit = "Hit";

    private Animator _animator;
    private PlayerState _state;
    private ParticleSystem _bloodFX;

    public event Action Changed;

    public int Current
    {
      get => _state.CurrentHealth;
      private set
      {
        if (value == _state.CurrentHealth) return;

        _state.CurrentHealth = value;
        Changed?.Invoke();
      }
    }

    public int Max => _state.MaxHealth;

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

    public void LoadProgress(PlayerProgress progress)
    {
      _state = progress.PlayerState;
      Changed?.Invoke();
    }

    public void SaveProgress(PlayerProgress progress)
    {
      progress.PlayerState.CurrentHealth = Current;
      progress.PlayerState.MaxHealth = Max;
    }

    private void BleedOut(Vector3 hitPoint)
    {
      _bloodFX.transform.position = hitPoint;
      _bloodFX.Play();
    }
  }
}