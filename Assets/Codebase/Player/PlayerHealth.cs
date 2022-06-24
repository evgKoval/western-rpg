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

    private void Awake() =>
      _animator = GetComponent<Animator>();

    public void TakeDamage(int damage)
    {
      if (Current <= 0)
        return;

      Current -= damage;
      Changed?.Invoke();

      _animator.SetTrigger(Hit);
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
  }
}