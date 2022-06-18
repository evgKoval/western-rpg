using System;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Player
{
  [RequireComponent(typeof(Animator))]
  public class Health : MonoBehaviour, IHealth
  {
    private const string Hit = "Hit";

    [SerializeField] [Range(1, 100)] private int _max;

    private Animator _animator;

    public event Action Changed;

    public int Current { get; private set; }
    public int Max => _max;

    private void Awake() =>
      _animator = GetComponent<Animator>();

    private void Start() =>
      Current = _max;

    public void TakeDamage(int damage)
    {
      if (Current <= 0)
        return;

      Current -= damage;
      Changed?.Invoke();

      _animator.SetTrigger(Hit);
    }
  }
}