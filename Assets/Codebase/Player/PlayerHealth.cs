using System;
using Codebase.Data;
using Codebase.Logic;
using Codebase.Services.Progress;
using UnityEngine;

namespace Codebase.Player
{
  [RequireComponent(typeof(Animator), typeof(AudioSource))]
  public class PlayerHealth : MonoBehaviour, IHealth, ISaveable
  {
    private const string Hit = "Hit";

    [SerializeField] private AudioClip _bleedOutSound;

    private Animator _animator;
    private PlayerState _state;
    private ParticleSystem _bloodFX;
    private AudioSource _audioSource;

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

      _audioSource.clip = _bleedOutSound;
      _audioSource.Play();
    }
  }
}