using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Codebase.Player
{
  [RequireComponent(typeof(Health), typeof(Animator))]
  public class Death : MonoBehaviour
  {
    private const string RigBodyLayer = "Rig_Layer_Body_Aim";
    private const string DeathState = "Die";

    private Health _health;
    private Animator _animator;
    private bool _isDead;

    private void Awake()
    {
      _health = GetComponent<Health>();
      _animator = GetComponent<Animator>();
    }

    private void Start() =>
      _health.Changed += HealthOnChanged;

    private void OnDestroy() =>
      _health.Changed -= HealthOnChanged;

    private void HealthOnChanged()
    {
      if (!_isDead && _health.Current <= 0)
        Die();
    }

    private void Die()
    {
      _isDead = true;

      TurnOffAbilities();
      MakeBodyDontLookAtCamera();

      _animator.SetTrigger(DeathState);
    }

    private void TurnOffAbilities()
    {
      if (TryGetComponent(out Aiming aiming))
        aiming.enabled = false;

      if (TryGetComponent(out Firing firing))
        firing.enabled = false;

      if (TryGetComponent(out Rotating rotating))
        rotating.enabled = false;
    }

    private void MakeBodyDontLookAtCamera() =>
      transform.Find(RigBodyLayer)
        .GetComponent<Rig>().weight = 0;
  }
}