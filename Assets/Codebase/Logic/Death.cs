using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Codebase.Logic
{
  [RequireComponent(typeof(IHealth), typeof(Animator), typeof(AudioSource))]
  public class Death : MonoBehaviour
  {
    private const string RigBodyLayer = "Rig_Layer_Body_Aim";
    private const string DeathState = "Die";

    [SerializeField] private AudioClip _deathSound;

    private IHealth _health;
    private Animator _animator;
    private bool _isDead;
    private AudioSource _audioSource;

    public event Action Happened;

    private void Awake()
    {
      _health = GetComponent<IHealth>();
      _animator = GetComponent<Animator>();
      _audioSource = GetComponent<AudioSource>();
    }

    private void Start() =>
      _health.Changed += HealthOnChanged;

    private void OnDestroy() =>
      _health.Changed -= HealthOnChanged;

    public void Die()
    {
      _isDead = true;

      TurnOffAbilities();
      MakeBodyDontLookAtCamera();

      _audioSource.clip = _deathSound;
      _audioSource.Play();

      _animator.SetTrigger(DeathState);
      Happened?.Invoke();
    }

    private void HealthOnChanged()
    {
      if (!_isDead && _health.Current <= 0)
        Die();
    }

    private void TurnOffAbilities()
    {
      foreach (Component component in GetComponents(typeof(IDeathable)))
      {
        MonoBehaviour ability = (MonoBehaviour)component;
        ability.enabled = false;
      }
    }

    private void MakeBodyDontLookAtCamera() =>
      transform.Find(RigBodyLayer)
        .GetComponent<Rig>().weight = 0;
  }
}